using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoHeSupporter
{
    public partial class GUIChangePassword : Form
    {

        public methods methods;
        public param param;

        public GUIChangePassword(methods _methods)
        {
            InitializeComponent();
            methods = _methods;
            param = methods._Param;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(tb_encrypedpwd.Text);
            System.Environment.Exit(1);
        }

        private void textchanged(object sender, EventArgs e)
        {
            //string Encr = Utilities.Encryption.AESEncryption.Encrypt(_param.AppCfg_MailPwdEnc, "JoHeSupporterEncryptioniPassword", "SaltString§$%&", "SHA1", 2, "16CHARSLONG12345", 256);
            tb_encrypedpwd.Text = "<MailPwdEnc>"+ Utilities.AESEncryption.Encrypt(tb_clearpwd.Text, "JoHeSupporterEncryptioniPassword", "SaltString§$%&", "SHA1", 2, "16CHARSLONG12345", 256)+"</MailPwdEnc>";
        }
    }
}
