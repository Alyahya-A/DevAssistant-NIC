
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.PullRequests
{
    partial class PullRequestsHome
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cardsPanel1 = new Dev.Assistant.App.PullRequests.CardsPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.WelcomeContainer = new System.Windows.Forms.SplitContainer();
            this.WelcomLabel = new System.Windows.Forms.Label();
            this.UpdatePRsBtin = new Dev.Assistant.Common.Controllers.DevButton();
            this.PrBodyContainer = new System.Windows.Forms.SplitContainer();
            this.HeaderContainer = new System.Windows.Forms.SplitContainer();
            this.PrTitleLabel = new System.Windows.Forms.Label();
            this.PrInfoLabel = new System.Windows.Forms.Label();
            this.PrContentContainer = new System.Windows.Forms.SplitContainer();
            this.CrInfoTxt = new System.Windows.Forms.RichTextBox();
            this.TodoCheckListBox = new System.Windows.Forms.CheckedListBox();
            this.GetPdsTablesBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.CheckRulesRadBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.GenerateIGBtn = new Dev.Assistant.Common.Controllers.DevButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WelcomeContainer)).BeginInit();
            this.WelcomeContainer.Panel1.SuspendLayout();
            this.WelcomeContainer.Panel2.SuspendLayout();
            this.WelcomeContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrBodyContainer)).BeginInit();
            this.PrBodyContainer.Panel1.SuspendLayout();
            this.PrBodyContainer.Panel2.SuspendLayout();
            this.PrBodyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderContainer)).BeginInit();
            this.HeaderContainer.Panel1.SuspendLayout();
            this.HeaderContainer.Panel2.SuspendLayout();
            this.HeaderContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrContentContainer)).BeginInit();
            this.PrContentContainer.Panel1.SuspendLayout();
            this.PrContentContainer.Panel2.SuspendLayout();
            this.PrContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardsPanel1
            // 
            this.cardsPanel1.AutoScroll = true;
            this.cardsPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.cardsPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cardsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsPanel1.Location = new System.Drawing.Point(0, 0);
            this.cardsPanel1.Name = "cardsPanel1";
            this.cardsPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.cardsPanel1.Size = new System.Drawing.Size(500, 522);
            this.cardsPanel1.TabIndex = 3;
            this.cardsPanel1.ViewModel = null;
            this.cardsPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.cardsPanel1_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.PrBodyContainer);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Size = new System.Drawing.Size(1154, 571);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.WelcomeContainer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.cardsPanel1);
            this.splitContainer2.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.Size = new System.Drawing.Size(500, 571);
            this.splitContainer2.SplitterDistance = 45;
            this.splitContainer2.TabIndex = 0;
            // 
            // WelcomeContainer
            // 
            this.WelcomeContainer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.WelcomeContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.WelcomeContainer.IsSplitterFixed = true;
            this.WelcomeContainer.Location = new System.Drawing.Point(0, 0);
            this.WelcomeContainer.Name = "WelcomeContainer";
            // 
            // WelcomeContainer.Panel1
            // 
            this.WelcomeContainer.Panel1.Controls.Add(this.WelcomLabel);
            // 
            // WelcomeContainer.Panel2
            // 
            this.WelcomeContainer.Panel2.Controls.Add(this.UpdatePRsBtin);
            this.WelcomeContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.WelcomeContainer.Size = new System.Drawing.Size(500, 45);
            this.WelcomeContainer.SplitterDistance = 411;
            this.WelcomeContainer.TabIndex = 0;
            // 
            // WelcomLabel
            // 
            this.WelcomLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.WelcomLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.WelcomLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.WelcomLabel.Location = new System.Drawing.Point(0, 0);
            this.WelcomLabel.Name = "WelcomLabel";
            this.WelcomLabel.Size = new System.Drawing.Size(411, 45);
            this.WelcomLabel.TabIndex = 0;
            this.WelcomLabel.Text = "Welcome ";
            this.WelcomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpdatePRsBtin
            // 
            this.UpdatePRsBtin.BackColor = System.Drawing.SystemColors.Control;
            this.UpdatePRsBtin.BackgroundColor = System.Drawing.SystemColors.Control;
            this.UpdatePRsBtin.BorderColor = System.Drawing.Color.DarkCyan;
            this.UpdatePRsBtin.BorderRadius = 13;
            this.UpdatePRsBtin.BorderSize = 1;
            this.UpdatePRsBtin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdatePRsBtin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpdatePRsBtin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdatePRsBtin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpdatePRsBtin.ForeColor = System.Drawing.Color.DarkCyan;
            this.UpdatePRsBtin.Location = new System.Drawing.Point(10, 10);
            this.UpdatePRsBtin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UpdatePRsBtin.Name = "UpdatePRsBtin";
            this.UpdatePRsBtin.Size = new System.Drawing.Size(65, 25);
            this.UpdatePRsBtin.TabIndex = 3;
            this.UpdatePRsBtin.Text = "Refresh";
            this.UpdatePRsBtin.TextColor = System.Drawing.Color.DarkCyan;
            this.UpdatePRsBtin.UseVisualStyleBackColor = false;
            this.UpdatePRsBtin.Click += new System.EventHandler(this.UpdatePRsBtin_Click);
            // 
            // PrBodyContainer
            // 
            this.PrBodyContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrBodyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrBodyContainer.IsSplitterFixed = true;
            this.PrBodyContainer.Location = new System.Drawing.Point(0, 0);
            this.PrBodyContainer.Name = "PrBodyContainer";
            this.PrBodyContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PrBodyContainer.Panel1
            // 
            this.PrBodyContainer.Panel1.Controls.Add(this.HeaderContainer);
            // 
            // PrBodyContainer.Panel2
            // 
            this.PrBodyContainer.Panel2.Controls.Add(this.PrContentContainer);
            this.PrBodyContainer.Size = new System.Drawing.Size(653, 571);
            this.PrBodyContainer.SplitterDistance = 82;
            this.PrBodyContainer.TabIndex = 0;
            // 
            // HeaderContainer
            // 
            this.HeaderContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.HeaderContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderContainer.IsSplitterFixed = true;
            this.HeaderContainer.Location = new System.Drawing.Point(0, 0);
            this.HeaderContainer.Name = "HeaderContainer";
            this.HeaderContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HeaderContainer.Panel1
            // 
            this.HeaderContainer.Panel1.Controls.Add(this.PrTitleLabel);
            // 
            // HeaderContainer.Panel2
            // 
            this.HeaderContainer.Panel2.Controls.Add(this.PrInfoLabel);
            this.HeaderContainer.Size = new System.Drawing.Size(653, 82);
            this.HeaderContainer.SplitterDistance = 44;
            this.HeaderContainer.TabIndex = 0;
            // 
            // PrTitleLabel
            // 
            this.PrTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PrTitleLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.PrTitleLabel.Location = new System.Drawing.Point(0, 0);
            this.PrTitleLabel.Name = "PrTitleLabel";
            this.PrTitleLabel.Size = new System.Drawing.Size(653, 44);
            this.PrTitleLabel.TabIndex = 0;
            this.PrTitleLabel.Text = "Title";
            this.PrTitleLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // PrInfoLabel
            // 
            this.PrInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PrInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.PrInfoLabel.Name = "PrInfoLabel";
            this.PrInfoLabel.Size = new System.Drawing.Size(653, 34);
            this.PrInfoLabel.TabIndex = 0;
            this.PrInfoLabel.Text = "PrInfo";
            this.PrInfoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PrContentContainer
            // 
            this.PrContentContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrContentContainer.IsSplitterFixed = true;
            this.PrContentContainer.Location = new System.Drawing.Point(0, 0);
            this.PrContentContainer.Name = "PrContentContainer";
            this.PrContentContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PrContentContainer.Panel1
            // 
            this.PrContentContainer.Panel1.Controls.Add(this.CrInfoTxt);
            this.PrContentContainer.Panel1.Controls.Add(this.TodoCheckListBox);
            this.PrContentContainer.Panel1.Padding = new System.Windows.Forms.Padding(15);
            // 
            // PrContentContainer.Panel2
            // 
            this.PrContentContainer.Panel2.Controls.Add(this.GetPdsTablesBtn);
            this.PrContentContainer.Panel2.Controls.Add(this.CheckRulesRadBtn);
            this.PrContentContainer.Panel2.Controls.Add(this.GenerateIGBtn);
            this.PrContentContainer.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrContentContainer.Panel2.Padding = new System.Windows.Forms.Padding(220, 17, 220, 17);
            this.PrContentContainer.Panel2.SizeChanged += new System.EventHandler(this.PrContentContainer_Panel2_SizeChanged);
            this.PrContentContainer.Size = new System.Drawing.Size(653, 485);
            this.PrContentContainer.SplitterDistance = 336;
            this.PrContentContainer.TabIndex = 0;
            // 
            // CrInfoTxt
            // 
            this.CrInfoTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrInfoTxt.BackColor = System.Drawing.SystemColors.Control;
            this.CrInfoTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CrInfoTxt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.CrInfoTxt.Location = new System.Drawing.Point(18, 18);
            this.CrInfoTxt.Name = "CrInfoTxt";
            this.CrInfoTxt.ReadOnly = true;
            this.CrInfoTxt.Size = new System.Drawing.Size(617, 150);
            this.CrInfoTxt.TabIndex = 1;
            this.CrInfoTxt.Text = "";
            // 
            // TodoCheckListBox
            // 
            this.TodoCheckListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TodoCheckListBox.BackColor = System.Drawing.SystemColors.Control;
            this.TodoCheckListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TodoCheckListBox.CheckOnClick = true;
            this.TodoCheckListBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TodoCheckListBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TodoCheckListBox.ForeColor = System.Drawing.Color.Teal;
            this.TodoCheckListBox.FormattingEnabled = true;
            this.TodoCheckListBox.HorizontalScrollbar = true;
            this.TodoCheckListBox.Location = new System.Drawing.Point(15, 181);
            this.TodoCheckListBox.Margin = new System.Windows.Forms.Padding(10);
            this.TodoCheckListBox.Name = "TodoCheckListBox";
            this.TodoCheckListBox.Size = new System.Drawing.Size(623, 140);
            this.TodoCheckListBox.TabIndex = 0;
            // 
            // GetPdsTablesBtn
            // 
            this.GetPdsTablesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GetPdsTablesBtn.BackColor = System.Drawing.SystemColors.Control;
            this.GetPdsTablesBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GetPdsTablesBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.GetPdsTablesBtn.BorderRadius = 30;
            this.GetPdsTablesBtn.BorderSize = 1;
            this.GetPdsTablesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetPdsTablesBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPdsTablesBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.GetPdsTablesBtn.Location = new System.Drawing.Point(209, 76);
            this.GetPdsTablesBtn.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.GetPdsTablesBtn.MaximumSize = new System.Drawing.Size(249, 56);
            this.GetPdsTablesBtn.Name = "GetPdsTablesBtn";
            this.GetPdsTablesBtn.Size = new System.Drawing.Size(249, 56);
            this.GetPdsTablesBtn.TabIndex = 7;
            this.GetPdsTablesBtn.Text = "Get PDS Tables";
            this.GetPdsTablesBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.GetPdsTablesBtn.UseVisualStyleBackColor = false;
            this.GetPdsTablesBtn.Click += new System.EventHandler(this.GetPdsTablesBtn_Click);
            // 
            // CheckRulesRadBtn
            // 
            this.CheckRulesRadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckRulesRadBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CheckRulesRadBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.CheckRulesRadBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.CheckRulesRadBtn.BorderColor = System.Drawing.Color.Empty;
            this.CheckRulesRadBtn.BorderRadius = 30;
            this.CheckRulesRadBtn.BorderSize = 1;
            this.CheckRulesRadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckRulesRadBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckRulesRadBtn.ForeColor = System.Drawing.Color.White;
            this.CheckRulesRadBtn.Location = new System.Drawing.Point(353, 11);
            this.CheckRulesRadBtn.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.CheckRulesRadBtn.MaximumSize = new System.Drawing.Size(249, 56);
            this.CheckRulesRadBtn.Name = "CheckRulesRadBtn";
            this.CheckRulesRadBtn.Size = new System.Drawing.Size(249, 56);
            this.CheckRulesRadBtn.TabIndex = 6;
            this.CheckRulesRadBtn.Text = "Check Spelling and Rules";
            this.CheckRulesRadBtn.TextColor = System.Drawing.Color.White;
            this.CheckRulesRadBtn.UseVisualStyleBackColor = false;
            this.CheckRulesRadBtn.Click += new System.EventHandler(this.CheckRulesRadBtn_Click);
            // 
            // GenerateIGBtn
            // 
            this.GenerateIGBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateIGBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GenerateIGBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.GenerateIGBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.GenerateIGBtn.BorderColor = System.Drawing.Color.Empty;
            this.GenerateIGBtn.BorderRadius = 30;
            this.GenerateIGBtn.BorderSize = 1;
            this.GenerateIGBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateIGBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateIGBtn.ForeColor = System.Drawing.Color.White;
            this.GenerateIGBtn.Location = new System.Drawing.Point(65, 11);
            this.GenerateIGBtn.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.GenerateIGBtn.MaximumSize = new System.Drawing.Size(249, 56);
            this.GenerateIGBtn.Name = "GenerateIGBtn";
            this.GenerateIGBtn.Size = new System.Drawing.Size(249, 56);
            this.GenerateIGBtn.TabIndex = 5;
            this.GenerateIGBtn.Text = "Generate IG";
            this.GenerateIGBtn.TextColor = System.Drawing.Color.White;
            this.GenerateIGBtn.UseVisualStyleBackColor = false;
            this.GenerateIGBtn.Click += new System.EventHandler(this.GenerateIGBtn_Click);
            // 
            // PullRequestsHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PullRequestsHome";
            this.Size = new System.Drawing.Size(1154, 571);
            this.Load += new System.EventHandler(this.PrReviewHome_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.WelcomeContainer.Panel1.ResumeLayout(false);
            this.WelcomeContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WelcomeContainer)).EndInit();
            this.WelcomeContainer.ResumeLayout(false);
            this.PrBodyContainer.Panel1.ResumeLayout(false);
            this.PrBodyContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PrBodyContainer)).EndInit();
            this.PrBodyContainer.ResumeLayout(false);
            this.HeaderContainer.Panel1.ResumeLayout(false);
            this.HeaderContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderContainer)).EndInit();
            this.HeaderContainer.ResumeLayout(false);
            this.PrContentContainer.Panel1.ResumeLayout(false);
            this.PrContentContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PrContentContainer)).EndInit();
            this.PrContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevButton AddBtn;
        private System.Windows.Forms.TextBox MSNames;
        private DevButton ClearBtn;
        private DevButton DeleteBtn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox ExcludActiveRadBtn;
        private System.Windows.Forms.CheckBox checkBox2;
        private CardsPanel cardsPanel1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label WelcomLabel;
        private CheckedListBox TodoCheckListBox;
        private SplitContainer PrBodyContainer;
        private SplitContainer PrContentContainer;
        private SplitContainer HeaderContainer;
        private Label PrTitleLabel;
        private Label PrInfoLabel;
        private SplitContainer WelcomeContainer;
        private DevButton UpdatePRsBtin;
        private DevButton GetOutput;
        private DevButton GenerateIGBtn;
        private DevButton CheckRulesRadBtn;
        private DevButton GetPdsTablesBtn;
        private RichTextBox CrInfoTxt;
    }
}

