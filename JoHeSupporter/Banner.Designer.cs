
namespace JoHeSupporter
{
    partial class Banner
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
            this.lbl_MessageText_OLD = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbl_MessageText = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbl_MessageText_OLD
            // 
            this.lbl_MessageText_OLD.AutoSize = true;
            this.lbl_MessageText_OLD.Location = new System.Drawing.Point(158, 99);
            this.lbl_MessageText_OLD.Name = "lbl_MessageText_OLD";
            this.lbl_MessageText_OLD.Size = new System.Drawing.Size(87, 13);
            this.lbl_MessageText_OLD.TabIndex = 0;
            this.lbl_MessageText_OLD.Text = "lbl_MessageText";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(117, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbl_MessageText
            // 
            this.lbl_MessageText.AutoSize = true;
            this.lbl_MessageText.Location = new System.Drawing.Point(243, 22);
            this.lbl_MessageText.Name = "lbl_MessageText";
            this.lbl_MessageText.Size = new System.Drawing.Size(55, 13);
            this.lbl_MessageText.TabIndex = 2;
            this.lbl_MessageText.TabStop = true;
            this.lbl_MessageText.Text = "linkLabel1";
            // 
            // Banner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 324);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_MessageText);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbl_MessageText_OLD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Banner";
            this.Text = "Banner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_MessageText_OLD;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lbl_MessageText;
    }
}