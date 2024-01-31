
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Staff
{
    partial class StaffHome
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
            this.components = new System.ComponentModel.Container();
            this.PrBodyContainer = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.ByManagerNameBox = new System.Windows.Forms.CheckBox();
            this.ByDeptBox = new System.Windows.Forms.CheckBox();
            this.BySectionBox = new System.Windows.Forms.CheckBox();
            this.ByPositionBox = new System.Windows.Forms.CheckBox();
            this.ByExtBox = new System.Windows.Forms.CheckBox();
            this.ByNameBox = new System.Windows.Forms.CheckBox();
            this.ByUsernameBox = new System.Windows.Forms.CheckBox();
            this.StaffCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.ListAllBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.ExtsGridView = new System.Windows.Forms.DataGridView();
            this.ListAllTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PrBodyContainer)).BeginInit();
            this.PrBodyContainer.Panel1.SuspendLayout();
            this.PrBodyContainer.Panel2.SuspendLayout();
            this.PrBodyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExtsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PrBodyContainer
            // 
            this.PrBodyContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrBodyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrBodyContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.PrBodyContainer.IsSplitterFixed = true;
            this.PrBodyContainer.Location = new System.Drawing.Point(0, 0);
            this.PrBodyContainer.Name = "PrBodyContainer";
            this.PrBodyContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PrBodyContainer.Panel1
            // 
            this.PrBodyContainer.Panel1.Controls.Add(this.label2);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByManagerNameBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByDeptBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.BySectionBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByPositionBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByExtBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByNameBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ByUsernameBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.StaffCountLabel);
            this.PrBodyContainer.Panel1.Controls.Add(this.label1);
            this.PrBodyContainer.Panel1.Controls.Add(this.SearchTextBox);
            this.PrBodyContainer.Panel1.Controls.Add(this.ListAllBtn);
            this.PrBodyContainer.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrBodyContainer.Panel1.Padding = new System.Windows.Forms.Padding(20, 27, 30, 27);
            // 
            // PrBodyContainer.Panel2
            // 
            this.PrBodyContainer.Panel2.Controls.Add(this.ExtsGridView);
            this.PrBodyContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.PrBodyContainer.Size = new System.Drawing.Size(1154, 571);
            this.PrBodyContainer.SplitterDistance = 82;
            this.PrBodyContainer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(20, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Search by:";
            // 
            // ByManagerNameBox
            // 
            this.ByManagerNameBox.AutoSize = true;
            this.ByManagerNameBox.Checked = true;
            this.ByManagerNameBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByManagerNameBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByManagerNameBox.Location = new System.Drawing.Point(558, 61);
            this.ByManagerNameBox.Name = "ByManagerNameBox";
            this.ByManagerNameBox.Size = new System.Drawing.Size(106, 19);
            this.ByManagerNameBox.TabIndex = 70;
            this.ByManagerNameBox.Text = "Manager name";
            this.ByManagerNameBox.UseVisualStyleBackColor = true;
            this.ByManagerNameBox.CheckedChanged += new System.EventHandler(this.ByManagerNameBox_CheckedChanged);
            // 
            // ByDeptBox
            // 
            this.ByDeptBox.AutoSize = true;
            this.ByDeptBox.Checked = true;
            this.ByDeptBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByDeptBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByDeptBox.Location = new System.Drawing.Point(501, 61);
            this.ByDeptBox.Name = "ByDeptBox";
            this.ByDeptBox.Size = new System.Drawing.Size(51, 19);
            this.ByDeptBox.TabIndex = 69;
            this.ByDeptBox.Text = "Dept";
            this.ByDeptBox.UseVisualStyleBackColor = true;
            this.ByDeptBox.CheckedChanged += new System.EventHandler(this.ByDeptBox_CheckedChanged);
            // 
            // BySectionBox
            // 
            this.BySectionBox.AutoSize = true;
            this.BySectionBox.Checked = true;
            this.BySectionBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BySectionBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BySectionBox.Location = new System.Drawing.Point(430, 61);
            this.BySectionBox.Name = "BySectionBox";
            this.BySectionBox.Size = new System.Drawing.Size(65, 19);
            this.BySectionBox.TabIndex = 68;
            this.BySectionBox.Text = "Section";
            this.BySectionBox.UseVisualStyleBackColor = true;
            this.BySectionBox.CheckedChanged += new System.EventHandler(this.BySectionBox_CheckedChanged);
            // 
            // ByPositionBox
            // 
            this.ByPositionBox.AutoSize = true;
            this.ByPositionBox.Checked = true;
            this.ByPositionBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByPositionBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByPositionBox.Location = new System.Drawing.Point(355, 61);
            this.ByPositionBox.Name = "ByPositionBox";
            this.ByPositionBox.Size = new System.Drawing.Size(69, 19);
            this.ByPositionBox.TabIndex = 67;
            this.ByPositionBox.Text = "Position";
            this.ByPositionBox.UseVisualStyleBackColor = true;
            this.ByPositionBox.CheckedChanged += new System.EventHandler(this.ByPositionBox_CheckedChanged);
            // 
            // ByExtBox
            // 
            this.ByExtBox.AutoSize = true;
            this.ByExtBox.Checked = true;
            this.ByExtBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByExtBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByExtBox.Location = new System.Drawing.Point(308, 61);
            this.ByExtBox.Name = "ByExtBox";
            this.ByExtBox.Size = new System.Drawing.Size(41, 19);
            this.ByExtBox.TabIndex = 66;
            this.ByExtBox.Text = "Ext";
            this.ByExtBox.UseVisualStyleBackColor = true;
            this.ByExtBox.CheckedChanged += new System.EventHandler(this.ByExtBox_CheckedChanged);
            // 
            // ByNameBox
            // 
            this.ByNameBox.AutoSize = true;
            this.ByNameBox.Checked = true;
            this.ByNameBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByNameBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByNameBox.Location = new System.Drawing.Point(224, 61);
            this.ByNameBox.Name = "ByNameBox";
            this.ByNameBox.Size = new System.Drawing.Size(78, 19);
            this.ByNameBox.TabIndex = 65;
            this.ByNameBox.Text = "Full name";
            this.ByNameBox.UseVisualStyleBackColor = true;
            this.ByNameBox.CheckedChanged += new System.EventHandler(this.ByNameBox_CheckedChanged);
            // 
            // ByUsernameBox
            // 
            this.ByUsernameBox.AutoSize = true;
            this.ByUsernameBox.Checked = true;
            this.ByUsernameBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ByUsernameBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ByUsernameBox.Location = new System.Drawing.Point(84, 61);
            this.ByUsernameBox.Name = "ByUsernameBox";
            this.ByUsernameBox.Size = new System.Drawing.Size(134, 19);
            this.ByUsernameBox.TabIndex = 64;
            this.ByUsernameBox.Text = "Username and Email";
            this.ByUsernameBox.UseVisualStyleBackColor = true;
            this.ByUsernameBox.CheckedChanged += new System.EventHandler(this.ByUsernameBox_CheckedChanged);
            // 
            // StaffCountLabel
            // 
            this.StaffCountLabel.AutoSize = true;
            this.StaffCountLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StaffCountLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.StaffCountLabel.Location = new System.Drawing.Point(674, 33);
            this.StaffCountLabel.Name = "StaffCountLabel";
            this.StaffCountLabel.Size = new System.Drawing.Size(79, 17);
            this.StaffCountLabel.TabIndex = 63;
            this.StaffCountLabel.Text = "Staff Count:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 62;
            this.label1.Text = "Search:";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchTextBox.ForeColor = System.Drawing.Color.DarkCyan;
            this.SearchTextBox.Location = new System.Drawing.Point(84, 28);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(580, 27);
            this.SearchTextBox.TabIndex = 61;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ListAllBtn
            // 
            this.ListAllBtn.BackColor = System.Drawing.SystemColors.Control;
            this.ListAllBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ListAllBtn.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ListAllBtn.BorderRadius = 13;
            this.ListAllBtn.BorderSize = 1;
            this.ListAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListAllBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListAllBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ListAllBtn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ListAllBtn.Location = new System.Drawing.Point(927, 27);
            this.ListAllBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ListAllBtn.Name = "ListAllBtn";
            this.ListAllBtn.Size = new System.Drawing.Size(197, 28);
            this.ListAllBtn.TabIndex = 4;
            this.ListAllBtn.Text = "Display all records to UI";
            this.ListAllBtn.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ListAllBtn.UseVisualStyleBackColor = false;
            this.ListAllBtn.Click += new System.EventHandler(this.ListAllBtn_Click);
            // 
            // ExtsGridView
            // 
            this.ExtsGridView.AllowUserToAddRows = false;
            this.ExtsGridView.AllowUserToDeleteRows = false;
            this.ExtsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ExtsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExtsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.ExtsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExtsGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ExtsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExtsGridView.Location = new System.Drawing.Point(0, 10);
            this.ExtsGridView.Name = "ExtsGridView";
            this.ExtsGridView.RowTemplate.Height = 25;
            this.ExtsGridView.Size = new System.Drawing.Size(1154, 475);
            this.ExtsGridView.TabIndex = 0;
            // 
            // StaffHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.PrBodyContainer);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "StaffHome";
            this.Size = new System.Drawing.Size(1154, 571);
            this.Load += new System.EventHandler(this.PhoneExtsHome_Load);
            this.PrBodyContainer.Panel1.ResumeLayout(false);
            this.PrBodyContainer.Panel1.PerformLayout();
            this.PrBodyContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PrBodyContainer)).EndInit();
            this.PrBodyContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExtsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer PrBodyContainer;
        private DevButton ListAllBtn;
        private DataGridView ExtsGridView;
        private Label label1;
        private TextBox SearchTextBox;
        private Label StaffCountLabel;
        private ToolTip ListAllTip;
        private CheckBox ByUsernameBox;
        private CheckBox ByNameBox;
        private CheckBox ByExtBox;
        private CheckBox ByPositionBox;
        private CheckBox BySectionBox;
        private CheckBox ByDeptBox;
        private CheckBox ByManagerNameBox;
        private Label label2;
    }
}

