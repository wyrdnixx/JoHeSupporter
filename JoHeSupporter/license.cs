using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoHeSupporter
{
    class license
    {

        public static SKGL.Validate SKGL_Key = new SKGL.Validate();
        // public static string LicenseFile = AppDomain.CurrentDomain.BaseDirectory + "License.lic";



        public bool checkLicense(string _licKey)
        {
            // Console.WriteLine(_licKey);
            SKGL_Key.secretPhase = "!JoHeSupporter";

            SKGL_Key.Key = _licKey;


            //Console.WriteLine(SKGL_Key.IsValid.ToString() + SKGL_Key.DaysLeft.ToString());

            // Lizenz prüfen.
            //if (SKGL_Key.IsValid && SKGL_Key.DaysLeft > 0)
            //13.11.2022 - nur noch prüfung ob Lizenz gülltig - Ablaufdatum ignorieren.
            if (SKGL_Key.IsValid )
            {
                return true;
            }
            else
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
