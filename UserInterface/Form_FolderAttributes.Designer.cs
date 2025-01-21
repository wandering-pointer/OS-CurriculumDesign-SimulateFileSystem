namespace SDFMS.UserInterface
{
    partial class Form_FolderAttributes
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
            btn_apply = new Button();
            btn_close = new Button();
            tb_name = new TextBox();
            label7 = new Label();
            tb_absPath = new TextBox();
            label6 = new Label();
            lb_beginBlockNum = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 28);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "目录名称：";
            // 
            // btn_apply
            // 
            btn_apply.Enabled = false;
            btn_apply.Location = new Point(11, 200);
            btn_apply.Name = "btn_apply";
            btn_apply.Size = new Size(88, 37);
            btn_apply.TabIndex = 11;
            btn_apply.Text = "应用";
            btn_apply.UseVisualStyleBackColor = true;
            btn_apply.Click += btn_apply_Click;
            // 
            // btn_close
            // 
            btn_close.Location = new Point(198, 200);
            btn_close.MaximumSize = new Size(88, 37);
            btn_close.MinimumSize = new Size(88, 37);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(88, 37);
            btn_close.TabIndex = 0;
            btn_close.Text = "取消";
            btn_close.UseVisualStyleBackColor = true;
            btn_close.Click += btn_close_Click;
            // 
            // tb_name
            // 
            tb_name.Location = new Point(86, 25);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(200, 23);
            tb_name.TabIndex = 14;
            tb_name.TextChanged += tb_extendName_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 74);
            label7.Name = "label7";
            label7.Size = new Size(68, 17);
            label7.TabIndex = 16;
            label7.Text = "绝对路径：";
            // 
            // tb_absPath
            // 
            tb_absPath.Location = new Point(86, 68);
            tb_absPath.Name = "tb_absPath";
            tb_absPath.ReadOnly = true;
            tb_absPath.Size = new Size(200, 23);
            tb_absPath.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 119);
            label6.Name = "label6";
            label6.Size = new Size(68, 17);
            label6.TabIndex = 18;
            label6.Text = "起始盘块：";
            // 
            // lb_beginBlockNum
            // 
            lb_beginBlockNum.AutoSize = true;
            lb_beginBlockNum.Location = new Point(86, 119);
            lb_beginBlockNum.Name = "lb_beginBlockNum";
            lb_beginBlockNum.Size = new Size(13, 17);
            lb_beginBlockNum.TabIndex = 19;
            lb_beginBlockNum.Text = "-";
            // 
            // Form_FolderAttributes
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 249);
            Controls.Add(lb_beginBlockNum);
            Controls.Add(label6);
            Controls.Add(tb_absPath);
            Controls.Add(label7);
            Controls.Add(tb_name);
            Controls.Add(btn_close);
            Controls.Add(btn_apply);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_FolderAttributes";
            Text = "文件夹 - 属性";
            FormClosing += Form_FolderAttributes_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btn_apply;
        private Button btn_close;
        private TextBox tb_name;
        private Label label7;
        private TextBox tb_absPath;
        private Label label6;
        private Label lb_beginBlockNum;
    }
}