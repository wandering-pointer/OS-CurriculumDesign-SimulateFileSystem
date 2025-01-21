namespace SDFMS.UserInterface
{
    partial class Form_SetFolderName
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
            tb_name = new TextBox();
            btn_Ok = new Button();
            btn_cancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "文件夹名称：";
            // 
            // tb_name
            // 
            tb_name.Location = new Point(98, 20);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(116, 23);
            tb_name.TabIndex = 1;
            // 
            // btn_Ok
            // 
            btn_Ok.Location = new Point(12, 58);
            btn_Ok.Name = "btn_Ok";
            btn_Ok.Size = new Size(92, 38);
            btn_Ok.TabIndex = 2;
            btn_Ok.Text = "确定";
            btn_Ok.UseVisualStyleBackColor = true;
            btn_Ok.Click += btn_Ok_Click;
            // 
            // btn_cancel
            // 
            btn_cancel.Location = new Point(142, 58);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(92, 38);
            btn_cancel.TabIndex = 7;
            btn_cancel.Text = "取消";
            btn_cancel.UseVisualStyleBackColor = true;
            btn_cancel.Click += btn_cancel_Click;
            // 
            // Form_SetFolderName
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(246, 108);
            ControlBox = false;
            Controls.Add(btn_cancel);
            Controls.Add(btn_Ok);
            Controls.Add(tb_name);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SetFolderName";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "新建文件夹";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_name;
        private Button btn_Ok;
        private Button btn_cancel;
    }
}