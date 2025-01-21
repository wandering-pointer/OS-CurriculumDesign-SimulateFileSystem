using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDFMS.Logic_IO.models;
using SDFMS.Other;

namespace SDFMS.UserInterface
{
    public partial class Form_FolderAttributes : Form
    {
        OFTLE oftle;
        FCB newFCB;
        bool isNameChanged;

        public Form_FolderAttributes(OFTLE oftle)
        {
            InitializeComponent();
            this.oftle = oftle;
            Load();
        }

        private void Load()
        {
            newFCB = oftle.fcb.Copy();
            Text = "文件夹 " + oftle.fullName + " 属性";
            lb_beginBlockNum.Text = oftle.fcb.beginBlockNum.ToString();
            tb_absPath.Text = oftle.absPath;
            tb_name.Text = oftle.fcb.name;
            btn_apply.Enabled = false;
            isNameChanged = false;

            tb_name.SelectionStart = tb_name.Text.Length;
            tb_name.SelectionLength = 0;

            btn_close.Focus();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                NameChecker.CheckFolderName(tb_name.Text);
                newFCB.name = tb_name.Text;

                newFCB.ToByteArray();
                oftle.fcb = newFCB;
                oftle.SaveFCB();
                Form_Main.instance.RefreshFileMonitor();
                btn_apply.Enabled = false;

                if (isNameChanged)
                {
                    isNameChanged = false;
                    Form_Main.instance.Log($"修改文件夹 {oftle.absPath} 名称为 {newFCB.name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tb_extendName_TextChanged(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            isNameChanged = true;
        }

        private void Form_FolderAttributes_FormClosing(object sender, FormClosingEventArgs e)
        {
            oftle.Close();
        }
    }
}
