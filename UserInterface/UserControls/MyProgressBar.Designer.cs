namespace SDFMS.UserInterface.UserControls
{
    partial class MyProgressBar
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
            panel_value = new Panel();
            SuspendLayout();
            // 
            // panel_value
            // 
            panel_value.BackColor = Color.LimeGreen;
            panel_value.Dock = DockStyle.Left;
            panel_value.Location = new Point(1, 1);
            panel_value.Margin = new Padding(0);
            panel_value.Name = "panel_value";
            panel_value.Size = new Size(0, 16);
            panel_value.TabIndex = 0;
            // 
            // MyProgressBar
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panel_value);
            Margin = new Padding(2);
            Name = "MyProgressBar";
            Padding = new Padding(1);
            Size = new Size(98, 18);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_value;
    }
}
