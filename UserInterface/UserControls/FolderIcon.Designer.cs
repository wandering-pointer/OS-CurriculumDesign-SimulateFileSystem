namespace SDFMS.UserInterface.UserControls
{
    partial class FolderIcon
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label_name = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = SDFS.Properties.Resources.folder;
            pictureBox1.Location = new Point(8, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 69);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            // 
            // label_name
            // 
            label_name.Location = new Point(8, 80);
            label_name.Name = "label_name";
            label_name.Size = new Size(75, 21);
            label_name.TabIndex = 1;
            label_name.Text = "label1";
            label_name.TextAlign = ContentAlignment.TopCenter;
            label_name.MouseEnter += label_name_MouseEnter;
            // 
            // FolderIcon
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Controls.Add(label_name);
            Controls.Add(pictureBox1);
            Name = "FolderIcon";
            Padding = new Padding(5);
            Size = new Size(93, 105);
            MouseEnter += FolderIcon_MouseEnter;
            MouseLeave += FolderIcon_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        public Label label_name;
    }
}
