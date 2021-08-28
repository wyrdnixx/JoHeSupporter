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
    public partial class Banner : Form
    {
        private String MessageText;
        private Timer bannerScrollingTimer = new Timer();

        public Banner(String _MessageText)
        {
            InitializeComponent();
            bannerScrollingTimer.Interval = 2;
            bannerScrollingTimer.Tick += new EventHandler(bannerScrollingTimerEvent);
            this.MessageText = _MessageText;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //this.Hide();
            // this.TopMost = true;
            this.Top = 0;
            this.Left = 0;

            this.lbl_MessageText.Text = MessageText;

            int heigth = (SystemInformation.VirtualScreen.Height / 100);

            // MessageBox.Show("Heigth: " + heigth);

            this.Size = new Size(SystemInformation.VirtualScreen.Width, heigth);
            /*  this.lbl_CloseBanner.Top = 3;
              this.lbl_CloseBanner.Left = 3;
              this.lbl_CloseBanner.Font = new Font("Arial", heigth * 2, FontStyle.Bold);*/
            this.ShowInTaskbar = false;

            this.lbl_MessageText.Top = 3;
            this.lbl_MessageText.Left = 40;
            //  this.lbl_MessageText.Font = new Font("Arial", heigth, FontStyle.Bold);

            //this.lbl_MessageText.Font = new Font("Arial", this.Width/ 8);
            this.lbl_MessageText.Font = new Font("Arial", this.Height / 2);

            //MessageBox.Show("Heigth: " + this.lbl_MessageText.Font.Height);


            this.btnClose.BackgroundImageLayout = ImageLayout.Stretch;
            this.btnClose.BackgroundImage = JoHeSupporter.Properties.Resources.close_icon;

            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.BackColor = Color.Transparent;
            this.btnClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = Color.Transparent;




            this.btnClose.Left = 0;
            this.btnClose.Top = 0;
            this.btnClose.Width = this.Size.Height;
            this.btnClose.Height = this.Size.Height;


            // wenn der Banner länger als die Bildschirmbreite ist...
            if (this.lbl_MessageText.Width > SystemInformation.VirtualScreen.Width)
            {
                // Wenn der Scrolling-Timer nicht schon läuft (wird sonnst doppelt gestartet)
                if (bannerScrollingTimer.Enabled == false)
                {
                    //bannerScrollingTimer.Tick += new EventHandler(bannerScrollingTimerEvent);
                    //                    bannerScrollingTimer.Interval = 100;  // jetzt oben global eingestellt.
                    //Console.WriteLine(bannerScrollingTimer.Interval);
                    bannerScrollingTimer.Start();
                }

            }
            else { this.lbl_MessageText.Left = 40; bannerScrollingTimer.Stop(); }


        }



        private void bannerScrollingTimerEvent(Object myObject,
                                            EventArgs myEventArgs)
        {

            //Console.WriteLine("Scroller");
            // Scrollen
            this.lbl_MessageText.Left = this.lbl_MessageText.Left - 1;

            // wenn der Banner ganz links angekommen ist, dann wieder ganz rechts anfangen..
            if (this.lbl_MessageText.Right <= 0)
            {
                this.lbl_MessageText.Left = SystemInformation.VirtualScreen.Width;
            }





        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            //Verstecke das Fenster, solange der Timer nicht das nächste mal anschlägt
            this.Visible = false;
        }
    }
}
