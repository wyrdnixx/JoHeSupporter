using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeSupporter
{
    public partial class Picture_Preview : Form
    {
        param Param;

        public Picture_Preview(param _param)
        {
            this.Param = _param;
            InitializeComponent();
        }

        private void Picture_Preview_Load(object sender, EventArgs e)
        {
            
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenwidthPercent = (screenWidth / 100) * 10;
            int screenheightPercent = (screenHeight / 100) * 10;
            
            this.Location = new Point(0+ screenwidthPercent, 0+ screenheightPercent);
            pbPreview.Location = new Point(0, 0);
            this.Height = screenHeight - screenheightPercent*2;
            this.Width = screenWidth - screenwidthPercent *2;

            pbPreview.Image = Image.FromFile(Param.screenshotfile);
            pbPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPreview.Width = this.Width;
            pbPreview.Height = this.Height;

        }

        private void pbPreview_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
