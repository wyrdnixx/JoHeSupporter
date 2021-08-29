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
        public DateTime validFrom;
        public DateTime validUntil;
        public String validTo;
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
           this.banner = new Banner(messageText, type);


            this.StartTimer();
            // show first time ist generated
            banner.Show();
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
        }

        #endregion


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
                        //Intervall recalculate from minutes to miliseconds
                        int Intervall = int.Parse(ConfiguredMessage[3]) * 1000 * 60;
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

                    if (!stillValid || !CheckValidTarget(x.validFrom, x.validUntil, x.validTo))
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



            }
            catch (Exception e) // evtl. non-AD-User
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
                        Console.WriteLine("Property found : " + _propertie + " : " + rpc[_propertie][0].ToString());
                        return rpc[_propertie][0].ToString();
                    }
                }



                //////////////////////////////////
            }
            catch (Exception e)
            {
                //MessageBox.Show("ADCheck-Error - maybe no domain user : " + e.Message);
            }

            return "";

        }
    /*    private string GetPropertyByName(Principal principal, string propertyName)
        {

            try
            {


                DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

                if (directoryEntry.Properties.Contains(propertyName))
                {
                    return directoryEntry.Properties[propertyName].Value.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting ad-object-properties: " + e.Message);
            }

            return null;
        } 
    */


    }

}
