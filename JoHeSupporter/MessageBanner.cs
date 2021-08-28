using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JoHeSupporter
{

    public class MessageObject
    {

        private String type;
        private DateTime validFrom;
        private DateTime validUntil;
        private String validTo;
        private int intervall;
        private String messageText;
        private Timer MessageTimer = new Timer();
        public String Hash;
        private Banner banner;


        public MessageObject(String _type, DateTime _validFrom, DateTime _validUntil, String _validTo, int _Intervall, String _MessageText)
        {
            this.type = _type;
            this.validFrom = _validFrom;
            this.validUntil = _validUntil;
            this.validTo = _validTo;
            this.intervall = _Intervall;
            this.messageText = _MessageText;
                       
            MessageTimer.Interval = _Intervall;
            MessageTimer.Tick += new EventHandler(DisplayMessage);

            this.Hash = Utilities.General.CreateMD5(validFrom.ToString() + validUntil.ToString() + validTo + messageText);
           this.banner = new Banner(messageText);

            this.StartTimer();
        }



        public void StartTimer()
        {            
            MessageTimer.Start();
        }
        public void DisposeMessageBanner()
        {
            MessageTimer.Stop();
            banner.Dispose();
            
        }

        private void DisplayMessage(Object myObject, EventArgs myEventArgs)
        {
            
                Console.WriteLine(Hash + " : DisplayMessage: " + messageText);
            if(!banner.Visible) { banner.Show(); }
        }

    }

    public partial class MessageBanner : Form
    {

        private List<MessageObject> MessageCache = new List<MessageObject>();
        


        static readonly System.Windows.Forms.Timer TimerConfigReader = new System.Windows.Forms.Timer();


        public MessageBanner()
        {
            InitializeComponent();
            TimerConfigReader.Interval = 10000;
            TimerConfigReader.Tick += new EventHandler(ConfigReader);
            TimerConfigReader.Start();
        }


        private void ConfigReader(Object myObject, EventArgs myEventArgs)
        {
            Console.WriteLine("MessageCache count: " + MessageCache.Count);


            StreamReader file = new StreamReader(File.Open(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt",
                           FileMode.Open,
                           FileAccess.Read,
                           FileShare.ReadWrite));
            // System.IO.StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "MessageBanner.txt");

            string line;

           List<String> actualHashList = new List<String>();

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Info; ") || line.StartsWith("Warning; ") || line.StartsWith("Resolved; "))
                {
                    // Es wurde eine Bannerzeile gefunden

                    try
                    {

                        //int _gotIntervall;
                        String[] ConfiguredMessage = line.Split(';');

                        String MessageType = ConfiguredMessage[0];
                        DateTime validFrom = DateTime.Parse(ConfiguredMessage[1]);
                        DateTime validUntil = DateTime.Parse(ConfiguredMessage[2]);
                        int Intervall = int.Parse(ConfiguredMessage[3]) * 1000;
                        String validTo = ConfiguredMessage[4];
                        String MessageText = ConfiguredMessage[5];

                        Console.WriteLine("Message aus Config File: " + MessageType + " - " + validFrom + " - " + validUntil + " - " + validTo + " - " + MessageText);



                        String hash = Utilities.General.CreateMD5(validFrom.ToString() + validUntil.ToString() + validTo + MessageText);
                        Console.WriteLine("calculated Hash: " + hash);
                        actualHashList.Add(hash);
                        if ( CheckValidTarget(validFrom, validUntil, validTo))
                        {
                               
                            
                            //// ToDo : funktioniert nicht - wird immer noch mehrfach erzeugt
                            
                            bool allreadyExists = false;                            
                            foreach ( MessageObject x in MessageCache )
                            {
                                if (x.Hash == hash) { allreadyExists = true; };
                            }

                            if (!allreadyExists)
                            {
                                MessageCache.Add(new MessageObject(MessageType, validFrom, validUntil, validTo, Intervall, MessageText));
                                //MessageObject msg = ;
                                //msg.startTimer();
                                //tmpList.Add(msg);
                                
                            }
                        }                        

                    }catch(Exception e)
                    { Console.WriteLine("Fehler in MessageBanner Config: " + e.Message, "MessageBanner Config error"); }

                    }
            }
            file.Close();

            file.Dispose();

            //cleanup
            try
            {


                foreach (MessageObject x in MessageCache)
                {
                    bool stillValid = false;
                    foreach (String h in actualHashList)
                    {
                        if (x.Hash == h) { stillValid = true; }
                    }

                    if (!stillValid)
                    {
                        x.DisposeMessageBanner();
                        MessageCache.Remove(x);
                    }
                }
            }
            catch (Exception) { //nothing - MessageCache maybe changed
                                };

        }




        private bool CheckValidTarget(DateTime validFrom, DateTime validUntil, String validTo)
        {
            //check for valid timerange
            DateTime now = DateTime.Now;
            if (validFrom < now && validUntil > now) 
            {
                // nothing
            } else
            {
                // not in valid timerange
                return false;
            }


            String TargetType = validTo.Split('[', ']')[1];
            String Target = validTo.Split(']')[1];
            Console.WriteLine("TargetType : >" + TargetType + "<" + " & " + "Target: >" + Target + "<");
            

            // ToDo: validate if any criteria matches - not only last from list

            switch (TargetType)
            {
                case "*": // meldung gillt für alle 
                    return true;
                case "U":
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

                    String adResult = adCheckField(ADField).ToLower().Trim();
                    if (adResult == ADString.ToLower().Trim())
                    {
                        return true;
                    }
                    break;
                case "G":

                    if (adCheckGroup(Target))
                    {
                        Console.WriteLine("AD-Group found: " + Target);
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            // last resort
            return false;
        }









        /// <summary>
        /// ////////// Vor Refactor MessageBanner.cs
        /// </summary>


        static System.Windows.Forms.Timer bannerUpdateTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer bannerScrollingTimer = new System.Windows.Forms.Timer();
                
        // wurde ein Banner zum Anzeigen in der Datei gefunden?
        bool MessageBannerFound = false;

        //int TestCounter = 0;   //TESTS

        List<String> MessagesList = new List<String>();

        

        // DefaultIntervall - wird durch Message überschrieben, wenn eine Meldungszeile gesetzt ist.
        //static int DefaultIntervall = 60;
        static int DefaultIntervall = 60 * 1000;   // TESTS
        static int Intervall;

        // Liste der Messages die für einen Client angezeigt werden sollen.
        List<MessageToShow> msgList = new List<MessageToShow>();

        //public MessageBanner()
        public void MessageBannerOld()
        {
            InitializeComponent();
            
            bannerScrollingTimer.Interval = 2;
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
            //if (!this.Visible && MessageBannerFound)
            if (!this.Visible && msgList.Count() > 0)
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
                this.lbl_MessageText.Left = this.lbl_MessageText.Left - 1;
            
            // wenn der Banner ganz links angekommen ist, dann wieder ganz rechts anfangen..
            if (this.lbl_MessageText.Right <= 0)
            {
                this.lbl_MessageText.Left = SystemInformation.VirtualScreen.Width;
            }





        }


        private void readBannerFile()
        {


            // Test - counter wie oft wird die Datei gelesen
            //TestCounter++;
            //Console.WriteLine("ReadBannerFile Counter: " + TestCounter);
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
                if (line.StartsWith("Info; ") || line.StartsWith("Warning; ") || line.StartsWith("Resolved; "))
                {
                    // Es wurde eine Bannerzeile gefunden
                    MessagesList.Add(line);
                }
            }
            file.Close();

            file.Dispose();

            

            msgList.Clear();
            

            // Messages durchlaufen
            MessagesList.ForEach(delegate (String msgLine)
            {
                try
                {

                
                int _gotIntervall;
                String[] ConfiguredMessage = msgLine.Split(';');

                //MessageType = msgLine.Split(';')[0];
                String MessageType = ConfiguredMessage[0];


                    //String MessageTargets = msgLine.Split(':')[2];
                    String MessageTargets = ConfiguredMessage[2];

                    // MessageText = msgLine.Split(':')[3];
                    String MessageText = ConfiguredMessage[3];

                    // Wenn das Intervall aus Datei gelesen werden konnte und eine Zahl ist, dann setze das neue Intervall (* 1000 -> Sekunden in milliSekunden)
                    // Int32.TryParse(msgLine.Split(';')[1], out _gotIntervall);
                    Int32.TryParse(ConfiguredMessage[1], out _gotIntervall);

                
                    Intervall = _gotIntervall * 1000;
                
                //Console.WriteLine("Targets: " + MessageTargets);
                MessageBannerFound = checkValidTargetOLD(MessageTargets);                


                    // nur wenn Messagebanner falide ist und das Intervall richtig gelesen werden konnte.
                if(MessageBannerFound && _gotIntervall != 0)
                {
                    msgList.Add(new MessageToShow(MessageType, Intervall, MessageText));
                }
                } catch (Exception e)
                {
                    Console.WriteLine("error parsing message Line: " + e.Message);
                }
            });


            if (msgList.Count > 0)
            {
                
                foreach (var msg in msgList)
                {
                    //lbl_MessageText.Text = MessageText;
                    lbl_MessageText.Text = msg.Text;

                    Console.WriteLine("Banner to show: " + msg.Text);

                    // OLD
                    //if (msgLine.StartsWith("Info: ")) { this.BackColor = Color.LightBlue; }
                    //if (msgLine.StartsWith("Warning: ")) { this.BackColor = Color.OrangeRed; }
                    //if (msgLine.StartsWith("Resolved: ")) { this.BackColor = Color.LightGreen; }
                    ///////

                    if (msg.Type.StartsWith("Info")) { this.BackColor = Color.LightBlue; }
                    if (msg.Type.StartsWith("Warning")) { this.BackColor = Color.OrangeRed; }
                    if (msg.Type.StartsWith("Resolved")) { this.BackColor = Color.LightGreen; }
                    Intervall = msg.Intervall;
                }
            }  else // wenn keine Messages anzuzeigen sind.
            {
                Intervall = DefaultIntervall;
            }          
            
            // Timer mit neuen Parametern neu starten 
            resetTimer();
        }


        private bool checkValidTargetOLD (String _MessageTargets)
        {
            String TargetType = _MessageTargets.Split('[', ']')[1];
            Console.WriteLine("TargetType : >" + TargetType + "<");

            String Target = _MessageTargets.Split(']')[1];

            Console.WriteLine("Target: >" + Target + "<");

            // ToDo: validate if any criteria matches - not only last from list

            switch (TargetType)
            {
                case "*": // meldung gillt für alle 
                    return true;
                case "U":
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
                    
                    String adResult = adCheckField(ADField).ToLower().Trim();                    
                    if (adResult == ADString.ToLower().Trim())
                    {
                        return true;
                    }
                    break;
                case "G":

                    if (adCheckGroup(Target))
                    {
                        Console.WriteLine("AD-Group found: "  + Target);
                        return true;
                    }
                    break;
                default:
                    return false;                    
            }

            // last resort
            return false;
        }

        private bool adCheckGroup(String _grp)
        {
            Console.WriteLine("Checking ActiveDirectory Groups: ");
            try
            {
                PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, UserPrincipal.Current.ToString());

                // if found - grab its groups
                if (user != null)
                {
                    PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                    // iterate over all groups
                    foreach (Principal p in groups)
                    {
                       
                        Console.WriteLine("Group-->: " + p.Name);
                        if (p.Name.ToLower() == _grp.ToLower()) return true;
                    }
                }



                    } catch (Exception e) // evtl. non-AD-User
            {
                Console.WriteLine("AD-Group Check failed: " + e.Message);
                
            }

            return false;

        }


        private string adCheckField(String _propertie)
        {
            Console.WriteLine("Checking AD-Property : " + _propertie);

            try
            {
                PrincipalContext ADDomain = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADDomain, UserPrincipal.Current.ToString());

//                PropertyInfo[] probs = Type.GetType(user.GetType().ToString()).GetProperties();



                var properties = typeof(UserPrincipal).GetProperties();

                IList<KeyValuePair<string, object>> propertyValues = new List<KeyValuePair<string, object>>();
                
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

            try
            {

            
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

            if (directoryEntry.Properties.Contains(propertyName))
            {
                return directoryEntry.Properties[propertyName].Value.ToString();
            }
            } catch (Exception e)
            {
                Console.WriteLine("Error getting ad-object-properties: " + e.Message);
            }

            return null;
        }
        

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

public class MessageToShow {

    public String Type;
    public int Intervall;
    public String Text;

    public MessageToShow(String _Type, int _Seconds, String _Text)
    {
        this.Type = _Type;
        this.Intervall = _Seconds;
        this.Text = _Text;
    }

}

