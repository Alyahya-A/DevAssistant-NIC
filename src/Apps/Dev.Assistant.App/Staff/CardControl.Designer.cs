namespace Dev.Assistant.App.Staff
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
            this.EmployeeNameLabel = new System.Windows.Forms.Label();
            this.EmployeeDeptNameLabel = new System.Windows.Forms.Label();
            this.EmployeeExtLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EmployeeEmailLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmployeeNameLabel
            // 
            this.EmployeeNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeNameLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EmployeeNameLabel.Location = new System.Drawing.Point(95, 0);
            this.EmployeeNameLabel.Name = "EmployeeNameLabel";
            this.EmployeeNameLabel.Size = new System.Drawing.Size(390, 20);
            this.EmployeeNameLabel.TabIndex = 0;
            this.EmployeeNameLabel.Text = "عبدالرحمن بن عبدالكريم اليحيى";
            this.EmployeeNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EmployeeDeptNameLabel
            // 
            this.EmployeeDeptNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EmployeeDeptNameLabel.Location = new System.Drawing.Point(242, 36);
            this.EmployeeDeptNameLabel.Name = "EmployeeDeptNameLabel";
            this.EmployeeDeptNameLabel.Size = new System.Drawing.Size(243, 15);
            this.EmployeeDeptNameLabel.TabIndex = 1;
            this.EmployeeDeptNameLabel.Text = "الخدمات الوسيطة";
            this.EmployeeDeptNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EmployeeExtLabel
            // 
            this.EmployeeExtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeExtLabel.AutoSize = true;
            this.EmployeeExtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EmployeeExtLabel.Location = new System.Drawing.Point(3, 3);
            this.EmployeeExtLabel.Name = "EmployeeExtLabel";
            this.EmployeeExtLabel.Size = new System.Drawing.Size(35, 16);
            this.EmployeeExtLabel.TabIndex = 2;
            this.EmployeeExtLabel.Text = "2083";
            this.EmployeeExtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.EmployeeEmailLabel);
            this.panel1.Controls.Add(this.EmployeeNameLabel);
            this.panel1.Controls.Add(this.EmployeeExtLabel);
            this.panel1.Controls.Add(this.EmployeeDeptNameLabel);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 56);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // EmployeeEmailLabel
            // 
            this.EmployeeEmailLabel.AutoSize = true;
            this.EmployeeEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EmployeeEmailLabel.Location = new System.Drawing.Point(3, 36);
            this.EmployeeEmailLabel.Name = "EmployeeEmailLabel";
            this.EmployeeEmailLabel.Size = new System.Drawing.Size(124, 17);
            this.EmployeeEmailLabel.TabIndex = 3;
            this.EmployeeEmailLabel.Text = "aayahya@nic.gov.sa";
            this.EmployeeEmailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "CardControl";
            this.Size = new System.Drawing.Size(496, 62);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label EmployeeNameLabel;
        private Label EmployeeDeptNameLabel;
        private Label EmployeeExtLabel;
        private Panel panel1;
        private Label EmployeeEmailLabel;
    }
}
