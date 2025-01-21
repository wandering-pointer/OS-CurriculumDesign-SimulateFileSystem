using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Other
{
    public class NameChecker
    {
        public static void CheckFileName(string fullName)
        {
            string[] temp = fullName.Split(".");
            if(temp.Length < 2 || temp.Length > 2 )
            {
                throw new Exception("名称格式不合格");
            }
            CheckFileName(temp[0], temp[1]);
        }

        public static void CheckFileName(string name, string extendName)
        {
            if (name.Contains('$') || name.Contains('.') || name.Contains('/') || name.Contains(' '))
            {
                throw new Exception("名称禁止含有“$”“.”“/”“空格”");
            }
            if (extendName.Contains('$') || extendName.Contains('.') || extendName.Contains('/') || extendName.Contains(' '))
            {
                throw new Exception("拓展名禁止含有“$”“.”“/”");
            }
            if (name.Equals(""))
            {
                throw new Exception("名称禁止为空");
            }
            if (extendName.Equals(""))
            {
                throw new Exception("拓展名禁止为空");
            }
        }

        public static void CheckFolderName(string name)
        {
            if (name.Contains('$') || name.Contains('.') || name.Contains('/') || name.Contains(' '))
            {
                throw new Exception("名称禁止含有“$”“.”“/”“空格”");
            }
            if (name.Equals(""))
            {
                throw new Exception("名称禁止为空");
            }
        }
    }
}
