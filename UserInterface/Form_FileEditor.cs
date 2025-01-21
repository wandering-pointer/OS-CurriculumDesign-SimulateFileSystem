using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using SDFMS.Logic_IO;

namespace SDFMS.UserInterface
{
    public partial class Form_FileEditor : Form
    {
        LogicFileStream fileStream;
        bool isSaved = true;

        public Form_FileEditor(LogicFileStream stream)
        {
            this.fileStream = stream;
            InitializeComponent();
            Text = stream.fullName;
            LoadFile();
            if (fileStream.oftle.access == FileAccess.Read)
            {
                btn_save.Enabled = false;
                tb_text.ReadOnly = true;
                Text += "（该文件不可修改）";
            }
            isSaved = true;
        }

        /// <summary>
        /// 加载文件内容
        /// </summary>
        public void LoadFile()
        {
            List<byte> bytes = new List<byte>();
            byte data;
            while ((data = fileStream.Read()) != 0)
            {
                bytes.Add(data);
            }
            tb_text.Text = Encoding.UTF8.GetString(bytes.ToArray());
            tb_text.SelectionStart = tb_text.Text.Length;
            tb_text.SelectionLength = 0;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public void SaveFile()
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(tb_text.Text + "\0");

                //如果文件尺寸修改前的尺寸，计算截取合适的文件尺寸
                byte fileSize = (byte)(bytes.Length / 64);
                if (bytes.Length % 64 != 0)
                {
                    fileSize++;
                }
                if (fileSize == 0)
                {
                    fileSize = 1;
                }
                if (fileSize < fileStream.GetFileSize())
                {
                    fileStream.CutLenth(fileSize);
                }

                fileStream.W_Seek(0);
                foreach (byte ch in bytes)
                {
                    fileStream.Write(ch);
                }
                isSaved = true;
                btn_save.Enabled = false;
                Form_Main.instance.RefreshFAT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFile();
            Form_Main.instance.Log($"修改文件 {fileStream.oftle.absPath}");
        }

        private void tb_text_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
            btn_save.Enabled = true;
        }

        private void Form_FileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved && fileStream.oftle.access != FileAccess.Read)
            {
                DialogResult result = MessageBox.Show("文件已修改，是否保存？", "SDFMS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                    Form_Main.instance.Log($"修改文件 {fileStream.oftle.absPath}");
                    fileStream.Close();
                    Form_Main.instance.RefreshFAT();
                    e.Cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    fileStream.Close();
                    Form_Main.instance.RefreshFAT();
                    e.Cancel = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                fileStream.Close();
                Form_Main.instance.RefreshFAT();
            }
        }

        private void Form_FileEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Main.instance.Log($"文件 {fileStream.oftle.absPath} 已关闭");
        }
    }
}
