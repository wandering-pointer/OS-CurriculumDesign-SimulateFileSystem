namespace SDFMS.UserControls
{
    partial class FatBlockMonitor
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
            label1 = new Label();
            lb_num = new Label();
            lb_state = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(16, 17);
            label1.TabIndex = 0;
            label1.Text = "#";
            label1.Click += label1_Click;
            label1.MouseEnter += label1_MouseEnter;
            // 
            // lb_num
            // 
            lb_num.AutoSize = true;
            lb_num.Location = new Point(15, 0);
            lb_num.Name = "lb_num";
            lb_num.Size = new Size(29, 17);
            lb_num.TabIndex = 1;
            lb_num.Text = "128";
            lb_num.Click += lb_num_Click;
            lb_num.MouseEnter += lb_num_MouseEnter;
            // 
            // lb_state
            // 
            lb_state.AutoSize = true;
            lb_state.Location = new Point(3, 17);
            lb_state.Name = "lb_state";
            lb_state.Size = new Size(15, 17);
            lb_state.TabIndex = 2;
            lb_state.Text = "0";
            lb_state.Click += lb_state_Click;
            lb_state.MouseEnter += lb_state_MouseEnter;
            // 
            // FatBlockMonitor
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(128, 255, 128);
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lb_state);
            Controls.Add(lb_num);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            Name = "FatBlockMonitor";
            Size = new Size(43, 39);
            Click += FatBlockMonitor_Click;
            MouseEnter += FatBlockMonitor_MouseEnter;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lb_state;
        internal Label lb_num;
    }
}
