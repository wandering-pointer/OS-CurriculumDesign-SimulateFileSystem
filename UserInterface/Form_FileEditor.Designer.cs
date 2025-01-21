namespace SDFMS.UserInterface
{
    partial class Form_FileEditor
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
            btn_save = new Button();
            tb_text = new TextBox();
            SuspendLayout();
            // 
            // btn_save
            // 
            btn_save.Enabled = false;
            btn_save.Location = new Point(9, 9);
            btn_save.Margin = new Padding(0);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(88, 35);
            btn_save.TabIndex = 0;
            btn_save.Text = "保存";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // tb_text
            // 
            tb_text.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_text.Location = new Point(9, 47);
            tb_text.Multiline = true;
            tb_text.Name = "tb_text";
            tb_text.ScrollBars = ScrollBars.Vertical;
            tb_text.Size = new Size(477, 266);
            tb_text.TabIndex = 1;
            tb_text.TextChanged += tb_text_TextChanged;
            // 
            // Form_FileEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 325);
            Controls.Add(tb_text);
            Controls.Add(btn_save);
            Name = "Form_FileEditor";
            Text = "fileName";
            FormClosing += Form_FileEditor_FormClosing;
            FormClosed += Form_FileEditor_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_save;
        private TextBox tb_text;
    }
}