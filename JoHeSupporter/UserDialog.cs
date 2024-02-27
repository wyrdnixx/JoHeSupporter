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

namespace JoHeSupporter
{
    public partial class UserDialog : Form
    {
        private methods Methods;
        private param Param;

        public UserDialog(methods _methods, param _param)
        {
            // methodenObjekt übergeben
            Methods = _methods;
            Param = _param;
                      
            InitializeComponent();

            //Priorität in dem Userdialog zur Auswahl
            if (Param.AppCfg_EnablePrio == "true")
            {
                this.check_HighPrio.Visible = true;
            }
            else this.check_HighPrio.Visible = false;

            fillSysinfo();

            //ToDO: make image clickable 
            pbScreenshot.Image = Image.FromFile(Param.screenshotfile);
            pbScreenshot.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void UserDialog_Load(object sender, EventArgs e)
        {
            lbl_versionInfo.Text = "JoHeSupporter v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            textBox_ContactInfo.Text = Param.UserMail;

            linkLabel_InfoURL.Text = Param.AppCfg_infoLinkText;

        }



        private void fillSysinfo()
        {
            /*
             * ToDo:
             * get client IP via powershell - evtl. use eventlog to get client IP
            (Get - WinEvent - FilterHashtable @{ LogName = 'Microsoft-Windows-TerminalServices-RemoteConnectionManager/Operational'; ID = 1149; StartTime = (Get - Date).AddDays(-31); } | ForEach - Object {[PSCustomObject] @{ User =$_.Properties[0].Value; IPAddress =$_.Properties[2].Value; TimeCreated =$_.TimeCreated; } } | Where - Object User - eq $env: USERNAME | Sort - Object - Property TimeCreated - Descending | Select - Object - First 1).IPAddress
            */

            tbSysinfo1.Text = "" +
            "USERNAME: " + Environment.GetEnvironmentVariable("USERNAME") + "\r" +
            "USERDOMAIN: " + Environment.GetEnvironmentVariable("USERDOMAIN") + "\r" +
            "COMPUTERNAME: " + Environment.GetEnvironmentVariable("COMPUTERNAME");
            ;

            tbSysinfo2.Text = "" +
            "CLIENTNAME: " + Environment.GetEnvironmentVariable("CLIENTNAME") + "\r" +
            "SESSIONNAME: " + Environment.GetEnvironmentVariable("SESSIONNAME") + "\r" +            
            "LOGONSERVER: " + Environment.GetEnvironmentVariable("LOGONSERVER");
        }

        private void linkLabel_InfoURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Param.AppCfg_infoLinkURL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim öffnen des Info-Link. \n" + ex.Message);                
            }

        }

        private void UserDialog_HelpButtonClicked(Object sender, CancelEventArgs e)

        {

            MessageBox.Show(@"Klicken um eine Supportmeldung per Mail zu erstellen.

[Shift] gedrückt halten um das Icon mit der Maus zu verschieben.

----------------------------------------------------------------

[SKGL Lizenzinfos] :

Copyright(c) 2011 - 2012, Artem Los

All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

*Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and / or other materials provided with the distribution.

TSOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 'AS IS' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
", "Version 1", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        



        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void button_sendReport_Click(object sender, EventArgs e)
        {

            
            if (textBox_description.Text == "")
            {   
                MessageBox.Show("Füllen Sie bitte die erforderlichen Felder aus.","JoHeSupporter");                
                if (textBox_description.Text == "") textBox_description.BackColor = Color.IndianRed;

            }  else if (textBox_ContactInfo.Text == "")
             {
                MessageBox.Show("Füllen Sie bitte die erforderlichen Felder aus.", "JoHeSupporter");
                if (textBox_ContactInfo.Text == "") textBox_ContactInfo.BackColor = Color.IndianRed;                
            } else if (Param.AppCfg_ContactMandatory == "true" && textBox_ContactPerson.Text == "")
            {
                textBox_ContactPerson.BackColor = Color.IndianRed;
                MessageBox.Show("Füllen Sie bitte die erforderlichen Felder aus.", "JoHeSupporter");
            }
            else
            {
                // Description aus Fehlertext übernehmen und ggf. kürzen
                string _descriptionShort = "";
                if (this.check_HighPrio.Checked) {
                    _descriptionShort = "HIGH: ";
                }

                _descriptionShort = _descriptionShort + textBox_description.Text;

                if (textBox_description.Text.Length >50)
                {
                    _descriptionShort = textBox_description.Text.Substring(0, 50);
                }


                // Dialog Textobjekte deaktivieren, damit ersichtlich ist, dass geklickt wurde.

                textBox_ContactInfo.Enabled = false;                
                textBox_ContactPerson.Enabled = false;
                textBox_description.Enabled = false;

               
                Methods.sendSupportMail(textBox_ContactInfo.Text, _descriptionShort, generateHTMLText());
               
                    // Meldung abschicken
                    
                this.Dispose();
            }


        }


        public string generateHTMLText()
        {
            string prio = "";
            if(Param.AppCfg_EnablePrio == "true")
            {
                if (check_HighPrio.Checked)
                {
                    prio = "Priorität:      <b><u>HIGH</u></b>";
                }
                else prio = "Priorität:     Normal";
            }

            

            // Zeilenumbrüche in Fehlerbeschreibungstext durch HTML Zeilenumbrüche ersetzen
            string Description2HTML = textBox_description.Text.Replace(System.Environment.NewLine,"<br>");




            string HTMLText = @"
                
            <h3>JoHeSupporter Meldung</h3>
            <br>
            <h4>Angaben des Meldenden:</h4>            
            Ansprechpartner: " + textBox_ContactPerson.Text + @"
            <br>
            Kontakt Mail: " + textBox_ContactInfo.Text + @"
            <br>            
            <br>"
            + prio + @"
            <br>
            <h4>Fehlerbeschreibung:</h4>
            " +
            Description2HTML + @"
            <br>
            <hr />
            <h3>Systemangaben:</h3>
            USERNAME: " + Environment.GetEnvironmentVariable("USERNAME") + @" <br>
            USERDOMAIN: " + Environment.GetEnvironmentVariable("USERDOMAIN") + @" <br>
            COMPUTERNAME: " + Environment.GetEnvironmentVariable("COMPUTERNAME") + @" <br>
            SESSIONNAME: " + Environment.GetEnvironmentVariable("SESSIONNAME") + @" <br>
            CLIENTNAME: " + Environment.GetEnvironmentVariable("CLIENTNAME") + @" <br>
            LOGONSERVER: " + Environment.GetEnvironmentVariable("LOGONSERVER") + @" <br>

            <hr />
            ";

            return HTMLText;

        }
    }
}
