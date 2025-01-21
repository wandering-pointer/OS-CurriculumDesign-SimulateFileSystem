namespace SDFMS.UserInterface
{
    partial class Form_FileAttributes
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            tb_extendName = new TextBox();
            lb_dirType = new Label();
            lb_size = new Label();
            rb_readWrite = new RadioButton();
            rb_system = new RadioButton();
            rb_readOnly = new RadioButton();
            btn_apply = new Button();
            btn_close = new Button();
            label5 = new Label();
            tb_name = new TextBox();
            label6 = new Label();
            label7 = new Label();
            tb_absPath = new TextBox();
            lb_beginBlockNum = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 63);
            label1.Name = "label1";
            label1.Size = new Size(0, 17);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 24);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "文件名称：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 163);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 2;
            label3.Text = "占用空间：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 208);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 3;
            label4.Text = "访问权限：";
            // 
            // tb_extendName
            // 
            tb_extendName.Location = new Point(209, 21);
            tb_extendName.Name = "tb_extendName";
            tb_extendName.Size = new Size(77, 23);
            tb_extendName.TabIndex = 4;
            tb_extendName.TextChanged += tb_extendName_TextChanged;
            // 
            // lb_dirType
            // 
            lb_dirType.AutoSize = true;
            lb_dirType.Location = new Point(86, 160);
            lb_dirType.Name = "lb_dirType";
            lb_dirType.Size = new Size(0, 17);
            lb_dirType.TabIndex = 5;
            // 
            // lb_size
            // 
            lb_size.AutoSize = true;
            lb_size.Location = new Point(86, 163);
            lb_size.Name = "lb_size";
            lb_size.Size = new Size(13, 17);
            lb_size.TabIndex = 6;
            lb_size.Text = "-";
            // 
            // rb_readWrite
            // 
            rb_readWrite.AutoSize = true;
            rb_readWrite.Location = new Point(86, 208);
            rb_readWrite.Name = "rb_readWrite";
            rb_readWrite.Size = new Size(86, 21);
            rb_readWrite.TabIndex = 7;
            rb_readWrite.TabStop = true;
            rb_readWrite.Text = "读写(RAW)";
            rb_readWrite.UseVisualStyleBackColor = true;
            rb_readWrite.CheckedChanged += rb_readWrite_CheckedChanged;
            // 
            // rb_system
            // 
            rb_system.AutoSize = true;
            rb_system.Location = new Point(86, 235);
            rb_system.Name = "rb_system";
            rb_system.Size = new Size(66, 21);
            rb_system.TabIndex = 8;
            rb_system.TabStop = true;
            rb_system.Text = "系统(R)";
            rb_system.UseVisualStyleBackColor = true;
            rb_system.CheckedChanged += rb_system_CheckedChanged;
            // 
            // rb_readOnly
            // 
            rb_readOnly.AutoSize = true;
            rb_readOnly.Location = new Point(86, 262);
            rb_readOnly.Name = "rb_readOnly";
            rb_readOnly.Size = new Size(66, 21);
            rb_readOnly.TabIndex = 9;
            rb_readOnly.TabStop = true;
            rb_readOnly.Text = "只读(R)";
            rb_readOnly.UseVisualStyleBackColor = true;
            rb_readOnly.CheckedChanged += rb_readOnly_CheckedChanged;
            // 
            // btn_apply
            // 
            btn_apply.Enabled = false;
            btn_apply.Location = new Point(12, 302);
            btn_apply.Name = "btn_apply";
            btn_apply.Size = new Size(88, 37);
            btn_apply.TabIndex = 10;
            btn_apply.Text = "应用";
            btn_apply.UseVisualStyleBackColor = true;
            btn_apply.Click += btn_apply_Click;
            // 
            // btn_close
            // 
            btn_close.Location = new Point(198, 302);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(88, 37);
            btn_close.TabIndex = 0;
            btn_close.Text = "取消";
            btn_close.UseVisualStyleBackColor = true;
            btn_close.Click += btn_close_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(192, 27);
            label5.Name = "label5";
            label5.Size = new Size(11, 17);
            label5.TabIndex = 12;
            label5.Text = ".";
            // 
            // tb_name
            // 
            tb_name.Location = new Point(86, 21);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(100, 23);
            tb_name.TabIndex = 13;
            tb_name.TextChanged += tb_name_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 121);
            label6.Name = "label6";
            label6.Size = new Size(68, 17);
            label6.TabIndex = 14;
            label6.Text = "起始盘块：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 80);
            label7.Name = "label7";
            label7.Size = new Size(68, 17);
            label7.TabIndex = 15;
            label7.Text = "绝对路径：";
            // 
            // tb_absPath
            // 
            tb_absPath.Location = new Point(86, 77);
            tb_absPath.Name = "tb_absPath";
            tb_absPath.ReadOnly = true;
            tb_absPath.Size = new Size(200, 23);
            tb_absPath.TabIndex = 16;
            // 
            // lb_beginBlockNum
            // 
            lb_beginBlockNum.AutoSize = true;
            lb_beginBlockNum.Location = new Point(86, 121);
            lb_beginBlockNum.Name = "lb_beginBlockNum";
            lb_beginBlockNum.Size = new Size(13, 17);
            lb_beginBlockNum.TabIndex = 17;
            lb_beginBlockNum.Text = "-";
            // 
            // Form_FileAttributes
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 351);
            Controls.Add(lb_beginBlockNum);
            Controls.Add(tb_absPath);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(tb_name);
            Controls.Add(label5);
            Controls.Add(btn_close);
            Controls.Add(btn_apply);
            Controls.Add(rb_readOnly);
            Controls.Add(rb_system);
            Controls.Add(rb_readWrite);
            Controls.Add(lb_size);
            Controls.Add(lb_dirType);
            Controls.Add(tb_extendName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(314, 390);
            MinimizeBox = false;
            MinimumSize = new Size(314, 390);
            Name = "Form_FileAttributes";
            Text = "文件 - 属性";
            FormClosing += Form_FileAttributes_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tb_extendName;
        private Label lb_dirType;
        private Label lb_size;
        private RadioButton rb_readWrite;
        private RadioButton rb_system;
        private RadioButton rb_readOnly;
        private Button btn_apply;
        private Button btn_close;
        private Label label5;
        private TextBox tb_name;
        private Label label6;
        private Label label7;
        private TextBox tb_absPath;
        private Label lb_beginBlockNum;
    }
}