using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFMS.Other
{
    /// <summary>
    /// 生成一个颜色列表，用于稳定的和谐的显示fat占用情况
    /// </summary>
    public class BlockColorGenerator
    {
        static Random random = new Random();
        static Color[] colors = new Color[128];
        static int usedIndex;

        static BlockColorGenerator()
        {
            RegenerateColor();
        }

        static public void RegenerateColor()
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = GenerateRandomColor();
                //if (i > 0)
                //{
                //    //了解到色相环上相对的颜色RGB相加为（255， 255， 255），所以我想让相邻颜色对比度高一些，但是效果很烂，我tm又不是美术生纠结这个干什么，操！平平淡淡才是真。
                //    int a = colors[i - 1].ToArgb();
                //    int b = colors[i].ToArgb();
                //    int ar, ag, ab, br, bg, bb, R, G, B;
                //    ar = a & 0x00FF0000;
                //    ag = a & 0x0000FF00;
                //    ab = a & 0x000000FF;
                //    br = b & 0x00FF0000;
                //    bg = b & 0x0000FF00;
                //    bb = b & 0x000000FF;
                //    R = (ar + br) >> 16;
                //    G = (ag + bg) >> 8;
                //    B = (ab + bb);
                //    while (!(R > 150 && R < 350 && G > 150 && G < 350 && B > 150 && B < 350))
                //    {
                //        colors[i] = GenerateRandomColor();
                //        a = colors[i - 1].ToArgb();
                //        b = colors[i].ToArgb();
                //        ar = a & 0x00FF0000;
                //        ag = a & 0x0000FF00;
                //        ab = a & 0x000000FF;
                //        br = b & 0x00FF0000;
                //        bg = b & 0x0000FF00;
                //        bb = b & 0x000000FF;
                //        R = (ar + br) >> 16;
                //        G = (ag + bg) >> 8;
                //        B = (ab + bb);
                //    }
                //}
            }
        }

        static public void Reset()
        {
            usedIndex = 0;
        }

        static public Color Generate()
        {
            return colors[usedIndex++];
        }

        private static Color GenerateRandomColor()
        {
            int red, green, blue;
            //大小限制为了生成的颜色不会太黑或太亮
            red = random.Next(70, 225);
            green = random.Next(70, 225);
            blue = random.Next(70, 200);

            //排除低饱和度颜色
            while (Math.Abs(red - green) < 40 && Math.Abs(green - blue) < 40 && Math.Abs(red - blue) < 40)
            {
                red = random.Next(70, 225);
                green = random.Next(70, 225);
                blue = random.Next(70, 200);
            }

            return Color.FromArgb(red, green, blue);
        }
    }
}

