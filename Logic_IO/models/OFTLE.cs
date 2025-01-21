using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDFMS.Basic_IO;

namespace SDFMS.Logic_IO.models
{
    public class OFTLE
    {
        public static int openingFileCount = 0;//已打开文件数
        static List<OFTLE> openedOFTLEs = new List<OFTLE>();//已打开文件列表

        public BasicDirController basicDirController;
        bool isThisOpen;//是否已打开

        public string fullName;//文件全名
        public string absPath;//文件的绝对路径
        public byte beginBlockNum;//文件起始物理盘块号
        public byte size;//文件大小
        public FCB fcb;//文件所属目录项
        public FileAccess access;//文件访问权限
        public FilePointer r_pointer = new FilePointer();//读指针
        public FilePointer w_pointer = new FilePointer();//写指针

        public OFTLE(FCB fcb)
        {
            if (openingFileCount == 5)
            {
                throw new Exception("打开的文件已达上限（5个），请关闭一些文件再重试");
            }

            this.fcb = fcb;
            isThisOpen = true;

            size = 0;

            beginBlockNum = fcb.beginBlockNum;
            size = fcb.fileSize;
            if (fcb.dirType == 0x04)
            {
                access = FileAccess.ReadWrite;
            }
            else if (fcb.dirType == 0x02 || fcb.dirType == 0x01)
            {
                access = FileAccess.Read;
            }

            openingFileCount++;
            openedOFTLEs.Add(this);
        }

        public string GetAttributes()
        {
            string res = Environment.NewLine;
            if(fcb.dirType < 8)//文件
            {
                res += $"\t文件 {fullName} 的属性{Environment.NewLine}";
                res += $"\t文件名称：{fullName}{Environment.NewLine}";
                res += $"\t绝对路径：{absPath}{Environment.NewLine}";
                res += $"\t起始盘块：{fcb.beginBlockNum}{Environment.NewLine}";
                res += $"\t占用空间：{$"{size * 64} 字节（{size} 盘块）"}{Environment.NewLine}";
                if (fcb.dirType == 0x04)
                {
                    res += $"\t访问权限：读写(RAW){Environment.NewLine}";
                }
                else if (fcb.dirType == 0x02)
                {
                    res += $"\t访问权限：系统(SYS){Environment.NewLine}";
                }
                else if (fcb.dirType == 0x01)
                {
                    res += $"\t访问权限：只读(R){Environment.NewLine}";
                }
            }
            else
            {
                res += $"\t目录 {fullName} 的属性{Environment.NewLine}";
                res += $"\t文件名称：{fullName}{Environment.NewLine}";
                res += $"\t绝对路径：{absPath}{Environment.NewLine}";
                res += $"\t起始盘块：{fcb.beginBlockNum}{Environment.NewLine}";
            }
            return res;
        }

        /// <summary>
        /// 关闭文件，释放许可
        /// </summary>
        public void Close()
        {
            SaveFCB();
            if(isThisOpen)
            {
                openingFileCount--;
                isThisOpen = false;
                openedOFTLEs.Remove(this);
            }
        }

        /// <summary>
        /// 保存FCB
        /// </summary>
        public void SaveFCB()
        {
            fcb.fileSize = size;
            byte[] temp = fcb.ToByteArray();
            basicDirController.ModifyFCB(fcb.FCB_blockNum, fcb.FCB_lineNum, temp);
        }

        /// <summary>
        /// 判断文件是否已打开
        /// </summary>
        /// <param name="absPath"></param>
        /// <returns></returns>
        static public bool isOpened(string absPath)
        {
            foreach (var oftle in openedOFTLEs)
            {
                if(oftle.absPath.Equals(absPath))
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetOpeningFileCount()
        {
            return openingFileCount;
        }


        public class FilePointer
        {
            public byte logicBlockNum;
            public byte offset;

            public FilePointer()
            {
                logicBlockNum = 0;
                offset = 0;
            }
        }
    }
}
