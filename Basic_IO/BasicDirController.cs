using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SDFMS.Basic_IO.models;
using SDFMS.Driver;
using SDFMS.Logic_IO.models;

namespace SDFMS.Basic_IO
{
    public class BasicDirController
    {
        string diskName;
        FAT8 fat8 = new FAT8();
        byte[] r_buffer = new byte[64];
        byte[] w_buffer = new byte[64];
        DiskDriver diskDriver;

        static bool isNewed = false;

        public BasicDirController(string diskName, bool isNewDisk)
        {
            if(isNewed)
            {
                //本来应该弄成全部静态方法的，后来写完了才想起来，懒得改了。。。
                throw new Exception("只能有一个BasicDirController实例");
            }
            isNewed = true;
            diskDriver = new DiskDriver(diskName);
            this.diskName = diskName;
            if (isNewDisk)
            {
                FormatDisk();
            }
            LoadFat8();
        }

        /// <summary>
        /// 从磁盘读取FAT到内存
        /// </summary>
        private void LoadFat8()
        {
            byte[] buffer1 = new byte[64];
            byte[] buffer2 = new byte[64];
            diskDriver.Read(0, buffer1);
            diskDriver.Read(1, buffer2);
            fat8.Load(buffer1, buffer2);
        }

        /// <summary>
        /// 保存FAT到磁盘
        /// </summary>
        private void SaveFAT8()
        {
            Array.Copy(fat8.GetFATArray(), 0, w_buffer, 0, 64);
            diskDriver.Write(0, w_buffer);
            Array.Copy(fat8.GetFATArray(), 64, w_buffer, 0, 64);
            diskDriver.Write(1, w_buffer);
        }

        /// <summary>
        /// 格式化磁盘：创建空FAT，根目录
        /// </summary>
        public void FormatDisk()
        {
            //全部写零
            w_buffer = new byte[64];
            for (int i = 1; i < 128; i++)
            {
                diskDriver.Write(i, w_buffer);
            }
            //fat的前三字节设为已占用
            w_buffer[0] = byte.Parse($"{0xFF}");
            w_buffer[1] = byte.Parse($"{0xFF}");
            w_buffer[2] = byte.Parse($"{0xFF}");
            diskDriver.Write(0, w_buffer);
            //创建根目录
            InitFolderBlock(2, 2);
        }

