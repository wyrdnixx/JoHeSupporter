﻿namespace JoHeSupporter
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_tel = new System.Windows.Forms.TextBox();
            this.check_HighPrio = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_Hello
            // 
            this.label_Hello.AutoSize = true;
            this.label_Hello.Location = new System.Drawing.Point(12, 28);
            this.label_Hello.Name = "label_Hello";
            this.label_Hello.Size = new System.Drawing.Size(301, 13);
            this.label_Hello.TabIndex = 0;
            this.label_Hello.Text = "Senden Sie eine Fehlermeldung per Mail an den EDV Support.";
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
            this.label_ContactName.Size = new System.Drawing.Size(198, 13);
            this.label_ContactName.TabIndex = 2;
            this.label_ContactName.Text = "Ihr Name / Name des Anpsrechpartners:";
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
            this.label_Description.Location = new System.Drawing.Point(15, 200);
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size(420, 26);
            this.label_Description.TabIndex = 6;
            this.label_Description.Text = "Bitte beschreiben Sie das Problem kurz und präzise.\r\nGeben Sie bitte ggf. relvant" +
    "e Informationen mit an z.B. Patienten Name, Fallnummer etc.";
            // 
            // textBox_description
            // 
            this.textBox_description.Location = new System.Drawing.Point(18, 239);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(417, 157);
            this.textBox_description.TabIndex = 3;
            // 
            // button_sendReport
            // 
            this.button_sendReport.Location = new System.Drawing.Point(41, 438);
            this.button_sendReport.Name = "button_sendReport";
            this.button_sendReport.Size = new System.Drawing.Size(125, 23);
            this.button_sendReport.TabIndex = 4;
            this.button_sendReport.Text = "Fehlerbericht senden";
            this.button_sendReport.UseVisualStyleBackColor = true;
            this.button_sendReport.Click += new System.EventHandler(this.button_sendReport_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(269, 438);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tel. / Durchwahl für ggf. Rückruf";
            // 
            // textBox_tel
            // 
            this.textBox_tel.Location = new System.Drawing.Point(237, 129);
            this.textBox_tel.Name = "textBox_tel";
            this.textBox_tel.Size = new System.Drawing.Size(200, 20);
            this.textBox_tel.TabIndex = 2;
            // 
            // check_HighPrio
            // 
            this.check_HighPrio.AutoSize = true;
            this.check_HighPrio.Location = new System.Drawing.Point(237, 163);
            this.check_HighPrio.Name = "check_HighPrio";
            this.check_HighPrio.Size = new System.Drawing.Size(144, 17);
            this.check_HighPrio.TabIndex = 12;
            this.check_HighPrio.Text = "Hohe Priorität / Dringend";
            this.check_HighPrio.UseVisualStyleBackColor = true;
            // 
            // UserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 491);
            this.Controls.Add(this.check_HighPrio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_tel);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_tel;
        private System.Windows.Forms.CheckBox check_HighPrio;
    }
}