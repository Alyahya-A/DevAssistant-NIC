
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App
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
            this.ReviewmeAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UtilitiesFormsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PullRequestsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.phoneExtsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyWorkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TasksboardAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastUpdateCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runningFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppsComboBox = new System.Windows.Forms.ComboBox();
            this.StartupAppLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ReviewmeInd = new System.Windows.Forms.Label();
            this.UtilitiesFormsInd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.LoggerText = new System.Windows.Forms.RichTextBox();
            this.PullRequestsInd = new System.Windows.Forms.Label();
            this.AppScreen = new System.Windows.Forms.Panel();
            this.VersionsLabel = new System.Windows.Forms.Label();
            this.UpdateAvailableLabel = new System.Windows.Forms.Label();
            this.RestartBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.UpdateBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.StaffInd = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.AppScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReviewmeAppMenuItem,
            this.UtilitiesFormsMenuItem,
            this.PullRequestsToolStripMenuItem1,
            this.phoneExtsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1154, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ReviewmeAppMenuItem
            // 
            this.ReviewmeAppMenuItem.Name = "ReviewmeAppMenuItem";
            this.ReviewmeAppMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.ReviewmeAppMenuItem.Size = new System.Drawing.Size(89, 24);
            this.ReviewmeAppMenuItem.Text = "Reviewme";
            this.ReviewmeAppMenuItem.Click += new System.EventHandler(this.ReviewmeAppMenuItem_Click);
            // 
            // UtilitiesFormsMenuItem
            // 
            this.UtilitiesFormsMenuItem.Name = "UtilitiesFormsMenuItem";
            this.UtilitiesFormsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.UtilitiesFormsMenuItem.Size = new System.Drawing.Size(97, 24);
            this.UtilitiesFormsMenuItem.Text = "UtilitiesOps";
            this.UtilitiesFormsMenuItem.Click += new System.EventHandler(this.UtilitiesFormsMenuItem_Click);
            // 
            // PullRequestsToolStripMenuItem1
            // 
            this.PullRequestsToolStripMenuItem1.Name = "PullRequestsToolStripMenuItem1";
            this.PullRequestsToolStripMenuItem1.Size = new System.Drawing.Size(108, 24);
            this.PullRequestsToolStripMenuItem1.Text = "Pull Requests";
            this.PullRequestsToolStripMenuItem1.Click += new System.EventHandler(this.pRReviewToolStripMenuItem1_Click);
            // 
            // phoneExtsToolStripMenuItem
            // 
            this.phoneExtsToolStripMenuItem.Name = "phoneExtsToolStripMenuItem";
            this.phoneExtsToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.phoneExtsToolStripMenuItem.Text = "Staff";
            this.phoneExtsToolStripMenuItem.Click += new System.EventHandler(this.phoneExtsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MyWorkMenuItem,
            this.TasksboardAppToolStripMenuItem,
            this.SettingsMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.aboutToolStripMenuItem.Text = "More";
            // 
            // MyWorkMenuItem
            // 
            this.MyWorkMenuItem.Name = "MyWorkMenuItem";
            this.MyWorkMenuItem.Size = new System.Drawing.Size(151, 24);
            this.MyWorkMenuItem.Text = "MyWork";
            this.MyWorkMenuItem.Click += new System.EventHandler(this.MyWorkMenuItem_Click);
            // 
            // TasksboardAppToolStripMenuItem
            // 
            this.TasksboardAppToolStripMenuItem.Name = "TasksboardAppToolStripMenuItem";
            this.TasksboardAppToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.TasksboardAppToolStripMenuItem.Text = "Tasksboard";
            this.TasksboardAppToolStripMenuItem.Visible = false;
            this.TasksboardAppToolStripMenuItem.Click += new System.EventHandler(this.TasksboardAppToolStripMenuItem_Click);
            // 
            // SettingsMenuItem
            // 
            this.SettingsMenuItem.Name = "SettingsMenuItem";
            this.SettingsMenuItem.Size = new System.Drawing.Size(151, 24);
            this.SettingsMenuItem.Text = "Settings";
            this.SettingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.releaseNotesToolStripMenuItem,
            this.lastUpdateCheckToolStripMenuItem,
            this.runningFromToolStripMenuItem});
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(151, 24);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            this.releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.releaseNotesToolStripMenuItem.Text = "Release Notes";
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.ReleaseNotesToolStripMenuItem_Click);
            // 
            // lastUpdateCheckToolStripMenuItem
            // 
            this.lastUpdateCheckToolStripMenuItem.Enabled = false;
            this.lastUpdateCheckToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lastUpdateCheckToolStripMenuItem.Name = "lastUpdateCheckToolStripMenuItem";
            this.lastUpdateCheckToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.lastUpdateCheckToolStripMenuItem.Text = "Last update check: ";
            // 
            // runningFromToolStripMenuItem
            // 
            this.runningFromToolStripMenuItem.Enabled = false;
            this.runningFromToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.runningFromToolStripMenuItem.Name = "runningFromToolStripMenuItem";
            this.runningFromToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.runningFromToolStripMenuItem.Text = "Running from: ";
            // 
            // AppsComboBox
            // 
            this.AppsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppsComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AppsComboBox.FormattingEnabled = true;
            this.AppsComboBox.Location = new System.Drawing.Point(992, 5);
            this.AppsComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AppsComboBox.Name = "AppsComboBox";
            this.AppsComboBox.Size = new System.Drawing.Size(154, 22);
            this.AppsComboBox.TabIndex = 3;
            this.AppsComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.AppsComboBox.SelectionChangeCommitted += new System.EventHandler(this.AppsComboBox_SelectionChangeCommitted);
            // 
            // StartupAppLabel
            // 
            this.StartupAppLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartupAppLabel.AutoSize = true;
            this.StartupAppLabel.BackColor = System.Drawing.Color.White;
            this.StartupAppLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartupAppLabel.Location = new System.Drawing.Point(898, 5);
            this.StartupAppLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartupAppLabel.Name = "StartupAppLabel";
            this.StartupAppLabel.Size = new System.Drawing.Size(90, 20);
            this.StartupAppLabel.TabIndex = 4;
            this.StartupAppLabel.Text = "Startup app:";
            // 
            // ReviewmeInd
            // 
            this.ReviewmeInd.BackColor = System.Drawing.Color.DarkCyan;
            this.ReviewmeInd.Font = new System.Drawing.Font("Segoe UI", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReviewmeInd.Location = new System.Drawing.Point(47, 23);
            this.ReviewmeInd.Name = "ReviewmeInd";
            this.ReviewmeInd.Size = new System.Drawing.Size(10, 2);
            this.ReviewmeInd.TabIndex = 9;
            // 
            // UtilitiesFormsInd
            // 
            this.UtilitiesFormsInd.BackColor = System.Drawing.Color.DarkCyan;
            this.UtilitiesFormsInd.Font = new System.Drawing.Font("Segoe UI", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UtilitiesFormsInd.Location = new System.Drawing.Point(141, 23);
            this.UtilitiesFormsInd.Name = "UtilitiesFormsInd";
            this.UtilitiesFormsInd.Size = new System.Drawing.Size(10, 2);
            this.UtilitiesFormsInd.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ClearBtn);
            this.groupBox1.Controls.Add(this.LoggerText);
            this.groupBox1.Location = new System.Drawing.Point(0, 598);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(1152, 246);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output (Logs):";
            // 
            // ClearBtn
            // 
            this.ClearBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ClearBtn.BackColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BackgroundColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClearBtn.BorderColor = System.Drawing.Color.Maroon;
            this.ClearBtn.BorderRadius = 12;
            this.ClearBtn.BorderSize = 1;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBtn.ForeColor = System.Drawing.Color.Maroon;
            this.ClearBtn.Location = new System.Drawing.Point(1062, 18);
            this.ClearBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(62, 24);
            this.ClearBtn.TabIndex = 13;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.TextColor = System.Drawing.Color.Maroon;
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // LoggerText
            // 
            this.LoggerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggerText.BackColor = System.Drawing.SystemColors.Control;
            this.LoggerText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoggerText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoggerText.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LoggerText.Location = new System.Drawing.Point(5, 18);
            this.LoggerText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LoggerText.Name = "LoggerText";
            this.LoggerText.ReadOnly = true;
            this.LoggerText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.LoggerText.Size = new System.Drawing.Size(1136, 218);
            this.LoggerText.TabIndex = 23;
            this.LoggerText.Text = "";
            // 
            // PullRequestsInd
            // 
            this.PullRequestsInd.BackColor = System.Drawing.Color.DarkCyan;
            this.PullRequestsInd.Font = new System.Drawing.Font("Segoe UI", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PullRequestsInd.Location = new System.Drawing.Point(243, 23);
            this.PullRequestsInd.Name = "PullRequestsInd";
            this.PullRequestsInd.Size = new System.Drawing.Size(10, 2);
            this.PullRequestsInd.TabIndex = 30;
            // 
            // AppScreen
            // 
            this.AppScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppScreen.AutoScroll = true;
            this.AppScreen.Controls.Add(this.VersionsLabel);
            this.AppScreen.Controls.Add(this.UpdateAvailableLabel);
            this.AppScreen.Controls.Add(this.RestartBtn);
            this.AppScreen.Controls.Add(this.UpdateBtn);
            this.AppScreen.Location = new System.Drawing.Point(0, 29);
            this.AppScreen.Name = "AppScreen";
            this.AppScreen.Size = new System.Drawing.Size(1154, 570);
            this.AppScreen.TabIndex = 31;
            // 
            // VersionsLabel
            // 
            this.VersionsLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VersionsLabel.Location = new System.Drawing.Point(343, 334);
            this.VersionsLabel.Name = "VersionsLabel";
            this.VersionsLabel.Size = new System.Drawing.Size(468, 38);
            this.VersionsLabel.TabIndex = 1;
            this.VersionsLabel.Text = "Current version: v2.1 \r\nNew version: 2.2";
            this.VersionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VersionsLabel.Visible = false;
            // 
            // UpdateAvailableLabel
            // 
            this.UpdateAvailableLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpdateAvailableLabel.ForeColor = System.Drawing.Color.Teal;
            this.UpdateAvailableLabel.Location = new System.Drawing.Point(191, 273);
            this.UpdateAvailableLabel.Name = "UpdateAvailableLabel";
            this.UpdateAvailableLabel.Size = new System.Drawing.Size(785, 61);
            this.UpdateAvailableLabel.TabIndex = 0;
            this.UpdateAvailableLabel.Text = "A New Update Available for the App!";
            this.UpdateAvailableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UpdateAvailableLabel.Visible = false;
            // 
            // RestartBtn
            // 
            this.RestartBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.RestartBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.RestartBtn.BorderColor = System.Drawing.Color.Empty;
            this.RestartBtn.BorderRadius = 31;
            this.RestartBtn.BorderSize = 0;
            this.RestartBtn.FlatAppearance.BorderSize = 0;
            this.RestartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RestartBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RestartBtn.ForeColor = System.Drawing.Color.White;
            this.RestartBtn.Location = new System.Drawing.Point(451, 521);
            this.RestartBtn.Name = "RestartBtn";
            this.RestartBtn.Size = new System.Drawing.Size(252, 64);
            this.RestartBtn.TabIndex = 8;
            this.RestartBtn.Text = "Restart";
            this.RestartBtn.TextColor = System.Drawing.Color.White;
            this.RestartBtn.UseVisualStyleBackColor = false;
            this.RestartBtn.Visible = false;
            this.RestartBtn.Click += new System.EventHandler(this.RestartBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.UpdateBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.UpdateBtn.BorderColor = System.Drawing.Color.Empty;
            this.UpdateBtn.BorderRadius = 31;
            this.UpdateBtn.BorderSize = 0;
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Location = new System.Drawing.Point(451, 483);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(252, 64);
            this.UpdateBtn.TabIndex = 7;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.TextColor = System.Drawing.Color.White;
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Visible = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // StaffInd
            // 
            this.StaffInd.BackColor = System.Drawing.Color.DarkCyan;
            this.StaffInd.Font = new System.Drawing.Font("Segoe UI", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StaffInd.Location = new System.Drawing.Point(322, 23);
            this.StaffInd.Name = "StaffInd";
            this.StaffInd.Size = new System.Drawing.Size(10, 2);
            this.StaffInd.TabIndex = 33;
            // 
            // AppHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1154, 846);
            this.Controls.Add(this.StaffInd);
            this.Controls.Add(this.AppScreen);
            this.Controls.Add(this.PullRequestsInd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.UtilitiesFormsInd);
            this.Controls.Add(this.ReviewmeInd);
            this.Controls.Add(this.StartupAppLabel);
            this.Controls.Add(this.AppsComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AppHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevAssistant App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.AppHome_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.AppScreen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ReviewmeAppMenuItem;
        private System.Windows.Forms.ComboBox AppsComboBox;
        private System.Windows.Forms.Label StartupAppLabel;
        private System.Windows.Forms.ToolStripMenuItem UtilitiesFormsMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label ReviewmeInd;
        private System.Windows.Forms.Label UtilitiesFormsInd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox LoggerText;
        private System.Windows.Forms.ToolStripMenuItem PullRequestsToolStripMenuItem1;
        private System.Windows.Forms.Label PullRequestsInd;
        private System.Windows.Forms.Panel AppScreen;
        private DevButton ClearLoggerBtn;
        private Label UpdateAvailableLabel;
        private Label VersionsLabel;
        private DevButton UpdateBtn;
        private DevButton RestartBtn;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem phoneExtsToolStripMenuItem;
        private Label StaffInd;
        private ToolStripMenuItem TasksboardAppToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripMenuItem releaseNotesToolStripMenuItem;
        private ToolStripMenuItem runningFromToolStripMenuItem;
        private ToolStripMenuItem lastUpdateCheckToolStripMenuItem;
        private ToolStripMenuItem MyWorkMenuItem;
        private DevButton ClearBtn;
        private ToolStripMenuItem SettingsMenuItem;
    }
}

