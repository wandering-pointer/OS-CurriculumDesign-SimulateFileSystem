using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Basic_IO.models
{
    public class FAT8
    {
        byte[] fat8Array = new byte[128];

        public FAT8() 
        {
            FAT_Iterator.fat = this;
        }

        public byte[] GetFATArray()
        {
            return fat8Array;
        }

        public FAT8 GetFAT()
        {
            return this;
        }

        /// <summary>
        /// 加载FAT
        /// </summary>
        /// <param name="buffer1"></param>
        /// <param name="buffer2"></param>
        public void Load(byte[] buffer1, byte[] buffer2)
        {
            for (int i = 0; i < 64; i++)
            {
                fat8Array[i] = buffer1[i];
            }
            for (int i = 64; i < 128; i++)
            {
                fat8Array[i] = buffer2[i - 64];
            }
            FAT_Iterator.fat8 = fat8Array;
        }

        /// <summary>
        /// 寻找空盘块
        /// </summary>
        /// <returns>255：磁盘已满</returns>
        public byte FindEmptyBlock()
        {
            for (byte i = 3; i < 128; i++)
            {
                if (fat8Array[i] == 0)
                {
                    return i;
                }
            }
            return 255;
        }

        /// <summary>
        /// 占用此盘块并赋值
        /// </summary>
        /// <param name="offset">盘块编号</param>
        /// <param name="value">next值</param>
        public void UseBlock(byte offset, byte value)
        {
            fat8Array[offset] = value;
        }

        /// <summary>
        /// 释放该盘块
        /// </summary>
        /// <param name="offset"></param>
        public void FreeBlock(byte offset)
        {
            fat8Array[offset] = 0;
        }

        /// <summary>
        /// 统计剩余空盘块数量
        /// </summary>
        /// <returns></returns>
        public byte GetEmptyBlockCount()
        {
            byte count = 0;
            for (byte i = 0; i < 128; i++)
            {
                if (fat8Array[i] == 0)
                    count++;
            }
            return count;
        }
    }
}
