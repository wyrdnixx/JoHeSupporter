using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace JoHeSupporter
{
    public partial class MessageBanner : Form
    {

        static System.Windows.Forms.Timer bannerUpdateTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer bannerScrollingTimer = new System.Windows.Forms.Timer();

        // nur für die While-Schleife des Timers
        static bool exitFlag = false;
        
        // wurde ein Banner zum Anzeigen in der Datei gefunden?
        bool MessageBannerFound = false;


        static string MessageType;
        static string MessageText;

        // DefaultIntervall - wird durch Message überschrieben, wenn eine Meldungszeile gesetzt ist.
        static int DefaultIntervall = 3;
        static int Intervall;

        public MessageBanner()
        {
            InitializeComponent();

            
            startTimer(DefaultIntervall);
           
        }

        // Verhindert, dass beim Anzeigen der Form der Focus verlohren geht.
        #region Window  Form Settings

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }

        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
           // this.TopMost = true;
            this.Top = 0;
            this.Left = 0;
            //this.Width = ;

            int heigth = (SystemInformation.VirtualScreen.Height / 100);

            // MessageBox.Show("Heigth: " + heigth);

            this.Size = new Size(SystemInformation.VirtualScreen.Width, heigth );
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

            readBannerFile();
            
        }

        private void startTimer(int _seconds)
        {
            bannerUpdateTimer.Tick += new EventHandler(bannerUpdateTimerEvent);

            // Sets the timer interval to 5 seconds.
            int mlseconds = _seconds * 1000;
            bannerUpdateTimer.Interval = mlseconds;
            bannerUpdateTimer.Start();

            


            // Runs the timer, and raises the event.
            while (exitFlag == false)
            {
                // Processes all the events in the queue.
                Application.DoEvents();
            }

        }

        private void bannerUpdateTimerEvent(Object myObject,
                                            EventArgs myEventArgs)
        {

            readBannerFile();

            // Nur, wenn die Form nicht schon angezeigt ist und auch ein Banner in der File gefunden wurde.
            if (!this.Visible && MessageBannerFound)
            {
                this.Visible = true;
            
            }

            // wenn der Banner länger als die Bildschirmbreite ist...
            if (this.lbl_MessageText.Width > SystemInformation.VirtualScreen.Width)
            {
                // Wenn der Scrolling-Timer nicht schon läuft (wird sonnst doppelt gestartet)
                if (bannerScrollingTimer.Enabled == false)
                {
                    bannerScrollingTimer.Tick += new EventHandler(bannerScrollingTimerEvent);
                    bannerScrollingTimer.Interval = 100;
                    bannerScrollingTimer.Start();
                }
                
            }
            else { this.lbl_MessageText.Left = 40; bannerScrollingTimer.Stop(); }


        }
        
        private void bannerScrollingTimerEvent(Object myObject,
                                            EventArgs myEventArgs)
        {
                // Scrollen
                this.lbl_MessageText.Left = this.lbl_MessageText.Left - 3;
            
            // wenn der Banner ganz links angekommen ist, dann wieder ganz rechts anfangen..
            if (this.lbl_MessageText.Right <= 0)
            {
                this.lbl_MessageText.Left = SystemInformation.VirtualScreen.Width;
            }





        }


        private void readBannerFile()
        {

            // MessageBannerFound Variable zurück setzen;
            MessageBannerFound = false;

                        
            StreamReader file =  new StreamReader(File.Open(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt",
                           FileMode.Open,
                           FileAccess.Read,
                           FileShare.ReadWrite));
            // System.IO.StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt");

            string line;
            
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Info: ") || line.StartsWith("Warning: ") || line.StartsWith("Resolved: "))
                {
                    // Es wurde eine Bannerzeile gefunden
                    MessageBannerFound = true;
                    

                    int _gotIntervall;
                    MessageType = line.Split(':')[0];

                    // Wenn das Intervall aus Datei gelesen werden konnte und eine Zahl ist, dann setze das neue Intervall (* 1000 -> Sekunden in milliSekunden)
                    Int32.TryParse(line.Split(':')[1], out _gotIntervall);
                    
                        Intervall = _gotIntervall * 1000;
                        //MessageBox.Show(Intervall.ToString());
                    

                    MessageText = line.Split(':')[2];
                    lbl_MessageText.Text = MessageText;

                    if (line.StartsWith("Info: ")) { this.BackColor = Color.LightBlue; }
                    if (line.StartsWith("Warning: ")) { this.BackColor = Color.OrangeRed; }
                    if (line.StartsWith("Resolved: ")) { this.BackColor = Color.LightGreen; }
                }                
            }
            file.Close();
            
            if (!MessageBannerFound) { Intervall = DefaultIntervall; }
            resetTimer();
        }

     /*   private void lbl_CloseBanner_Click(object sender, EventArgs e)
        {


            readBannerFile();

            //Verstecke das Fenster, solange der Timer nicht das nächste mal anschlägt
            this.Visible = false;
            resetTimer();
            bannerScrollingTimer.Stop();

        } */

        private void resetTimer()
        {

            // Timer reseten, damit er bei 0 anfängt
            bannerUpdateTimer.Stop();
            // Aktualisiere das Intervall - wurde evtl. durch Meldungszeile aktualisiert.
            bannerUpdateTimer.Interval = Intervall;
            bannerUpdateTimer.Start();

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            readBannerFile();

            //Verstecke das Fenster, solange der Timer nicht das nächste mal anschlägt
            this.Visible = false;
            resetTimer();
            bannerScrollingTimer.Stop();
        }
    }

}
