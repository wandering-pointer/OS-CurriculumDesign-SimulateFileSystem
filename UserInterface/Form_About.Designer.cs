namespace SDFMS.UserInterface
{
    partial class Form_About
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
            label3 = new Label();
            label1 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            pictureBox1 = new PictureBox();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 57);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 2;
            label3.Text = "完成时间：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "作者：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(86, 57);
            label4.Name = "label4";
            label4.Size = new Size(154, 17);
            label4.TabIndex = 3;
            label4.Text = "2024.09.13（除后续微调）";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 204);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 4;
            label5.Text = "使用教程：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(86, 204);
            label6.Name = "label6";
            label6.Size = new Size(44, 17);
            label6.TabIndex = 5;
            label6.Text = "见文档";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 106);
            label7.Name = "label7";
            label7.Size = new Size(68, 17);
            label7.TabIndex = 6;
            label7.Text = "完成耗时：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(86, 106);
            label8.Name = "label8";
            label8.Size = new Size(39, 17);
            label8.TabIndex = 7;
            label8.Text = "3-4周";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(36, 152);
            label9.Name = "label9";
            label9.Size = new Size(44, 17);
            label9.TabIndex = 8;
            label9.Text = "感想：";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(88, 152);
            label10.Name = "label10";
            label10.Size = new Size(68, 17);
            label10.TabIndex = 9;
            label10.Text = "好玩，好累";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = SDFS.Properties.Resources.angle;
            pictureBox1.Location = new Point(198, 204);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(157, 63);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(86, 9);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(206, 17);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/Movingpoint-P";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form_About
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(358, 279);
            Controls.Add(linkLabel1);
            Controls.Add(pictureBox1);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_About";
            Text = "关于";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Label label1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private PictureBox pictureBox1;
        private LinkLabel linkLabel1;
    }
}