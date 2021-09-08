using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeSupporter
{
    public partial class Banner : Form
    {
        private String MessageText;
        private Timer bannerScrollingTimer = new Timer();
        private String type;

        public Banner(String _MessageText, String _type)
        {
            InitializeComponent();
            bannerScrollingTimer.Interval = 2;
            bannerScrollingTimer.Tick += new EventHandler(bannerScrollingTimerEvent);
            this.MessageText = _MessageText;
            this.type = _type;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //this.Hide();
            // this.TopMost = true;
            this.Top = 0;
            this.Left = 0;

            // Force Banner on top / in front of other windows
            this.TopMost = true;

            // longer text enables scrolling - prevent from scroll over closebutton
            btnClose.BringToFront();

            // set colors for tpyes of banners
            if (type == "Info") { this.BackColor = Color.LightSeaGreen; }
            if (type == "Resolved") {this.BackColor = Color.LimeGreen; }
            if (type == "Warning") { this.BackColor = Color.Coral; }



            // set text
            this.linklbl_MessageText.Text = MessageText;

            // If text contains links - make them clickable
            if (this.linklbl_MessageText.Text.IndexOf("http://") != -1 | this.linklbl_MessageText.Text.IndexOf("https://") != -1)
            {
                this.linklbl_MessageText.Enabled = true;
                
                foreach(Match m in Regex.Matches(this.linklbl_MessageText.Text, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
                {
                    Console.WriteLine("Found URL in text " + m.Index + " : " + m.ToString());


                    // ToDo : Tests change link text - not completed
                    // exception if two links are present
                    try
                    {
                        this.linklbl_MessageText.Text = this.linklbl_MessageText.Text.Remove(m.Index, m.Length);
                        this.linklbl_MessageText.Text = this.linklbl_MessageText.Text.Insert(m.Index, m.ToString());

                        this.linklbl_MessageText.Links.Add(m.Index, m.Length, m.ToString());
                        this.linklbl_MessageText.Links[0].LinkData = m.ToString();
                    }catch(Exception ex)
                    {
                        Console.WriteLine("Exception on Link: \n" + this.linklbl_MessageText.Text + "\n" + m.ToString() + "\n" + ex.Message);
                    }
                    


                }
                // Link länge im Text
//                this.lbl_MessageText.Links.Add(this.lbl_MessageText.Text.IndexOf("http"), found.Length, found.ToString());
                this.linklbl_MessageText.LinkClicked += new LinkLabelLinkClickedEventHandler(lbl_MessageText_LinkClicked);

            } else
            { // disable link function if no links found
                this.linklbl_MessageText.Enabled = false;
            }

                int heigth = (SystemInformation.VirtualScreen.Height / 100);

            // MessageBox.Show("Heigth: " + heigth);

            this.Size = new Size(SystemInformation.VirtualScreen.Width, heigth);
            /*  this.lbl_CloseBanner.Top = 3;
              this.lbl_CloseBanner.Left = 3;
              this.lbl_CloseBanner.Font = new Font("Arial", heigth * 2, FontStyle.Bold);*/
            this.ShowInTaskbar = false;

            this.linklbl_MessageText.Top = 3;
            this.linklbl_MessageText.Left = 40;
            //  this.lbl_MessageText.Font = new Font("Arial", heigth, FontStyle.Bold);

            //this.lbl_MessageText.Font = new Font("Arial", this.Width/ 8);
            this.linklbl_MessageText.Font = new Font("Arial", this.Height / 2);

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
            if (this.linklbl_MessageText.Width > SystemInformation.VirtualScreen.Width)
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
            else { this.linklbl_MessageText.Left = 40; bannerScrollingTimer.Stop(); }


        }
        /// <summary>
        /// Link in text is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_MessageText_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)        
        {
            string target = e.Link.LinkData as string;
            Console.WriteLine("Link clicked: " + target);
            try { 
                System.Diagnostics.Process.Start(target); 
            } catch(Exception ex)
            {
                MessageBox.Show("Fehler beim Link öffnen: " + target + System.Environment.NewLine  +  ex.Message, "JoHeSupporter");
            }
            

            // schließe Banner nach dem Link geklickt wurde
            this.Visible = false;
        }

        private void bannerScrollingTimerEvent(Object myObject,
                                            EventArgs myEventArgs)
        {

            //Console.WriteLine("Scroller");
            // Scrollen
            this.linklbl_MessageText.Left = this.linklbl_MessageText.Left - 1;

            // wenn der Banner ganz links angekommen ist, dann wieder ganz rechts anfangen..
            if (this.linklbl_MessageText.Right <= 0)
            {
                this.linklbl_MessageText.Left = SystemInformation.VirtualScreen.Width;
            }





        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            //Verstecke das Fenster, solange der Timer nicht das nächste mal anschlägt
            this.Visible = false;
        }
    }
}
