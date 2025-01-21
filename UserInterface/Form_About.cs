using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDFMS.UserInterface
{
    public partial class Form_About : Form
    {
        public Form_About()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hentai！不要再摸啦！", "(╬◣д◢)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            string url = "https://github.com/Movingpoint-P";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
