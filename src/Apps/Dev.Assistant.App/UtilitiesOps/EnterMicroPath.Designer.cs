
namespace Dev.Assistant.App.UtilitiesOps
{
    partial class EnterMicroPath
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
            this.label2 = new System.Windows.Forms.Label();
            this.MicroPathTxt = new System.Windows.Forms.TextBox();
            this.TipLabel = new System.Windows.Forms.Label();
            this.SaveBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.BrowseBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(29, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Please enter MicroServicesCore path:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MicroPathTxt
            // 
            this.MicroPathTxt.Location = new System.Drawing.Point(33, 62);
            this.MicroPathTxt.Name = "MicroPathTxt";
            this.MicroPathTxt.Size = new System.Drawing.Size(359, 23);
            this.MicroPathTxt.TabIndex = 7;
            // 
            // TipLabel
            // 
            this.TipLabel.AutoSize = true;
            this.TipLabel.Location = new System.Drawing.Point(34, 87);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(314, 15);
            this.TipLabel.TabIndex = 8;
            this.TipLabel.Text = "For example: C:\\Project\\MicroServices\\Core\\Development";
            this.TipLabel.Click += new System.EventHandler(this.TipLabel_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.SaveBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.SaveBtn.BorderColor = System.Drawing.Color.Empty;
            this.SaveBtn.BorderRadius = 22;
            this.SaveBtn.BorderSize = 0;
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(242, 132);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(150, 44);
            this.SaveBtn.TabIndex = 6;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.TextColor = System.Drawing.Color.White;
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.BackColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.BorderRadius = 22;
            this.BrowseBtn.BorderSize = 1;
            this.BrowseBtn.FlatAppearance.BorderSize = 0;
            this.BrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BrowseBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.Location = new System.Drawing.Point(33, 132);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(150, 44);
            this.BrowseBtn.TabIndex = 9;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.UseVisualStyleBackColor = false;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // EnterMicroPath
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(421, 191);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.TipLabel);
            this.Controls.Add(this.MicroPathTxt);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(437, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(437, 230);
            this.Name = "EnterMicroPath";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Micros Path";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private Common.Controllers.DevButton SaveBtn;
        private System.Windows.Forms.TextBox MicroPathTxt;
        private System.Windows.Forms.Label TipLabel;
        private Common.Controllers.DevButton BrowseBtn;
    }
}