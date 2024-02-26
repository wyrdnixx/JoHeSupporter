using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeSupporter
{
    class license
    {

        public static SKGL.Validate SKGL_Key = new SKGL.Validate();
        // public static string LicenseFile = AppDomain.CurrentDomain.BaseDirectory + "License.lic";



        public bool checkLicense(string _licKey)
        {
            // Console.WriteLine(_licKey);
            // Version 3.0.3.0 : Assembly Major release Version einbezogen            
            string strVersion = Application.ProductVersion.Split('.')[0];
            //MessageBox.Show(strVersion);
            SKGL_Key.secretPhase = "!JoHeSupporter"+strVersion;

            SKGL_Key.Key = _licKey;


            //Console.WriteLine(SKGL_Key.IsValid.ToString() + SKGL_Key.DaysLeft.ToString());

            // Lizenz prüfen.
            // Version 3.0.3.0 : Ablaufdatum entfernt
            //if (SKGL_Key.IsValid && SKGL_Key.DaysLeft > 0)
            if (SKGL_Key.IsValid )
                {
                //MessageBox.Show("Is valid");
                return true;
            }
            else
                //MessageBox.Show("Is invalid");
            return false;

        }

        public string getLicenseExpireDate()
        {
            string _date = SKGL_Key.ExpireDate.ToShortDateString();

            return _date;
            //return SKGL_Key.ExpireDate.ToString();
        }

        public int getLicenseDaysLeft()
        {
            return SKGL_Key.DaysLeft;   
        }
    }
}
