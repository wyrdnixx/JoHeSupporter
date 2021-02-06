namespace JoHeSupporter
{
    partial class GUIChangePassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIChangePassword));
            this.btn_savepassword = new System.Windows.Forms.Button();
            this.tb_clearpwd = new System.Windows.Forms.TextBox();
            this.tb_encrypedpwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_savepassword
            // 
            this.btn_savepassword.Location = new System.Drawing.Point(165, 126);
            this.btn_savepassword.Name = "btn_savepassword";
            this.btn_savepassword.Size = new System.Drawing.Size(272, 23);
            this.btn_savepassword.TabIndex = 0;
            this.btn_savepassword.Text = "In Zwischenableage kopieren und Ende";
            this.btn_savepassword.UseVisualStyleBackColor = true;
            this.btn_savepassword.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_clearpwd
            // 
            this.tb_clearpwd.Location = new System.Drawing.Point(165, 53);
            this.tb_clearpwd.Name = "tb_clearpwd";
            this.tb_clearpwd.Size = new System.Drawing.Size(465, 20);
            this.tb_clearpwd.TabIndex = 1;
            this.tb_clearpwd.TextChanged += new System.EventHandler(this.textchanged);
            // 
            // tb_encrypedpwd
            // 
            this.tb_encrypedpwd.Location = new System.Drawing.Point(165, 79);
            this.tb_encrypedpwd.Name = "tb_encrypedpwd";
            this.tb_encrypedpwd.ReadOnly = true;
            this.tb_encrypedpwd.Size = new System.Drawing.Size(465, 20);
            this.tb_encrypedpwd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Das Passwort für den Mail-Account verschlüsseln für die Config.xml Datei.\r\nBitte " +
    "das verschlüsselte Passwort in die Config.xml eintragen.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Eintrag für Config.xml";
            // 
            // GUIChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 161);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_encrypedpwd);
            this.Controls.Add(this.tb_clearpwd);
            this.Controls.Add(this.btn_savepassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIChangePassword";
            this.Text = "JoHeSupporter ChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_savepassword;
        private System.Windows.Forms.TextBox tb_clearpwd;
        private System.Windows.Forms.TextBox tb_encrypedpwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}