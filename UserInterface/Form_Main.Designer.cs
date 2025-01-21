namespace SDFMS
{
    partial class Form_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            fatMonitor = new UserControls.FatMonitor();
            groupBox1 = new GroupBox();
            tb_logs = new TextBox();
            cms_logs = new ContextMenuStrip(components);
            cms_logsClear = new ToolStripMenuItem();
            groupBox2 = new GroupBox();
            panel_fileMonitor = new Panel();
            cms_unSelect = new ContextMenuStrip(components);
            tsmi_createFolder = new ToolStripMenuItem();
            tsmi_createFile = new ToolStripMenuItem();
            tsmi_refresh = new ToolStripMenuItem();
            tb_currentPath = new TextBox();
            btn_returnSuperio = new Button();
            label1 = new Label();
            groupBox3 = new GroupBox();
            lb_ascii = new Label();
            lb_blockData = new Label();
            cms_folderSelect = new ContextMenuStrip(components);
            tsmi_open = new ToolStripMenuItem();
            tsmi_reName = new ToolStripMenuItem();
            tsmi_delete = new ToolStripMenuItem();
            tsmi_folderProperty = new ToolStripMenuItem();
            groupBox4 = new GroupBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            cms_fileSelect = new ContextMenuStrip(components);
            tsmi_fileOpen = new ToolStripMenuItem();
            tsmi_fileReName = new ToolStripMenuItem();
            tsmi_fileDelete = new ToolStripMenuItem();
            tsmi_fileProperty = new ToolStripMenuItem();
            label2 = new Label();
            groupBox5 = new GroupBox();
            btn_refreshFatMonitor = new Button();
            btn_about = new Button();
            btn_FormatDisk = new Button();
            btn_DevSettings = new Button();
            btn_openCMD = new Button();
            btn_changeColor = new Button();
            uc_diskOccupancy = new UserInterface.UserControls.MyProgressBar();
            lb_diskOccupancy = new Label();
            label3 = new Label();
            groupBox1.SuspendLayout();
            cms_logs.SuspendLayout();
            groupBox2.SuspendLayout();
            panel_fileMonitor.SuspendLayout();
            cms_unSelect.SuspendLayout();
            groupBox3.SuspendLayout();
            cms_folderSelect.SuspendLayout();
            groupBox4.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            cms_fileSelect.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // fatMonitor
            // 
            fatMonitor.BackColor = Color.FromArgb(255, 255, 192);
            fatMonitor.Location = new Point(12, 328);
            fatMonitor.Name = "fatMonitor";
            fatMonitor.Size = new Size(688, 312);
            fatMonitor.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tb_logs);
            groupBox1.Location = new Point(706, 328);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(325, 312);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "运行日志";
            // 
            // tb_logs
            // 
            tb_logs.ContextMenuStrip = cms_logs;
            tb_logs.Dock = DockStyle.Fill;
            tb_logs.Location = new Point(3, 19);
            tb_logs.Multiline = true;
            tb_logs.Name = "tb_logs";
            tb_logs.ReadOnly = true;
            tb_logs.ScrollBars = ScrollBars.Both;
            tb_logs.Size = new Size(319, 290);
            tb_logs.TabIndex = 10;
            tb_logs.TabStop = false;
            tb_logs.WordWrap = false;
            tb_logs.TextChanged += tb_logs_TextChanged;
            tb_logs.MouseEnter += tb_logs_MouseEnter;
            // 
            // cms_logs
            // 
            cms_logs.Items.AddRange(new ToolStripItem[] { cms_logsClear });
            cms_logs.Name = "cms_logs";
            cms_logs.Size = new Size(101, 26);
            // 
            // cms_logsClear
            // 
            cms_logsClear.Name = "cms_logsClear";
            cms_logsClear.Size = new Size(100, 22);
            cms_logsClear.Text = "清空";
            cms_logsClear.Click += cms_logsClear_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(panel_fileMonitor);
            groupBox2.Location = new Point(364, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(336, 310);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "文件操作窗口（右键菜单操作）";
            // 
            // panel_fileMonitor
            // 
            panel_fileMonitor.BackColor = SystemColors.Window;
            panel_fileMonitor.ContextMenuStrip = cms_unSelect;
            panel_fileMonitor.Controls.Add(tb_currentPath);
            panel_fileMonitor.Controls.Add(btn_returnSuperio);
            panel_fileMonitor.Controls.Add(label1);
            panel_fileMonitor.Dock = DockStyle.Fill;
            panel_fileMonitor.Location = new Point(3, 19);
            panel_fileMonitor.Name = "panel_fileMonitor";
            panel_fileMonitor.Size = new Size(330, 288);
            panel_fileMonitor.TabIndex = 0;
            panel_fileMonitor.MouseEnter += panel_fileMonitor_MouseEnter;
            // 
            // cms_unSelect
            // 
            cms_unSelect.Items.AddRange(new ToolStripItem[] { tsmi_createFolder, tsmi_createFile, tsmi_refresh });
            cms_unSelect.Name = "cms_unSelect";
            cms_unSelect.Size = new Size(137, 70);
            // 
            // tsmi_createFolder
            // 
            tsmi_createFolder.Name = "tsmi_createFolder";
            tsmi_createFolder.Size = new Size(136, 22);
            tsmi_createFolder.Text = "新建文件夹";
            tsmi_createFolder.Click += tsmi_createFolder_Click;
            // 
            // tsmi_createFile
            // 
            tsmi_createFile.Name = "tsmi_createFile";
            tsmi_createFile.Size = new Size(136, 22);
            tsmi_createFile.Text = "新建文件";
            tsmi_createFile.Click += tsmi_createFile_Click;
            // 
            // tsmi_refresh
            // 
            tsmi_refresh.Name = "tsmi_refresh";
            tsmi_refresh.Size = new Size(136, 22);
            tsmi_refresh.Text = "刷新";
            tsmi_refresh.Click += tsmi_refresh_Click;
            // 
            // tb_currentPath
            // 
            tb_currentPath.Location = new Point(64, 4);
            tb_currentPath.Name = "tb_currentPath";
            tb_currentPath.ReadOnly = true;
            tb_currentPath.Size = new Size(263, 23);
            tb_currentPath.TabIndex = 3;
            tb_currentPath.Text = "root:";
            // 
            // btn_returnSuperio
            // 
            btn_returnSuperio.Location = new Point(5, 32);
            btn_returnSuperio.Name = "btn_returnSuperio";
            btn_returnSuperio.Size = new Size(112, 33);
            btn_returnSuperio.TabIndex = 2;
            btn_returnSuperio.Text = "↑ 返回上级目录";
            btn_returnSuperio.UseVisualStyleBackColor = true;
            btn_returnSuperio.Click += btn_returnSuperio_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 5);
            label1.Margin = new Padding(5);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 0;
            label1.Text = "当前路径:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lb_ascii);
            groupBox3.Controls.Add(lb_blockData);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.FlatStyle = FlatStyle.Flat;
            groupBox3.Location = new Point(3, 19);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(343, 171);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "                        十六进制                                ascii          ";
            // 
            // lb_ascii
            // 
            lb_ascii.BackColor = SystemColors.ControlLight;
            lb_ascii.Dock = DockStyle.Right;
            lb_ascii.Font = new Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_ascii.Location = new Point(245, 19);
            lb_ascii.Margin = new Padding(1);
            lb_ascii.Name = "lb_ascii";
            lb_ascii.Size = new Size(95, 149);
            lb_ascii.TabIndex = 1;
            lb_ascii.MouseEnter += lb_ascii_MouseEnter;
            // 
            // lb_blockData
            // 
            lb_blockData.BackColor = SystemColors.ControlLight;
            lb_blockData.Dock = DockStyle.Left;
            lb_blockData.Font = new Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_blockData.Location = new Point(3, 19);
            lb_blockData.Margin = new Padding(1);
            lb_blockData.Name = "lb_blockData";
            lb_blockData.Size = new Size(240, 149);
            lb_blockData.TabIndex = 0;
            lb_blockData.MouseEnter += lb_blockData_MouseEnter;
            // 
            // cms_folderSelect
            // 
            cms_folderSelect.Items.AddRange(new ToolStripItem[] { tsmi_open, tsmi_reName, tsmi_delete, tsmi_folderProperty });
            cms_folderSelect.Name = "cms_select";
            cms_folderSelect.Size = new Size(113, 92);
            // 
            // tsmi_open
            // 
            tsmi_open.Name = "tsmi_open";
            tsmi_open.Size = new Size(112, 22);
            tsmi_open.Text = "打开";
            tsmi_open.Click += folderIcon_DoubleClick;
            // 
            // tsmi_reName
            // 
            tsmi_reName.Name = "tsmi_reName";
            tsmi_reName.Size = new Size(112, 22);
            tsmi_reName.Text = "重命名";
            tsmi_reName.Click += tsmi_folderProperty_Click;
            // 
            // tsmi_delete
            // 
            tsmi_delete.Name = "tsmi_delete";
            tsmi_delete.Size = new Size(112, 22);
            tsmi_delete.Text = "删除";
            tsmi_delete.Click += tsmi_delete_Click;
            // 
            // tsmi_folderProperty
            // 
            tsmi_folderProperty.Name = "tsmi_folderProperty";
            tsmi_folderProperty.Size = new Size(112, 22);
            tsmi_folderProperty.Text = "属性";
            tsmi_folderProperty.Click += tsmi_folderProperty_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(groupBox3);
            groupBox4.Location = new Point(12, 126);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(349, 193);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "盘块内容查看（单击下方FAT示意图的任意块）";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
            contextMenuStrip1.Name = "cms_select";
            contextMenuStrip1.Size = new Size(113, 92);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(112, 22);
            toolStripMenuItem1.Text = "打开";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(112, 22);
            toolStripMenuItem2.Text = "重命名";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(112, 22);
            toolStripMenuItem3.Text = "删除";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(112, 22);
            toolStripMenuItem4.Text = "属性";
            // 
            // cms_fileSelect
            // 
            cms_fileSelect.Items.AddRange(new ToolStripItem[] { tsmi_fileOpen, tsmi_fileReName, tsmi_fileDelete, tsmi_fileProperty });
            cms_fileSelect.Name = "cms_fileSelect";
            cms_fileSelect.Size = new Size(113, 92);
            // 
            // tsmi_fileOpen
            // 
            tsmi_fileOpen.Name = "tsmi_fileOpen";
            tsmi_fileOpen.Size = new Size(112, 22);
            tsmi_fileOpen.Text = "打开";
            tsmi_fileOpen.Click += fileIcon_DoubleClick;
            // 
            // tsmi_fileReName
            // 
            tsmi_fileReName.Name = "tsmi_fileReName";
            tsmi_fileReName.Size = new Size(112, 22);
            tsmi_fileReName.Text = "重命名";
            tsmi_fileReName.Click += tsmi_fileProperty_Click;
            // 
            // tsmi_fileDelete
            // 
            tsmi_fileDelete.Name = "tsmi_fileDelete";
            tsmi_fileDelete.Size = new Size(112, 22);
            tsmi_fileDelete.Text = "删除";
            tsmi_fileDelete.Click += tsmi_fileDelete_Click;
            // 
            // tsmi_fileProperty
            // 
            tsmi_fileProperty.Name = "tsmi_fileProperty";
            tsmi_fileProperty.Size = new Size(112, 22);
            tsmi_fileProperty.Text = "属性";
            tsmi_fileProperty.Click += tsmi_fileProperty_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(70, 41);
            label2.Name = "label2";
            label2.Size = new Size(206, 31);
            label2.TabIndex = 7;
            label2.Text = "模拟磁盘文件系统";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btn_refreshFatMonitor);
            groupBox5.Controls.Add(btn_about);
            groupBox5.Controls.Add(btn_FormatDisk);
            groupBox5.Controls.Add(btn_DevSettings);
            groupBox5.Controls.Add(btn_openCMD);
            groupBox5.Controls.Add(btn_changeColor);
            groupBox5.Controls.Add(uc_diskOccupancy);
            groupBox5.Controls.Add(lb_diskOccupancy);
            groupBox5.Controls.Add(label3);
            groupBox5.Location = new Point(709, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(319, 310);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "监控与管理";
            groupBox5.Enter += groupBox5_Enter;
            groupBox5.MouseHover += groupBox5_MouseHover;
            // 
            // btn_refreshFatMonitor
            // 
            btn_refreshFatMonitor.Location = new Point(169, 94);
            btn_refreshFatMonitor.Name = "btn_refreshFatMonitor";
            btn_refreshFatMonitor.Size = new Size(121, 46);
            btn_refreshFatMonitor.TabIndex = 17;
            btn_refreshFatMonitor.Text = "刷新FAT监视器";
            btn_refreshFatMonitor.UseVisualStyleBackColor = true;
            btn_refreshFatMonitor.Click += btn_refreshFatMonitor_Click;
            // 
            // btn_about
            // 
            btn_about.Location = new Point(169, 232);
            btn_about.Name = "btn_about";
            btn_about.Size = new Size(121, 46);
            btn_about.TabIndex = 1;
            btn_about.Text = "关于...";
            btn_about.UseVisualStyleBackColor = true;
            btn_about.Click += btn_about_Click;
            // 
            // btn_FormatDisk
            // 
            btn_FormatDisk.Location = new Point(27, 232);
            btn_FormatDisk.Name = "btn_FormatDisk";
            btn_FormatDisk.Size = new Size(121, 46);
            btn_FormatDisk.TabIndex = 15;
            btn_FormatDisk.Text = "重置系统";
            btn_FormatDisk.UseVisualStyleBackColor = true;
            btn_FormatDisk.Click += btn_FormatDisk_Click;
            // 
            // btn_DevSettings
            // 
            btn_DevSettings.Location = new Point(169, 162);
            btn_DevSettings.Name = "btn_DevSettings";
            btn_DevSettings.Size = new Size(121, 46);
            btn_DevSettings.TabIndex = 14;
            btn_DevSettings.Text = "开发者选项";
            btn_DevSettings.UseVisualStyleBackColor = true;
            btn_DevSettings.Click += btn_DevSettings_Click;
            // 
            // btn_openCMD
            // 
            btn_openCMD.Location = new Point(27, 162);
            btn_openCMD.Name = "btn_openCMD";
            btn_openCMD.Size = new Size(121, 46);
            btn_openCMD.TabIndex = 13;
            btn_openCMD.Text = "命令行控制";
            btn_openCMD.UseVisualStyleBackColor = true;
            btn_openCMD.Click += btn_openCMD_Click;
            // 
            // btn_changeColor
            // 
            btn_changeColor.Location = new Point(27, 94);
            btn_changeColor.Name = "btn_changeColor";
            btn_changeColor.Size = new Size(121, 46);
            btn_changeColor.TabIndex = 12;
            btn_changeColor.Text = "换个颜色";
            btn_changeColor.UseVisualStyleBackColor = true;
            btn_changeColor.Click += btn_changeColor_Click;
            // 
            // uc_diskOccupancy
            // 
            uc_diskOccupancy.BorderStyle = BorderStyle.FixedSingle;
            uc_diskOccupancy.Location = new Point(81, 36);
            uc_diskOccupancy.Margin = new Padding(1);
            uc_diskOccupancy.Name = "uc_diskOccupancy";
            uc_diskOccupancy.Padding = new Padding(1);
            uc_diskOccupancy.Size = new Size(180, 24);
            uc_diskOccupancy.TabIndex = 11;
            // 
            // lb_diskOccupancy
            // 
            lb_diskOccupancy.AutoSize = true;
            lb_diskOccupancy.Location = new Point(264, 41);
            lb_diskOccupancy.Name = "lb_diskOccupancy";
            lb_diskOccupancy.Size = new Size(26, 17);
            lb_diskOccupancy.TabIndex = 10;
            lb_diskOccupancy.Text = "0%";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 41);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 9;
            label3.Text = "磁盘占用：";
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1034, 651);
            Controls.Add(groupBox5);
            Controls.Add(label2);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(fatMonitor);
            MaximizeBox = false;
            MaximumSize = new Size(1050, 690);
            Name = "Form_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "模拟磁盘文件系统 (SDFS)";
            FormClosing += Main_FormClosing;
            Shown += Form_Main_Shown;
            MouseEnter += Form_Main_MouseEnter;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            cms_logs.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            panel_fileMonitor.ResumeLayout(false);
            panel_fileMonitor.PerformLayout();
            cms_unSelect.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            cms_folderSelect.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            cms_fileSelect.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private TextBox tb_logs;
        public UserControls.FatMonitor fatMonitor;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ContextMenuStrip cms_unSelect;
        private ToolStripMenuItem tsmi_createFolder;
        private ToolStripMenuItem tsmi_createFile;
        private ContextMenuStrip cms_folderSelect;
        private ToolStripMenuItem tsmi_open;
        private ToolStripMenuItem tsmi_reName;
        private ToolStripMenuItem tsmi_delete;
        private ToolStripMenuItem tsmi_folderProperty;
        private Label label1;
        private ToolStripMenuItem tsmi_refresh;
        private Button btn_returnSuperio;
        private TextBox tb_currentPath;
        private GroupBox groupBox4;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ContextMenuStrip cms_fileSelect;
        private ToolStripMenuItem tsmi_fileOpen;
        private ToolStripMenuItem tsmi_fileReName;
        private ToolStripMenuItem tsmi_fileDelete;
        private ToolStripMenuItem tsmi_fileProperty;
        private ContextMenuStrip cms_logs;
        private ToolStripMenuItem cms_logsClear;
        private Label label2;
        private GroupBox groupBox5;
        private Label label3;
        private Label lb_diskOccupancy;
        private UserInterface.UserControls.MyProgressBar uc_diskOccupancy;
        private Button btn_changeColor;
        private Label lb_ascii;
        private Label lb_blockData;
        public Button btn_openCMD;
        public Panel panel_fileMonitor;
        private Button btn_FormatDisk;
        private Button btn_about;
        private Button btn_refreshFatMonitor;
        public Button btn_DevSettings;
    }
}
