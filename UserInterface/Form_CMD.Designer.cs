namespace SDFMS.UserInterface
{
    partial class Form_CMD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CMD));
            panel1 = new Panel();
            tb_info = new TextBox();
            panel2 = new Panel();
            lb_help = new Label();
            tb_command = new TextBox();
            lb_currentDir = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(tb_info);
            panel1.Location = new Point(0, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(622, 380);
            panel1.TabIndex = 0;
            // 
            // tb_info
            // 
            tb_info.Dock = DockStyle.Fill;
            tb_info.Location = new Point(0, 0);
            tb_info.Multiline = true;
            tb_info.Name = "tb_info";
            tb_info.ReadOnly = true;
            tb_info.ScrollBars = ScrollBars.Vertical;
            tb_info.Size = new Size(622, 380);
            tb_info.TabIndex = 10;
            tb_info.Text = "欢迎来到命令行控制窗口！\r\n输入 help 查询所有命令\r\n上下方向键可以选择历史命令\r\n";
            // 
            // panel2
            // 
            panel2.Controls.Add(lb_help);
            panel2.Controls.Add(tb_command);
            panel2.Controls.Add(lb_currentDir);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 398);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(622, 46);
            panel2.TabIndex = 10;
            panel2.SizeChanged += panel2_SizeChanged;
            // 
            // lb_help
            // 
            lb_help.AutoSize = true;
            lb_help.ForeColor = SystemColors.Control;
            lb_help.Location = new Point(82, 49);
            lb_help.Name = "lb_help";
            lb_help.Size = new Size(412, 510);
            lb_help.TabIndex = 2;
            lb_help.Text = resources.GetString("lb_help.Text");
            // 
            // tb_command
            // 
            tb_command.AcceptsReturn = true;
            tb_command.Dock = DockStyle.Left;
            tb_command.Location = new Point(66, 10);
            tb_command.Name = "tb_command";
            tb_command.Size = new Size(543, 23);
            tb_command.TabIndex = 1;
            tb_command.KeyDown += tb_command_KeyDown;
            tb_command.KeyPress += tb_command_KeyPress;
            // 
            // lb_currentDir
            // 
            lb_currentDir.AutoSize = true;
            lb_currentDir.Dock = DockStyle.Left;
            lb_currentDir.Location = new Point(10, 10);
            lb_currentDir.Name = "lb_currentDir";
            lb_currentDir.Padding = new Padding(3);
            lb_currentDir.Size = new Size(56, 23);
            lb_currentDir.TabIndex = 0;
            lb_currentDir.Text = "root:/>";
            // 
            // Form_CMD
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 444);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form_CMD";
            Text = "命令行";
            FormClosing += Form_CMD_FormClosing;
            Shown += Form_CMD_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox tb_info;
        private TextBox tb_command;
        private Label lb_currentDir;
        private Label lb_help;
    }
}