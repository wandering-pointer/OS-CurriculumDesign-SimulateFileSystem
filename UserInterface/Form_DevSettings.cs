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

namespace SDFMS.UserInterface
{
    public partial class Form_DevSettings : Form
    {
        public Form_DevSettings()
        {
            InitializeComponent();
        }

        private void cb_cmdOpenFile_CheckedChanged(object sender, EventArgs e)
        {
            DevSettings.isAllowEditFileInCMD = cb_cmdOpenFile.Checked;
        }

        private void cb_blockLinkHighLight_CheckedChanged(object sender, EventArgs e)
        {
            DevSettings.isAllowBlockLinkedHighLight = cb_blockLinkHighLight.Checked;
        }

        private void Form_DevSettings_Shown(object sender, EventArgs e)
        {
            cb_cmdOpenFile.Checked = DevSettings.isAllowEditFileInCMD;
            cb_blockLinkHighLight.Checked = DevSettings.isAllowBlockLinkedHighLight;
            nud_sleepTime.Value = DevSettings.analysPerLineCmdSleepTime;
        }

        private void btn_runCmds_Click(object sender, EventArgs e)
        {
            try
            {
                Form_CMD form_CMD = new Form_CMD(true);
                form_CMD.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nud_sleepTime_ValueChanged(object sender, EventArgs e)
        {
            DevSettings.analysPerLineCmdSleepTime = (int)nud_sleepTime.Value;
        }
    }
}
