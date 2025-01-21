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
    public partial class FileIcon : UserControl
    {
        Form_Main form;

        public FileIcon()
        {
            InitializeComponent();
            form = Form_Main.instance;
        }

        /// <summary>
        /// 关联多个位置，使其双击有同样的效果
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

        private void FileIcon_MouseEnter(object sender, EventArgs e)
        {
            form.UnSelectAllItems();
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void FileIcon_MouseLeave(object sender, EventArgs e)
        {
            //BorderStyle = BorderStyle.None;
        }
    }
}
