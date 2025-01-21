using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using SDFMS.Basic_IO;
using SDFMS.Basic_IO.models;
using SDFMS.Driver;
using SDFMS.Logic_IO;
using SDFMS.Logic_IO.models;
using SDFMS.Other;
using SDFMS.UserControls;
using SDFMS.UserInterface;
using SDFMS.UserInterface.UserControls;

namespace SDFMS
{
    public partial class Form_Main : Form
    {
        public static Form_Main instance;

        public string diskName = "disk.data";
        FileIcon[] fileIcons;
        FolderIcon[] folderIcons;
        public string selectedFileItem;
        bool isRestart = false;

        public Form_Main()
        {
            instance = this;
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            bool isNewDisk = CreateDisk();
            LogicDirManager.Init(new BasicDirController(diskName, isNewDisk));
            Log("模拟磁盘加载完成");
            InitIcons();
        }

        private void Form_Main_Shown(object sender, EventArgs e)
        {
            RefreshFileMonitor();
            RefreshFAT();
        }

        /// <summary>
        /// 创建虚拟磁盘文件
        /// </summary>
        public bool CreateDisk()
        {
            if (!File.Exists(diskName))
            {
                if (MessageBox.Show("未检测到模拟磁盘文件，是否立即创建空白模拟磁盘文件？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                File.Create(diskName).Close();
                Log("创建模拟磁盘文件");
                return true;
            }
            return false;
        }

        /// <summary>
        /// 加载文件夹和文件的图标
        /// </summary>
        public void InitIcons()
        {
            fileIcons = new FileIcon[6];
            folderIcons = new FolderIcon[6];
            for (int i = 0; i < 6; i++)
            {
                fileIcons[i] = new FileIcon();
                folderIcons[i] = new FolderIcon();
                Point point = new Point((i % 3) * (fileIcons[i].Width + 5) + 20, (i / 3) * (fileIcons[i].Height + 5) + 70);
                fileIcons[i].Location = point;
                panel_fileMonitor.Controls.Add(fileIcons[i]);
                folderIcons[i].Location = point;
                panel_fileMonitor.Controls.Add(folderIcons[i]);
                fileIcons[i].ContextMenuStrip = cms_fileSelect;
                folderIcons[i].ContextMenuStrip = cms_folderSelect;
                //fileIcons[i].Hide();
                //folderIcons[i].Hide();
                folderIcons[i].SetDoubleClickEven(folderIcon_DoubleClick);
                fileIcons[i].SetDoubleClickEven(fileIcon_DoubleClick);
            }
        }

        /// <summary>
        /// 刷新文件操作窗口
        /// </summary>
        public void RefreshFileMonitor()
        {
            FCB[] currentDirTable = LogicDirManager.GetCurrentNodeTable();
            for (int i = 0; i < 6; i++)
            {
                folderIcons[i].Hide();
                fileIcons[i].Hide();
            }
            Thread.Sleep(10);//让图标闪一下，加强用户操作的反馈感
            for (int i = 0; i < 6; i++)
            {
                if (currentDirTable[i + 2].dirType >= 0x08)
                {
                    folderIcons[i].label_name.Text = currentDirTable[i + 2].name;
                    folderIcons[i].Show();
                }
                else if (!currentDirTable[i + 2].name.Contains('$'))
                {
                    fileIcons[i].label_name.Text = currentDirTable[i + 2].name + "." + currentDirTable[i + 2].extendName;
                    fileIcons[i].Show();
                }
            }
            tb_currentPath.Text = LogicDirManager.currentAbsPath;
        }

        /// <summary>
        /// 刷新fat显示和磁盘占用
        /// </summary>
        public void RefreshFAT()
        {
            //fatMonitor.RefreshData(logicDirManager.GetFat8()); 
            fatMonitor.RefreshData(FAT_Iterator.fat8);
            float diskOccupancy = (128f - FAT_Iterator.fat.GetEmptyBlockCount()) * 100f / 128f;
            lb_diskOccupancy.Text = diskOccupancy.ToString("f1") + "%";
            uc_diskOccupancy.SetValue(diskOccupancy);
        }

        /// <summary>
        /// 取消选中所有图标
        /// </summary>
        public void UnSelectAllItems()
        {
            for (int i = 0; i < 6; i++)
            {
                fileIcons[i].BorderStyle = BorderStyle.None;
                folderIcons[i].BorderStyle = BorderStyle.None;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(OFTLE.GetOpeningFileCount() > 0)
            {
                e.Cancel = true;
                MessageBox.Show("有文件未关闭", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isRestart)
            {
                LogicDirManager.Close();
                DiskDriver.instance.Close();
            }
        }

        /// <summary>
        /// 单击fat监视器的盘块，显示对应盘块的内容
        /// </summary>
        /// <param name="blockNum"></param>
        public void ShowDiskBlockContent(byte blockNum)
        {
            byte[] bytes = LogicDirManager.GetPureByte(blockNum);
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    if(bytes[i] == 10)
            //    {
            //        bytes[i] = 64;
            //    }
            //}
            string data = BitConverter.ToString(bytes).Replace("-", " ");
            string ascii = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ascii = Encoding.ASCII.GetString(bytes).Replace('\0', ' ').Replace("\n", " ").Replace("\r", " ");
            }
            for (int i = 8; i < ascii.Length; i += 8)
            {
                ascii = ascii.Insert(i, Environment.NewLine);
                i += 2;
            }
            lb_blockData.Text = data;
            lb_ascii.Text = ascii;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(string log)
        {
            tb_logs.Text += $"{DateTime.Now.ToString("HH:mm:ss")} - {log}{Environment.NewLine}";
            tb_logs.SelectionStart = tb_logs.Text.Length;
            tb_logs.SelectionLength = 0;
        }

        /// <summary>
        /// 右键菜单-创建文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_createFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Form_SetFolderName form = new Form_SetFolderName();
                DialogResult d = form.ShowDialog();
                if (d == DialogResult.Cancel)
                {
                    return;
                }
                LogicDirManager.CreateFolder(form.GetName());
                RefreshFileMonitor();
                RefreshFAT();
                Log($"在 {LogicDirManager.currentAbsPath} 创建文件夹 {form.GetName()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 右键菜单，删除文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否删除该文件夹？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                for (int i = 0; i < 6; i++)
                {
                    if (folderIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        LogicDirManager.DeleteFolder(folderIcons[i].label_name.Text);
                        RefreshFileMonitor();
                        RefreshFAT();
                        Log($"在 {LogicDirManager.currentAbsPath} 删除文件夹 {folderIcons[i].label_name.Text}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 双击文件夹进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (folderIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        string temp = folderIcons[i].label_name.Text;
                        LogicDirManager.EnterFolder(folderIcons[i].label_name.Text);
                        RefreshFileMonitor();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 按钮-返回上级目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_returnSuperio_Click(object sender, EventArgs e)
        {
            LogicDirManager.EnterFolder("..");
            RefreshFileMonitor();
        }

        private void btn_changeColor_Click(object sender, EventArgs e)
        {
            BlockColorGenerator.RegenerateColor();
            RefreshFAT();
        }

        private void btn_openCMD_Click(object sender, EventArgs e)
        {
            btn_openCMD.Enabled = false;
            btn_DevSettings.Enabled = false;
            Form_CMD cmd = new Form_CMD();
            panel_fileMonitor.Enabled = false;
            cmd.Show();
        }

        private void btn_DevSettings_Click(object sender, EventArgs e)
        {
            Form_DevSettings devSettings = new Form_DevSettings();
            devSettings.ShowDialog();
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            Form_About about = new Form_About();
            about.ShowDialog();
        }

        private void btn_refreshFatMonitor_Click(object sender, EventArgs e)
        {
            fatMonitor.Visible = false;
            RefreshFAT();
            fatMonitor.Visible = true;
        }

        private void btn_FormatDisk_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("删除当前模拟磁盘的文件，重启本应用", "重置系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.OK)
            {
                isRestart = true;
                DiskDriver.instance.Close();
                File.Delete(diskName);
                Assembly assembly = Assembly.GetEntryAssembly();
                try
                {
                    Process.Start(Path.GetFileName(assembly.Location));
                    Thread.Sleep(1000);
                    Application.Restart();
                }
                catch 
                {
                    Environment.Exit(0);
                }
            }
        }

        private void panel_fileMonitor_MouseEnter(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
            UnSelectAllItems();
        }

        /// <summary>
        /// 右键菜单，新建文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_createFile_Click(object sender, EventArgs e)
        {
            try
            {
                Form_SetFileName form = new Form_SetFileName();
                DialogResult d = form.ShowDialog();
                if (d == DialogResult.Cancel)
                {
                    return;
                }
                LogicDirManager.CreateFile(form.GetName(), 0x04);
                RefreshFileMonitor();
                RefreshFAT();
                Log($"在 {LogicDirManager.currentAbsPath} 创建文件 {form.GetName()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 右键菜单，删除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_fileDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否删除该文件？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                for (int i = 0; i < 6; i++)
                {
                    if (fileIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        LogicDirManager.DeleteFile(fileIcons[i].label_name.Text);
                        RefreshFileMonitor();
                        RefreshFAT();
                        Log($"在 {LogicDirManager.currentAbsPath} 删除文件 {fileIcons[i].label_name.Text}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 双击文件打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (fileIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        OFTLE oftle = LogicDirManager.Get_OFTLE_ByName(fileIcons[i].label_name.Text);
                        LogicFileStream fileStream = new LogicFileStream(oftle);
                        Form_FileEditor fileEditor = new Form_FileEditor(fileStream);
                        Log($"打开文件 {oftle.absPath}");
                        fileEditor.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新文件操作窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_refresh_Click(object sender, EventArgs e)
        {
            RefreshFileMonitor();
        }

        /// <summary>
        /// 查看/修改文件夹属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_folderProperty_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (folderIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        Form_FolderAttributes form = new Form_FolderAttributes(LogicDirManager.Get_OFTLE_ByName(folderIcons[i].label_name.Text));
                        //Log($"在 {LogicDirManager.currentAbsPath} 查看/修改文件夹属性 {fileIcons[i].label_name.Text}");
                        form.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查看修改文件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_fileProperty_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (fileIcons[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        Form_FileAttributes form = new Form_FileAttributes(LogicDirManager.Get_OFTLE_ByName(fileIcons[i].label_name.Text));
                        //Log($"在 {LogicDirManager.currentAbsPath} 查看/修改文件属性 {fileIcons[i].label_name.Text}");
                        form.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 右键菜单清空日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cms_logsClear_Click(object sender, EventArgs e)
        {
            tb_logs.Text = string.Empty;
        }

        /// <summary>
        /// 保持日志显示在最底部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_logs_TextChanged(object sender, EventArgs e)
        {
            tb_logs.SelectionStart = tb_logs.Text.Length;
            tb_logs.ScrollToCaret();
        }

        private void lb_blockData_MouseEnter(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
        }

        private void lb_ascii_MouseEnter(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
        }

        private void groupBox5_MouseHover(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
        }

        private void tb_logs_MouseEnter(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
        }

        private void Form_Main_MouseEnter(object sender, EventArgs e)
        {
            fatMonitor.RemoveSelectColor();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
