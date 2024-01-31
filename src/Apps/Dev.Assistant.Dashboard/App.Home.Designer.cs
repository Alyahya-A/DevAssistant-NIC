
namespace Dev.Assistant.Dashboard
{
    partial class AppHome
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppHome));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DashboardAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppsComboBox = new System.Windows.Forms.ComboBox();
            this.StartupAppLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.AppScreen = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DashboardAppMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1154, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DashboardAppMenuItem
            // 
            this.DashboardAppMenuItem.Name = "DashboardAppMenuItem";
            this.DashboardAppMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.DashboardAppMenuItem.Size = new System.Drawing.Size(120, 24);
            this.DashboardAppMenuItem.Text = "LogEvents App";
            this.DashboardAppMenuItem.Click += new System.EventHandler(this.ReviewmeAppMenuItem_Click);
            // 
            // AppsComboBox
            // 
            this.AppsComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AppsComboBox.FormattingEnabled = true;
            this.AppsComboBox.Location = new System.Drawing.Point(979, 5);
            this.AppsComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AppsComboBox.Name = "AppsComboBox";
            this.AppsComboBox.Size = new System.Drawing.Size(154, 22);
            this.AppsComboBox.TabIndex = 3;
            this.AppsComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // StartupAppLabel
            // 
            this.StartupAppLabel.AutoSize = true;
            this.StartupAppLabel.BackColor = System.Drawing.Color.White;
            this.StartupAppLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartupAppLabel.Location = new System.Drawing.Point(885, 5);
            this.StartupAppLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartupAppLabel.Name = "StartupAppLabel";
            this.StartupAppLabel.Size = new System.Drawing.Size(90, 20);
            this.StartupAppLabel.TabIndex = 4;
            this.StartupAppLabel.Text = "Startup app:";
            // 
            // AppScreen
            // 
            this.AppScreen.AutoSize = true;
            this.AppScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppScreen.Location = new System.Drawing.Point(0, 28);
            this.AppScreen.MinimumSize = new System.Drawing.Size(1154, 570);
            this.AppScreen.Name = "AppScreen";
            this.AppScreen.Size = new System.Drawing.Size(1154, 818);
            this.AppScreen.TabIndex = 31;
            // 
            // AppHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1154, 846);
            this.Controls.Add(this.AppScreen);
            this.Controls.Add(this.StartupAppLabel);
            this.Controls.Add(this.AppsComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1170, 885);
            this.Name = "AppHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevAssistant Admin v1.0";
            this.Load += new System.EventHandler(this.AppHome_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DashboardAppMenuItem;
        private System.Windows.Forms.ComboBox AppsComboBox;
        private System.Windows.Forms.Label StartupAppLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox LoggerText;
        private System.Windows.Forms.Panel AppScreen;
        private Label UpdateAvailableLabel;
        private Label VersionsLabel;
    }
}

