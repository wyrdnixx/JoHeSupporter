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
        }

        private void UserDialog_Load(object sender, EventArgs e)
        {
            lbl_versionInfo.Text = "JoHeSupporter v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            textBox_ContactInfo.Text = Param.UserMail;
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

            
            if (textBox_ContactPerson.Text == "" || textBox_description.Text == "")
            {   
                MessageBox.Show("Füllen Sie bitte die erforderlichen Felder aus.","JoHeSupporter");

                if (textBox_ContactPerson.Text == "") textBox_ContactPerson.BackColor = Color.IndianRed;                
                if (textBox_description.Text == "") textBox_description.BackColor = Color.IndianRed;

            } else if (textBox_tel.Text == "" && textBox_ContactInfo.Text == "")
            {
                MessageBox.Show("Füllen Sie bitte die erforderlichen Felder aus.", "JoHeSupporter");
                if (textBox_ContactInfo.Text == "") textBox_ContactInfo.BackColor = Color.IndianRed;
                if (textBox_tel.Text == "") textBox_tel.BackColor = Color.IndianRed;
            }
            else
            {
                // Description aus Fehlertext übernehmen und ggf. kürzen
                string _descriptionShort = textBox_description.Text;
                if (textBox_description.Text.Length >50)
                {
                    _descriptionShort = textBox_description.Text.Substring(0, 50);
                }


                // Dialog Textobjekte deaktivieren, damit ersichtlich ist, dass geklickt wurde.

                textBox_ContactInfo.Enabled = false;
                textBox_tel.Enabled = false;
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

            if (radio_prioHigh.Checked) prio = "<b><u>Hoch</u></b>";
            if (radio_prioNormal.Checked) prio = "Normal";
            if (radio_prioLow.Checked) prio = "Niedrig";

            // Zeilenumbrüche in Fehlerbeschreibungstext durch HTML Zeilenumbrüche ersetzen
            string Description2HTML = textBox_description.Text.Replace(System.Environment.NewLine,"<br>");




            string HTMLText = @"
                
            <h1>JoHeSupporter Meldung</h1>
            <br>
            <h2>Angaben des Meldenden:</h2>            
            Ansprechpartner: " + textBox_ContactPerson.Text + @"
            <br>
            Kontakt Mail: " + textBox_ContactInfo.Text + @"
            <br>
            Kontakt Tel: " + textBox_tel.Text + @"
            <br>
            Priorität: " + prio + @"
            <br>
            <h2>Fehlerbeschreibung:</h2>
            " +
            Description2HTML + @"
            <br>
            <hr />
            <h2>Systemangaben:</h2>
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
