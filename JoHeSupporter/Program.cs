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


            if (args.Length == 0)       // wenn keine Argumente übergeben wurden...
            {
                // setze die default config.xml Datei
                param.Cfgxml = AppDomain.CurrentDomain.BaseDirectory + "config.xml";

            }
            else if (args.Length == 1)     // Wenn ein Parameter übergeben wurde setze diesen als Config.xml Datei
            {

                param.Cfgxml = AppDomain.CurrentDomain.BaseDirectory + args[0].ToString();
                
            }
            else if (args.Length > 1)       // Wenn mehr als ein Argument angegeben wurden.
            {
                MessageBox.Show("Programm wurde mit zu vielen Parametern gestartet. \nBitte nur die Konfigurationsdatei als Argument angeben.", "JoheSupporter");
                System.Environment.Exit(1);
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //MessageBanner msgBanner = new MessageBanner();
            //new Thread(() => new MessageBanner().ShowDialog()).Start();
            new Thread(() => Application.Run(new MessageBanner())).Start();


            methods methods = new methods(param);
            param.UserMail = methods.getUserMail();
                        
            Application.Run(new GUI(methods));

       

        }
    }
}
