using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Logic_IO.models
{
    public class FCB
    {
        public string name;//名称
        public string extendName;//拓展名

        /// <summary>
        /// 8：文件夹， 4：可读写文件， 2：系统文件， 1：只读文件
        /// </summary>
        public byte dirType;//类型/权限
        public byte beginBlockNum;//指向的文件（夹）起始物理盘块号
        public byte fileSize;//文件大小

        /// <summary>
        /// 该FCB存放在的盘块号
        /// </summary>
        public byte FCB_blockNum;

        /// <summary>
        /// 该FCB在所在的盘块中的第几行
        /// </summary>
        public byte FCB_lineNum;


        public static FCB Parse(byte[] data, byte FCB_blockNum, byte FCB_lineNum)
        {
            FCB ret = new FCB();
            byte[] temp1 = new byte[3];
            Array.Copy(data, 0, temp1, 0, 3);
            ret.name = Encoding.UTF8.GetString(temp1).Replace("\0", "");

            byte[] temp2 = new byte[2];
            Array.Copy(data, 3, temp2, 0, 2);
            ret.extendName = Encoding.UTF8.GetString(temp2).Replace("\0", "");

            ret.dirType = data[5];
            ret.beginBlockNum = data[6];
            ret.fileSize = data[7];

            ret.FCB_blockNum = FCB_blockNum;
            ret.FCB_lineNum = FCB_lineNum;

            return ret;
        }

        /// <summary>
        /// 把FCB实例转换为可直接写入磁盘的字节数组
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] ToByteArray()
        {
            byte[] temp;
            byte[] data = new byte[8];

            //检查主名称合法性,格式化
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            if (nameBytes.Length > 3)
            {
                throw new Exception($"主名称大于3字节，当前名称占用字节：{nameBytes.Length}");
            }
            byte[] nameBytes_formated = new byte[3];
            Array.Copy(nameBytes, 0, nameBytes_formated, 0, nameBytes.Length);
            name = Encoding.UTF8.GetString(nameBytes_formated);

            //检查拓展名合法性,格式化
            byte[] extendNameBytes = Encoding.UTF8.GetBytes(extendName);
            if (extendNameBytes.Length > 2)
            {
                throw new Exception($"拓展名大于2字节，当前名称占用字节：{extendNameBytes.Length}");
            }
            byte[] extendNameBytes_formated = new byte[2];
            Array.Copy(extendNameBytes, 0, extendNameBytes_formated, 0, extendNameBytes.Length);
            extendName = Encoding.UTF8.GetString(extendNameBytes_formated);

            temp = Encoding.UTF8.GetBytes(name + extendName);
            temp.CopyTo(data, 0);
            data[5] = dirType;
            data[6] = beginBlockNum;
            data[7] = fileSize;
            return data;
        }

        /// <summary>
        /// 生成一个数值相同的实例
        /// </summary>
        /// <returns></returns>
        public FCB Copy()
        {
            FCB ret = new FCB();
            ret.name = name;
            ret.extendName = extendName;
            ret.dirType = dirType;
            ret.beginBlockNum = beginBlockNum;
            ret.fileSize = fileSize;
            ret.FCB_blockNum = FCB_blockNum;
            ret.FCB_lineNum = FCB_lineNum;
            return ret;
        }
    }
}
