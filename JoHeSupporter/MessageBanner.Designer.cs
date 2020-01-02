namespace JoHeSupporter
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
            this.lbl_CloseBanner = new System.Windows.Forms.Label();
            this.lbl_MessageText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_CloseBanner
            // 
            this.lbl_CloseBanner.AutoSize = true;
            this.lbl_CloseBanner.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_CloseBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CloseBanner.Location = new System.Drawing.Point(55, 62);
            this.lbl_CloseBanner.Name = "lbl_CloseBanner";
            this.lbl_CloseBanner.Size = new System.Drawing.Size(33, 31);
            this.lbl_CloseBanner.TabIndex = 0;
            this.lbl_CloseBanner.Text = "X";
            this.lbl_CloseBanner.Click += new System.EventHandler(this.lbl_CloseBanner_Click);
            // 
            // lbl_MessageText
            // 
            this.lbl_MessageText.AutoSize = true;
            this.lbl_MessageText.Location = new System.Drawing.Point(144, 73);
            this.lbl_MessageText.Name = "lbl_MessageText";
            this.lbl_MessageText.Size = new System.Drawing.Size(35, 13);
            this.lbl_MessageText.TabIndex = 1;
            this.lbl_MessageText.Text = "label1";
            // 
            // MessageBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 158);
            this.Controls.Add(this.lbl_MessageText);
            this.Controls.Add(this.lbl_CloseBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBanner";
            this.Text = "MessageBanner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_CloseBanner;
        private System.Windows.Forms.Label lbl_MessageText;
    }
}