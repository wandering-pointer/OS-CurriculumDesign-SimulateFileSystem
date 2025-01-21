using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDFMS.UserInterface.UserControls
{
    public partial class MyProgressBar : UserControl
    {
        public MyProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置进度，有效范围 0 - 100
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            if (value > 100)
            {
                value = 100;
            }
            if (value < 0)
            {
                value = 0;
            }

            panel_value.Width = (int)(Width * value / 100);

            int red;
            int green;
            if (value < 50)
            {
                red = 0;
                green = 118 + (int)(118 * value / 50);
            }
            else if(value < 75)
            {
                red = (int)(235 * (value - 50) / 25);
                green = 235;
            }
            else
            {
                red = 235;
                green = (int)(235 * (100 - value) / 25);
            }
            panel_value.BackColor = Color.FromArgb(red, green, 0);
        }
    }
}
