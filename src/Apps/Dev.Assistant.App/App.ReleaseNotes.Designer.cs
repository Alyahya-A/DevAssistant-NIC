
namespace Dev.Assistant.App
{
    partial class AppReleaseNotes
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(911, 573);
            this.webBrowser.TabIndex = 11;
            // 
            // AppReleaseNotes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(911, 573);
            this.Controls.Add(this.webBrowser);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(927, 612);
            this.Name = "AppReleaseNotes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release Notes";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private Common.Controllers.DevButton ContinueBtn;
        private Common.Controllers.DevButton ResolvedBtn;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}