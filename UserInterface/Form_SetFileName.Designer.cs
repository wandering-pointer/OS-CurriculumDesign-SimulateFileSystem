namespace SDFMS.UserInterface
{
    partial class Form_SetFileName
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
            button_Ok = new Button();
            tb_name = new TextBox();
            tb_extendName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_cancel = new Button();
            SuspendLayout();
            // 
            // button_Ok
            // 
            button_Ok.Location = new Point(9, 68);
            button_Ok.Name = "button_Ok";
            button_Ok.Size = new Size(93, 36);
            button_Ok.TabIndex = 0;
            button_Ok.Text = "确定";
            button_Ok.UseVisualStyleBackColor = true;
            button_Ok.Click += button_Ok_Click;
            // 
            // tb_name
            // 
            tb_name.Location = new Point(9, 29);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(100, 23);
            tb_name.TabIndex = 1;
            // 
            // tb_extendName
            // 
            tb_extendName.Location = new Point(132, 29);
            tb_extendName.Name = "tb_extendName";
            tb_extendName.Size = new Size(100, 23);
            tb_extendName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(115, 35);
            label1.Name = "label1";
            label1.Size = new Size(11, 17);
            label1.TabIndex = 3;
            label1.Text = ".";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 9);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 4;
            label2.Text = "主名";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(132, 9);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 5;
            label3.Text = "拓展名";
            // 
            // btn_cancel
            // 
            btn_cancel.Location = new Point(152, 68);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(93, 36);
            btn_cancel.TabIndex = 6;
            btn_cancel.Text = "取消";
            btn_cancel.UseVisualStyleBackColor = true;
            btn_cancel.Click += btn_cancel_Click;
            // 
            // Form_SetFileName
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(257, 116);
            ControlBox = false;
            Controls.Add(btn_cancel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tb_extendName);
            Controls.Add(tb_name);
            Controls.Add(button_Ok);
            MaximizeBox = false;
            MaximumSize = new Size(273, 155);
            MinimizeBox = false;
            MinimumSize = new Size(273, 155);
            Name = "Form_SetFileName";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "新建文件";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_Ok;
        private TextBox tb_name;
        private TextBox tb_extendName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btn_cancel;
    }
}