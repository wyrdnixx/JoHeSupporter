using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JoHeSupporter
{

    

    public class param
    {

        private int posOffset;
        String cfgxml;


        int appCfg_IconSize;
        string appCfg_License;
        string appCfg_MailSrv;
        string appCfg_MailPrt;
        string appCfg_MailTLS;
        string appCfg_MailFrom;
        string appCfg_MailTo;
        string appCfg_MailUsr;
        string appCfg_MailPwd;
        string appCfg_UseUserMail;
        string appCfg_UseMailAuth;
        string appCfg_CustomIconFile;
        string appCfg_EnablePrio;
        int appCfg_PosOffset;
        List<string> appCfg_AttachFile;

        public string screenshotfile = System.IO.Path.GetTempPath().ToString() + "JoHeSupporter.jpg";

        string userMail;

        public param()
        {
          //  PosOffset = 10;
            AppCfg_AttachFile = new List<string>();
            //Cfgxml = new XmlDocument();
        }

        public int PosOffset
        {
            get
            {
                return posOffset;
            }

            set
            {
                posOffset = value;
            }
        }

        public String Cfgxml
        {
            get
            {
                return cfgxml;
            }

            set
            {
                cfgxml = value;
            }
        }

        public string AppCfg_MailSrv
        {
            get
            {
                return appCfg_MailSrv;
            }

            set
            {
                appCfg_MailSrv = value;
            }
        }

        public string AppCfg_MailUsr
        {
            get
            {
                return appCfg_MailUsr;
            }

            set
            {
                appCfg_MailUsr = value;
            }
        }

        public string AppCfg_MailPwd
        {
            get
            {
                return appCfg_MailPwd;
            }

            set
            {
                appCfg_MailPwd = value;
            }
        }

        public string AppCfg_MailFrom
        {
            get
            {
                return appCfg_MailFrom;
            }

            set
            {
                appCfg_MailFrom = value;
            }
        }

        public string AppCfg_MailPrt
        {
            get
            {
                return appCfg_MailPrt;
            }

            set
            {
                appCfg_MailPrt = value;
            }
        }

        public string AppCfg_MailTo
        {
            get
            {
                return appCfg_MailTo;
            }

            set
            {
                appCfg_MailTo = value;
            }
        }

        public string AppCfg_MailTLS
        {
            get
            {
                return appCfg_MailTLS;
            }

            set
            {
                appCfg_MailTLS = value;
            }
        }

        public List<string> AppCfg_AttachFile
        {
            get
            {
                return appCfg_AttachFile;
            }

            set
            {
                appCfg_AttachFile = value;
            }
        }

        public int AppCfg_IconSize
        {
            get
            {
                return appCfg_IconSize;
            }

            set
            {
                appCfg_IconSize = value;
            }
        }

        public string AppCfg_License
        {
            get
            {
                return appCfg_License;
            }

            set
            {
                appCfg_License = value;
            }
        }

        public string UserMail
        {
            get
            {
                return userMail;
            }

            set
            {
                userMail = value;
            }
        }

        public string AppCfg_UseUserMail
        {
            get
            {
                return appCfg_UseUserMail;
            }

            set
            {
                appCfg_UseUserMail = value;
            }
        }

        public string AppCfg_UseMailAuth
        {
            get
            {
                return appCfg_UseMailAuth;
            }

            set
            {
                appCfg_UseMailAuth = value;
            }
        }

        public string AppCfg_CustomIconFile
        {
            get
            {
                return appCfg_CustomIconFile;
            }

            set
            {
                appCfg_CustomIconFile = value;
            }
        }

        public string AppCfg_EnablePrio
        {
            get
            {
                return appCfg_EnablePrio;
            }

            set
            {
                appCfg_EnablePrio = value;
            }
        }

        public int AppCfg_PosOffset
        {
            get
            {
                return appCfg_PosOffset;
            }

            set
            {
                appCfg_PosOffset = value;
            }
        }
    }
}
