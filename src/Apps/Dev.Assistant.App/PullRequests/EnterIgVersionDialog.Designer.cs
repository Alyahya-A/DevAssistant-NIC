
namespace Dev.Assistant.App.PullRequests
{
    partial class EnterIgVersionDialog
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
            this.IgVersionTxt = new System.Windows.Forms.TextBox();
            this.SaveBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.TipLabel = new System.Windows.Forms.Label();
            this.WrittenByLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(29, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Please enter IG Version";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IgVersionTxt
            // 
            this.IgVersionTxt.Location = new System.Drawing.Point(33, 77);
            this.IgVersionTxt.Name = "IgVersionTxt";
            this.IgVersionTxt.Size = new System.Drawing.Size(359, 23);
            this.IgVersionTxt.TabIndex = 7;
            this.IgVersionTxt.Text = "V1";
            this.IgVersionTxt.TextChanged += new System.EventHandler(this.IgVersionTxt_TextChanged);
            this.IgVersionTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IgVersionTxt_KeyPress);
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
            this.SaveBtn.Location = new System.Drawing.Point(135, 128);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(150, 44);
            this.SaveBtn.TabIndex = 6;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.TextColor = System.Drawing.Color.White;
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // TipLabel
            // 
            this.TipLabel.AutoSize = true;
            this.TipLabel.ForeColor = System.Drawing.Color.Red;
            this.TipLabel.Location = new System.Drawing.Point(34, 104);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(0, 15);
            this.TipLabel.TabIndex = 8;
            // 
            // WrittenByLabel
            // 
            this.WrittenByLabel.AutoSize = true;
            this.WrittenByLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.WrittenByLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.WrittenByLabel.Location = new System.Drawing.Point(29, 12);
            this.WrittenByLabel.Name = "WrittenByLabel";
            this.WrittenByLabel.Size = new System.Drawing.Size(100, 17);
            this.WrittenByLabel.TabIndex = 9;
            this.WrittenByLabel.Text = "WrittenByLabel";
            this.WrittenByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EnterIgVersionDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(421, 191);
            this.Controls.Add(this.WrittenByLabel);
            this.Controls.Add(this.TipLabel);
            this.Controls.Add(this.IgVersionTxt);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(437, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(437, 230);
            this.Name = "EnterIgVersionDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Micros Path";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private Common.Controllers.DevButton SaveBtn;
        private System.Windows.Forms.TextBox IgVersionTxt;
        private Label TipLabel;
        private Label WrittenByLabel;
    }
}