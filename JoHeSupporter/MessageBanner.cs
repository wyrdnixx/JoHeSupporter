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
using System.Threading;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace JoHeSupporter
{
    public partial class MessageBanner : Form
    {

        static System.Windows.Forms.Timer bannerUpdateTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer bannerScrollingTimer = new System.Windows.Forms.Timer();

        // nur für die While-Schleife des Timers
        //static bool exitFlag = false;
        
        // wurde ein Banner zum Anzeigen in der Datei gefunden?
        bool MessageBannerFound = false;

        int TestCounter = 0;   //TESTS

        List<String> MessagesList = new List<String>();

        static string MessageType;
        static string MessageText;

        // DefaultIntervall - wird durch Message überschrieben, wenn eine Meldungszeile gesetzt ist.
        //static int DefaultIntervall = 60;
        static int DefaultIntervall = 20 * 1000;   // TESTS
        static int Intervall;

        public MessageBanner()
        {
            InitializeComponent();
            
            bannerScrollingTimer.Interval = 100;
            bannerScrollingTimer.Tick += new EventHandler(bannerScrollingTimerEvent);
            bannerUpdateTimer.Tick += new EventHandler(bannerUpdateTimerEvent);

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

            //int mlseconds = _seconds * 1000;
            // interval in mlsecunds -> _Seconds * 1000
            bannerUpdateTimer.Interval = _seconds * 1000;
            bannerUpdateTimer.Start();
                        
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
                this.lbl_MessageText.Left = this.lbl_MessageText.Left - 3;
            
            // wenn der Banner ganz links angekommen ist, dann wieder ganz rechts anfangen..
            if (this.lbl_MessageText.Right <= 0)
            {
                this.lbl_MessageText.Left = SystemInformation.VirtualScreen.Width;
            }





        }


        private void readBannerFile()
        {


            // Test - counter wie oft wird die Datei gelesen
            TestCounter++;
            Console.WriteLine("ReadBannerFile Counter: " + TestCounter);
            ///////////


            // MessageBannerFound Variable zurück setzen;
            MessageBannerFound = false;

                        
            StreamReader file =  new StreamReader(File.Open(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt",
                           FileMode.Open,
                           FileAccess.Read,
                           FileShare.ReadWrite));
            // System.IO.StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt");

            string line;

            // List mit den Messages leeren.
            MessagesList.Clear();

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Info: ") || line.StartsWith("Warning: ") || line.StartsWith("Resolved: "))
                {
                    // Es wurde eine Bannerzeile gefunden
                    MessagesList.Add(line);
                }
            }
            file.Close();

            //MessageBox.Show("List Size: " + Messages.Count());

            // Messages durchlaufen
            MessagesList.ForEach(delegate (String msgLine)
            {
                


                int _gotIntervall;
                MessageType = msgLine.Split(':')[0];

                // Wenn das Intervall aus Datei gelesen werden konnte und eine Zahl ist, dann setze das neue Intervall (* 1000 -> Sekunden in milliSekunden)
                Int32.TryParse(msgLine.Split(':')[1], out _gotIntervall);

                Intervall = _gotIntervall * 1000;
                //MessageBox.Show(Intervall.ToString());

                String MessageTargets = msgLine.Split(':')[2];

                //Console.WriteLine("Targets: " + MessageTargets);
                MessageBannerFound = checkValidTarget(MessageTargets);

                if (MessageBannerFound)
                {
                    MessageText = msgLine.Split(':')[3];
                    lbl_MessageText.Text = MessageText;

                    if (msgLine.StartsWith("Info: ")) { this.BackColor = Color.LightBlue; }
                    if (msgLine.StartsWith("Warning: ")) { this.BackColor = Color.OrangeRed; }
                    if (msgLine.StartsWith("Resolved: ")) { this.BackColor = Color.LightGreen; }
                }                

            }); 

            

                    ///////
            
            
            if (!MessageBannerFound) { Intervall = DefaultIntervall; }
            resetTimer();
        }


        private bool checkValidTarget (String _MessageTargets)
        {
            String TargetType = _MessageTargets.Split('[', ']')[1];
            Console.WriteLine("TargetType : >" + TargetType + "<");

            String Target = _MessageTargets.Split(']')[1];

            Console.WriteLine("Target: >" + Target + "<");

            // ToDo: validate if any criteria matches - not only last from list

            switch (TargetType)
            {
                case "User":
                    //Console.WriteLine("Current User: " + Environment.UserName.Trim().ToLower());
                    //Console.WriteLine("Target User: " + Target.Trim().ToLower());
                    if (Environment.UserName.Trim().ToLower() == Target.Trim().ToLower())
                    {                        
                        return true;
                    }
                    break;
                case "AD":
                    String ADField = Target.Split('(', ')')[1];
                    String ADString = Target.Split(')')[1];
                    //Console.WriteLine("ADField " + ADField);
                    //Console.WriteLine("ADString " + ADString);

                    String adResult = adCheck(ADField).ToLower().Trim();


                    if (adResult == ADString.ToLower().Trim())
                    {
                        return true;
                    }
                    break;
                default:
                    return false;                    
            }

            // last resort
            return false;
        }


        #region ToDo
        private string adCheck(String _propertie)
        {
            Console.WriteLine("Checking AD-Property : " + _propertie);

            try
            {
                PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, UserPrincipal.Current.ToString());

//                PropertyInfo[] probs = Type.GetType(user.GetType().ToString()).GetProperties();



                var properties = typeof(UserPrincipal).GetProperties();

                IList<KeyValuePair<string, object>> propertyValues = new List<KeyValuePair<string, object>>();

                //foreach (var propertyInfo in properties)
                //{
                //    if (propertyInfo.Name == _propertie)
                //    {
                //        Console.WriteLine("AD-Property found: " + propertyInfo.Name + " : " + propertyInfo.GetValue(user));
                //        return propertyInfo.GetValue(user).ToString();
                //    }

                //}


                DirectorySearcher deSearch = new DirectorySearcher((DirectoryEntry)user.GetUnderlyingObject());
                deSearch.PropertiesToLoad.Add(_propertie);
                SearchResultCollection results = deSearch.FindAll();

                if (results != null && results.Count > 0)
                {
                    ResultPropertyCollection rpc = results[0].Properties;
                    foreach (string rp in rpc.PropertyNames)
                    {
                        // if (rp == "mobile")
                        //   Console.WriteLine(rpc["mobile"][0].ToString());
                        Console.WriteLine("Property found : " + _propertie + " : "+ rpc[_propertie][0].ToString());
                        return rpc[_propertie][0].ToString();
                    }
                }



                //////////////////////////////////
                } catch   (Exception e)
            {
                //MessageBox.Show("ADCheck-Error - maybe no domain user : " + e.Message);
            }

            return "";      
            
        }
        private string GetPropertyByName(Principal principal, string propertyName)
        {
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

            if (directoryEntry.Properties.Contains(propertyName))
            {
                return directoryEntry.Properties[propertyName].Value.ToString();
            }

            return null;
        }
        #endregion ToDo

        private void resetTimer()
        {

            // Timer reseten, damit er bei 0 anfängt
            bannerUpdateTimer.Stop();
            // Aktualisiere das Intervall - wurde evtl. durch Meldungszeile aktualisiert.
            bannerUpdateTimer.Interval = Intervall ;

            //Starte Timer mit dem aktuellen intervall
            bannerUpdateTimer.Start();
            //startTimer(Intervall);




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