        /// <summary>
        /// 初始化一个盘块为目录
        /// </summary>
        /// <param name="blockNum"></param>
        private void InitFolderBlock(byte blockNum, byte superioBlockNum)
        {
            //.当前目录
            FCB temp;
            temp = new FCB();
            temp.name = ".\0\0";
            temp.extendName = "\0\0";
            temp.dirType = byte.Parse($"{0x08}");
            temp.beginBlockNum = blockNum;
            Array.Copy(temp.ToByteArray(), 0, w_buffer, 0, 8);

            //..父目录
            temp = new FCB();
            temp.name = "..\0";
            temp.extendName = "\0\0";
            temp.dirType = byte.Parse($"{0x08}");
            temp.beginBlockNum = superioBlockNum;
            Array.Copy(temp.ToByteArray(), 0, w_buffer, 8, 8);

            //空目录项
            temp = new FCB();
            temp.name = "$\0\0";
            temp.extendName = "\0\0";
            temp.dirType = byte.Parse($"{0x00}");
            temp.beginBlockNum = 0;
            for (int i = 16; i < 64; i += 8)
            {
                Array.Copy(temp.ToByteArray(), 0, w_buffer, i, 8);
            }
            diskDriver.Write(blockNum, w_buffer);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name">主名称</param>
        /// <param name="typeName">扩展名</param>
        /// <param name="superioFolderBlockNum">记录此文件（夹）的文件夹盘块号</param>
        /// <param name="dirType">文件类型，建议写二位十六进制</param>
        /// <param name="dirEntrieNum">目录项应该保存在父目录的第几行</param>
        /// <exception cref="Exception"></exception>
        public void CreateFolder(string name, string typeName, byte superioFolderBlockNum, byte dirEntrieNum)
        {
            byte dirType = 0x08;

            //查找空盘块
            byte beginBlockNum = fat8.FindEmptyBlock();
            if (beginBlockNum == 255)
            {
                throw new Exception("磁盘已满");
            }

            //编辑目录项,同时验证名称合法性
            FCB temp = new FCB();
            temp.name = name;
            temp.extendName = typeName;
            temp.dirType = byte.Parse($"{dirType}");
            temp.beginBlockNum = beginBlockNum;
            temp.fileSize = 1;
            byte[] tempBytes = temp.ToByteArray();

            InitFolderBlock(beginBlockNum, superioFolderBlockNum);

            //保存目录项
            Array.Copy(r_buffer, 0, w_buffer, 0, r_buffer.Length);
            Array.Copy(tempBytes, 0, w_buffer, dirEntrieNum * 8, tempBytes.Length);
            diskDriver.Write(superioFolderBlockNum, w_buffer);

            //占用此盘块
            fat8.UseBlock(beginBlockNum, 255);
            SaveFAT8();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="name">主名称</param>
        /// <param name="extendName">扩展名</param>
        /// <param name="superioFolderBlockNum">记录此文件（夹）的文件夹盘块号</param>
        /// <param name="fileType">文件类型，建议写二位十六进制</param>
        /// <param name="dirEntrieNum">目录项应该保存在父目录的第几行</param>
        /// <exception cref="Exception"></exception>
        public void CreateFile(string name, string extendName, byte superioFolderBlockNum, byte fileType, byte dirEntrieNum)
        {
            //查找空盘块
            byte beginBlockNum = fat8.FindEmptyBlock();
            if (beginBlockNum == 255)
            {
                throw new Exception("磁盘已满");
            }

            //编辑目录项,同时验证名称合法性
            FCB temp = new FCB();
            temp.name = name;
            temp.extendName = extendName;
            temp.dirType = byte.Parse($"{fileType}");
            temp.beginBlockNum = beginBlockNum;
            temp.fileSize = 1;
            byte[] tempBytes = temp.ToByteArray();

            Array.Fill(w_buffer, (byte)0);
            diskDriver.Write(beginBlockNum, w_buffer);

            //保存目录项
            Array.Copy(r_buffer, 0, w_buffer, 0, r_buffer.Length);
            Array.Copy(tempBytes, 0, w_buffer, dirEntrieNum * 8, tempBytes.Length);
            diskDriver.Write(superioFolderBlockNum, w_buffer);

            //占用此盘块
            fat8.UseBlock(beginBlockNum, 255);
            SaveFAT8();
        }

        /// <summary>
        /// 删除目录项
        /// </summary>
        /// <param name="beginBlockNum">需要删除的目录项的盘块号</param>
        /// <param name="superioFolderBlockNum">需要删除的目录项所属的父目录的盘块号</param>
        /// <exception cref="Exception"></exception>
        public void DeleteDir(byte beginBlockNum, byte superioFolderBlockNum)
        {
            diskDriver.Read(superioFolderBlockNum, r_buffer);
            for(byte i = 0; i < 64; i += 8)
            {
                if (r_buffer[i + 6] == beginBlockNum)
                {
                    //在父目录中将属于此目录的目录项设为空
                    Array.Copy(r_buffer, 0, w_buffer, 0, r_buffer.Length);
                    FCB temp;
                    temp = new FCB();
                    temp.name = "$\0\0";
                    temp.extendName = "\0\0";
                    temp.dirType = byte.Parse($"{0x00}");
                    temp.beginBlockNum = 0;
                    byte[] tempBytes = temp.ToByteArray();
                    Array.Copy(tempBytes, 0, w_buffer, i, tempBytes.Length);
                    diskDriver.Write(superioFolderBlockNum, w_buffer);
                    fat8.FreeBlock(beginBlockNum);
                    SaveFAT8();
                    return;
                }
            }
            throw new Exception("未在父目录找到匹配此目录的目录项，磁盘可能已损坏");
        }

        /// <summary>
        /// 获得对应盘块的全部目录项
        /// </summary>
        /// <param name="blockNum"></param>
        /// <returns>总共8条目录项的数组</returns>
        public FCB[] GetDirNodeTableByBlockNum(byte blockNum)
        {
            FCB[] dirNodeTable = new FCB[8];
            diskDriver.Read(blockNum, r_buffer);
            byte[] tempBytes = new byte[8];
            for(byte i = 0; i < 8; i++)
            {
                Array.Copy(r_buffer, i * 8, tempBytes, 0, tempBytes.Length);
                dirNodeTable[i] = FCB.Parse(tempBytes, blockNum, i);
            }
            return dirNodeTable;
        }

        /*        /// <summary>
                /// 修改指定起始盘块的文件
                /// </summary>
                /// <param name="beginBlockNum">文件起始盘块号</param>
                /// <param name="data">文件内容</param>
                /// <param name="oldFileSize">当前文件大小</param>
                /// <param name="superioFolderBlockNum">需要删除的目录项所属的父目录的盘块号</param>
                /// <exception cref="Exception"></exception>
                public void ModifyFile(byte beginBlockNum, string data, byte oldFileSize, byte superioFolderBlockNum)
                {
                    data += "\0";

                    //计算文件大小
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    byte needSize = (byte)(dataBytes.Length / 64);
                    if(dataBytes.Length % 64 != 0)
                    {
                        needSize++;
                    }
                    if(fat8Array.GetEmptyBlockCount() < (needSize - oldFileSize))
                    {
                        throw new Exception($"磁盘容量不足,需要容量：{needSize - oldFileSize}个盘块，可用容量：{fat8Array.GetEmptyBlockCount()}个盘块");
                    }

                    //在父目录中写入文件大小
                    diskDriver.Read(superioFolderBlockNum, r_buffer);
                    for (byte j = 0; j < 64; j += 8)
                    {
                        if (r_buffer[j + 6] == beginBlockNum)
                        {
                            Array.Copy(r_buffer, 0, w_buffer, 0, r_buffer.Length);
                            w_buffer[j + 7] = needSize;
                            diskDriver.Write(superioFolderBlockNum, w_buffer);
                            fat8Array.FreeBlock(beginBlockNum);
                            SaveFAT8();
                            return;
                        }
                    }

                    FAT_Iterator fAT_Iter = new FAT_Iterator(fat8Array, beginBlockNum);
                    byte blockPoiter = beginBlockNum;
                    byte i;
                    for(i = 0; i < needSize && blockPoiter != 255; i++)//在当前持有的盘块中写入
                    {
                        Array.Copy(dataBytes, i * 64, w_buffer, 0, w_buffer.Length);
                        diskDriver.Write(blockPoiter, w_buffer);
                        blockPoiter = fAT_Iter.Next();
                    }
                    if(i < needSize)//新数据超出原有长度，申请新的盘块
                    {
                        while(i < needSize)
                        {
                            Array.Copy(dataBytes, i * 64, w_buffer, 0, w_buffer.Length);
                            diskDriver.Write(fat8Array.FindEmptyBlock(), w_buffer);
                            i++;
                        }
                    }
                    else if(blockPoiter != 255)//新数据少于原有长度,释放剩余盘块
                    {
                        while(blockPoiter != 255)
                        {
                            fat8Array.FreeBlock(blockPoiter);
                            blockPoiter = fAT_Iter.Next();
                        }
                    }
                    SaveFAT8();
                }

                /// <summary>
                /// 打开指定起始盘块的文件
                /// </summary>
                /// <param name="beginBlockNum"></param>
                /// <returns></returns>
                public string CMD_OpenFile(byte beginBlockNum)
                {
                    List<byte[]> byteArrayList = new List<byte[]>();
                    FAT_Iterator fAT_Iterator = new FAT_Iterator(fat8Array, beginBlockNum);
                    byte blockPointer = beginBlockNum;
                    while (blockPointer != 255)
                    {
                        byte[] bytes = new byte[64];
                        diskDriver.Read(blockPointer, bytes);
                        byteArrayList.Add(bytes);
                        blockPointer = fAT_Iterator.Next();
                    }
                    byte[] dataBytes = new byte[64 * byteArrayList.Count];
                    for(int i = 0; byteArrayList.Count > i; i++)
                    {
                        Array.Copy(byteArrayList[i], 0, dataBytes, i * 64, 64);
                    }
                    string data = Encoding.UTF8.GetString(dataBytes).Replace("\0", String.Empty);
                    return data;
                }*/

        /// <summary>
        /// 删除指定起始盘块的文件
        /// </summary>
        /// <param name="beginBlockNum"></param>
        /// <param name="superioFolderBlockNum"></param>
        public void DeleteFile(byte beginBlockNum, byte superioFolderBlockNum)
        {
            FAT_Iterator fAT_Iterator = new FAT_Iterator(beginBlockNum);
            byte blockPointer = beginBlockNum;
            while(blockPointer != 255)
            {
                byte temp = blockPointer;
                blockPointer = fAT_Iterator.Next();
                fat8.FreeBlock(temp);
            }
            DeleteDir(beginBlockNum, superioFolderBlockNum);
            SaveFAT8();
        }

        /// <summary>
        /// 修改指定盘块的指定目录项
        /// </summary>
        /// <param name="FCB_blockNum"></param>
        /// <param name="FCB_lineNum"></param>
        /// <param name="newFCB"></param>
        public void ModifyFCB(byte FCB_blockNum, byte FCB_lineNum, byte[] newFCB)
        {
            diskDriver.Read(FCB_blockNum, r_buffer);
            Array.Copy(r_buffer, w_buffer, 64);
            Array.Copy(newFCB, 0, w_buffer, FCB_lineNum * 8, newFCB.Length);
            diskDriver.Write(FCB_blockNum, w_buffer);
        }

        /// <summary>
        /// 获得不做任何处理的盘块字节内容,仅用作显示调试用，与系统无关
        /// </summary>
        /// <param name="BlockNum"></param>
        /// <returns></returns>
        public byte[] GetPureByte(byte BlockNum)
        {
            byte[] tempBytes = new byte[64];
            diskDriver.Read(BlockNum, tempBytes);
            return tempBytes;
        }

        public FAT8 GetFat8()
        {
            return fat8.GetFAT();
        }

        public void Close()
        {
            SaveFAT8();
            diskDriver.Close();
        }
    }
}
