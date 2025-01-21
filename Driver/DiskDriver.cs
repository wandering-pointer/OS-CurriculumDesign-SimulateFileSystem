using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Driver
{
    /// <summary>
    /// 模拟磁盘驱动，操作磁头在磁盘上以盘块为单位读写
    /// </summary>
    public class DiskDriver
    {
        string diskName;
        FileStream fileStream;
        public static DiskDriver instance;

        public DiskDriver(string diskName)
        {
            this.diskName = diskName;
            fileStream = new FileStream(diskName, FileMode.Open, FileAccess.ReadWrite);
            instance = this;
        }

        /// <summary>
        /// 物理盘块写
        /// </summary>
        /// <param name="blockNum">物理盘块号</param>
        /// <param name="buffer"></param>
        public void Write(int blockNum, byte[] buffer)
        {
            fileStream.Seek(blockNum * 64, SeekOrigin.Begin);
            fileStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 物理盘块读
        /// </summary>
        /// <param name="blockNum">物理盘块号</param>
        /// <param name="buffer"></param>
        public void Read(int blockNum, byte[] buffer)
        {
            fileStream.Seek(blockNum * 64, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, buffer.Length);
        }

        public void Close()
        {
            fileStream.Close();
        }
    }
}
