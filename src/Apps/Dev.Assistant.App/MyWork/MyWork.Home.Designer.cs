
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.MyWork
{
    partial class MyWorkHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyWorkHome));
            this.SearchByBox = new System.Windows.Forms.GroupBox();
            this.ReviewedByMeRadBtn = new System.Windows.Forms.RadioButton();
            this.ByTitleRadBtn = new System.Windows.Forms.RadioButton();
            this.ByCRNumRadBtn = new System.Windows.Forms.RadioButton();
            this.ByLastActivityRadBtn = new System.Windows.Forms.RadioButton();
            this.CriteriaInput = new System.Windows.Forms.TextBox();
            this.EnterCriteriaLabel = new System.Windows.Forms.Label();
            this.UsernameInput = new System.Windows.Forms.TextBox();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.CriteriaInputErrMeg = new System.Windows.Forms.Label();
            this.OptionsBox = new System.Windows.Forms.GroupBox();
            this.MissingRequirementsOnlyRadBtn = new System.Windows.Forms.CheckBox();
            this.ExcludeActiveRadBtn = new System.Windows.Forms.CheckBox();
            this.ExcludeUnderDevRadBtn = new System.Windows.Forms.CheckBox();
            this.GetDevNameRadBtn = new System.Windows.Forms.CheckBox();
            this.CopiedRadBtn = new System.Windows.Forms.CheckBox();
            this.CheckIgInAttachmentsRadBtn = new System.Windows.Forms.CheckBox();
            this.NumServicesMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CheckBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.label2 = new System.Windows.Forms.Label();
            this.UsernameErrMeg = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EnterContractNameLabel = new System.Windows.Forms.Label();
            this.SearchByBox.SuspendLayout();
            this.OptionsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchByBox
            // 
            this.SearchByBox.Controls.Add(this.ReviewedByMeRadBtn);
            this.SearchByBox.Controls.Add(this.ByTitleRadBtn);
            this.SearchByBox.Controls.Add(this.ByCRNumRadBtn);
            this.SearchByBox.Controls.Add(this.ByLastActivityRadBtn);
            this.SearchByBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchByBox.Location = new System.Drawing.Point(16, 181);
            this.SearchByBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchByBox.Name = "SearchByBox";
            this.SearchByBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchByBox.Size = new System.Drawing.Size(417, 46);
            this.SearchByBox.TabIndex = 4;
            this.SearchByBox.TabStop = false;
            this.SearchByBox.Text = "Search by";
            // 
            // ReviewedByMeRadBtn
            // 
            this.ReviewedByMeRadBtn.AutoSize = true;
            this.ReviewedByMeRadBtn.Location = new System.Drawing.Point(306, 19);
            this.ReviewedByMeRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ReviewedByMeRadBtn.Name = "ReviewedByMeRadBtn";
            this.ReviewedByMeRadBtn.Size = new System.Drawing.Size(104, 17);
            this.ReviewedByMeRadBtn.TabIndex = 4;
            this.ReviewedByMeRadBtn.TabStop = true;
            this.ReviewedByMeRadBtn.Text = "Reviewed By Me";
            this.ReviewedByMeRadBtn.UseVisualStyleBackColor = true;
            this.ReviewedByMeRadBtn.CheckedChanged += new System.EventHandler(this.ReviewedByMeRadBtn_CheckedChanged);
            // 
            // ByTitleRadBtn
            // 
            this.ByTitleRadBtn.AutoSize = true;
            this.ByTitleRadBtn.Location = new System.Drawing.Point(242, 19);
            this.ByTitleRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ByTitleRadBtn.Name = "ByTitleRadBtn";
            this.ByTitleRadBtn.Size = new System.Drawing.Size(60, 17);
            this.ByTitleRadBtn.TabIndex = 3;
            this.ByTitleRadBtn.TabStop = true;
            this.ByTitleRadBtn.Text = "By Title";
            this.ByTitleRadBtn.UseVisualStyleBackColor = true;
            this.ByTitleRadBtn.CheckedChanged += new System.EventHandler(this.ByTitleRadBtn_CheckedChanged);
            // 
            // ByCRNumRadBtn
            // 
            this.ByCRNumRadBtn.AutoSize = true;
            this.ByCRNumRadBtn.Location = new System.Drawing.Point(131, 19);
            this.ByCRNumRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ByCRNumRadBtn.Name = "ByCRNumRadBtn";
            this.ByCRNumRadBtn.Size = new System.Drawing.Size(107, 17);
            this.ByCRNumRadBtn.TabIndex = 2;
            this.ByCRNumRadBtn.TabStop = true;
            this.ByCRNumRadBtn.Text = "By CR Number(s)";
            this.ByCRNumRadBtn.UseVisualStyleBackColor = true;
            this.ByCRNumRadBtn.CheckedChanged += new System.EventHandler(this.ByCRNumRadBtn_CheckedChanged);
            // 
            // ByLastActivityRadBtn
            // 
            this.ByLastActivityRadBtn.AutoSize = true;
            this.ByLastActivityRadBtn.Checked = true;
            this.ByLastActivityRadBtn.Location = new System.Drawing.Point(9, 19);
            this.ByLastActivityRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ByLastActivityRadBtn.Name = "ByLastActivityRadBtn";
            this.ByLastActivityRadBtn.Size = new System.Drawing.Size(120, 17);
            this.ByLastActivityRadBtn.TabIndex = 0;
            this.ByLastActivityRadBtn.TabStop = true;
            this.ByLastActivityRadBtn.Text = "By Recent Activities";
            this.ByLastActivityRadBtn.UseVisualStyleBackColor = true;
            this.ByLastActivityRadBtn.CheckedChanged += new System.EventHandler(this.ByLastActivityRadBtn_CheckedChanged);
            // 
            // CriteriaInput
            // 
            this.CriteriaInput.Location = new System.Drawing.Point(16, 319);
            this.CriteriaInput.Margin = new System.Windows.Forms.Padding(10);
            this.CriteriaInput.Name = "CriteriaInput";
            this.CriteriaInput.Size = new System.Drawing.Size(417, 23);
            this.CriteriaInput.TabIndex = 1;
            // 
            // EnterCriteriaLabel
            // 
            this.EnterCriteriaLabel.AutoSize = true;
            this.EnterCriteriaLabel.Location = new System.Drawing.Point(16, 300);
            this.EnterCriteriaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterCriteriaLabel.Name = "EnterCriteriaLabel";
            this.EnterCriteriaLabel.Size = new System.Drawing.Size(323, 15);
            this.EnterCriteriaLabel.TabIndex = 5;
            this.EnterCriteriaLabel.Text = "Enter the period when tasks state changed (Default 15 days):";
            this.EnterCriteriaLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // UsernameInput
            // 
            this.UsernameInput.Location = new System.Drawing.Point(16, 257);
            this.UsernameInput.Margin = new System.Windows.Forms.Padding(10);
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(417, 23);
            this.UsernameInput.TabIndex = 6;
            this.UsernameInput.Text = "NICHQ\\";
            this.UsernameInput.TextChanged += new System.EventHandler(this.UsernameInput_TextChanged);
            // 
            // NoteLabel
            // 
            this.NoteLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.NoteLabel.Location = new System.Drawing.Point(16, 542);
            this.NoteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NoteLabel.Name = "NoteLabel";
            this.NoteLabel.Size = new System.Drawing.Size(417, 21);
            this.NoteLabel.TabIndex = 9;
            this.NoteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CriteriaInputErrMeg
            // 
            this.CriteriaInputErrMeg.AutoSize = true;
            this.CriteriaInputErrMeg.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CriteriaInputErrMeg.ForeColor = System.Drawing.Color.Red;
            this.CriteriaInputErrMeg.Location = new System.Drawing.Point(21, 345);
            this.CriteriaInputErrMeg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CriteriaInputErrMeg.Name = "CriteriaInputErrMeg";
            this.CriteriaInputErrMeg.Size = new System.Drawing.Size(0, 15);
            this.CriteriaInputErrMeg.TabIndex = 15;
            // 
            // OptionsBox
            // 
            this.OptionsBox.Controls.Add(this.MissingRequirementsOnlyRadBtn);
            this.OptionsBox.Controls.Add(this.ExcludeActiveRadBtn);
            this.OptionsBox.Controls.Add(this.ExcludeUnderDevRadBtn);
            this.OptionsBox.Controls.Add(this.GetDevNameRadBtn);
            this.OptionsBox.Controls.Add(this.CopiedRadBtn);
            this.OptionsBox.Controls.Add(this.CheckIgInAttachmentsRadBtn);
            this.OptionsBox.Location = new System.Drawing.Point(16, 362);
            this.OptionsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OptionsBox.Size = new System.Drawing.Size(417, 111);
            this.OptionsBox.TabIndex = 18;
            this.OptionsBox.TabStop = false;
            this.OptionsBox.Text = "Options";
            // 
            // MissingRequirementsOnlyRadBtn
            // 
            this.MissingRequirementsOnlyRadBtn.AutoSize = true;
            this.MissingRequirementsOnlyRadBtn.Checked = true;
            this.MissingRequirementsOnlyRadBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MissingRequirementsOnlyRadBtn.Location = new System.Drawing.Point(219, 86);
            this.MissingRequirementsOnlyRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MissingRequirementsOnlyRadBtn.Name = "MissingRequirementsOnlyRadBtn";
            this.MissingRequirementsOnlyRadBtn.Size = new System.Drawing.Size(166, 19);
            this.MissingRequirementsOnlyRadBtn.TabIndex = 39;
            this.MissingRequirementsOnlyRadBtn.Text = "Missing requirements only";
            this.MissingRequirementsOnlyRadBtn.UseVisualStyleBackColor = true;
            this.MissingRequirementsOnlyRadBtn.Visible = false;
            // 
            // ExcludeActiveRadBtn
            // 
            this.ExcludeActiveRadBtn.AutoSize = true;
            this.ExcludeActiveRadBtn.Checked = true;
            this.ExcludeActiveRadBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExcludeActiveRadBtn.Location = new System.Drawing.Point(8, 35);
            this.ExcludeActiveRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExcludeActiveRadBtn.Name = "ExcludeActiveRadBtn";
            this.ExcludeActiveRadBtn.Size = new System.Drawing.Size(131, 19);
            this.ExcludeActiveRadBtn.TabIndex = 38;
            this.ExcludeActiveRadBtn.Text = "Exclude Active tasks";
            this.ExcludeActiveRadBtn.UseVisualStyleBackColor = true;
            this.ExcludeActiveRadBtn.CheckedChanged += new System.EventHandler(this.ExcludeActiveRadBtn_CheckedChanged);
            // 
            // ExcludeUnderDevRadBtn
            // 
            this.ExcludeUnderDevRadBtn.AutoSize = true;
            this.ExcludeUnderDevRadBtn.Checked = true;
            this.ExcludeUnderDevRadBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExcludeUnderDevRadBtn.Location = new System.Drawing.Point(8, 18);
            this.ExcludeUnderDevRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ExcludeUnderDevRadBtn.Name = "ExcludeUnderDevRadBtn";
            this.ExcludeUnderDevRadBtn.Size = new System.Drawing.Size(208, 19);
            this.ExcludeUnderDevRadBtn.TabIndex = 37;
            this.ExcludeUnderDevRadBtn.Text = "Exclude \"Under Development\" CRs";
            this.ExcludeUnderDevRadBtn.UseVisualStyleBackColor = true;
            this.ExcludeUnderDevRadBtn.CheckedChanged += new System.EventHandler(this.ExcludeUnderDevRadBtn_CheckedChanged);
            // 
            // GetDevNameRadBtn
            // 
            this.GetDevNameRadBtn.AutoSize = true;
            this.GetDevNameRadBtn.Location = new System.Drawing.Point(8, 52);
            this.GetDevNameRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetDevNameRadBtn.Name = "GetDevNameRadBtn";
            this.GetDevNameRadBtn.Size = new System.Drawing.Size(132, 19);
            this.GetDevNameRadBtn.TabIndex = 36;
            this.GetDevNameRadBtn.Text = "Get developer name";
            this.GetDevNameRadBtn.UseVisualStyleBackColor = true;
            this.GetDevNameRadBtn.CheckedChanged += new System.EventHandler(this.GetDevNameRadBtn_CheckedChanged);
            // 
            // CopiedRadBtn
            // 
            this.CopiedRadBtn.AutoSize = true;
            this.CopiedRadBtn.Location = new System.Drawing.Point(8, 86);
            this.CopiedRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CopiedRadBtn.Name = "CopiedRadBtn";
            this.CopiedRadBtn.Size = new System.Drawing.Size(209, 19);
            this.CopiedRadBtn.TabIndex = 34;
            this.CopiedRadBtn.Text = "Copy the output to your Clipboard";
            this.CopiedRadBtn.UseVisualStyleBackColor = true;
            this.CopiedRadBtn.CheckedChanged += new System.EventHandler(this.CopiedRadBtn_CheckedChanged);
            // 
            // CheckIgInAttachmentsRadBtn
            // 
            this.CheckIgInAttachmentsRadBtn.AutoSize = true;
            this.CheckIgInAttachmentsRadBtn.Location = new System.Drawing.Point(8, 69);
            this.CheckIgInAttachmentsRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckIgInAttachmentsRadBtn.Name = "CheckIgInAttachmentsRadBtn";
            this.CheckIgInAttachmentsRadBtn.Size = new System.Drawing.Size(271, 19);
            this.CheckIgInAttachmentsRadBtn.TabIndex = 22;
            this.CheckIgInAttachmentsRadBtn.Text = "Check if the IG is attached in CR\'s attachments";
            this.CheckIgInAttachmentsRadBtn.UseVisualStyleBackColor = true;
            this.CheckIgInAttachmentsRadBtn.CheckedChanged += new System.EventHandler(this.CheckIgInAttachmentsRadBtn_CheckedChanged);
            // 
            // NumServicesMsg
            // 
            this.NumServicesMsg.AutoSize = true;
            this.NumServicesMsg.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NumServicesMsg.ForeColor = System.Drawing.Color.Teal;
            this.NumServicesMsg.Location = new System.Drawing.Point(450, 10);
            this.NumServicesMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NumServicesMsg.Name = "NumServicesMsg";
            this.NumServicesMsg.Size = new System.Drawing.Size(0, 17);
            this.NumServicesMsg.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(2, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "                ";
            // 
            // CheckBtn
            // 
            this.CheckBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.CheckBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.CheckBtn.BorderColor = System.Drawing.Color.Empty;
            this.CheckBtn.BorderRadius = 27;
            this.CheckBtn.BorderSize = 0;
            this.CheckBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CheckBtn.ForeColor = System.Drawing.Color.White;
            this.CheckBtn.Location = new System.Drawing.Point(122, 481);
            this.CheckBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckBtn.Name = "CheckBtn";
            this.CheckBtn.Size = new System.Drawing.Size(205, 53);
            this.CheckBtn.TabIndex = 2;
            this.CheckBtn.Text = "Check";
            this.CheckBtn.TextColor = System.Drawing.Color.White;
            this.CheckBtn.UseVisualStyleBackColor = false;
            this.CheckBtn.Click += new System.EventHandler(this.CheckBtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(18, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(415, 123);
            this.label2.TabIndex = 30;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameErrMeg
            // 
            this.UsernameErrMeg.AutoSize = true;
            this.UsernameErrMeg.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UsernameErrMeg.ForeColor = System.Drawing.Color.Red;
            this.UsernameErrMeg.Location = new System.Drawing.Point(21, 282);
            this.UsernameErrMeg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UsernameErrMeg.Name = "UsernameErrMeg";
            this.UsernameErrMeg.Size = new System.Drawing.Size(0, 15);
            this.UsernameErrMeg.TabIndex = 31;
            // 
            // ResultBox
            // 
            this.ResultBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ResultBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ResultBox.Location = new System.Drawing.Point(447, 24);
            this.ResultBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ResultBox.Multiline = true;
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.ReadOnly = true;
            this.ResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultBox.Size = new System.Drawing.Size(689, 532);
            this.ResultBox.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 21);
            this.label1.TabIndex = 33;
            this.label1.Text = "How...?\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EnterContractNameLabel
            // 
            this.EnterContractNameLabel.AutoSize = true;
            this.EnterContractNameLabel.Location = new System.Drawing.Point(16, 238);
            this.EnterContractNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterContractNameLabel.Name = "EnterContractNameLabel";
            this.EnterContractNameLabel.Size = new System.Drawing.Size(327, 15);
            this.EnterContractNameLabel.TabIndex = 7;
            this.EnterContractNameLabel.Text = "Enter your username (if empty, it will serach for all API\'s CRs)";
            // 
            // MyWorkHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsernameErrMeg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NumServicesMsg);
            this.Controls.Add(this.OptionsBox);
            this.Controls.Add(this.CriteriaInputErrMeg);
            this.Controls.Add(this.NoteLabel);
            this.Controls.Add(this.EnterContractNameLabel);
            this.Controls.Add(this.UsernameInput);
            this.Controls.Add(this.EnterCriteriaLabel);
            this.Controls.Add(this.SearchByBox);
            this.Controls.Add(this.CheckBtn);
            this.Controls.Add(this.CriteriaInput);
            this.Controls.Add(this.ResultBox);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1154, 568);
            this.Name = "MyWorkHome";
            this.Size = new System.Drawing.Size(1154, 568);
            this.SearchByBox.ResumeLayout(false);
            this.SearchByBox.PerformLayout();
            this.OptionsBox.ResumeLayout(false);
            this.OptionsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevButton CheckBtn;
        private System.Windows.Forms.GroupBox SearchByBox;
        private System.Windows.Forms.RadioButton ByLastActivityRadBtn;
        private System.Windows.Forms.TextBox CriteriaInput;
        private System.Windows.Forms.Label EnterCriteriaLabel;
        private System.Windows.Forms.TextBox UsernameInput;
        private System.Windows.Forms.Label NoteLabel;
        private DevButton AddBtn;
        private System.Windows.Forms.TextBox MSNames;
        private DevButton ClearBtn;
        private System.Windows.Forms.RadioButton ByCRNumRadBtn;
        private System.Windows.Forms.Label CriteriaInputErrMeg;
        private System.Windows.Forms.GroupBox OptionsBox;
        private System.Windows.Forms.CheckBox CheckIgInAttachmentsRadBtn;
        private System.Windows.Forms.Label NumServicesMsg;
        private DevButton DeleteBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label UsernameErrMeg;
        private System.Windows.Forms.TextBox ResultBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CopiedRadBtn;
        private System.Windows.Forms.Label EnterContractNameLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox ExcludActiveRadBtn;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox GetDevNameRadBtn;
        private System.Windows.Forms.RadioButton ByTitleRadBtn;
        private System.Windows.Forms.RadioButton ReviewedByMeRadBtn;
        private CheckBox ExcludeUnderDevRadBtn;
        private CheckBox ExcludeActiveRadBtn;
        private CheckBox MissingRequirementsOnlyRadBtn;
    }
}

