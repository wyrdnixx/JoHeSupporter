using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Reflection;
//using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace JoHeSupporter
{
    public class methods
    {

        #region Method_init
        private param _param;
        public string _LicenseExpireDate;
        
        public methods(param _givenparam)
        {
            // Parameterobjekt übergeben
            _param = _givenparam;

            // XML Datei laden
//            _param.Cfgxml = AppDomain.CurrentDomain.BaseDirectory + "config.xml";            
            parsecfgxml(_param.Cfgxml);


            #region Lizenzprüfung
            license _license = new license();

    

            // lizenz für das Programm prüfen
            if (!_license.checkLicense(_param.AppCfg_License))
            {
                string _licenseDate = "";


                // Versuche zu lesen, ob das Lizenzdatum abgelaufen ist.
                try
                {
                    _licenseDate = _license.getLicenseExpireDate();
                    
                }
                catch (Exception) { }

                if (_licenseDate != "")     // Wenn ein Ablaufdatum gelesen werden konnte
                {
                    MessageBox.Show("Lizenz ist abgelaufen. \nAblaufdatum: " + _license.getLicenseExpireDate() + "\nProgramm wird beendet.", "JoHeSupporter");
                }
                else                        // Wenn kein Abalufdatum aus dem Schlüssel gelesen werden konnte.                
                {
                    MessageBox.Show("Lizenz ist ungültig. Programm wird beendet.", "JoHeSupporter");
                }
                // Programm wegen Lizenzfehler beenden                
                System.Environment.Exit(1);
            }

            // Mail Nachricht senden, wenn die Lizenz in weniger als 30 Tagen abläuft.
            //13.11.2022 - nur noch prüfung ob Lizenz gülltig - Ablaufdatum ignorieren.
            //if (_license.getLicenseDaysLeft() < 30)
            //{
            //   
            //   sendLicenseWarningMail(_license.getLicenseExpireDate().ToString());
            //    
            //}
            //
            // Lizenzablaufdatum für HTML Text auslesen.
            //try
            //{
            //    _LicenseExpireDate = _license.getLicenseExpireDate().ToString();
            //    //MessageBox.Show(_LicenseExpireDate);
            //}
            //catch (Exception e) { MessageBox.Show(e.Message.ToString()); }

            #endregion Lizenzprüfung

        }

        public param _Param
        {
            get { return _param; }
            set { _param = value; }
        }


        #endregion Method_init

        #region Config
        public void parsecfgxml(String _cfgxml)
        {

            XmlDocument xml = new XmlDocument();
          
            // Laden der XML Datei
            try
            {
                xml.Load(_cfgxml);                
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim laden der Konfigurationsdatei.\nFehler: \n" + e.Message, "Fehler");
                // Programm mit Fehlercode beenden
                System.Environment.Exit(1);
            }


            // Parsen der XML Datei
            try
            {
                XmlNode node = xml.DocumentElement.SelectSingleNode("/appcfg");

                if (node == null) throw new System.ArgumentException("XML Node \"appcfg\" nicht gefunden!");
                //List<AppCfgs> appxfgs = new List<AppCfgs>;
                foreach (XmlElement element in node)
                {
                    switch (element.Name)
                    {
                        case "IconSize":
                            _param.AppCfg_IconSize = Int32.Parse(element.InnerText);
                            break;
                        case "License":
                            _param.AppCfg_License = element.InnerText;
                            break;
                        case "MailSrv":
                            _param.AppCfg_MailSrv = element.InnerText;                            
                            break;
                        case "MailPrt":
                            _param.AppCfg_MailPrt = element.InnerText;
                            break;
                        case "MailTLS":
                            _param.AppCfg_MailTLS = element.InnerText;
                            break;
                        case "DebugTLS":
                            _param.AppCfg_DebugTLS = element.InnerText;
                            break;
                        case "UseUserMail":
                            _param.AppCfg_UseUserMail = element.InnerText;
                            break;
                        case "MailFrom":
                            _param.AppCfg_MailFrom = element.InnerText;
                            break;
                        case "MailTo":
                            _param.AppCfg_MailTo = element.InnerText;
                            break;
                        case "UseMailAuth":
                            _param.AppCfg_UseMailAuth = element.InnerText;
                            break;
                        case "MailUsr":
                            _param.AppCfg_MailUsr = element.InnerText;
                            break;
                        case "MailPwd":
                            _param.AppCfg_MailPwd = element.InnerText;
                            break;
                        case "MailPwdEnc":
                            _param.AppCfg_MailPwdEnc = element.InnerText;
                            break;
                        case "AttachFile":
                            _param.AppCfg_AttachFile.Add(element.InnerText);
                            break;
                        case "InfoLinkURL":
                            _param.AppCfg_infoLinkURL= element.InnerText;
                            break;
                        case "InfoLinkText":
                            _param.AppCfg_infoLinkText = element.InnerText;
                            break;
                        case "PosOffset":
                            int offset;
                            bool res;
                            res = int.TryParse(element.InnerText, out offset);
                            if (res)
                            {
                                _param.AppCfg_PosOffset = offset;
                            }
                            else _param.AppCfg_PosOffset = 175;
                            break;
                        case "CustomIconFile":
                            _param.AppCfg_CustomIconFile = element.InnerText;
                            break;
                        case "EnablePrio":
                            _param.AppCfg_EnablePrio = element.InnerText;
                            break;
                        case "ContactMandatory":
                            _param.AppCfg_ContactMandatory = element.InnerText;
                            break;
                    }
                }

                //MessageBox.Show("Debug Daten: "
                //    + "\n" + _param.AppCfg_MailSrv.ToString()
                //    + "\n" + _param.AppCfg_MailPrt.ToString()
                //    + "\n" + _param.AppCfg_MailUsr.ToString()
                //    );
            }
            catch (Exception e)
            {
                MessageBox.Show("Inhaltlicher Fehler in der config.xml\nFehler: \n" + e.Message, "Fehler" );
                // Programm mit Fehlercode beenden
                System.Environment.Exit(1);
            }

            if (_param.AppCfg_License == null)
            {
                MessageBox.Show("JoHeSupporter - Lizenzfehler\nEs ist kein Lizenzcode in der Config.xml vorhanden!\nProgramm wird beendet." , "JoHeSupporter");
                System.Environment.Exit(1);
            }

        }

        #endregion Config

        #region # ActiveDirectory / Mail info

        public string getUserMail()
        {
            string eMail;
            try
            {
            
                string user = UserPrincipal.Current.UserPrincipalName;
               eMail = UserPrincipal.Current.EmailAddress;
                // MessageBox.Show("user: " + user + "\nMail: " + eMail );
                return eMail;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        #endregion

        #region Mail

        //  //13.11.2022 - nur noch prüfung ob Lizenz gülltig - Ablaufdatum ignorieren.
        public void sendLicenseWarningMail(string _expireDate)
        {

            try
            { 
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_param.AppCfg_MailSrv.ToString());

            mail.From = new MailAddress(_param.AppCfg_MailFrom);
            mail.IsBodyHtml = true;

            mail.To.Add(_param.AppCfg_MailTo);


            string _client;
            string _user = Environment.GetEnvironmentVariable("USERNAME");


            if (Environment.GetEnvironmentVariable("CLIENTNAME") != "")
            {
                _client = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }
            else _client = Environment.GetEnvironmentVariable("CLIENTNAME");

            string _subject = _user.ToString() + " - " + _client.ToString() + " : JoHesupporter Lizenzwarnung.";

            mail.Subject = _subject;

                mail.Body = "JoHeSupporter Lizenzwarnung.<br>Lizenz läuft ab am: " + _expireDate;

            int _port;
            bool IntResTryParse = int.TryParse(_param.AppCfg_MailPrt, out _port);
            SmtpServer.Port = _port;

            SmtpServer.Credentials = new System.Net.NetworkCredential(_param.AppCfg_MailUsr, _param.AppCfg_MailPwd);

            if (_param.AppCfg_MailTLS == "True" || _param.AppCfg_MailTLS == "true")
            {
                SmtpServer.EnableSsl = true;
            }
            else SmtpServer.EnableSsl = false;

                
                
               SmtpServer.Send(mail);
                
                
          //  MessageBox.Show("Vielen Dank. Ihre Supportanfrage wurde gesendet!", "JoHeSupporter");
        }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString()
                    + "\n\n" + "XML Parameter: "
                    + "\n" + _param.AppCfg_MailSrv.ToString()
                    + "\n" + _param.AppCfg_MailPrt
                    + "\n" + _param.AppCfg_MailUsr
                  //  + "\n" + _param.AppCfg_MailPwd
                 
                    , "JoHeSupporter");
            }
}
        // Debug SMTP Tls Certificate
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // Test - Zertifikat ausgeben
            if (_param.AppCfg_DebugTLS == "True" || _param.AppCfg_DebugTLS == "true")
            {
                MessageBox.Show("SMTP-Zertifikat: " + Environment.NewLine +
                "SN: " + certificate.GetSerialNumberString() + Environment.NewLine +
                "Hash: " + certificate.GetCertHashString() + Environment.NewLine +
                "From: " + certificate.GetEffectiveDateString() + Environment.NewLine +
                "To: " + certificate.GetExpirationDateString() + Environment.NewLine +
                "Issuer: " + certificate.Issuer + Environment.NewLine +
                "Subject: " + certificate.Subject
                );
            }

            return true;
        }

        public void sendSupportMail(string _contactInfo,string _descriptionShort, string _htmltext)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_param.AppCfg_MailSrv.ToString());

            try
            {

                //MessageBox.Show("Debug Daten: "
                //    + "\n" + _param.AppCfg_MailSrv.ToString()     
                //    + "\n" + _param.AppCfg_MailPrt.ToString()
                //    + "\n" + _param.AppCfg_MailUsr.ToString() 
                //    );
                
                //SmtpClient SmtpServer = new SmtpClient(Param.AppCfg_MailSrv);


                // wenn AppCfg_UseUserMail festgelegt wurde, die user mail zu verwenden nimm diese, an sonnsten die AppCfg_MailFrom
                if (_param.AppCfg_UseUserMail == "true" && IsValidMailAddress(_contactInfo))
                {                                        
                        mail.From = new MailAddress(_contactInfo);
                        mail.ReplyToList.Add(_contactInfo);

                } else
                {
                    mail.From = new MailAddress(_param.AppCfg_MailFrom);
                    if (IsValidMailAddress(_contactInfo))
                    {
                        mail.ReplyToList.Add(_contactInfo);
                    }

                }
                
                

                

                mail.IsBodyHtml = true;
                
                mail.To.Add(_param.AppCfg_MailTo);


                string _client;
                string _user = Environment.GetEnvironmentVariable("USERNAME");


                if (Environment.GetEnvironmentVariable("CLIENTNAME") != "")
                {
                    _client = Environment.GetEnvironmentVariable("COMPUTERNAME");
                }
                else _client = Environment.GetEnvironmentVariable("CLIENTNAME");
                
                string _subject = _user.ToString() + " - " + _client.ToString() + " : " + _descriptionShort.Replace(System.Environment.NewLine, " "); ;

                mail.Subject = _subject;



                // Lizenzinfo am Ende des Mail Textes einfügen.
                //13.11.2022 - nur noch prüfung ob Lizenz gülltig - Ablaufdatum ignorieren.
                //< small > JoHeSupporter " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - Lizenz gülltig bis: " + _LicenseExpireDate + @" </ small >
                _htmltext = _htmltext +  @"
                <center>
                <small>JoHeSupporter " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + @"</small>                    
                </center>
                " ;

                // Logfiles etc. an die Mail anhängen.
                foreach (var file in _param.AppCfg_AttachFile) 
                {
                    string fullfilepath = Environment.ExpandEnvironmentVariables(file);
                    try
                    {
                        

                        //MessageBox.Show(fullfilepath, "JoHeSupporter");
                        Attachment aFile = new Attachment(fullfilepath);
                        mail.Attachments.Add(aFile);
                    } catch (FileNotFoundException )
                    {
                        _htmltext = _htmltext + "<p>Anzuhängende Datei wurde nicht gefunden: <br>" + file + "</p>";
                    }
                    catch (Exception )
                    {
                        _htmltext = _htmltext + "<p><b>Fehler beim Anhängen der Datei: <br>" + file + "</p>";
                    }
                    
                }

                // Screenshot File anhängen - Inline Bild lässt sich schlecht skalieren falls es schlecht zu lesen ist.
              try
                {
                    Attachment aFile = new Attachment(_Param.screenshotfile);
                    mail.Attachments.Add(aFile);
                } catch (Exception ex)
                {
                    _htmltext = _htmltext + "<b>Fehler beim Anhängen des Screenshots als Datei: <br>" + ex.Message + "</b>";
                }
                                               

                // Mail View generieren (htmltext und inline Screenshot)
                mail.AlternateViews.Add(createAlternateView(_htmltext, _Param.screenshotfile));



                int _port ;
                bool IntResTryParse = int.TryParse(_param.AppCfg_MailPrt, out _port);
                SmtpServer.Port = _port;


                // wenn Appcfg_UseMailAuth / Authentifizierung aktiviert ist
                if (_param.AppCfg_UseMailAuth == "true")
                {


                    //string Encr = Utilities.Encryption.AESEncryption.Encrypt(_param.AppCfg_MailPwdEnc, "JoHeSupporterEncryptioniPassword", "SaltString§$%&", "SHA1", 2, "16CHARSLONG12345", 256);
                    //Console.WriteLine(Encr);
                    //MessageBox.Show("Encrypted: " + Encr);
                    string DecryptedPwd="";
                    try
                    {
                        DecryptedPwd = Utilities.AESEncryption.Decrypt(_param.AppCfg_MailPwdEnc, "JoHeSupporterEncryptioniPassword", "SaltString§$%&", "SHA1", 2, "16CHARSLONG12345", 256);
                    }catch (Exception e)
                    {
                        MessageBox.Show("Konfigurationsproblem: \nDas Passwort aus der Konfiguration konnte nicht entschlüsselt werden.\n\nIhre Supportanfrage konnte leider nicht gesendet werden.");

                        // An dieser Stelle abbrechen - Konfigurationsproblem
                        return ;
                    }
                     
                    //MessageBox.Show("Decrypted: " + Decr);
                    
                    
                    SmtpServer.Credentials = new System.Net.NetworkCredential(_param.AppCfg_MailUsr, DecryptedPwd);

                    // Temp Variable mit klartextpasswort überschreiben und dann auf null setzen. (Besser wäre Objekt erzeugen und dann dispose machen)
                    DecryptedPwd = "";
                    DecryptedPwd = null;
                }                

                // TLS aktiviert oder nicht?
                if (_param.AppCfg_MailTLS == "True" || _param.AppCfg_MailTLS == "true")
                {
                    SmtpServer.EnableSsl = true;
                } else SmtpServer.EnableSsl = false;


                // versende die Mail
                SmtpServer.Send(mail);
                MessageBox.Show("Vielen Dank. Ihre Supportanfrage wurde gesendet!", "JoHeSupporter");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()
                    + "\n\n" + "XML Parameter: "
                    + "\n" + _param.AppCfg_MailSrv.ToString()
                    + "\n" + _param.AppCfg_MailPrt
                    + "\n" + _param.AppCfg_MailUsr
                  //  + "\n" + _param.AppCfg_MailPwd

                    , "JoHeSupporter");
            }

            // Mail object entladen 
            // ( sonnst war beim zweiten aufruf der Handle auf die Screenshot datei noch offen -> TakeScreenshot Methode)
            mail.Dispose();
            // SmtpServer objekt entladen / damit werden auch anmeldedaten aus dem RAM entladen
            SmtpServer.Dispose();


        }


        /// <summary>
        /// Prüft auf gülltiges Format einer Mail Adresse
        /// </summary>
        /// <param name="_text">String, der zu prüfen ist</param>
        /// <returns>true oder false</returns>
        public bool IsValidMailAddress(string _text)
        {
            if (_text != "")
            {

                try
                {
                    MailAddress m = new MailAddress(_text);
                    return true;
                }
                catch (FormatException) { return false; }
            } else { return false; }
        }


        /// <summary>
        /// generiert ein "AlternateView" aus HTML String und einer Bild Datei
        /// </summary>
        /// <param name="_htmlText"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private AlternateView createAlternateView(String _htmlText, String filePath)
        {
            LinkedResource inline = new LinkedResource(filePath);
            inline.ContentId = Guid.NewGuid().ToString();
            string htmlBody = _htmlText + @"<br>Screenshot:<br><img src='cid:" + inline.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }

        #endregion Mail

        #region EnryptPassword

        public string EncryptPasswordForConfig(string _cleartext)
        {
            return Utilities.AESEncryption.Encrypt(_cleartext, "JoHeSupporterEncryptioniPassword", "SaltString§$%&", "SHA1", 2, "16CHARSLONG12345", 256);

        }
        #endregion EnryptPassword


        #region Screenshot


        /// <summary>
        /// Saves an Shreenshot to a file
        /// </summary>
        public void saveScreenshot()
        {

            Bitmap _test = TakeScreenshot();

            try
            {
                
                //_test.Save(@"c:\tmp\JoHeSupporter.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                _test.Save(_Param.screenshotfile, System.Drawing.Imaging.ImageFormat.Jpeg);
                

            } catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Screenshot erstellen! \n" + ex.Message, "JoHeSupporter");
            }
            
            _test.Dispose();
        }

        /// <summary>
        /// Returns a Bitmap Object that contains an actual Screenshoot.
        /// </summary>
        /// <returns>Bitmanp Object</returns>
        // private Bitmap TakeScreenshot(bool onlyForm)
        private Bitmap TakeScreenshot()
        {
            int StartX, StartY;
            int Width, Height;

            StartX = 0;
            StartY = 0;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            Bitmap Screenshot = new Bitmap(Width, Height);            
            Graphics G = Graphics.FromImage(Screenshot);

            G.CopyFromScreen(StartX, StartY, 0, 0, new Size(Width, Height), CopyPixelOperation.SourceCopy);
                       
            return Screenshot;
        }

        
        #endregion Screenshot
    }
}
