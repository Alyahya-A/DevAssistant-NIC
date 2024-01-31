namespace Dev.Assistant.App.PullRequests
{
    partial class CardControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TtileLabel = new System.Windows.Forms.Label();
            this.DeveloperNameLabel = new System.Windows.Forms.Label();
            this.CommentsNumLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CreatedAtLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TtileLabel
            // 
            this.TtileLabel.AutoSize = true;
            this.TtileLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TtileLabel.Location = new System.Drawing.Point(3, 2);
            this.TtileLabel.Name = "TtileLabel";
            this.TtileLabel.Size = new System.Drawing.Size(38, 20);
            this.TtileLabel.TabIndex = 0;
            this.TtileLabel.Text = "Ttile";
            this.TtileLabel.Click += new System.EventHandler(this.panel1_Click);
            // 
            // DeveloperNameLabel
            // 
            this.DeveloperNameLabel.AutoSize = true;
            this.DeveloperNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeveloperNameLabel.Location = new System.Drawing.Point(3, 36);
            this.DeveloperNameLabel.Name = "DeveloperNameLabel";
            this.DeveloperNameLabel.Size = new System.Drawing.Size(92, 15);
            this.DeveloperNameLabel.TabIndex = 1;
            this.DeveloperNameLabel.Text = "DeveloperName";
            this.DeveloperNameLabel.Click += new System.EventHandler(this.panel1_Click);
            // 
            // CommentsNumLabel
            // 
            this.CommentsNumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentsNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CommentsNumLabel.Location = new System.Drawing.Point(237, 33);
            this.CommentsNumLabel.Name = "CommentsNumLabel";
            this.CommentsNumLabel.Size = new System.Drawing.Size(234, 20);
            this.CommentsNumLabel.TabIndex = 2;
            this.CommentsNumLabel.Text = "Comment(s)";
            this.CommentsNumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CommentsNumLabel.Click += new System.EventHandler(this.panel1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CreatedAtLabel);
            this.panel1.Controls.Add(this.TtileLabel);
            this.panel1.Controls.Add(this.CommentsNumLabel);
            this.panel1.Controls.Add(this.DeveloperNameLabel);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 56);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // CreatedAtLabel
            // 
            this.CreatedAtLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CreatedAtLabel.Location = new System.Drawing.Point(275, 2);
            this.CreatedAtLabel.Name = "CreatedAtLabel";
            this.CreatedAtLabel.Size = new System.Drawing.Size(196, 20);
            this.CreatedAtLabel.TabIndex = 3;
            this.CreatedAtLabel.Text = "dd MMMM (hh:mm tt)";
            this.CreatedAtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CreatedAtLabel.Click += new System.EventHandler(this.panel1_Click);
            // 
            // CardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "CardControl";
            this.Size = new System.Drawing.Size(482, 62);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label TtileLabel;
        private Label DeveloperNameLabel;
        private Label CommentsNumLabel;
        private Panel panel1;
        private Label CreatedAtLabel;
    }
}
