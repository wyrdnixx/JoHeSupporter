namespace JoHeSupporter
{
    partial class UserDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDialog));
            this.label_Hello = new System.Windows.Forms.Label();
            this.textBox_ContactPerson = new System.Windows.Forms.TextBox();
            this.label_ContactName = new System.Windows.Forms.Label();
            this.textBox_ContactInfo = new System.Windows.Forms.TextBox();
            this.label__ContactInfo = new System.Windows.Forms.Label();
            this.label_Description = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.button_sendReport = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.lbl_versionInfo = new System.Windows.Forms.Label();
            this.check_HighPrio = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel_InfoURL = new System.Windows.Forms.LinkLabel();
            this.pbScreenshot = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSysinfo1 = new System.Windows.Forms.Label();
            this.tbSysinfo2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Hello
            // 
            this.label_Hello.AutoSize = true;
            this.label_Hello.Location = new System.Drawing.Point(12, 28);
            this.label_Hello.Name = "label_Hello";
            this.label_Hello.Size = new System.Drawing.Size(297, 13);
            this.label_Hello.TabIndex = 0;
            this.label_Hello.Text = "Senden Sie eine Fehlermeldung per Mail an den IT-Helpdesk.";
            // 
            // textBox_ContactPerson
            // 
            this.textBox_ContactPerson.Location = new System.Drawing.Point(237, 92);
            this.textBox_ContactPerson.Name = "textBox_ContactPerson";
            this.textBox_ContactPerson.Size = new System.Drawing.Size(200, 20);
            this.textBox_ContactPerson.TabIndex = 1;
            // 
            // label_ContactName
            // 
            this.label_ContactName.AutoSize = true;
            this.label_ContactName.Location = new System.Drawing.Point(12, 95);
            this.label_ContactName.Name = "label_ContactName";
            this.label_ContactName.Size = new System.Drawing.Size(143, 13);
            this.label_ContactName.TabIndex = 2;
            this.label_ContactName.Text = "Kontakt Name und/oder Tel.";
            // 
            // textBox_ContactInfo
            // 
            this.textBox_ContactInfo.Location = new System.Drawing.Point(237, 54);
            this.textBox_ContactInfo.Name = "textBox_ContactInfo";
            this.textBox_ContactInfo.Size = new System.Drawing.Size(200, 20);
            this.textBox_ContactInfo.TabIndex = 7;
            // 
            // label__ContactInfo
            // 
            this.label__ContactInfo.AutoSize = true;
            this.label__ContactInfo.Location = new System.Drawing.Point(12, 57);
            this.label__ContactInfo.Name = "label__ContactInfo";
            this.label__ContactInfo.Size = new System.Drawing.Size(135, 13);
            this.label__ContactInfo.TabIndex = 2;
            this.label__ContactInfo.Text = "Mail des Ansprechpartners:";
            // 
            // label_Description
            // 
            this.label_Description.AutoSize = true;
            this.label_Description.Location = new System.Drawing.Point(12, 149);
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size(420, 26);
            this.label_Description.TabIndex = 6;
            this.label_Description.Text = "Bitte beschreiben Sie das Problem kurz und präzise.\r\nGeben Sie bitte ggf. relvant" +
    "e Informationen mit an z.B. Patienten Name, Fallnummer etc.";
            // 
            // textBox_description
            // 
            this.textBox_description.Location = new System.Drawing.Point(18, 178);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(417, 183);
            this.textBox_description.TabIndex = 3;
            // 
            // button_sendReport
            // 
            this.button_sendReport.Location = new System.Drawing.Point(42, 434);
            this.button_sendReport.Name = "button_sendReport";
            this.button_sendReport.Size = new System.Drawing.Size(125, 23);
            this.button_sendReport.TabIndex = 4;
            this.button_sendReport.Text = "Fehlerbericht senden";
            this.button_sendReport.UseVisualStyleBackColor = true;
            this.button_sendReport.Click += new System.EventHandler(this.button_sendReport_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(273, 434);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(125, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Abbrechen";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // lbl_versionInfo
            // 
            this.lbl_versionInfo.AutoSize = true;
            this.lbl_versionInfo.Location = new System.Drawing.Point(316, 0);
            this.lbl_versionInfo.Name = "lbl_versionInfo";
            this.lbl_versionInfo.Size = new System.Drawing.Size(121, 13);
            this.lbl_versionInfo.TabIndex = 10;
            this.lbl_versionInfo.Text = "JoHeSupporter: v_INFO";
            // 
            // check_HighPrio
            // 
            this.check_HighPrio.AutoSize = true;
            this.check_HighPrio.Location = new System.Drawing.Point(237, 129);
            this.check_HighPrio.Name = "check_HighPrio";
            this.check_HighPrio.Size = new System.Drawing.Size(144, 17);
            this.check_HighPrio.TabIndex = 12;
            this.check_HighPrio.Text = "Hohe Priorität / Dringend";
            this.check_HighPrio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(415, 39);
            this.label2.TabIndex = 13;
            this.label2.Text = "Wenn Sie auf \"Fehlerbericht senden\" klicken wird auch dieser Screenshot übertrage" +
    "n.\r\nWenn Sie dies nicht wünschen brechen Sie den Vorgang bitte ab.\r\n\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(343, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Beim Klicken auf das Icon wurde ein Bildschirmfoto erstellt.";
            // 
            // linkLabel_InfoURL
            // 
            this.linkLabel_InfoURL.AutoSize = true;
            this.linkLabel_InfoURL.Location = new System.Drawing.Point(326, 13);
            this.linkLabel_InfoURL.Name = "linkLabel_InfoURL";
            this.linkLabel_InfoURL.Size = new System.Drawing.Size(55, 13);
            this.linkLabel_InfoURL.TabIndex = 16;
            this.linkLabel_InfoURL.TabStop = true;
            this.linkLabel_InfoURL.Text = "linkLabel1";
            this.linkLabel_InfoURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_InfoURL_LinkClicked);
            // 
            // pbScreenshot
            // 
            this.pbScreenshot.Location = new System.Drawing.Point(477, 149);
            this.pbScreenshot.Name = "pbScreenshot";
            this.pbScreenshot.Size = new System.Drawing.Size(612, 308);
            this.pbScreenshot.TabIndex = 17;
            this.pbScreenshot.TabStop = false;
            this.pbScreenshot.Click += new System.EventHandler(this.pbScreenshot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(740, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Systeminformationen";
            // 
            // tbSysinfo1
            // 
            this.tbSysinfo1.AutoSize = true;
            this.tbSysinfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSysinfo1.Location = new System.Drawing.Point(561, 57);
            this.tbSysinfo1.Name = "tbSysinfo1";
            this.tbSysinfo1.Size = new System.Drawing.Size(66, 13);
            this.tbSysinfo1.TabIndex = 19;
            this.tbSysinfo1.Text = "tbSysinfo1";
            // 
            // tbSysinfo2
            // 
            this.tbSysinfo2.AutoSize = true;
            this.tbSysinfo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSysinfo2.Location = new System.Drawing.Point(821, 54);
            this.tbSysinfo2.Name = "tbSysinfo2";
            this.tbSysinfo2.Size = new System.Drawing.Size(66, 13);
            this.tbSysinfo2.TabIndex = 19;
            this.tbSysinfo2.Text = "tbSysinfo2";
            // 
            // UserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 469);
            this.Controls.Add(this.tbSysinfo2);
            this.Controls.Add(this.tbSysinfo1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbScreenshot);
            this.Controls.Add(this.linkLabel_InfoURL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.check_HighPrio);
            this.Controls.Add(this.lbl_versionInfo);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_sendReport);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.label_Description);
            this.Controls.Add(this.label__ContactInfo);
            this.Controls.Add(this.label_ContactName);
            this.Controls.Add(this.textBox_ContactInfo);
            this.Controls.Add(this.textBox_ContactPerson);
            this.Controls.Add(this.label_Hello);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserDialog";
            this.Text = "JoHeSupporter";
            this.Load += new System.EventHandler(this.UserDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Label label_Hello;
        private System.Windows.Forms.TextBox textBox_ContactPerson;
        private System.Windows.Forms.Label label_ContactName;
        private System.Windows.Forms.TextBox textBox_ContactInfo;
        private System.Windows.Forms.Label label__ContactInfo;
        private System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.Button button_sendReport;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label lbl_versionInfo;
        private System.Windows.Forms.CheckBox check_HighPrio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel_InfoURL;
        private System.Windows.Forms.PictureBox pbScreenshot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tbSysinfo1;
        private System.Windows.Forms.Label tbSysinfo2;
    }
}