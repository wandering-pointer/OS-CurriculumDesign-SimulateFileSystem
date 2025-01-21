using SDFMS.Logic_IO;
using SDFMS.Logic_IO.models;
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
    public partial class Form_CMD : Form
    {
        Form_Main form;
        string[] cmds;
        bool isEditing = false;
        LogicFileStream openingFile;
        LinkedList<string> usedCmds = new LinkedList<string>();
        LinkedListNode<string> usedCmd;
        bool isRunCmds;

        public Form_CMD(bool isRunCmds = false)
        {
            InitializeComponent();
            form = Form_Main.instance;
            this.isRunCmds = isRunCmds;
        }

        private string Analysis(string command)
        {
            try
            {
                cmds = command.Split(" ");
                if (cmds.Length == 0 || cmds[0].Equals(""))//空
                {
                    return "";
                }
                if (cmds[0].Equals("help"))//帮助
                {
                    return lb_help.Text;
                }
                else if (cmds[0].Equals("cls"))//清屏
                {
                    return null;
                }
                else if (cmds[0].Equals("cd"))//改变当前目录
                {
                    return CMD_ChangeCurrentDir();
                }
                else if (cmds[0].Equals("dir"))//显示当前目录
                {
                    return LogicDirManager.CurrentDirToString();
                }
                else if (cmds[0].Equals("new"))//新建
                {
                    return CMD_NewSomething();
                }
                else if (cmds[0].Equals("del"))//删除
                {
                    return CMD_DeleteSomething();
                }
                else if (cmds[0].Equals("info"))//查看属性
                {
                    return CMD_CheckAttributes();
                }
                else if (cmds[0].Equals("mod"))//改变属性
                {
                    return CMD_ModAttributes();
                }
                else if (cmds[0].Equals("open"))//打开文件
                {
                    return CMD_OpenFile();
                }
                else if (cmds[0].Equals("close"))//关闭文件
                {
                    return CMD_CloseFile();
                }
                else if (cmds[0].Equals("write"))//写入
                {
                    return CMD_WriteFile();
                }
                else if (cmds[0].Equals("cut"))//截短文件
                {
                    return CMD_CutFielLenth();
                }
                else if (cmds[0].Equals("quit"))//退出
                {
                    Close();
                    return "";
                }
                else if (cmds[0].Equals("clr_root"))//清空根目录
                {
                    LogicDirManager.DeleteFolder("root:");
                    form.RefreshFileMonitor();
                    form.RefreshFAT();
                    form.Log("清空根目录");
                    return "根目录已清空";
                }
                else
                {
                    return $"'{cmds[0]}'不是命令";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 截短文件
        /// </summary>
        /// <returns></returns>
        private string CMD_CutFielLenth()
        {
            if (!DevSettings.isAllowEditFileInCMD)
            {
                return "此命令为开发者选项，操作权限不足";
            }
            if (isEditing)
            {
                if (cmds.Length == 2)
                {
                    byte lenth = Byte.Parse(cmds[1]);
                    openingFile.CutLenth(lenth);
                    form.Log($"命令行内对文件 {openingFile.fullName} 截短至长度：{cmds[1]}");
                    return $"已对文件 {openingFile.fullName} 截短至长度：{cmds[1]}";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return "没有打开的文件";
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <returns></returns>
        private string CMD_WriteFile()
        {
            if (!DevSettings.isAllowEditFileInCMD)
            {
                return "此命令为开发者选项，操作权限不足";
            }
            if (isEditing)
            {
                if (cmds.Length == 2)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(cmds[1]);
                    foreach (byte ch in bytes)
                    {
                        openingFile.Write(ch);
                    }
                    form.RefreshFAT();
                    form.Log($"命令行内对文件 {openingFile.fullName} 写入");
                    return "已写入";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return "没有打开的文件";
            }
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <returns></returns>
        private string CMD_CloseFile()
        {
            if (!DevSettings.isAllowEditFileInCMD)
            {
                return "此命令为开发者选项，操作权限不足";
            }
            if (isEditing)
            {
                openingFile.Close();
                isEditing = false;
                form.RefreshFAT();
                form.Log($"命令行内关闭文件 {openingFile.fullName}");
                return $"文件 {openingFile.fullName} 已关闭";
            }
            else
            {
                return "没有打开的文件";
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <returns></returns>
        private string CMD_OpenFile()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数 [名称.拓展名]";
            }
            else if (cmds[1].Equals("cmd"))//打开文件-命令行编辑
            {
                if (!DevSettings.isAllowEditFileInCMD)
                {
                    return "此命令为开发者选项，操作权限不足";
                }
                if (cmds.Length < 3)
                {
                    return "无效的命令：缺少参数 [名称.拓展名]";
                }
                else if (cmds.Length == 3)//打开文件-命令行编辑-执行
                {
                    if (isEditing)
                    {
                        return "已存在使用CMD打开的文件，关闭后再试";
                    }
                    NameChecker.CheckFileName(cmds[2]);
                    OFTLE oftle;
                    openingFile = new LogicFileStream(oftle = LogicDirManager.Get_OFTLE_ByName(cmds[2]));
                    isEditing = true;
                    form.Log($"命令行内打开文件 {oftle.absPath}");
                    return $"文件 {cmds[2]} 已打开";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else if (cmds.Length == 2)//打开文件-窗口编辑
            {
                if (cmds.Length < 2)
                {
                    return "无效的命令：缺少参数 [名称.拓展名]";
                }
                else if (cmds.Length == 2)//打开文件-命令行编辑-执行
                {
                    NameChecker.CheckFileName(cmds[1]);
                    OFTLE oftle;
                    Form_FileEditor form_FileEditor = new Form_FileEditor(new LogicFileStream(oftle = LogicDirManager.Get_OFTLE_ByName(cmds[1])));
                    form_FileEditor.Show();
                    form.Log($"打开文件 {oftle.absPath}");
                    return $"文件 {cmds[1]} 已打开";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return "无效的命令：出现多余的参数";
            }
        }

        /// <summary>
        /// 改变属性
        /// </summary>
        /// <returns></returns>
        private string CMD_ModAttributes()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数 [dir]或[file]或[auth]";
            }
            else if (cmds[1].Equals("dir"))//改变属性-文件夹名称
            {
                if (cmds.Length < 4)
                {
                    return "无效的命令：缺少参数 [旧名称] [新名称]";
                }
                else if (cmds.Length == 4)//改变属性-文件夹名称-执行
                {
                    NameChecker.CheckFolderName(cmds[2]);
                    NameChecker.CheckFolderName(cmds[3]);
                    OFTLE oftle = LogicDirManager.Get_OFTLE_ByName(cmds[2]);
                    oftle.fcb.name = cmds[3];
                    oftle.Close();
                    form.RefreshFileMonitor();
                    form.Log($"修改文件夹 {oftle.absPath} 名称为 {cmds[3]}");
                    return "文件夹重命名成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else if (cmds[1].Equals("file"))//改变属性-文件名称
            {
                if (cmds.Length < 4)
                {
                    return "无效的命令：缺少参数 [旧 名称.拓展名] [新 名称.拓展名]";
                }
                else if (cmds.Length == 4)//改变属性-文件名称-执行
                {
                    NameChecker.CheckFileName(cmds[2]);
                    NameChecker.CheckFileName(cmds[3]);
                    OFTLE oftle = LogicDirManager.Get_OFTLE_ByName(cmds[2]);
                    string[] temp = cmds[3].Split('.');
                    oftle.fcb.name = temp[0];
                    oftle.fcb.extendName = temp[1];
                    oftle.Close();
                    form.RefreshFileMonitor();
                    form.Log($"修改文件 {oftle.absPath} 名称为 {cmds[3]}");
                    return "文件重命名成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else if (cmds[1].Equals("auth"))//改变属性-文件权限
            {
                if (cmds.Length < 4)
                {
                    return "无效的命令：缺少参数 [名称.拓展名] [权限名]";
                }
                else if (cmds.Length == 4)//改变属性-文件权限-执行
                {
                    NameChecker.CheckFileName(cmds[2]);
                    OFTLE oftle = LogicDirManager.Get_OFTLE_ByName(cmds[2]);
                    if (cmds[3].Equals("raw"))
                    {
                        oftle.fcb.dirType = 0x04;
                    }
                    else if (cmds[3].Equals("sys"))
                    {
                        oftle.fcb.dirType = 0x02;
                    }
                    else if (cmds[3].Equals("r"))
                    {
                        oftle.fcb.dirType = 0x01;
                    }
                    else
                    {
                        oftle.Close();
                        return "权限错误，可选类型：[raw] [sys] [r]";
                    }
                    oftle.Close();
                    form.RefreshFileMonitor();
                    form.Log($"修改文件 {oftle.absPath} 访问权限为 “{cmds[3]}”");
                    return "文件权限修改成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return $"'{cmds[1]}'不是有效参数";
            }
        }

        /// <summary>
        /// 查看属性
        /// </summary>
        /// <returns></returns>
        private string CMD_CheckAttributes()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数[名称]或[名称.拓展名]";
            }
            else if (cmds.Length == 2)//查看属性-执行
            {
                OFTLE oftle = LogicDirManager.Get_OFTLE_ByName(cmds[1]);
                string res = oftle.GetAttributes();
                oftle.Close();
                return res;
            }
            else
            {
                return "无效的命令：出现多余的参数";
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string CMD_DeleteSomething()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数[dir]或[file]";
            }
            else if (cmds[1].Equals("dir"))//删除-文件夹
            {
                if (cmds.Length < 3)
                {
                    return "无效的命令：缺少参数[名称]";
                }
                else if (cmds.Length == 3)//删除-文件夹-执行
                {
                    if (cmds[2].Equals("root:"))
                    {
                        return "清空根目录使用命令 clr_root";
                    }
                    NameChecker.CheckFolderName(cmds[2]);
                    LogicDirManager.DeleteFolder(cmds[2]);
                    form.RefreshFileMonitor();
                    form.RefreshFAT();
                    form.Log($"在 {LogicDirManager.currentAbsPath} 删除文件夹 {cmds[2]}");
                    return "删除文件夹成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else if (cmds[1].Equals("file"))//删除-文件
            {
                if (cmds.Length < 3)
                {
                    return "无效的命令：缺少参数[名称.拓展名]";
                }
                else if (cmds.Length == 3)//删除-文件-执行
                {
                    NameChecker.CheckFileName(cmds[2]);
                    LogicDirManager.DeleteFile(cmds[2]);
                    form.RefreshFileMonitor();
                    form.RefreshFAT();
                    form.Log($"在 {LogicDirManager.currentAbsPath} 删除文件 {cmds[2]}");
                    return "删除文件成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return $"'{cmds[1]}'不是有效参数";
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        private string CMD_NewSomething()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数[dir]或[file]";
            }
            else if (cmds[1].Equals("dir"))//新建-文件夹
            {
                if (cmds.Length < 3)
                {
                    return "无效的命令：缺少参数[名称]";
                }
                else if (cmds.Length == 3)//新建-文件夹-执行
                {
                    NameChecker.CheckFolderName(cmds[2]);
                    LogicDirManager.CreateFolder(cmds[2]);
                    form.RefreshFileMonitor();
                    form.RefreshFAT();
                    form.Log($"在 {LogicDirManager.currentAbsPath} 创建文件夹 {cmds[2]}");
                    return "新建文件夹成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else if (cmds[1].Equals("file"))//新建-文件
            {
                if (cmds.Length < 3)
                {
                    return "无效的命令：缺少参数[名称.拓展名]";
                }
                else if (cmds.Length == 3)//新建-文件-执行
                {
                    NameChecker.CheckFileName(cmds[2]);
                    LogicDirManager.CreateFile(cmds[2], 0x04);
                    form.RefreshFileMonitor();
                    form.RefreshFAT();
                    form.Log($"在 {LogicDirManager.currentAbsPath} 创建文件 {cmds[2]}");
                    return "新建文件成功";
                }
                else
                {
                    return "无效的命令：出现多余的参数";
                }
            }
            else
            {
                return $"'{cmds[1]}'不是有效参数";
            }
        }

        /// <summary>
        /// 改变当前目录
        /// </summary>
        /// <returns></returns>
        private string CMD_ChangeCurrentDir()
        {
            if (cmds.Length < 2)
            {
                return "无效的命令：缺少参数[路径]";
            }
            string path = cmds[1];
            if (LogicDirManager.QueryPath(path) == 255)
            {
                return "找不到指定的路径";
            }
            LogicDirManager.QueryPath(path, true);//改变当前目录-执行
            form.RefreshFileMonitor();
            return "";
        }

        private void WriteInfo(string info)
        {
            if (info == null)
            {
                tb_info.Text = string.Empty;
            }
            else
            {
                tb_info.Text += lb_currentDir.Text + tb_command.Text + Environment.NewLine;
                tb_info.Text += info + Environment.NewLine;
                lb_currentDir.Text = LogicDirManager.currentAbsPath + ">";
            }
            tb_command.Text = String.Empty;
            tb_info.SelectionStart = tb_info.Text.Length;
            tb_info.ScrollToCaret();
            tb_command.Width = panel2.Width - lb_currentDir.Width - 20;
        }

        private void RunCmds()
        {
            StreamReader file = new StreamReader("cmds.txt");
            while (!file.EndOfStream)
            {
                tb_command.Text = file.ReadLine();
                WriteInfo(Analysis(tb_command.Text));
                Application.DoEvents();
                Thread.Sleep(DevSettings.analysPerLineCmdSleepTime);
            }
            file.Close();
        }

        private void Form_CMD_Shown(object sender, EventArgs e)
        {
            lb_currentDir.Text = LogicDirManager.currentAbsPath + ">";
            tb_info.SelectionStart = tb_info.Text.Length;
            tb_info.SelectionStart = 0;
            tb_command.Width = panel2.Width - lb_currentDir.Width - 20;
            tb_command.Focus();

            if (isRunCmds)
            {
                DevSettings.isAllowEditFileInCMD = true;
                try
                {
                    RunCmds();
                }
                catch (Exception ex)
                {
                    DevSettings.isAllowEditFileInCMD = false;
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DevSettings.isAllowEditFileInCMD = false;
                Close();
            }
        }

        private void tb_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
                usedCmds.AddFirst(tb_command.Text);
                usedCmd = null;
                WriteInfo(Analysis(tb_command.Text));
            }
        }

        private void tb_command_KeyDown(object sender, KeyEventArgs e)
        {
            if (usedCmds.Count > 0)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (usedCmd == null)//新命令输入后首次读取特殊处理
                    {
                        usedCmd = usedCmds.First;
                        tb_command.Text = usedCmd.Value;
                        tb_command.SelectionStart = tb_command.Text.Length;
                        e.Handled = true;
                        return;
                    }
                    else if (usedCmd.Next != null)//先移动，再读取
                    {
                        usedCmd = usedCmd.Next;
                    }
                    tb_command.Text = usedCmd.Value;
                    tb_command.SelectionStart = tb_command.Text.Length;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (usedCmd == null)//新命令输入后首次读取禁止向下
                    {
                        //e.Handled = true;
                        tb_command.SelectionStart = tb_command.Text.Length;
                        return;
                    }
                    else if (usedCmd.Previous != null)//先移动，再读取
                    {
                        usedCmd = usedCmd.Previous;
                    }
                    tb_command.Text = usedCmd.Value;
                    tb_command.SelectionStart = tb_command.Text.Length;
                }
                //e.Handled = true;
            }
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            tb_command.Width = panel2.Width - lb_currentDir.Width - 20;
        }

        private void Form_CMD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isEditing)
            {
                openingFile.Close();
            }
            form.btn_openCMD.Enabled = true;
            form.btn_DevSettings.Enabled = true;
            form.panel_fileMonitor.Enabled = true;
        }
    }
}
