using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SDFMS.Basic_IO;
using SDFMS.Logic_IO.models;

namespace SDFMS.Logic_IO
{
    /// <summary>
    /// 对应逻辑层的逻辑目录管理，提供用户接口，负责目录或文件的建立、查看、修改属性、删除、路径解析；用户当前路径的载体
    /// </summary>
    public class LogicDirManager
    {
        static BasicDirController basicDirController;
        static public byte currentFolderBlockNum = 2;//记录当前用户所在的目录所属的逻辑盘块号
        static public byte currentFolderItemsNum = 2;
        static FCB[] currentDirTable;//记录当前目录的所有目录项
        static public string currentAbsPath = "root:";//记录当前路径

        /// <summary>
        /// 因为要传参，所以选择手动静态初始化
        /// </summary>
        /// <param name="basicDirController1"></param>
        static public void Init(BasicDirController basicDirController1)
        {
            basicDirController = basicDirController1;
            RefreshCurrentFolder();
        }

        /// <summary>
        /// 刷新当前文件夹状态
        /// </summary>
        static public void RefreshCurrentFolder()
        {
            currentDirTable = basicDirController.GetDirNodeTableByBlockNum(currentFolderBlockNum);
            byte realItemCount = 0;
            for (int i = 0; i < currentDirTable.Length; i++)
            {
                if (!currentDirTable[i].name.Contains('$'))
                {
                    realItemCount++;
                }
            }
            currentFolderItemsNum = realItemCount;
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isNeedReturn">是否需要留在目标路径中，默认值false</param>
        /// <returns>找到返回目录所在盘块号，找不到返回255</returns>
        static public byte QueryPath(string path, bool isNeedStay = false)
        {
            byte oldBlockNum = currentFolderBlockNum;

            if(path.Equals("root:"))
            {
                currentFolderBlockNum = 0x02;
                RefreshCurrentFolder();
                if (isNeedStay)
                {
                    currentAbsPath = "root:";
                }
                if (!isNeedStay)
                {
                    //回到原来的目录
                    currentFolderBlockNum = oldBlockNum;
                    RefreshCurrentFolder();
                }
                return 0x02;
            }

            byte resultBlockNum = 255;
            string[] foldersName = path.Split("/");
            int i = 0;
            if (foldersName.Contains("root:"))
            {
                //判断使用了绝对路径，先返回根目录
                currentFolderBlockNum = 0x02;
                RefreshCurrentFolder();
                if (isNeedStay)
                {
                    currentAbsPath = "root:";
                }
                i = 1;
            }
            string folderName;
            for(; i < foldersName.Length; i++)
            {
                folderName = foldersName[i];
                resultBlockNum = FindFolderBlockNumByName(folderName);
                if (resultBlockNum == 255)
                {
                    break;
                }
                currentFolderBlockNum = resultBlockNum;
                RefreshCurrentFolder();

                if(isNeedStay)
                {
                    NoteAbsPath(folderName);
                }
            }

            if(!isNeedStay)
            {
                //因为搜索是真正进入了一层层的目录中，所以搜索结束后要回到原来的目录
                currentFolderBlockNum = oldBlockNum;
                RefreshCurrentFolder();
            }
            return resultBlockNum;
        }

        /// <summary>
        /// 记录当前绝对路径
        /// </summary>
        /// <param name="name"></param>
        static private void NoteAbsPath(string name)
        {
            if (name.Equals(".."))
            {
                for (int i = currentAbsPath.Length - 1; i >= 0; i--)
                {
                    if (currentAbsPath[i] == '/')
                    {
                        currentAbsPath = currentAbsPath.Remove(i);
                        break;
                    }
                }
            }
            else if (name.Equals("."))
            {

            }
            else
            {
                currentAbsPath += $"/{name}";
            }
        }

        /// <summary>
        /// 进入目标路径
        /// </summary>
        /// <param path="path"></param>
        static public void EnterFolder(string path)
        {
            if (QueryPath(path, true) == 255)
            {
                throw new Exception("目标文件夹不存在");
            }
            
            //之前写的是只进一层，path只能是当前文件夹的内容，后面才改成全路径，因此套了一层
            /*
             byte temp = FindFolderBlockNumByName(path);
            if(temp == 255)
            {
                throw new Exception("目标路径不存在");
            }
            currentFolderBlockNum = temp;
            RefreshCurrentFolder();
            if (path.Equals(".."))
            {
                for (int i = currentAbsPath.Length - 1; i >= 0; i--)
                {
                    if (currentAbsPath[i] == '/')
                    {
                        currentAbsPath = currentAbsPath.Remove(i);
                        break;
                    }
                }
            }
            else if (path.Equals("."))
            {

            }
            else
            {
                currentAbsPath += $"/{path}";
            }
             */
        }

        /// <summary>
        /// 在当前目录创建文件夹
        /// </summary>
        /// <param path="name">名称</param>
        /// <param path="dirType">属性</param>
        static public void CreateFolder(string name)
        {
            if(FindFolderBlockNumByName(name) != 255)
            {
                throw new Exception("已经存在同名文件夹");
            }
            if(currentFolderItemsNum >= 8)
            {
                throw new Exception("该文件夹已满（每个文件夹最多保存6个内容）");
            }
            basicDirController.CreateFolder(name, "", currentFolderBlockNum, FindEmptyDirectoryEntrie());
            RefreshCurrentFolder();
        }

        /// <summary>
        /// 删除当前目录的目标文件夹，递归删除目标文件夹下面的文件夹和文件
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception">有文件未关闭</exception>
        static public void DeleteFolder(string name)
        {
            byte old = currentFolderBlockNum;//由于删除文件夹需要真的进入目标文件夹
            try
            {
                DeleteFolder_Recursion(name);
            }
            catch (Exception e)
            {
                while (old != currentFolderBlockNum)//出现异常时“当前文件夹”可能没有回到操作发起的位置，因此需要回溯
                {
                    EnterFolder("..");
                }
                RefreshCurrentFolder();
                throw new Exception(e.Message);
            }
            if (!name.Equals("root:"))
            {
                byte targetBlockNum = FindFolderBlockNumByName(name);
                basicDirController.DeleteDir(targetBlockNum, currentFolderBlockNum);
            }
            RefreshCurrentFolder();
        }

        /// <summary>
        /// 递归删除目标文件夹下面的文件夹和文件
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        static private void DeleteFolder_Recursion(string name)
        {
            EnterFolder(name);
            foreach (FCB f in currentDirTable)
            {
                if (!f.name.Contains("$") && !f.name.Contains("."))
                {
                    if(f.dirType == 0x08)
                    {
                        DeleteFolder_Recursion(f.name.Replace("\0", ""));
                        byte targetBlockNum = FindFolderBlockNumByName(f.name.Replace("\0", ""));
                        basicDirController.DeleteDir(targetBlockNum, currentFolderBlockNum);
                    }
                    else
                    {
                        try
                        {
                            Get_OFTLE_ByName(f.name.Replace("\0", "") + "." + f.extendName.Replace("\0", "")).Close();
                            byte targetBlockNum = FindFileBlockNumByName(f.name.Replace("\0", ""), f.extendName.Replace("\0", ""));
                            basicDirController.DeleteFile(targetBlockNum, currentFolderBlockNum);
                        }
                        catch(Exception e)
                        {
                            
                            throw new Exception("有文件未关闭，删除已终止");
                        }
                    }
                }
            }
            EnterFolder("..");
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param path="fullName"></param>
        static public void CreateFile(string fullName, byte fileType)
        {
            string[] names = fullName.Split('.');
            if (FindFileBlockNumByName(names[0], names[1]) != 255)
            {
                throw new Exception("已经存在同名文件");
            }
            if (currentFolderItemsNum >= 8)
            {
                throw new Exception("该文件夹已满（每个文件夹最多保存6个内容）");
            }
            basicDirController.CreateFile(names[0], names[1], currentFolderBlockNum, fileType, FindEmptyDirectoryEntrie());
            RefreshCurrentFolder();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param path="fullName"></param>
        static public void DeleteFile(string fullName)
        {
            Get_OFTLE_ByName(fullName).Close();
            string[] names = fullName.Split('.');
            byte targetBlockNum = FindFileBlockNumByName(names[0], names[1]);
            basicDirController.DeleteFile(targetBlockNum, currentFolderBlockNum);
            RefreshCurrentFolder();
        }

        /// <summary>
        /// 寻找空目录项
        /// </summary>
        /// <returns>找到应返回2 - 7，找不到返回255</returns>
        static private byte FindEmptyDirectoryEntrie()
        {
            for (byte i = 0; i < 8; i++)
            {
                if (currentDirTable[i].name.Contains('$'))
                {
                    return i;
                }
            }
            return 255;
        }

        /// <summary>
        /// 在当前目录寻找文件夹并获得所在的逻辑盘块号
        /// </summary>
        /// <param path="name">名称</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        static private byte FindFolderBlockNumByName(string name)
        {
            for (byte i = 0; i < currentDirTable.Length; i++)
            {
                string a = currentDirTable[i].name.Replace("\0", String.Empty);
                if (a.Equals(name) && currentDirTable[i].dirType >= 8)
                {
                    return currentDirTable[i].beginBlockNum;
                }
            }
            return 255;
        }

        /// <summary>
        /// 在当前目录寻找文件并获得所在的逻辑盘块号
        /// </summary>
        /// <param path="fullName"></param>
        /// <returns></returns>
        static public byte FindFileBlockNumByName(string fileName, string extendName)
        {
            for (byte i = 0; i < currentDirTable.Length; i++)
            {
                string a = currentDirTable[i].name.Replace("\0", "");
                string b = currentDirTable[i].extendName.Replace("\0", "");
                if (a.Equals(fileName) && b.Equals(extendName) && currentDirTable[i].dirType < 8)
                {
                    return currentDirTable[i].beginBlockNum;
                }
            }
            return 255;
        }

        /// <summary>
        /// 在当前目录寻找文件（夹）并创建OFTLE
        /// </summary>
        /// <param path="fileName"></param>
        /// <param path="extendName"></param>
        /// <returns></returns>
        static public OFTLE Get_OFTLE_ByName(string fullName)
        {
            string[] names = fullName.Split('.');
            if(names.Length == 2)
            {
                string fileName = names[0];
                string extendName = names[1];
                for (byte i = 0; i < currentDirTable.Length; i++)
                {
                    string a = currentDirTable[i].name.Replace("\0", "");
                    string b = currentDirTable[i].extendName.Replace("\0", "");
                    if (a.Equals(fileName) && b.Equals(extendName) && currentDirTable[i].dirType < 8)
                    {
                        if (OFTLE.isOpened(currentAbsPath + "/" + fullName))
                        {
                            throw new Exception("该文件已打开，请关闭后再试");
                        }

                        OFTLE oftle = new OFTLE(currentDirTable[i]);
                        oftle.absPath = currentAbsPath + "/" + fullName;
                        oftle.fullName = fullName;
                        oftle.basicDirController = basicDirController;

                        return oftle;
                    }
                }
            }
            else if(names.Length == 1)
            {
                string fileName = names[0];
                for (byte i = 0; i < currentDirTable.Length; i++)
                {
                    string a = currentDirTable[i].name.Replace("\0", "");
                    if (a.Equals(fileName) && currentDirTable[i].dirType == 8)
                    {
                        OFTLE oftle = new OFTLE(currentDirTable[i]);
                        oftle.absPath = currentAbsPath + "/" + fullName;
                        oftle.fullName = fullName;
                        oftle.basicDirController = basicDirController;
                        return oftle;
                    }
                }
            }
            
            throw new Exception("该文件或目录不存在");
        }

        static public string CurrentDirToString()
        {
            string res = Environment.NewLine;
            res += $" {currentAbsPath} 的目录{Environment.NewLine}";
            int dir = 0;
            int file = 0;
            foreach(FCB fcb in currentDirTable)
            {
                if(!fcb.name.Contains("$"))
                {
                    if (fcb.dirType == 0x08)
                    {
                        res += $"\t{fcb.name.Replace("\0", "")}\t<目录>{Environment.NewLine}";
                        dir++;
                    }
                    else
                    {
                        res += $"\t{fcb.name.Replace("\0", "")}.{fcb.extendName.Replace("\0", "")}\t<文件>{Environment.NewLine}";
                        file++;
                    }
                }
            }
            res += $"\t\t{dir} 个目录， {file}个文件";
            return res;
        }

        static public byte[] GetPureByte(byte blockNum)
        {
            return basicDirController.GetPureByte(blockNum);
        }

        static public FCB[] GetCurrentNodeTable()
        {
            RefreshCurrentFolder();
            return currentDirTable;
        }

        static public byte[] GetFat8()
        {
            return basicDirController.GetFat8().GetFATArray();
        }

        static public void Close()
        {
            basicDirController.Close();
        }
    }


}
