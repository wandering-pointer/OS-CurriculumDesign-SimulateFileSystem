using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDFMS.Basic_IO;
using SDFMS.Basic_IO.models;
using SDFMS.Logic_IO.models;

namespace SDFMS.Logic_IO
{
    /// <summary>
    /// 对应逻辑层的逻辑文件管理，提供用户接口，负责文件的打开、关闭、读取、写入（随机写、截短）、权限管理。
    /// 每个打开的文件，对应一个LogicFileStream的实例
    /// </summary>
    public class LogicFileStream
    {
        public OFTLE oftle;//打开文件信息表
        public string fullName;
        public bool isOpen = false;//文件是否打开
        private byte[] r_buffer;//读缓冲区
        private byte[] w_buffer;//写缓冲区
        public BasicFileController basicFileController;

        /// <summary>
        /// 获得文件的流，即打开文件
        /// </summary>
        /// <param name="oftle"></param>
        public LogicFileStream(OFTLE oftle)
        {
            isOpen = true;
            this.oftle = oftle;
            fullName = oftle.fullName;
            r_buffer = new byte[64];
            w_buffer = new byte[64];
            basicFileController = basicFileController = new BasicFileController(oftle.beginBlockNum);
            basicFileController.Read(0, r_buffer);
            Array.Copy(r_buffer, w_buffer, 64);
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="data">一个字节</param>
        public void Write(byte data)
        {
            if(oftle.access == FileAccess.Read)
            {
                throw new Exception("该文件只读");
            }

            w_buffer[oftle.w_pointer.offset] = data;

            if(oftle.w_pointer.logicBlockNum == oftle.r_pointer.logicBlockNum)
            {
                //在同一个盘块读写时保证内容一致性
                r_buffer[oftle.w_pointer.offset] = data;
            }

            ref byte offset = ref oftle.w_pointer.offset;
            ref byte logicBlockNum = ref oftle.w_pointer.logicBlockNum;

            offset++;
            if(offset == 64)//进入下一个块
            {
                //这里有个非致命的小bug，写回整个缓冲区时才会判断是否有剩余的磁盘空间，如果没有，整个缓冲区的内容会被丢弃（抛异常）
                basicFileController.Write(logicBlockNum, w_buffer);//当前块写回
                Array.Fill<byte>(w_buffer, 0);//清空写缓冲区
                offset = 0;
                byte oldSize = logicBlockNum;
                logicBlockNum++;
                if(oftle.size < logicBlockNum + 1)
                {
                    oftle.size = (byte)(logicBlockNum +1);
                }
                if (logicBlockNum > oldSize)
                {
                    //oftle.size = logicBlockNum;
                }
                else
                {
                    basicFileController.Read(logicBlockNum, w_buffer);//读取已存在块，防止写回时0覆盖
                }
            }
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <returns>下一个字节</returns>
        public byte Read()
        {
            byte data = r_buffer[oftle.r_pointer.offset];

            ref byte offset = ref oftle.r_pointer.offset;
            ref byte logicBlockNum = ref oftle.r_pointer.logicBlockNum;

            offset++;
            if (offset == 64)//进入下一个块
            {
                offset = 0;
                logicBlockNum++;
                if (logicBlockNum > oftle.size)
                {
                    logicBlockNum--;
                    offset = 63;
                    return 0;
                }

                if (oftle.w_pointer.logicBlockNum == oftle.r_pointer.logicBlockNum)
                {
                    //在读指针进入和写指针同一个盘块，而写缓冲区还没有写回磁盘时，不能直接读取磁盘内容，要直接复制写缓冲区，不然读写内容不一致
                    Array.Copy(w_buffer, r_buffer, 64);
                }
                else
                {
                    basicFileController.Read(logicBlockNum, r_buffer);//读取新块
                }
            }
            return data;
        }

        /// <summary>
        /// 读指针跳到指定位置（从文件头开始，按字节）
        /// </summary>
        /// <param name="offset"></param>
        public void R_Seek(int offset)
        {
            if(offset < 0)
            {
                throw new Exception("位置不能为负数");
            }
            else if (offset < oftle.size * 64)
            {
                oftle.r_pointer.offset = (byte)(offset % 64);
                oftle.r_pointer.logicBlockNum = (byte)(offset / 64);
                basicFileController.Read(oftle.r_pointer.logicBlockNum, r_buffer);//读取已存在块，防止写回时0覆盖
            }
            else
            {
                throw new Exception("超出文件当前大小");
            }
        }

        /// <summary>
        /// 写指针跳到指定位置（从文件头开始，按字节）
        /// </summary>
        /// <param name="offset"></param>
        public void W_Seek(int offset, bool isNeedWriteBack = true)
        {
            if (offset < 0)
            {
                throw new Exception("位置不能为负数");
            }
            else if (offset < oftle.size * 64)
            {
                if(isNeedWriteBack)
                {
                    basicFileController.Write(oftle.w_pointer.logicBlockNum, w_buffer);//当前块写回
                }
                oftle.w_pointer.offset = (byte)(offset % 64);
                oftle.w_pointer.logicBlockNum = (byte)(offset / 64);
                basicFileController.Read(oftle.w_pointer.logicBlockNum, w_buffer);//读取新块
            }
            else
            {
                throw new Exception("超出文件当前大小");
            }
        }

        /// <summary>
        /// 写指针跳到文件尾部（最后一个盘块的最后一个字节）
        /// </summary>
        public void W_SeekEnd()
        {
            oftle.w_pointer.offset = 63;
            oftle.w_pointer.logicBlockNum = oftle.size;
            basicFileController.Read(oftle.r_pointer.logicBlockNum, r_buffer);//读取新块
        }

        /// <summary>
        /// 截短文件，截短后读指针和写指针跳到文件开头
        /// </summary>
        /// <param name="lenth"></param>
        public void CutLenth(byte lenth)
        {
            if(lenth <= oftle.size && lenth > 0)
            {
                oftle.size = lenth;
                basicFileController.CutLenth(lenth);
                W_Seek(0, false);
                R_Seek(0);
            }
            else
            {
                throw new Exception($"长度必须大于0，小于等于文件当前长度，当前长度：{oftle.size}");
            }
        }

        public byte GetFileSize()
        {
            return oftle.size;
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        public void Close()
        {
            if(isOpen)
            {
                try
                {
                    basicFileController.Write(oftle.w_pointer.logicBlockNum, w_buffer);//当前块写回
                }
                catch (Exception e) { }
                basicFileController.Close();
                oftle.Close();
                isOpen = false;
                LogicDirManager.RefreshCurrentFolder();
            }
        }
    }
}
