using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Basic_IO.models
{
    /// <summary>
    /// 根据FAT获得连续的文件盘块编号
    /// </summary>
    public class FAT_Iterator
    {
        public static FAT8 fat;
        public static byte[] fat8;

        byte index;
        byte iterator;
        public byte previousBlockNum;

        /// <summary>
        /// 一个平平无奇的构造器
        /// </summary>
        /// <param name="fat8">FAT表</param>
        /// <param name="index">起始盘块号</param>
        public FAT_Iterator(byte index)
        {
            fat8 = fat.GetFATArray();
            this.index = index;
            this.iterator = index;
        }

        /// <summary>
        /// 获得下一个盘块编号
        /// </summary>
        /// <returns>下一个盘块编号</returns>
        public byte Next()
        {
            if (iterator > 127)
            {
                return 255;
            }
            previousBlockNum = iterator;
            iterator = fat8[iterator];
            return iterator;
        }

        /// <summary>
        /// 从iterator指向的盘块开始，连续释放盘块直到文件末尾
        /// </summary>
        /// <param name="blockNum"></param>
        public void FreeBehind()
        {
            byte temp;
            while(iterator != 255)
            {
                temp = iterator;
                Next();
                fat.FreeBlock(temp);
            }
        }

        /// <summary>
        /// 重置盘块号流到初始盘块号
        /// </summary>
        public void Reset()
        {
            iterator = index;
        }
    }
}
