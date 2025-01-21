using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDFMS.Basic_IO.models;
using SDFMS.Driver;
using SDFMS.Logic_IO.models;

namespace SDFMS.Basic_IO
{
    public class BasicFileController
    {
        FAT_Iterator fat_Iter;
        DiskDriver diskDriver = DiskDriver.instance;
        byte beginBlockNum;

        public BasicFileController(byte beginBlockNum) 
        {
            fat_Iter = new FAT_Iterator(beginBlockNum);
            this.beginBlockNum = beginBlockNum;
        }

        /// <summary>
        /// 读取，把逻辑盘块号转换为物理盘块号
        /// </summary>
        /// <param name="logicBlockNum">逻辑盘块号</param>
        /// <param name="buffer">缓冲区</param>
        public void Read(byte logicBlockNum, byte[] buffer)
        {
            fat_Iter.Reset();
            byte targetBlockNum = beginBlockNum;
            for (int i = 0; i < logicBlockNum; i++)
            {
                targetBlockNum = fat_Iter.Next();
            }
            fat_Iter.Reset();
            diskDriver.Read(targetBlockNum, buffer);
        }

        /// <summary>
        /// 写入，把逻辑盘块号转换为物理盘块号
        /// </summary>
        /// <param name="logicBlockNum">逻辑盘块号</param>
        /// <param name="buffer">缓冲区</param>
        /// <exception cref="Exception"></exception>
        public void Write(byte logicBlockNum, byte[] buffer)
        {
            fat_Iter.Reset();
            byte targetBlockNum = beginBlockNum;
            for (int i = 0; i < logicBlockNum; i++)
            {
                targetBlockNum = fat_Iter.Next();
            }
            if(targetBlockNum != 255)
            {
                diskDriver.Write(targetBlockNum, buffer);
            }
            else
            {
                //申请新盘块
                byte newBlockNum = FAT_Iterator.fat.FindEmptyBlock();
                if(newBlockNum == 255)
                {
                    throw new Exception("磁盘已满");
                }
                FAT_Iterator.fat.UseBlock(newBlockNum, 255);//占用新的盘块
                FAT_Iterator.fat.UseBlock(fat_Iter.previousBlockNum, newBlockNum);//把旧的末尾盘块接上新的末尾盘块
                diskDriver.Write(newBlockNum, buffer);
            }
            fat_Iter.Reset();
        }

        /// <summary>
        /// 截取文件长度到指定值
        /// </summary>
        /// <param name="lenth"></param>
        public void CutLenth(byte lenth)
        {
            fat_Iter.Reset();
            for(int i = 0;i < lenth;i++)
            {
                fat_Iter.Next();
            }
            FAT_Iterator.fat.UseBlock(fat_Iter.previousBlockNum, 255);
            fat_Iter.FreeBehind();
            fat_Iter.Reset();
        }

        public void Close()
        {
            //diskDriver.Close();
        }
    }
}
