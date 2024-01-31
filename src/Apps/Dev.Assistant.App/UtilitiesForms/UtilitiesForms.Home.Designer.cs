
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.UtilitiesForms
{
    partial class UtilitiesFormsHome
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
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.DbNameBox = new System.Windows.Forms.GroupBox();
            this.MicroProjectRadBtn = new System.Windows.Forms.RadioButton();
            this.OLSDB = new System.Windows.Forms.RadioButton();
            this.ODSASIS = new System.Windows.Forms.RadioButton();
            this.Input = new System.Windows.Forms.TextBox();
            this.EnterServNameLabel = new System.Windows.Forms.Label();
            this.EnterContractNameLabel = new System.Windows.Forms.Label();
            this.ContractNameInput = new System.Windows.Forms.TextBox();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.MSNames = new System.Windows.Forms.TextBox();
            this.AddCommentAuthBox = new System.Windows.Forms.CheckBox();
            this.ServiceNameErrMeg = new System.Windows.Forms.Label();
            this.BirthDateCheck = new System.Windows.Forms.CheckBox();
            this.OptionsBox = new System.Windows.Forms.GroupBox();
            this.ServiceAccessCheck = new System.Windows.Forms.CheckBox();
            this.IncludeDisplayIrregularCheck = new System.Windows.Forms.CheckBox();
            this.NumServicesMsg = new System.Windows.Forms.Label();
            this.MicroPathTxt = new System.Windows.Forms.TextBox();
            this.DeleteBtnTip = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ClearBtnTip = new System.Windows.Forms.ToolTip(this.components);
            this.DeleteBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.ClearBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.AddBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.GetValueBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.MicroPathLabel = new System.Windows.Forms.Label();
            this.GetIDTypesBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ContractNameErrMeg = new System.Windows.Forms.Label();
            this.BrowseBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.LeftContainer = new System.Windows.Forms.SplitContainer();
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.DbNameBox.SuspendLayout();
            this.OptionsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftContainer)).BeginInit();
            this.LeftContainer.Panel1.SuspendLayout();
            this.LeftContainer.Panel2.SuspendLayout();
            this.LeftContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultGridView
            // 
            this.ResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ResultGridView.Location = new System.Drawing.Point(19, 43);
            this.ResultGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ResultGridView.MultiSelect = false;
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.ReadOnly = true;
            this.ResultGridView.RowTemplate.Height = 25;
            this.ResultGridView.Size = new System.Drawing.Size(681, 495);
            this.ResultGridView.TabIndex = 0;
            // 
            // DbNameBox
            // 
            this.DbNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbNameBox.Controls.Add(this.MicroProjectRadBtn);
            this.DbNameBox.Controls.Add(this.OLSDB);
            this.DbNameBox.Controls.Add(this.ODSASIS);
            this.DbNameBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DbNameBox.Location = new System.Drawing.Point(5, 133);
            this.DbNameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DbNameBox.Name = "DbNameBox";
            this.DbNameBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DbNameBox.Size = new System.Drawing.Size(343, 46);
            this.DbNameBox.TabIndex = 4;
            this.DbNameBox.TabStop = false;
            this.DbNameBox.Text = "DB Name";
            // 
            // MicroProjectRadBtn
            // 
            this.MicroProjectRadBtn.AutoSize = true;
            this.MicroProjectRadBtn.Location = new System.Drawing.Point(246, 19);
            this.MicroProjectRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MicroProjectRadBtn.Name = "MicroProjectRadBtn";
            this.MicroProjectRadBtn.Size = new System.Drawing.Size(87, 17);
            this.MicroProjectRadBtn.TabIndex = 3;
            this.MicroProjectRadBtn.TabStop = true;
            this.MicroProjectRadBtn.Text = "Micro Project";
            this.MicroProjectRadBtn.UseVisualStyleBackColor = true;
            this.MicroProjectRadBtn.CheckedChanged += new System.EventHandler(this.MicroProjectRadBtn_CheckedChanged);
            // 
            // OLSDB
            // 
            this.OLSDB.AutoSize = true;
            this.OLSDB.Location = new System.Drawing.Point(107, 19);
            this.OLSDB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OLSDB.Name = "OLSDB";
            this.OLSDB.Size = new System.Drawing.Size(111, 17);
            this.OLSDB.TabIndex = 2;
            this.OLSDB.TabStop = true;
            this.OLSDB.Text = "OLSDB for Absher";
            this.OLSDB.UseVisualStyleBackColor = true;
            this.OLSDB.CheckedChanged += new System.EventHandler(this.OLSDB_CheckedChanged);
            // 
            // ODSASIS
            // 
            this.ODSASIS.AutoSize = true;
            this.ODSASIS.Checked = true;
            this.ODSASIS.Location = new System.Drawing.Point(11, 19);
            this.ODSASIS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ODSASIS.Name = "ODSASIS";
            this.ODSASIS.Size = new System.Drawing.Size(69, 17);
            this.ODSASIS.TabIndex = 0;
            this.ODSASIS.TabStop = true;
            this.ODSASIS.Text = "ODSASIS";
            this.ODSASIS.UseVisualStyleBackColor = true;
            this.ODSASIS.CheckedChanged += new System.EventHandler(this.ODSASIS_CheckedChanged);
            // 
            // Input
            // 
            this.Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Input.Location = new System.Drawing.Point(5, 20);
            this.Input.Margin = new System.Windows.Forms.Padding(10);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(343, 23);
            this.Input.TabIndex = 1;
            this.Input.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // EnterServNameLabel
            // 
            this.EnterServNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnterServNameLabel.AutoSize = true;
            this.EnterServNameLabel.Location = new System.Drawing.Point(5, 4);
            this.EnterServNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterServNameLabel.Name = "EnterServNameLabel";
            this.EnterServNameLabel.Size = new System.Drawing.Size(227, 15);
            this.EnterServNameLabel.TabIndex = 5;
            this.EnterServNameLabel.Text = "Enter Micro Service Name or Service Path:";
            // 
            // EnterContractNameLabel
            // 
            this.EnterContractNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnterContractNameLabel.AutoSize = true;
            this.EnterContractNameLabel.Location = new System.Drawing.Point(5, 67);
            this.EnterContractNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterContractNameLabel.Name = "EnterContractNameLabel";
            this.EnterContractNameLabel.Size = new System.Drawing.Size(142, 15);
            this.EnterContractNameLabel.TabIndex = 7;
            this.EnterContractNameLabel.Text = "Enter Contract Username:";
            // 
            // ContractNameInput
            // 
            this.ContractNameInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContractNameInput.Location = new System.Drawing.Point(5, 84);
            this.ContractNameInput.Margin = new System.Windows.Forms.Padding(10);
            this.ContractNameInput.Name = "ContractNameInput";
            this.ContractNameInput.Size = new System.Drawing.Size(343, 23);
            this.ContractNameInput.TabIndex = 6;
            this.ContractNameInput.Text = "ws_";
            this.ContractNameInput.TextChanged += new System.EventHandler(this.ContractNameInput_TextChanged);
            this.ContractNameInput.Enter += new System.EventHandler(this.ContractNameInput_Enter);
            // 
            // NoteLabel
            // 
            this.NoteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NoteLabel.Location = new System.Drawing.Point(6, 422);
            this.NoteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NoteLabel.Name = "NoteLabel";
            this.NoteLabel.Size = new System.Drawing.Size(329, 21);
            this.NoteLabel.TabIndex = 9;
            this.NoteLabel.Text = "The output will be Copied to your Clipboard.";
            // 
            // MSNames
            // 
            this.MSNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MSNames.BackColor = System.Drawing.SystemColors.Menu;
            this.MSNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MSNames.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.MSNames.Location = new System.Drawing.Point(1, 43);
            this.MSNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MSNames.Multiline = true;
            this.MSNames.Name = "MSNames";
            this.MSNames.ReadOnly = true;
            this.MSNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MSNames.Size = new System.Drawing.Size(349, 67);
            this.MSNames.TabIndex = 11;
            // 
            // AddCommentAuthBox
            // 
            this.AddCommentAuthBox.AutoSize = true;
            this.AddCommentAuthBox.Location = new System.Drawing.Point(8, 79);
            this.AddCommentAuthBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddCommentAuthBox.Name = "AddCommentAuthBox";
            this.AddCommentAuthBox.Size = new System.Drawing.Size(295, 19);
            this.AddCommentAuthBox.TabIndex = 14;
            this.AddCommentAuthBox.Text = "Add comment \"In AuthServer: Create a new user...\"";
            this.AddCommentAuthBox.UseVisualStyleBackColor = true;
            this.AddCommentAuthBox.CheckedChanged += new System.EventHandler(this.AddCommentAuthBox_CheckedChanged);
            // 
            // ServiceNameErrMeg
            // 
            this.ServiceNameErrMeg.AutoSize = true;
            this.ServiceNameErrMeg.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ServiceNameErrMeg.ForeColor = System.Drawing.Color.Red;
            this.ServiceNameErrMeg.Location = new System.Drawing.Point(5, 47);
            this.ServiceNameErrMeg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ServiceNameErrMeg.Name = "ServiceNameErrMeg";
            this.ServiceNameErrMeg.Size = new System.Drawing.Size(0, 15);
            this.ServiceNameErrMeg.TabIndex = 15;
            // 
            // BirthDateCheck
            // 
            this.BirthDateCheck.AutoSize = true;
            this.BirthDateCheck.Location = new System.Drawing.Point(8, 60);
            this.BirthDateCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BirthDateCheck.Name = "BirthDateCheck";
            this.BirthDateCheck.Size = new System.Drawing.Size(209, 19);
            this.BirthDateCheck.TabIndex = 16;
            this.BirthDateCheck.Text = "Include BirthDate validation tables ";
            this.BirthDateCheck.UseVisualStyleBackColor = true;
            // 
            // OptionsBox
            // 
            this.OptionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsBox.Controls.Add(this.ServiceAccessCheck);
            this.OptionsBox.Controls.Add(this.IncludeDisplayIrregularCheck);
            this.OptionsBox.Controls.Add(this.AddCommentAuthBox);
            this.OptionsBox.Controls.Add(this.BirthDateCheck);
            this.OptionsBox.Location = new System.Drawing.Point(5, 185);
            this.OptionsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OptionsBox.Size = new System.Drawing.Size(329, 101);
            this.OptionsBox.TabIndex = 18;
            this.OptionsBox.TabStop = false;
            this.OptionsBox.Text = "Options";
            // 
            // ServiceAccessCheck
            // 
            this.ServiceAccessCheck.AutoSize = true;
            this.ServiceAccessCheck.Location = new System.Drawing.Point(8, 41);
            this.ServiceAccessCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ServiceAccessCheck.Name = "ServiceAccessCheck";
            this.ServiceAccessCheck.Size = new System.Drawing.Size(305, 19);
            this.ServiceAccessCheck.TabIndex = 23;
            this.ServiceAccessCheck.Text = "Include MicroListPersonServiceAccessRecords tables ";
            this.ServiceAccessCheck.UseVisualStyleBackColor = true;
            // 
            // IncludeDisplayIrregularCheck
            // 
            this.IncludeDisplayIrregularCheck.AutoSize = true;
            this.IncludeDisplayIrregularCheck.Checked = true;
            this.IncludeDisplayIrregularCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeDisplayIrregularCheck.Location = new System.Drawing.Point(8, 22);
            this.IncludeDisplayIrregularCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IncludeDisplayIrregularCheck.Name = "IncludeDisplayIrregularCheck";
            this.IncludeDisplayIrregularCheck.Size = new System.Drawing.Size(298, 19);
            this.IncludeDisplayIrregularCheck.TabIndex = 22;
            this.IncludeDisplayIrregularCheck.Text = "Include DisplayIrregular tables (for ID && OperatorID)";
            this.IncludeDisplayIrregularCheck.UseVisualStyleBackColor = true;
            // 
            // NumServicesMsg
            // 
            this.NumServicesMsg.AutoSize = true;
            this.NumServicesMsg.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NumServicesMsg.ForeColor = System.Drawing.Color.Teal;
            this.NumServicesMsg.Location = new System.Drawing.Point(29, 541);
            this.NumServicesMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NumServicesMsg.Name = "NumServicesMsg";
            this.NumServicesMsg.Size = new System.Drawing.Size(0, 17);
            this.NumServicesMsg.TabIndex = 20;
            // 
            // MicroPathTxt
            // 
            this.MicroPathTxt.Location = new System.Drawing.Point(60, 6);
            this.MicroPathTxt.Margin = new System.Windows.Forms.Padding(10);
            this.MicroPathTxt.Name = "MicroPathTxt";
            this.MicroPathTxt.Size = new System.Drawing.Size(497, 23);
            this.MicroPathTxt.TabIndex = 21;
            this.MicroPathTxt.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "Thanks for Meshari  Aljaloud";
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBtn.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DeleteBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.DeleteBtn.BorderRadius = 12;
            this.DeleteBtn.BorderSize = 1;
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.DeleteBtn.Location = new System.Drawing.Point(352, 49);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(62, 24);
            this.DeleteBtn.TabIndex = 19;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.BackColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BackgroundColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClearBtn.BorderColor = System.Drawing.Color.Maroon;
            this.ClearBtn.BorderRadius = 12;
            this.ClearBtn.BorderSize = 1;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBtn.ForeColor = System.Drawing.Color.Maroon;
            this.ClearBtn.Location = new System.Drawing.Point(352, 41);
            this.ClearBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(62, 24);
            this.ClearBtn.TabIndex = 12;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.TextColor = System.Drawing.Color.Maroon;
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.BackColor = System.Drawing.SystemColors.Control;
            this.AddBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AddBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.AddBtn.BorderRadius = 12;
            this.AddBtn.BorderSize = 1;
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.AddBtn.Location = new System.Drawing.Point(352, 19);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(62, 24);
            this.AddBtn.TabIndex = 10;
            this.AddBtn.Text = "Add";
            this.AddBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // GetValueBtn
            // 
            this.GetValueBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GetValueBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.GetValueBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.GetValueBtn.BorderColor = System.Drawing.Color.Empty;
            this.GetValueBtn.BorderRadius = 27;
            this.GetValueBtn.BorderSize = 0;
            this.GetValueBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetValueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetValueBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GetValueBtn.ForeColor = System.Drawing.Color.White;
            this.GetValueBtn.Location = new System.Drawing.Point(76, 304);
            this.GetValueBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetValueBtn.Name = "GetValueBtn";
            this.GetValueBtn.Size = new System.Drawing.Size(185, 53);
            this.GetValueBtn.TabIndex = 2;
            this.GetValueBtn.Text = "Get Tables";
            this.GetValueBtn.TextColor = System.Drawing.Color.White;
            this.GetValueBtn.UseVisualStyleBackColor = false;
            this.GetValueBtn.Click += new System.EventHandler(this.GetValueBtn_Click);
            // 
            // MicroPathLabel
            // 
            this.MicroPathLabel.AutoSize = true;
            this.MicroPathLabel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MicroPathLabel.Location = new System.Drawing.Point(19, 6);
            this.MicroPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MicroPathLabel.Name = "MicroPathLabel";
            this.MicroPathLabel.Size = new System.Drawing.Size(27, 22);
            this.MicroPathLabel.TabIndex = 25;
            this.MicroPathLabel.Text = "Micro\r\nPath:";
            // 
            // GetIDTypesBtn
            // 
            this.GetIDTypesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GetIDTypesBtn.BackColor = System.Drawing.SystemColors.Control;
            this.GetIDTypesBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GetIDTypesBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.GetIDTypesBtn.BorderRadius = 19;
            this.GetIDTypesBtn.BorderSize = 1;
            this.GetIDTypesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetIDTypesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetIDTypesBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetIDTypesBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.GetIDTypesBtn.Location = new System.Drawing.Point(109, 366);
            this.GetIDTypesBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetIDTypesBtn.Name = "GetIDTypesBtn";
            this.GetIDTypesBtn.Size = new System.Drawing.Size(120, 37);
            this.GetIDTypesBtn.TabIndex = 27;
            this.GetIDTypesBtn.Text = "Get ID Types";
            this.GetIDTypesBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.GetIDTypesBtn.UseVisualStyleBackColor = false;
            this.GetIDTypesBtn.Click += new System.EventHandler(this.GetIDTypesBtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(0, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 21);
            this.label2.TabIndex = 30;
            this.label2.Text = "If you\'re calling PDS, multi Micros or both add all services to the list.";
            // 
            // ContractNameErrMeg
            // 
            this.ContractNameErrMeg.AutoSize = true;
            this.ContractNameErrMeg.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ContractNameErrMeg.ForeColor = System.Drawing.Color.Red;
            this.ContractNameErrMeg.Location = new System.Drawing.Point(5, 111);
            this.ContractNameErrMeg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ContractNameErrMeg.Name = "ContractNameErrMeg";
            this.ContractNameErrMeg.Size = new System.Drawing.Size(0, 15);
            this.ContractNameErrMeg.TabIndex = 31;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.BackColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.BorderRadius = 12;
            this.BrowseBtn.BorderSize = 1;
            this.BrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.Location = new System.Drawing.Point(564, 5);
            this.BrowseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(64, 24);
            this.BrowseBtn.TabIndex = 32;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.UseVisualStyleBackColor = false;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // LeftContainer
            // 
            this.LeftContainer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LeftContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LeftContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.LeftContainer.IsSplitterFixed = true;
            this.LeftContainer.Location = new System.Drawing.Point(15, 0);
            this.LeftContainer.Name = "LeftContainer";
            this.LeftContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LeftContainer.Panel1
            // 
            this.LeftContainer.Panel1.Controls.Add(this.MSNames);
            this.LeftContainer.Panel1.Controls.Add(this.ClearBtn);
            this.LeftContainer.Panel1.Controls.Add(this.label2);
            this.LeftContainer.Panel1.Controls.Add(this.label4);
            // 
            // LeftContainer.Panel2
            // 
            this.LeftContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.LeftContainer.Panel2.Controls.Add(this.EnterServNameLabel);
            this.LeftContainer.Panel2.Controls.Add(this.Input);
            this.LeftContainer.Panel2.Controls.Add(this.ContractNameErrMeg);
            this.LeftContainer.Panel2.Controls.Add(this.GetValueBtn);
            this.LeftContainer.Panel2.Controls.Add(this.DbNameBox);
            this.LeftContainer.Panel2.Controls.Add(this.GetIDTypesBtn);
            this.LeftContainer.Panel2.Controls.Add(this.ContractNameInput);
            this.LeftContainer.Panel2.Controls.Add(this.EnterContractNameLabel);
            this.LeftContainer.Panel2.Controls.Add(this.NoteLabel);
            this.LeftContainer.Panel2.Controls.Add(this.AddBtn);
            this.LeftContainer.Panel2.Controls.Add(this.ServiceNameErrMeg);
            this.LeftContainer.Panel2.Controls.Add(this.OptionsBox);
            this.LeftContainer.Panel2.Controls.Add(this.DeleteBtn);
            this.LeftContainer.Panel2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.LeftContainer.Size = new System.Drawing.Size(414, 568);
            this.LeftContainer.SplitterDistance = 118;
            this.LeftContainer.TabIndex = 33;
            // 
            // MainContainer
            // 
            this.MainContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.IsSplitterFixed = true;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.LeftContainer);
            this.MainContainer.Panel1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.ResultGridView);
            this.MainContainer.Panel2.Controls.Add(this.BrowseBtn);
            this.MainContainer.Panel2.Controls.Add(this.NumServicesMsg);
            this.MainContainer.Panel2.Controls.Add(this.MicroPathLabel);
            this.MainContainer.Panel2.Controls.Add(this.MicroPathTxt);
            this.MainContainer.Size = new System.Drawing.Size(1154, 568);
            this.MainContainer.SplitterDistance = 429;
            this.MainContainer.TabIndex = 34;
            // 
            // UtilitiesFormsHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.MainContainer);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(1154, 568);
            this.Name = "UtilitiesFormsHome";
            this.Size = new System.Drawing.Size(1154, 568);
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.DbNameBox.ResumeLayout(false);
            this.DbNameBox.PerformLayout();
            this.OptionsBox.ResumeLayout(false);
            this.OptionsBox.PerformLayout();
            this.LeftContainer.Panel1.ResumeLayout(false);
            this.LeftContainer.Panel1.PerformLayout();
            this.LeftContainer.Panel2.ResumeLayout(false);
            this.LeftContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftContainer)).EndInit();
            this.LeftContainer.ResumeLayout(false);
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultGridView;
        private DevButton GetValueBtn;
        private System.Windows.Forms.GroupBox DbNameBox;
        private System.Windows.Forms.RadioButton ODSASIS;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Label EnterServNameLabel;
        private System.Windows.Forms.Label EnterContractNameLabel;
        private System.Windows.Forms.TextBox ContractNameInput;
        private System.Windows.Forms.Label NoteLabel;
        private DevButton AddBtn;
        private System.Windows.Forms.TextBox MSNames;
        private DevButton ClearBtn;
        private System.Windows.Forms.RadioButton OLSDB;
        private System.Windows.Forms.CheckBox AddCommentAuthBox;
        private System.Windows.Forms.Label ServiceNameErrMeg;
        private System.Windows.Forms.CheckBox BirthDateCheck;
        private System.Windows.Forms.GroupBox OptionsBox;
        private System.Windows.Forms.CheckBox IncludeDisplayIrregularCheck;
        private System.Windows.Forms.Label NumServicesMsg;
        private System.Windows.Forms.TextBox MicroPathTxt;
        private DevButton DeleteBtn;
        private System.Windows.Forms.ToolTip DeleteBtnTip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip ClearBtnTip;
        private System.Windows.Forms.Label MicroPathLabel;
        private DevButton GetIDTypesBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ContractNameErrMeg;
        private System.Windows.Forms.CheckBox ServiceAccessCheck;
        private DevButton BrowseBtn;
        private RadioButton MicroProjectRadBtn;
        private SplitContainer LeftContainer;
        private SplitContainer MainContainer;
    }
}

