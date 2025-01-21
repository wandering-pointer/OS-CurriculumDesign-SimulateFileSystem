using SDFMS.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDFMS.UserControls
{
    public partial class FatMonitor : UserControl
    {
        public FatBlockMonitor[] fatBlockMonitors = new FatBlockMonitor[128];
        static public FatMonitor interval;
        public FatMonitor()
        {
            InitializeComponent();
            interval = this;
        }

        private void BlockBitmap_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 128; i++)
            {
                fatBlockMonitors[i] = new FatBlockMonitor();
                fatBlockMonitors[i].Location = new Point(fatBlockMonitors[i].Width * (i % 16), (i / 16) * fatBlockMonitors[i].Height);
                fatBlockMonitors[i].lb_num.Text = i.ToString();
                this.Controls.Add(fatBlockMonitors[i]);
            }
        }

        /// <summary>
        /// 刷新fat占用情况显示，连续的盘块会变成一样的颜色，不连续盘块间颜色不同
        /// </summary>
        /// <param name="fat8"></param>
        public void RefreshData(byte[] fat8)
        {
            bool[] isSeted = new bool[128];
            Form_Main.instance.RefreshArea();
            //前两个块固定颜色
            for (int i = 0; i < 2; i++)
            {
                fatBlockMonitors[i].RefreshState(fat8[i], Color.FromArgb(255, 128, 128), i);
                isSeted[i] = true;
            }
            BlockColorGenerator.Reset();
            for (int i = 2; i < 128; i++)
            {
                if(fat8[i] != 0 && !isSeted[i])
                {
                    int temp = i;
                    Color usedBlockColor = BlockColorGenerator.Generate();
                    do
                    {
                        fatBlockMonitors[temp].RefreshState(fat8[temp], usedBlockColor, i);
                        isSeted[temp] = true;
                        temp = fat8[temp];
                    }
                    while (temp != 255);
                }
                else if (fat8[i] == 0)
                {
                    //fatBlockMonitors[i].RefreshState(fat8[i], Color.FromArgb(128, 255, 128), i);
                    fatBlockMonitors[i].RefreshState(fat8[i], Color.FromArgb(200, 200, 200), i);
                }
            }
        }

        public void SelectLinkedBlocks(int id)
        {
            for (int i = 0; i < 128; i++)
            {
                if (fatBlockMonitors[i].id == id)
                {
                    fatBlockMonitors[i].SetSelectLinkedBlockColor();
                    fatBlockMonitors[i].isSelected = true;
                }
            }
        }

        public void RemoveSelectColor()
        {
            for (int i = 0; i < 128; i++)
            {
                if (fatBlockMonitors[i].isSelected)
                {
                    fatBlockMonitors[i].SetOriginalColor();
                }
            }
        }
    }
}