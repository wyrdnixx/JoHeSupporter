﻿namespace JoHeSupporter
{
    partial class MessageBanner
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
            this.lbl_MessageText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_MessageText
            // 
            this.lbl_MessageText.AutoSize = true;
            this.lbl_MessageText.Location = new System.Drawing.Point(248, 77);
            this.lbl_MessageText.Name = "lbl_MessageText";
            this.lbl_MessageText.Size = new System.Drawing.Size(35, 13);
            this.lbl_MessageText.TabIndex = 1;
            this.lbl_MessageText.Text = "label1";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(29, 28);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(157, 110);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MessageBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 158);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbl_MessageText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBanner";
            this.Text = "MessageBanner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_MessageText;
        private System.Windows.Forms.Button btnClose;
    }
}