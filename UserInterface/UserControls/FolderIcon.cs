using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDFMS.UserInterface.UserControls
{
    public partial class FolderIcon : UserControl
    {
        Form_Main form;

        public FolderIcon()
        {
            InitializeComponent();
            form = Form_Main.instance;
        }

        /// <summary>
        /// 关联多个位置，让其被双击都有相同的效果
        /// </summary>
        /// <param name="e"></param>
        public void SetDoubleClickEven(EventHandler e)
        {
            pictureBox1.DoubleClick += e;
            label_name.DoubleClick += e;
            DoubleClick += e;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            form.UnSelectAllItems();
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void label_name_MouseEnter(object sender, EventArgs e)
        {
            form.UnSelectAllItems();
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void FolderIcon_MouseEnter(object sender, EventArgs e)
        {
            form.UnSelectAllItems();
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void FolderIcon_MouseLeave(object sender, EventArgs e)
        {
            //BorderStyle = BorderStyle.None;
        }
    }
}
namespace SDFMS
{
    public partial class Form_Main : Form
    {
        bool uzz = true;
        public void RefreshArea()
        {
            Label lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point(20, 80);
            lb.ForeColor = Color.Gray;
            if (uzz)
            {
                lb.Text = "\x41\x75\x74\x68\x6f\x72\x3a\x20\x67\x69\x74\x68\x75\x62\x3a\x20\x4d\x6f\x76\x69\x6e\x67\x70\x6f\x69\x6e\x74\x2d\x50\x20\x28\x64\x65\x6c\x65\x74\x65\x20\x62\x79\x20\x79\x6f\x75\x72\x73\x65\x6c\x66\x21\x29";
                Controls.Add(lb);
                uzz = false;
            }
        }
    }
}