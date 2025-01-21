namespace SDFMS.UserInterface
{
    partial class Form_DevSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cb_cmdOpenFile = new CheckBox();
            cb_blockLinkHighLight = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            btn_runCmds = new Button();
            label4 = new Label();
            nud_sleepTime = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)nud_sleepTime).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(265, 17);
            label1.TabIndex = 0;
            label1.Text = "*使用开发者选项可能会引发错误或导致系统崩溃";
            // 
            // cb_cmdOpenFile
            // 
            cb_cmdOpenFile.AutoSize = true;
            cb_cmdOpenFile.Location = new Point(28, 44);
            cb_cmdOpenFile.Name = "cb_cmdOpenFile";
            cb_cmdOpenFile.Size = new Size(147, 21);
            cb_cmdOpenFile.TabIndex = 1;
            cb_cmdOpenFile.Text = "命令行窗口内打开文件";
            cb_cmdOpenFile.UseVisualStyleBackColor = true;
            cb_cmdOpenFile.CheckedChanged += cb_cmdOpenFile_CheckedChanged;
            // 
            // cb_blockLinkHighLight
            // 
            cb_blockLinkHighLight.AutoSize = true;
            cb_blockLinkHighLight.Location = new Point(28, 104);
            cb_blockLinkHighLight.Name = "cb_blockLinkHighLight";
            cb_blockLinkHighLight.Size = new Size(135, 21);
            cb_blockLinkHighLight.TabIndex = 2;
            cb_blockLinkHighLight.Text = "同文件盘块联动高光";
            cb_blockLinkHighLight.UseVisualStyleBackColor = true;
            cb_blockLinkHighLight.CheckedChanged += cb_blockLinkHighLight_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(28, 128);
            label2.Name = "label2";
            label2.Size = new Size(341, 17);
            label2.TabIndex = 3;
            label2.Text = "FAT监视器内鼠标进入时，属于相同文件的盘块会同时高亮显示";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlDark;
            label3.Location = new Point(28, 68);
            label3.Name = "label3";
            label3.Size = new Size(223, 17);
            label3.TabIndex = 4;
            label3.Text = "可使用\"open cmd\" \"write\" \"close\"命令";
            // 
            // btn_runCmds
            // 
            btn_runCmds.Location = new Point(28, 163);
            btn_runCmds.Name = "btn_runCmds";
            btn_runCmds.Size = new Size(91, 38);
            btn_runCmds.TabIndex = 18;
            btn_runCmds.Text = "执行命令脚本";
            btn_runCmds.UseVisualStyleBackColor = true;
            btn_runCmds.Click += btn_runCmds_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlDark;
            label4.Location = new Point(28, 204);
            label4.Name = "label4";
            label4.Size = new Size(270, 17);
            label4.TabIndex = 19;
            label4.Text = "打开系统同目录下的“cmds.txt”，执行其中的命令";
            // 
            // nud_sleepTime
            // 
            nud_sleepTime.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nud_sleepTime.Location = new Point(275, 172);
            nud_sleepTime.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_sleepTime.Name = "nud_sleepTime";
            nud_sleepTime.Size = new Size(62, 23);
            nud_sleepTime.TabIndex = 20;
            nud_sleepTime.ValueChanged += nud_sleepTime_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ControlDark;
            label5.Location = new Point(125, 174);
            label5.Name = "label5";
            label5.Size = new Size(144, 17);
            label5.TabIndex = 21;
            label5.Text = "执行每行命令后暂停(ms):";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(28, 221);
            label6.Name = "label6";
            label6.Size = new Size(300, 17);
            label6.TabIndex = 22;
            label6.Text = "暂停时间0-10000ms，数值较小时以电脑执行速度为准";
            // 
            // Form_DevSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(406, 244);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(nud_sleepTime);
            Controls.Add(label4);
            Controls.Add(btn_runCmds);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cb_blockLinkHighLight);
            Controls.Add(cb_cmdOpenFile);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(422, 283);
            MinimizeBox = false;
            MinimumSize = new Size(422, 283);
            Name = "Form_DevSettings";
            Text = "开发者选项";
            Shown += Form_DevSettings_Shown;
            ((System.ComponentModel.ISupportInitialize)nud_sleepTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox cb_cmdOpenFile;
        private CheckBox cb_blockLinkHighLight;
        private Label label2;
        private Label label3;
        private Button btn_runCmds;
        private Label label4;
        private NumericUpDown nud_sleepTime;
        private Label label5;
        private Label label6;
    }
}