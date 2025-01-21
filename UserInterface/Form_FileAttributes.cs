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
    public partial class Form_FileAttributes : Form
    {
        OFTLE oftle;
        FCB newFCB;
        bool isNameChanged;
        bool isDirTypeChanged;
        string string_dirType;

        public Form_FileAttributes(OFTLE oftle)
        {
            InitializeComponent();
            this.oftle = oftle;
            Load();
        }

        private void Load()
        {
            newFCB = oftle.fcb.Copy();

            Text = "文件 " + oftle.fullName + " 属性";
            lb_size.Text = $"{oftle.size * 64} 字节（{oftle.size} 盘块）";
            tb_absPath.Text = oftle.absPath;
            lb_beginBlockNum.Text = oftle.fcb.beginBlockNum.ToString();

            tb_name.Text = oftle.fcb.name;
            tb_extendName.Text = oftle.fcb.extendName;
            if (oftle.fcb.dirType == 0x04)
            {
                rb_readWrite.Checked = true;
            }
            else if (oftle.fcb.dirType == 0x02)
            {
                rb_system.Checked = true;
            }
            else if (oftle.fcb.dirType == 0x01)
            {
                rb_readOnly.Checked = true;
            }
            btn_apply.Enabled = false;
            isNameChanged = false;
            isDirTypeChanged = false;

            tb_name.SelectionStart = tb_name.Text.Length;
            tb_name.SelectionLength = 0;

            tb_extendName.SelectionStart = tb_extendName.Text.Length;
            tb_extendName.SelectionLength = 0;

            btn_close.Focus();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                NameChecker.CheckFileName(tb_name.Text, tb_extendName.Text);
                newFCB.name = tb_name.Text;
                newFCB.extendName = tb_extendName.Text;

                newFCB.ToByteArray();
                oftle.fcb = newFCB;
                oftle.SaveFCB();
                Form_Main.instance.RefreshFileMonitor();
                btn_apply.Enabled = false;

                if (isNameChanged)
                {
                    isNameChanged = false;
                    Form_Main.instance.Log($"修改文件 {oftle.absPath} 名称为 {newFCB.name + "." + newFCB.extendName}");
                }
                if(isDirTypeChanged)
                {
                    isDirTypeChanged = false;
                    Form_Main.instance.Log($"修改文件 {oftle.absPath} 访问权限为 “{string_dirType}”");
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

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            isNameChanged = true;
        }

        private void rb_readWrite_CheckedChanged(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                newFCB.dirType = 0x04;
                string_dirType = "raw";
                isDirTypeChanged = true;
            }
        }

        private void rb_system_CheckedChanged(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                newFCB.dirType = 0x02;
                string_dirType = "sys";
                isDirTypeChanged = true;
            }
        }

        private void rb_readOnly_CheckedChanged(object sender, EventArgs e)
        {
            btn_apply.Enabled = true;
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                newFCB.dirType = 0x01;
                string_dirType = "r";
                isDirTypeChanged = true;
            }
        }

        private void Form_FileAttributes_FormClosing(object sender, FormClosingEventArgs e)
        {
            oftle.Close();
        }
    }
}
