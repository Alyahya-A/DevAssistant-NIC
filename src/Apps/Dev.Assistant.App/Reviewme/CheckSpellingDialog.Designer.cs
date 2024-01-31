
namespace Dev.Assistant.App.Reviewme
{
    partial class CheckSpellingDialog
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
            this.TipLabel = new System.Windows.Forms.Label();
            this.CancelBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.ContinueBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(35, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(355, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Remarks and Misspelling found in your models!&";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TipLabel
            // 
            this.TipLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TipLabel.Location = new System.Drawing.Point(12, 55);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(401, 119);
            this.TipLabel.TabIndex = 8;
            this.TipLabel.Text = "Please resolve all remarks and correct spelling mistake in your models if there i" +
    "s any.\r\n\r\nAfter you finish save your model file \r\nThen select \"Continue\" to cont" +
    "inue creating the IG";
            this.TipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TipLabel.Click += new System.EventHandler(this.TipLabel_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.SystemColors.Control;
            this.CancelBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CancelBtn.BorderColor = System.Drawing.Color.DarkRed;
            this.CancelBtn.BorderRadius = 22;
            this.CancelBtn.BorderSize = 1;
            this.CancelBtn.FlatAppearance.BorderSize = 0;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CancelBtn.ForeColor = System.Drawing.Color.DarkRed;
            this.CancelBtn.Location = new System.Drawing.Point(108, 236);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(208, 44);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.TextColor = System.Drawing.Color.DarkRed;
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.ContinueBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.ContinueBtn.BorderColor = System.Drawing.Color.Empty;
            this.ContinueBtn.BorderRadius = 22;
            this.ContinueBtn.BorderSize = 0;
            this.ContinueBtn.FlatAppearance.BorderSize = 0;
            this.ContinueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ContinueBtn.ForeColor = System.Drawing.Color.White;
            this.ContinueBtn.Location = new System.Drawing.Point(108, 186);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(208, 44);
            this.ContinueBtn.TabIndex = 11;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.TextColor = System.Drawing.Color.White;
            this.ContinueBtn.UseVisualStyleBackColor = false;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // CheckSpellingDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(425, 291);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.TipLabel);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(441, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(441, 330);
            this.Name = "CheckSpellingDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Misspelling Found!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TipLabel;
        private Common.Controllers.DevButton CancelBtn;
        private Common.Controllers.DevButton ContinueBtn;
        private Common.Controllers.DevButton ResolvedBtn;
    }
}