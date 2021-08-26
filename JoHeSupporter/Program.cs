using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeSupporter
{
    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Objekte für Parameterklasse und Methodenklasse
            param param = new param();

            param.Cfgxml = AppDomain.CurrentDomain.BaseDirectory +"config.xml";

            methods methods = new methods(param);

            // normaler Programmstart
            if (args.Length == 0)
            {
                // Open database (or create if doesn't exist)
                using (var db = new LiteDatabase(AppDomain.CurrentDomain.BaseDirectory + "MyData.db"))
                {
                    string nane = "hans";
                   

                }
            }
            else if (args.Length == 1 && args[0] == "-encryptpwd")     // Wenn mit -encryptpwd passwort gestartet wurde
            {
                // Mailpasswort verschlüsseln
                Application.Run(new GUIChangePassword(methods));

                System.Environment.Exit(1);

            }
            else       // Wenn unguelltige Parameter uebergeben wurden
            {
                MessageBox.Show("Programm wurde mit ungültigen Parametern gestartet. \n Bitte ohne parameter starten oder \n JoHeSupporter.exe -encryptpwd MeinMailPasswort \n um ein Email Passwort für die Config.xml zu erstellen.", "JoheSupporter");
                System.Environment.Exit(1);
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //MessageBanner msgBanner = new MessageBanner();
            //new Thread(() => new MessageBanner().ShowDialog()).Start();
             new Thread(() => Application.Run(new MessageBanner())).Start();



            param.UserMail = methods.getUserMail();
                        
            Application.Run(new GUI(methods));

       

        }


    }
}
