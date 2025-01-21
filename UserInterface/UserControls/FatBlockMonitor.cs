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
    public partial class FatBlockMonitor : UserControl
    {
        public FatBlockMonitor()
        {
            InitializeComponent();
        }

        Color staticColor;
        //Color seleted = Color.FromArgb(150, 220, 255);
        //Color selectLinked = Color.FromArgb(110, 180, 255);
        Color seleted = Color.FromArgb(255, 255, 255);
        Color selectLinked = Color.FromArgb(230, 230 ,230);
        public int id;

        public void RefreshState(byte value, Color color, int id)
        {
            lb_state.Text = value.ToString();
            BackColor = staticColor = color;
            this.id = id;
        }

        private void FatBlockMonitor_Click(object sender, EventArgs e)
        {
            Form_Main.instance.ShowDiskBlockContent(byte.Parse(lb_num.Text));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form_Main.instance.ShowDiskBlockContent(byte.Parse(lb_num.Text));
        }

        private void lb_num_Click(object sender, EventArgs e)
        {
            Form_Main.instance.ShowDiskBlockContent(byte.Parse(lb_num.Text));
        }

        private void lb_state_Click(object sender, EventArgs e)
        {
            Form_Main.instance.ShowDiskBlockContent(byte.Parse(lb_num.Text));
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            setMouseEnterColor();
        }

        private void lb_num_MouseEnter(object sender, EventArgs e)
        {
            setMouseEnterColor();
        }

        private void lb_state_MouseEnter(object sender, EventArgs e)
        {
            setMouseEnterColor();
        }

        private void FatBlockMonitor_MouseEnter(object sender, EventArgs e)
        {
            setMouseEnterColor();
        }

        public bool isSelected = false;
        private void setMouseEnterColor()
        {
            FatMonitor.interval.RemoveSelectColor();
            isSelected = true;
            if (DevSettings.isAllowBlockLinkedHighLight)
            {
                FatMonitor.interval.SelectLinkedBlocks(id);//连续盘块联动显示
            }
            BackColor = seleted;
        }

        public void SetSelectLinkedBlockColor()
        {
            BackColor = selectLinked;
        }

        public void SetOriginalColor()
        {
            isSelected = false;
            BackColor = staticColor;
        }
    }
}
