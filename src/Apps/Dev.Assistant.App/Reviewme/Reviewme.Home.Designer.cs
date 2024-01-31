using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Reviewme
{
    partial class ReviewmeHome
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
            this.label1 = new System.Windows.Forms.Label();
            this.UserInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathInput = new System.Windows.Forms.TextBox();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.GetPackageReferencesBtn = new System.Windows.Forms.RadioButton();
            this.CheckRulesRadBtn = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.GenerateIGRadBtn = new System.Windows.Forms.RadioButton();
            this.CompareTRadBtn = new System.Windows.Forms.RadioButton();
            this.GetAllSerivcesBtn = new System.Windows.Forms.RadioButton();
            this.ConverterBtn = new System.Windows.Forms.RadioButton();
            this.GetAllMethodsBtn = new System.Windows.Forms.RadioButton();
            this.GetDbLengthBtn = new System.Windows.Forms.RadioButton();
            this.ReviewReportRadBtn = new System.Windows.Forms.RadioButton();
            this.GetPropsInfoBtn = new System.Windows.Forms.RadioButton();
            this.GenerateNdbJsonRadBtn = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.GetOutput = new Dev.Assistant.Common.Controllers.DevButton();
            this.CompareGroupBox = new System.Windows.Forms.GroupBox();
            this.IsText = new System.Windows.Forms.RadioButton();
            this.IsSql = new System.Windows.Forms.RadioButton();
            this.IsModel = new System.Windows.Forms.RadioButton();
            this.IsJson = new System.Windows.Forms.RadioButton();
            this.GenerateJsonOptions = new System.Windows.Forms.GroupBox();
            this.OpenJsonBox = new System.Windows.Forms.CheckBox();
            this.SqlOptions = new System.Windows.Forms.GroupBox();
            this.RemoveAsBox = new System.Windows.Forms.CheckBox();
            this.ToLowerCase = new System.Windows.Forms.CheckBox();
            this.OnlyKeys = new System.Windows.Forms.CheckBox();
            this.Input2 = new System.Windows.Forms.TextBox();
            this.Input2Label = new System.Windows.Forms.Label();
            this.BrowseBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.GenerateIGOptions = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GenMicroBtn = new System.Windows.Forms.RadioButton();
            this.GenApiBtn = new System.Windows.Forms.RadioButton();
            this.GenerateIgsOptionRatBrn = new System.Windows.Forms.CheckBox();
            this.AddBracketBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.IGVersionInput = new System.Windows.Forms.TextBox();
            this.EnterServNameLabel = new System.Windows.Forms.Label();
            this.WrittenByInput = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ImageResult = new System.Windows.Forms.PictureBox();
            this.ClassConversionOpt = new System.Windows.Forms.GroupBox();
            this.suffixTxtBox = new System.Windows.Forms.TextBox();
            this.AddSuffixLabel = new System.Windows.Forms.Label();
            this.PrepareXmlBtn = new System.Windows.Forms.CheckBox();
            this.ConverterOptions = new System.Windows.Forms.GroupBox();
            this.VbToCharpBtn = new System.Windows.Forms.RadioButton();
            this.Blob2ImageBtn = new System.Windows.Forms.RadioButton();
            this.Base2ImageBtn = new System.Windows.Forms.RadioButton();
            this.DecryptTokenBtn = new System.Windows.Forms.RadioButton();
            this.JsonToCsharpBtn = new System.Windows.Forms.RadioButton();
            this.JsonToXmlBtn = new System.Windows.Forms.RadioButton();
            this.XmlToJsonBtn = new System.Windows.Forms.RadioButton();
            this.CodeToSqlBtn = new System.Windows.Forms.RadioButton();
            this.SqlToCodeBtn = new System.Windows.Forms.RadioButton();
            this.SqlCoverterOptions = new System.Windows.Forms.GroupBox();
            this.RemoveCommentBox = new System.Windows.Forms.CheckBox();
            this.RemoveWhiteSpaceskBox = new System.Windows.Forms.CheckBox();
            this.GetPropsOpt = new System.Windows.Forms.GroupBox();
            this.BypassValidationsBtn = new System.Windows.Forms.CheckBox();
            this.AddSuffixTip = new System.Windows.Forms.ToolTip(this.components);
            this.CSharpConverterTip = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBox.SuspendLayout();
            this.CompareGroupBox.SuspendLayout();
            this.GenerateJsonOptions.SuspendLayout();
            this.SqlOptions.SuspendLayout();
            this.GenerateIGOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageResult)).BeginInit();
            this.ClassConversionOpt.SuspendLayout();
            this.ConverterOptions.SuspendLayout();
            this.SqlCoverterOptions.SuspendLayout();
            this.GetPropsOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter your";
            // 
            // UserInput
            // 
            this.UserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserInput.Location = new System.Drawing.Point(0, 0);
            this.UserInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UserInput.MaxLength = 0;
            this.UserInput.Multiline = true;
            this.UserInput.Name = "UserInput";
            this.UserInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.UserInput.Size = new System.Drawing.Size(418, 452);
            this.UserInput.TabIndex = 3;
            this.UserInput.TextChanged += new System.EventHandler(this.UserInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(97, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(146, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "here:";
            // 
            // PathLabel
            // 
            this.PathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathLabel.AutoSize = true;
            this.PathLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PathLabel.Location = new System.Drawing.Point(26, 499);
            this.PathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(180, 20);
            this.PathLabel.TabIndex = 14;
            this.PathLabel.Text = "or Enter file/contarct path";
            // 
            // PathInput
            // 
            this.PathInput.AllowDrop = true;
            this.PathInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathInput.Location = new System.Drawing.Point(26, 522);
            this.PathInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PathInput.Name = "PathInput";
            this.PathInput.Size = new System.Drawing.Size(756, 23);
            this.PathInput.TabIndex = 15;
            // 
            // GroupBox
            // 
            this.GroupBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.GroupBox.Controls.Add(this.GetPackageReferencesBtn);
            this.GroupBox.Controls.Add(this.CheckRulesRadBtn);
            this.GroupBox.Controls.Add(this.label7);
            this.GroupBox.Controls.Add(this.GenerateIGRadBtn);
            this.GroupBox.Controls.Add(this.CompareTRadBtn);
            this.GroupBox.Controls.Add(this.GetAllSerivcesBtn);
            this.GroupBox.Controls.Add(this.ConverterBtn);
            this.GroupBox.Controls.Add(this.GetAllMethodsBtn);
            this.GroupBox.Controls.Add(this.GetDbLengthBtn);
            this.GroupBox.Controls.Add(this.ReviewReportRadBtn);
            this.GroupBox.Controls.Add(this.GetPropsInfoBtn);
            this.GroupBox.Controls.Add(this.GenerateNdbJsonRadBtn);
            this.GroupBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupBox.Location = new System.Drawing.Point(886, 33);
            this.GroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox.Size = new System.Drawing.Size(255, 256);
            this.GroupBox.TabIndex = 16;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Select a Service:";
            // 
            // GetPackageReferencesBtn
            // 
            this.GetPackageReferencesBtn.AutoSize = true;
            this.GetPackageReferencesBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPackageReferencesBtn.Location = new System.Drawing.Point(9, 187);
            this.GetPackageReferencesBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetPackageReferencesBtn.Name = "GetPackageReferencesBtn";
            this.GetPackageReferencesBtn.Size = new System.Drawing.Size(178, 17);
            this.GetPackageReferencesBtn.TabIndex = 38;
            this.GetPackageReferencesBtn.Text = "Get PackageReferences issues";
            this.GetPackageReferencesBtn.UseVisualStyleBackColor = true;
            this.GetPackageReferencesBtn.CheckedChanged += new System.EventHandler(this.GetPackageReferencesBtn_CheckedChanged);
            // 
            // CheckRulesRadBtn
            // 
            this.CheckRulesRadBtn.AutoSize = true;
            this.CheckRulesRadBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckRulesRadBtn.Location = new System.Drawing.Point(9, 95);
            this.CheckRulesRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckRulesRadBtn.Name = "CheckRulesRadBtn";
            this.CheckRulesRadBtn.Size = new System.Drawing.Size(155, 17);
            this.CheckRulesRadBtn.TabIndex = 13;
            this.CheckRulesRadBtn.Text = "Check Spelling and Rules";
            this.CheckRulesRadBtn.UseVisualStyleBackColor = true;
            this.CheckRulesRadBtn.CheckedChanged += new System.EventHandler(this.CheckRulesRadBtn_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.DarkCyan;
            this.label7.Location = new System.Drawing.Point(136, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "New";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Visible = false;
            // 
            // GenerateIGRadBtn
            // 
            this.GenerateIGRadBtn.AutoSize = true;
            this.GenerateIGRadBtn.Checked = true;
            this.GenerateIGRadBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateIGRadBtn.Location = new System.Drawing.Point(8, 26);
            this.GenerateIGRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateIGRadBtn.Name = "GenerateIGRadBtn";
            this.GenerateIGRadBtn.Size = new System.Drawing.Size(151, 17);
            this.GenerateIGRadBtn.TabIndex = 14;
            this.GenerateIGRadBtn.TabStop = true;
            this.GenerateIGRadBtn.Text = "Generate IG as Word file";
            this.GenerateIGRadBtn.UseVisualStyleBackColor = true;
            this.GenerateIGRadBtn.CheckedChanged += new System.EventHandler(this.GenerateIGRadBtn_CheckedChanged);
            // 
            // CompareTRadBtn
            // 
            this.CompareTRadBtn.AutoSize = true;
            this.CompareTRadBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompareTRadBtn.Location = new System.Drawing.Point(9, 233);
            this.CompareTRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CompareTRadBtn.Name = "CompareTRadBtn";
            this.CompareTRadBtn.Size = new System.Drawing.Size(71, 17);
            this.CompareTRadBtn.TabIndex = 12;
            this.CompareTRadBtn.Text = "Compare";
            this.CompareTRadBtn.UseVisualStyleBackColor = true;
            this.CompareTRadBtn.CheckedChanged += new System.EventHandler(this.CompareTRadBtn_CheckedChanged);
            this.CompareTRadBtn.Click += new System.EventHandler(this.CompareTRadBtn_Click);
            // 
            // GetAllSerivcesBtn
            // 
            this.GetAllSerivcesBtn.AutoSize = true;
            this.GetAllSerivcesBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetAllSerivcesBtn.Location = new System.Drawing.Point(9, 164);
            this.GetAllSerivcesBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetAllSerivcesBtn.Name = "GetAllSerivcesBtn";
            this.GetAllSerivcesBtn.Size = new System.Drawing.Size(195, 17);
            this.GetAllSerivcesBtn.TabIndex = 11;
            this.GetAllSerivcesBtn.Text = "Get all services for contract or sln";
            this.GetAllSerivcesBtn.UseVisualStyleBackColor = true;
            this.GetAllSerivcesBtn.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ConverterBtn
            // 
            this.ConverterBtn.AutoSize = true;
            this.ConverterBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConverterBtn.Location = new System.Drawing.Point(9, 210);
            this.ConverterBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConverterBtn.Name = "ConverterBtn";
            this.ConverterBtn.Size = new System.Drawing.Size(75, 17);
            this.ConverterBtn.TabIndex = 9;
            this.ConverterBtn.Text = "Converter";
            this.ConverterBtn.UseVisualStyleBackColor = true;
            this.ConverterBtn.CheckedChanged += new System.EventHandler(this.ConverterBtnlBtn_CheckedChanged);
            // 
            // GetAllMethodsBtn
            // 
            this.GetAllMethodsBtn.AutoSize = true;
            this.GetAllMethodsBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetAllMethodsBtn.Location = new System.Drawing.Point(9, 141);
            this.GetAllMethodsBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetAllMethodsBtn.Name = "GetAllMethodsBtn";
            this.GetAllMethodsBtn.Size = new System.Drawing.Size(156, 17);
            this.GetAllMethodsBtn.TabIndex = 8;
            this.GetAllMethodsBtn.Text = "Get all methods of a class";
            this.GetAllMethodsBtn.UseVisualStyleBackColor = true;
            this.GetAllMethodsBtn.CheckedChanged += new System.EventHandler(this.GetAllMethodsBtn_CheckedChanged);
            // 
            // GetDbLengthBtn
            // 
            this.GetDbLengthBtn.AutoSize = true;
            this.GetDbLengthBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetDbLengthBtn.Location = new System.Drawing.Point(9, 118);
            this.GetDbLengthBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetDbLengthBtn.Name = "GetDbLengthBtn";
            this.GetDbLengthBtn.Size = new System.Drawing.Size(144, 17);
            this.GetDbLengthBtn.TabIndex = 5;
            this.GetDbLengthBtn.Text = "Get Db length from Sql";
            this.GetDbLengthBtn.UseVisualStyleBackColor = true;
            this.GetDbLengthBtn.CheckedChanged += new System.EventHandler(this.GetDbLengthBtn_CheckedChanged);
            // 
            // ReviewReportRadBtn
            // 
            this.ReviewReportRadBtn.AutoSize = true;
            this.ReviewReportRadBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReviewReportRadBtn.Location = new System.Drawing.Point(9, 95);
            this.ReviewReportRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ReviewReportRadBtn.Name = "ReviewReportRadBtn";
            this.ReviewReportRadBtn.Size = new System.Drawing.Size(131, 17);
            this.ReviewReportRadBtn.TabIndex = 15;
            this.ReviewReportRadBtn.Text = "Review Report (Beta)";
            this.ReviewReportRadBtn.UseVisualStyleBackColor = true;
            this.ReviewReportRadBtn.Visible = false;
            this.ReviewReportRadBtn.CheckedChanged += new System.EventHandler(this.ReviewReportRadBtn_CheckedChanged);
            // 
            // GetPropsInfoBtn
            // 
            this.GetPropsInfoBtn.AutoSize = true;
            this.GetPropsInfoBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPropsInfoBtn.Location = new System.Drawing.Point(8, 72);
            this.GetPropsInfoBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetPropsInfoBtn.Name = "GetPropsInfoBtn";
            this.GetPropsInfoBtn.Size = new System.Drawing.Size(132, 17);
            this.GetPropsInfoBtn.TabIndex = 3;
            this.GetPropsInfoBtn.Text = "Get props info for IG";
            this.GetPropsInfoBtn.UseVisualStyleBackColor = true;
            this.GetPropsInfoBtn.CheckedChanged += new System.EventHandler(this.GetPropsInfoBtn_CheckedChanged);
            // 
            // GenerateNdbJsonRadBtn
            // 
            this.GenerateNdbJsonRadBtn.AutoSize = true;
            this.GenerateNdbJsonRadBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateNdbJsonRadBtn.Location = new System.Drawing.Point(8, 49);
            this.GenerateNdbJsonRadBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateNdbJsonRadBtn.Name = "GenerateNdbJsonRadBtn";
            this.GenerateNdbJsonRadBtn.Size = new System.Drawing.Size(123, 17);
            this.GenerateNdbJsonRadBtn.TabIndex = 39;
            this.GenerateNdbJsonRadBtn.Text = "Generate Ndb Json";
            this.GenerateNdbJsonRadBtn.UseVisualStyleBackColor = true;
            this.GenerateNdbJsonRadBtn.CheckedChanged += new System.EventHandler(this.GenerateNdbJsonRadBtn_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(2, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "               ";
            // 
            // GetOutput
            // 
            this.GetOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GetOutput.BackColor = System.Drawing.Color.DarkCyan;
            this.GetOutput.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.GetOutput.BorderColor = System.Drawing.Color.Empty;
            this.GetOutput.BorderRadius = 30;
            this.GetOutput.BorderSize = 1;
            this.GetOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetOutput.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetOutput.ForeColor = System.Drawing.Color.White;
            this.GetOutput.Location = new System.Drawing.Point(886, 505);
            this.GetOutput.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.GetOutput.Name = "GetOutput";
            this.GetOutput.Size = new System.Drawing.Size(253, 57);
            this.GetOutput.TabIndex = 4;
            this.GetOutput.Text = "Get the Result";
            this.GetOutput.TextColor = System.Drawing.Color.White;
            this.GetOutput.UseVisualStyleBackColor = false;
            this.GetOutput.Click += new System.EventHandler(this.GetOutput_Click);
            // 
            // CompareGroupBox
            // 
            this.CompareGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CompareGroupBox.Controls.Add(this.IsText);
            this.CompareGroupBox.Controls.Add(this.IsSql);
            this.CompareGroupBox.Controls.Add(this.IsModel);
            this.CompareGroupBox.Controls.Add(this.IsJson);
            this.CompareGroupBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompareGroupBox.Location = new System.Drawing.Point(886, 295);
            this.CompareGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CompareGroupBox.Name = "CompareGroupBox";
            this.CompareGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CompareGroupBox.Size = new System.Drawing.Size(255, 51);
            this.CompareGroupBox.TabIndex = 19;
            this.CompareGroupBox.TabStop = false;
            this.CompareGroupBox.Text = "Compare between two";
            this.CompareGroupBox.Visible = false;
            // 
            // IsText
            // 
            this.IsText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.IsText.AutoSize = true;
            this.IsText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IsText.Location = new System.Drawing.Point(198, 24);
            this.IsText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IsText.Name = "IsText";
            this.IsText.Size = new System.Drawing.Size(44, 17);
            this.IsText.TabIndex = 7;
            this.IsText.Text = "Text";
            this.IsText.UseVisualStyleBackColor = true;
            this.IsText.CheckedChanged += new System.EventHandler(this.IsText_CheckedChanged);
            // 
            // IsSql
            // 
            this.IsSql.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.IsSql.AutoSize = true;
            this.IsSql.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IsSql.Location = new System.Drawing.Point(143, 24);
            this.IsSql.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IsSql.Name = "IsSql";
            this.IsSql.Size = new System.Drawing.Size(44, 17);
            this.IsSql.TabIndex = 6;
            this.IsSql.Text = "SQL";
            this.IsSql.UseVisualStyleBackColor = true;
            this.IsSql.CheckedChanged += new System.EventHandler(this.IsSql_CheckedChanged);
            // 
            // IsModel
            // 
            this.IsModel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.IsModel.AutoSize = true;
            this.IsModel.Checked = true;
            this.IsModel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IsModel.Location = new System.Drawing.Point(7, 24);
            this.IsModel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IsModel.Name = "IsModel";
            this.IsModel.Size = new System.Drawing.Size(58, 17);
            this.IsModel.TabIndex = 3;
            this.IsModel.TabStop = true;
            this.IsModel.Text = "Model";
            this.IsModel.UseVisualStyleBackColor = true;
            this.IsModel.CheckedChanged += new System.EventHandler(this.IsModel_CheckedChanged);
            // 
            // IsJson
            // 
            this.IsJson.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.IsJson.AutoSize = true;
            this.IsJson.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IsJson.Location = new System.Drawing.Point(78, 24);
            this.IsJson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IsJson.Name = "IsJson";
            this.IsJson.Size = new System.Drawing.Size(52, 17);
            this.IsJson.TabIndex = 5;
            this.IsJson.Text = "JSON";
            this.IsJson.UseVisualStyleBackColor = true;
            this.IsJson.CheckedChanged += new System.EventHandler(this.IsJson_CheckedChanged);
            // 
            // GenerateJsonOptions
            // 
            this.GenerateJsonOptions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.GenerateJsonOptions.Controls.Add(this.OpenJsonBox);
            this.GenerateJsonOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateJsonOptions.Location = new System.Drawing.Point(886, 295);
            this.GenerateJsonOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateJsonOptions.Name = "GenerateJsonOptions";
            this.GenerateJsonOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateJsonOptions.Size = new System.Drawing.Size(255, 51);
            this.GenerateJsonOptions.TabIndex = 20;
            this.GenerateJsonOptions.TabStop = false;
            this.GenerateJsonOptions.Text = "Options";
            this.GenerateJsonOptions.Visible = false;
            // 
            // OpenJsonBox
            // 
            this.OpenJsonBox.AutoSize = true;
            this.OpenJsonBox.Location = new System.Drawing.Point(9, 22);
            this.OpenJsonBox.Name = "OpenJsonBox";
            this.OpenJsonBox.Size = new System.Drawing.Size(203, 21);
            this.OpenJsonBox.TabIndex = 8;
            this.OpenJsonBox.Text = "Open json file after generated";
            this.OpenJsonBox.UseVisualStyleBackColor = true;
            this.OpenJsonBox.CheckedChanged += new System.EventHandler(this.OpenJsonBox_CheckedChanged);
            // 
            // SqlOptions
            // 
            this.SqlOptions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SqlOptions.Controls.Add(this.RemoveAsBox);
            this.SqlOptions.Controls.Add(this.ToLowerCase);
            this.SqlOptions.Controls.Add(this.OnlyKeys);
            this.SqlOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SqlOptions.Location = new System.Drawing.Point(886, 350);
            this.SqlOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SqlOptions.Name = "SqlOptions";
            this.SqlOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SqlOptions.Size = new System.Drawing.Size(255, 96);
            this.SqlOptions.TabIndex = 20;
            this.SqlOptions.TabStop = false;
            this.SqlOptions.Text = "Options";
            this.SqlOptions.Visible = false;
            // 
            // RemoveAsBox
            // 
            this.RemoveAsBox.AutoSize = true;
            this.RemoveAsBox.Checked = true;
            this.RemoveAsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RemoveAsBox.Location = new System.Drawing.Point(9, 70);
            this.RemoveAsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RemoveAsBox.Name = "RemoveAsBox";
            this.RemoveAsBox.Size = new System.Drawing.Size(146, 21);
            this.RemoveAsBox.TabIndex = 2;
            this.RemoveAsBox.Text = "Remove As from Sql";
            this.RemoveAsBox.UseVisualStyleBackColor = true;
            // 
            // ToLowerCase
            // 
            this.ToLowerCase.AutoSize = true;
            this.ToLowerCase.Location = new System.Drawing.Point(9, 46);
            this.ToLowerCase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ToLowerCase.Name = "ToLowerCase";
            this.ToLowerCase.Size = new System.Drawing.Size(94, 21);
            this.ToLowerCase.TabIndex = 1;
            this.ToLowerCase.Text = "Lower Case";
            this.ToLowerCase.UseVisualStyleBackColor = true;
            // 
            // OnlyKeys
            // 
            this.OnlyKeys.AutoSize = true;
            this.OnlyKeys.Location = new System.Drawing.Point(9, 21);
            this.OnlyKeys.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OnlyKeys.Name = "OnlyKeys";
            this.OnlyKeys.Size = new System.Drawing.Size(84, 21);
            this.OnlyKeys.TabIndex = 0;
            this.OnlyKeys.Text = "Only Keys";
            this.OnlyKeys.UseVisualStyleBackColor = true;
            // 
            // Input2
            // 
            this.Input2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input2.Location = new System.Drawing.Point(0, 0);
            this.Input2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Input2.MaxLength = 0;
            this.Input2.Multiline = true;
            this.Input2.Name = "Input2";
            this.Input2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Input2.Size = new System.Drawing.Size(422, 452);
            this.Input2.TabIndex = 21;
            // 
            // Input2Label
            // 
            this.Input2Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Input2Label.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Input2Label.Location = new System.Drawing.Point(449, 15);
            this.Input2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Input2Label.Name = "Input2Label";
            this.Input2Label.Size = new System.Drawing.Size(162, 20);
            this.Input2Label.TabIndex = 22;
            this.Input2Label.Text = "Input 2 (New Service):";
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.BackColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.BrowseBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.BorderRadius = 12;
            this.BrowseBtn.BorderSize = 1;
            this.BrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.Location = new System.Drawing.Point(790, 521);
            this.BrowseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(80, 24);
            this.BrowseBtn.TabIndex = 33;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.BrowseBtn.UseVisualStyleBackColor = false;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // GenerateIGOptions
            // 
            this.GenerateIGOptions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.GenerateIGOptions.Controls.Add(this.label8);
            this.GenerateIGOptions.Controls.Add(this.GenMicroBtn);
            this.GenerateIGOptions.Controls.Add(this.GenApiBtn);
            this.GenerateIGOptions.Controls.Add(this.GenerateIgsOptionRatBrn);
            this.GenerateIGOptions.Controls.Add(this.AddBracketBox);
            this.GenerateIGOptions.Controls.Add(this.label5);
            this.GenerateIGOptions.Controls.Add(this.IGVersionInput);
            this.GenerateIGOptions.Controls.Add(this.EnterServNameLabel);
            this.GenerateIGOptions.Controls.Add(this.WrittenByInput);
            this.GenerateIGOptions.Controls.Add(this.panel1);
            this.GenerateIGOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateIGOptions.Location = new System.Drawing.Point(886, 295);
            this.GenerateIGOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateIGOptions.Name = "GenerateIGOptions";
            this.GenerateIGOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateIGOptions.Size = new System.Drawing.Size(255, 186);
            this.GenerateIGOptions.TabIndex = 34;
            this.GenerateIGOptions.TabStop = false;
            this.GenerateIGOptions.Text = "Options";
            this.GenerateIGOptions.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(94, 15);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Project Type";
            // 
            // GenMicroBtn
            // 
            this.GenMicroBtn.AutoSize = true;
            this.GenMicroBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenMicroBtn.Location = new System.Drawing.Point(158, 28);
            this.GenMicroBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenMicroBtn.Name = "GenMicroBtn";
            this.GenMicroBtn.Size = new System.Drawing.Size(54, 17);
            this.GenMicroBtn.TabIndex = 39;
            this.GenMicroBtn.Text = "Micro";
            this.GenMicroBtn.UseVisualStyleBackColor = true;
            this.GenMicroBtn.CheckedChanged += new System.EventHandler(this.GenMicroBtn_CheckedChanged);
            // 
            // GenApiBtn
            // 
            this.GenApiBtn.AutoSize = true;
            this.GenApiBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenApiBtn.Location = new System.Drawing.Point(42, 28);
            this.GenApiBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenApiBtn.Name = "GenApiBtn";
            this.GenApiBtn.Size = new System.Drawing.Size(41, 17);
            this.GenApiBtn.TabIndex = 37;
            this.GenApiBtn.Text = "API";
            this.GenApiBtn.UseVisualStyleBackColor = true;
            this.GenApiBtn.CheckedChanged += new System.EventHandler(this.GenApiBtn_CheckedChanged);
            // 
            // GenerateIgsOptionRatBrn
            // 
            this.GenerateIgsOptionRatBrn.AutoSize = true;
            this.GenerateIgsOptionRatBrn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateIgsOptionRatBrn.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GenerateIgsOptionRatBrn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.GenerateIgsOptionRatBrn.Location = new System.Drawing.Point(7, 161);
            this.GenerateIgsOptionRatBrn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GenerateIgsOptionRatBrn.Name = "GenerateIgsOptionRatBrn";
            this.GenerateIgsOptionRatBrn.Size = new System.Drawing.Size(150, 17);
            this.GenerateIgsOptionRatBrn.TabIndex = 36;
            this.GenerateIgsOptionRatBrn.Text = "Generate IGs for contract";
            this.GenerateIgsOptionRatBrn.UseVisualStyleBackColor = true;
            this.GenerateIgsOptionRatBrn.CheckedChanged += new System.EventHandler(this.GenerateIgsOptionRatBrn_CheckedChanged);
            // 
            // AddBracketBox
            // 
            this.AddBracketBox.AutoSize = true;
            this.AddBracketBox.Checked = true;
            this.AddBracketBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddBracketBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBracketBox.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddBracketBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.AddBracketBox.Location = new System.Drawing.Point(7, 143);
            this.AddBracketBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddBracketBox.Name = "AddBracketBox";
            this.AddBracketBox.Size = new System.Drawing.Size(244, 17);
            this.AddBracketBox.TabIndex = 35;
            this.AddBracketBox.Text = "Add empty () after data type. Ex: Integer(5)";
            this.AddBracketBox.UseVisualStyleBackColor = true;
            this.AddBracketBox.CheckedChanged += new System.EventHandler(this.AddBracketBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(7, 99);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Enter IG version (Document version):";
            // 
            // IGVersionInput
            // 
            this.IGVersionInput.Location = new System.Drawing.Point(7, 114);
            this.IGVersionInput.Margin = new System.Windows.Forms.Padding(10);
            this.IGVersionInput.Name = "IGVersionInput";
            this.IGVersionInput.Size = new System.Drawing.Size(241, 25);
            this.IGVersionInput.TabIndex = 8;
            this.IGVersionInput.Text = "V1";
            this.IGVersionInput.TextChanged += new System.EventHandler(this.IGVersionInput_TextChanged);
            this.IGVersionInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IGVersionInput_KeyPress);
            // 
            // EnterServNameLabel
            // 
            this.EnterServNameLabel.AutoSize = true;
            this.EnterServNameLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnterServNameLabel.Location = new System.Drawing.Point(7, 55);
            this.EnterServNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterServNameLabel.Name = "EnterServNameLabel";
            this.EnterServNameLabel.Size = new System.Drawing.Size(169, 13);
            this.EnterServNameLabel.TabIndex = 7;
            this.EnterServNameLabel.Text = "Enter author name (Written By):";
            // 
            // WrittenByInput
            // 
            this.WrittenByInput.Location = new System.Drawing.Point(7, 70);
            this.WrittenByInput.Margin = new System.Windows.Forms.Padding(10);
            this.WrittenByInput.Name = "WrittenByInput";
            this.WrittenByInput.Size = new System.Drawing.Size(241, 25);
            this.WrittenByInput.TabIndex = 6;
            this.WrittenByInput.TextChanged += new System.EventHandler(this.WrittenByInput_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(7, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 30);
            this.panel1.TabIndex = 40;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplitContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainSplitContainer.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MainSplitContainer.IsSplitterFixed = true;
            this.MainSplitContainer.Location = new System.Drawing.Point(26, 42);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.UserInput);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.ImageResult);
            this.MainSplitContainer.Panel2.Controls.Add(this.Input2);
            this.MainSplitContainer.Size = new System.Drawing.Size(844, 452);
            this.MainSplitContainer.SplitterDistance = 418;
            this.MainSplitContainer.TabIndex = 35;
            // 
            // ImageResult
            // 
            this.ImageResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageResult.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ImageResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageResult.Location = new System.Drawing.Point(0, 0);
            this.ImageResult.Name = "ImageResult";
            this.ImageResult.Size = new System.Drawing.Size(422, 452);
            this.ImageResult.TabIndex = 22;
            this.ImageResult.TabStop = false;
            this.ImageResult.Visible = false;
            // 
            // ClassConversionOpt
            // 
            this.ClassConversionOpt.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ClassConversionOpt.Controls.Add(this.suffixTxtBox);
            this.ClassConversionOpt.Controls.Add(this.AddSuffixLabel);
            this.ClassConversionOpt.Controls.Add(this.PrepareXmlBtn);
            this.ClassConversionOpt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClassConversionOpt.Location = new System.Drawing.Point(886, 436);
            this.ClassConversionOpt.Name = "ClassConversionOpt";
            this.ClassConversionOpt.Size = new System.Drawing.Size(255, 65);
            this.ClassConversionOpt.TabIndex = 37;
            this.ClassConversionOpt.TabStop = false;
            this.ClassConversionOpt.Text = "Class Conversion Options";
            this.ClassConversionOpt.Visible = false;
            // 
            // suffixTxtBox
            // 
            this.suffixTxtBox.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.suffixTxtBox.Location = new System.Drawing.Point(73, 36);
            this.suffixTxtBox.Name = "suffixTxtBox";
            this.suffixTxtBox.Size = new System.Drawing.Size(169, 22);
            this.suffixTxtBox.TabIndex = 11;
            // 
            // AddSuffixLabel
            // 
            this.AddSuffixLabel.AutoSize = true;
            this.AddSuffixLabel.Location = new System.Drawing.Point(9, 39);
            this.AddSuffixLabel.Name = "AddSuffixLabel";
            this.AddSuffixLabel.Size = new System.Drawing.Size(63, 15);
            this.AddSuffixLabel.TabIndex = 10;
            this.AddSuffixLabel.Text = "Add suffix:";
            // 
            // PrepareXmlBtn
            // 
            this.PrepareXmlBtn.AutoSize = true;
            this.PrepareXmlBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PrepareXmlBtn.Location = new System.Drawing.Point(9, 19);
            this.PrepareXmlBtn.Name = "PrepareXmlBtn";
            this.PrepareXmlBtn.Size = new System.Drawing.Size(172, 17);
            this.PrepareXmlBtn.TabIndex = 9;
            this.PrepareXmlBtn.Text = "Prepare XML Documentation";
            this.PrepareXmlBtn.UseVisualStyleBackColor = true;
            this.PrepareXmlBtn.CheckedChanged += new System.EventHandler(this.PrepareXmlBtn_CheckedChanged);
            // 
            // ConverterOptions
            // 
            this.ConverterOptions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConverterOptions.Controls.Add(this.VbToCharpBtn);
            this.ConverterOptions.Controls.Add(this.Blob2ImageBtn);
            this.ConverterOptions.Controls.Add(this.Base2ImageBtn);
            this.ConverterOptions.Controls.Add(this.DecryptTokenBtn);
            this.ConverterOptions.Controls.Add(this.JsonToCsharpBtn);
            this.ConverterOptions.Controls.Add(this.JsonToXmlBtn);
            this.ConverterOptions.Controls.Add(this.XmlToJsonBtn);
            this.ConverterOptions.Controls.Add(this.CodeToSqlBtn);
            this.ConverterOptions.Controls.Add(this.SqlToCodeBtn);
            this.ConverterOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConverterOptions.Location = new System.Drawing.Point(886, 295);
            this.ConverterOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConverterOptions.Name = "ConverterOptions";
            this.ConverterOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConverterOptions.Size = new System.Drawing.Size(255, 137);
            this.ConverterOptions.TabIndex = 21;
            this.ConverterOptions.TabStop = false;
            this.ConverterOptions.Text = "Options";
            this.ConverterOptions.Visible = false;
            // 
            // VbToCharpBtn
            // 
            this.VbToCharpBtn.AutoSize = true;
            this.VbToCharpBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.VbToCharpBtn.Location = new System.Drawing.Point(9, 114);
            this.VbToCharpBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VbToCharpBtn.Name = "VbToCharpBtn";
            this.VbToCharpBtn.Size = new System.Drawing.Size(82, 17);
            this.VbToCharpBtn.TabIndex = 46;
            this.VbToCharpBtn.Text = "To C# Class";
            this.VbToCharpBtn.UseVisualStyleBackColor = true;
            this.VbToCharpBtn.CheckedChanged += new System.EventHandler(this.VbToCharpBtn_CheckedChanged);
            // 
            // Blob2ImageBtn
            // 
            this.Blob2ImageBtn.AutoSize = true;
            this.Blob2ImageBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Blob2ImageBtn.Location = new System.Drawing.Point(120, 68);
            this.Blob2ImageBtn.Margin = new System.Windows.Forms.Padding(2);
            this.Blob2ImageBtn.Name = "Blob2ImageBtn";
            this.Blob2ImageBtn.Size = new System.Drawing.Size(97, 17);
            this.Blob2ImageBtn.TabIndex = 45;
            this.Blob2ImageBtn.Text = "Blob to Image";
            this.Blob2ImageBtn.UseVisualStyleBackColor = true;
            this.Blob2ImageBtn.CheckedChanged += new System.EventHandler(this.Blob2ImageBtn_CheckedChanged);
            // 
            // Base2ImageBtn
            // 
            this.Base2ImageBtn.AutoSize = true;
            this.Base2ImageBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Base2ImageBtn.Location = new System.Drawing.Point(120, 46);
            this.Base2ImageBtn.Margin = new System.Windows.Forms.Padding(2);
            this.Base2ImageBtn.Name = "Base2ImageBtn";
            this.Base2ImageBtn.Size = new System.Drawing.Size(109, 17);
            this.Base2ImageBtn.TabIndex = 44;
            this.Base2ImageBtn.Text = "Base64 to Image";
            this.Base2ImageBtn.UseVisualStyleBackColor = true;
            this.Base2ImageBtn.CheckedChanged += new System.EventHandler(this.Base2ImageBtn_CheckedChanged);
            // 
            // DecryptTokenBtn
            // 
            this.DecryptTokenBtn.AutoSize = true;
            this.DecryptTokenBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DecryptTokenBtn.Location = new System.Drawing.Point(120, 24);
            this.DecryptTokenBtn.Margin = new System.Windows.Forms.Padding(2);
            this.DecryptTokenBtn.Name = "DecryptTokenBtn";
            this.DecryptTokenBtn.Size = new System.Drawing.Size(117, 17);
            this.DecryptTokenBtn.TabIndex = 43;
            this.DecryptTokenBtn.Text = "Decrypt Jwt Token";
            this.DecryptTokenBtn.UseVisualStyleBackColor = true;
            this.DecryptTokenBtn.CheckedChanged += new System.EventHandler(this.DecryptTokenBtn_CheckedChanged);
            // 
            // JsonToCsharpBtn
            // 
            this.JsonToCsharpBtn.AutoSize = true;
            this.JsonToCsharpBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JsonToCsharpBtn.Location = new System.Drawing.Point(120, 90);
            this.JsonToCsharpBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.JsonToCsharpBtn.Name = "JsonToCsharpBtn";
            this.JsonToCsharpBtn.Size = new System.Drawing.Size(108, 17);
            this.JsonToCsharpBtn.TabIndex = 41;
            this.JsonToCsharpBtn.Text = "Json to C# Class";
            this.JsonToCsharpBtn.UseVisualStyleBackColor = true;
            this.JsonToCsharpBtn.CheckedChanged += new System.EventHandler(this.JsonToCsharpBtn_CheckedChanged);
            // 
            // JsonToXmlBtn
            // 
            this.JsonToXmlBtn.AutoSize = true;
            this.JsonToXmlBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JsonToXmlBtn.Location = new System.Drawing.Point(9, 91);
            this.JsonToXmlBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.JsonToXmlBtn.Name = "JsonToXmlBtn";
            this.JsonToXmlBtn.Size = new System.Drawing.Size(83, 17);
            this.JsonToXmlBtn.TabIndex = 40;
            this.JsonToXmlBtn.Text = "Json to Xml";
            this.JsonToXmlBtn.UseVisualStyleBackColor = true;
            this.JsonToXmlBtn.CheckedChanged += new System.EventHandler(this.JsonToXmlBtn_CheckedChanged);
            // 
            // XmlToJsonBtn
            // 
            this.XmlToJsonBtn.AutoSize = true;
            this.XmlToJsonBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.XmlToJsonBtn.Location = new System.Drawing.Point(9, 68);
            this.XmlToJsonBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.XmlToJsonBtn.Name = "XmlToJsonBtn";
            this.XmlToJsonBtn.Size = new System.Drawing.Size(83, 17);
            this.XmlToJsonBtn.TabIndex = 39;
            this.XmlToJsonBtn.Text = "Xml to Json";
            this.XmlToJsonBtn.UseVisualStyleBackColor = true;
            this.XmlToJsonBtn.CheckedChanged += new System.EventHandler(this.XmlToJsonBtn_CheckedChanged);
            // 
            // CodeToSqlBtn
            // 
            this.CodeToSqlBtn.AutoSize = true;
            this.CodeToSqlBtn.Checked = true;
            this.CodeToSqlBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CodeToSqlBtn.Location = new System.Drawing.Point(9, 24);
            this.CodeToSqlBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CodeToSqlBtn.Name = "CodeToSqlBtn";
            this.CodeToSqlBtn.Size = new System.Drawing.Size(88, 17);
            this.CodeToSqlBtn.TabIndex = 38;
            this.CodeToSqlBtn.TabStop = true;
            this.CodeToSqlBtn.Text = "Code to SQL";
            this.CodeToSqlBtn.UseVisualStyleBackColor = true;
            this.CodeToSqlBtn.CheckedChanged += new System.EventHandler(this.CodeToSqlBtn_CheckedChanged);
            // 
            // SqlToCodeBtn
            // 
            this.SqlToCodeBtn.AutoSize = true;
            this.SqlToCodeBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SqlToCodeBtn.Location = new System.Drawing.Point(9, 46);
            this.SqlToCodeBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SqlToCodeBtn.Name = "SqlToCodeBtn";
            this.SqlToCodeBtn.Size = new System.Drawing.Size(88, 17);
            this.SqlToCodeBtn.TabIndex = 37;
            this.SqlToCodeBtn.Text = "SQL to Code";
            this.SqlToCodeBtn.UseVisualStyleBackColor = true;
            this.SqlToCodeBtn.CheckedChanged += new System.EventHandler(this.SqlToCodeBtn_CheckedChanged_1);
            // 
            // SqlCoverterOptions
            // 
            this.SqlCoverterOptions.Controls.Add(this.RemoveCommentBox);
            this.SqlCoverterOptions.Controls.Add(this.RemoveWhiteSpaceskBox);
            this.SqlCoverterOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SqlCoverterOptions.Location = new System.Drawing.Point(886, 436);
            this.SqlCoverterOptions.Name = "SqlCoverterOptions";
            this.SqlCoverterOptions.Size = new System.Drawing.Size(255, 61);
            this.SqlCoverterOptions.TabIndex = 36;
            this.SqlCoverterOptions.TabStop = false;
            this.SqlCoverterOptions.Text = "SQL Convert Options";
            this.SqlCoverterOptions.Visible = false;
            // 
            // RemoveCommentBox
            // 
            this.RemoveCommentBox.AutoSize = true;
            this.RemoveCommentBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemoveCommentBox.Location = new System.Drawing.Point(9, 38);
            this.RemoveCommentBox.Name = "RemoveCommentBox";
            this.RemoveCommentBox.Size = new System.Drawing.Size(121, 17);
            this.RemoveCommentBox.TabIndex = 10;
            this.RemoveCommentBox.Text = "Remove comments";
            this.RemoveCommentBox.UseVisualStyleBackColor = true;
            // 
            // RemoveWhiteSpaceskBox
            // 
            this.RemoveWhiteSpaceskBox.AutoSize = true;
            this.RemoveWhiteSpaceskBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemoveWhiteSpaceskBox.Location = new System.Drawing.Point(9, 20);
            this.RemoveWhiteSpaceskBox.Name = "RemoveWhiteSpaceskBox";
            this.RemoveWhiteSpaceskBox.Size = new System.Drawing.Size(135, 17);
            this.RemoveWhiteSpaceskBox.TabIndex = 9;
            this.RemoveWhiteSpaceskBox.Text = "Remove white spaces";
            this.RemoveWhiteSpaceskBox.UseVisualStyleBackColor = true;
            // 
            // GetPropsOpt
            // 
            this.GetPropsOpt.Controls.Add(this.BypassValidationsBtn);
            this.GetPropsOpt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPropsOpt.Location = new System.Drawing.Point(886, 296);
            this.GetPropsOpt.Name = "GetPropsOpt";
            this.GetPropsOpt.Size = new System.Drawing.Size(255, 44);
            this.GetPropsOpt.TabIndex = 38;
            this.GetPropsOpt.TabStop = false;
            this.GetPropsOpt.Text = "Get Props Options";
            this.GetPropsOpt.Visible = false;
            // 
            // BypassValidationsBtn
            // 
            this.BypassValidationsBtn.AutoSize = true;
            this.BypassValidationsBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BypassValidationsBtn.Location = new System.Drawing.Point(9, 20);
            this.BypassValidationsBtn.Name = "BypassValidationsBtn";
            this.BypassValidationsBtn.Size = new System.Drawing.Size(193, 17);
            this.BypassValidationsBtn.TabIndex = 9;
            this.BypassValidationsBtn.Text = "Bypass C# Standards Validations";
            this.BypassValidationsBtn.UseVisualStyleBackColor = true;
            // 
            // ReviewmeHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.GenerateIGOptions);
            this.Controls.Add(this.ClassConversionOpt);
            this.Controls.Add(this.SqlCoverterOptions);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.Input2Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.PathInput);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.GetOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SqlOptions);
            this.Controls.Add(this.ConverterOptions);
            this.Controls.Add(this.GetPropsOpt);
            this.Controls.Add(this.GenerateJsonOptions);
            this.Controls.Add(this.CompareGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(864, 570);
            this.Name = "ReviewmeHome";
            this.Size = new System.Drawing.Size(1156, 574);
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.CompareGroupBox.ResumeLayout(false);
            this.CompareGroupBox.PerformLayout();
            this.GenerateJsonOptions.ResumeLayout(false);
            this.GenerateJsonOptions.PerformLayout();
            this.SqlOptions.ResumeLayout(false);
            this.SqlOptions.PerformLayout();
            this.GenerateIGOptions.ResumeLayout(false);
            this.GenerateIGOptions.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel1.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageResult)).EndInit();
            this.ClassConversionOpt.ResumeLayout(false);
            this.ClassConversionOpt.PerformLayout();
            this.ConverterOptions.ResumeLayout(false);
            this.ConverterOptions.PerformLayout();
            this.SqlCoverterOptions.ResumeLayout(false);
            this.SqlCoverterOptions.PerformLayout();
            this.GetPropsOpt.ResumeLayout(false);
            this.GetPropsOpt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.TextBox PathInput;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.RadioButton GetPropsInfoBtn;
        private System.Windows.Forms.RadioButton GetDbLengthBtn;
        private System.Windows.Forms.RadioButton GetAllMethodsBtn;
        private System.Windows.Forms.RadioButton ConverterBtn;
        private System.Windows.Forms.Label label4;
        private DevButton GetOutput;
        private System.Windows.Forms.RadioButton GetAllSerivcesBtn;
        private System.Windows.Forms.RadioButton CompareTRadBtn;
        private System.Windows.Forms.GroupBox CompareGroupBox;
        private System.Windows.Forms.RadioButton IsText;
        private System.Windows.Forms.RadioButton IsSql;
        private System.Windows.Forms.RadioButton IsModel;
        private System.Windows.Forms.RadioButton IsJson;
        private System.Windows.Forms.GroupBox SqlOptions;
        private System.Windows.Forms.CheckBox RemoveAsBox;
        private System.Windows.Forms.CheckBox ToLowerCase;
        private System.Windows.Forms.CheckBox OnlyKeys;
        private System.Windows.Forms.TextBox Input2;
        private System.Windows.Forms.Label Input2Label;
        private System.Windows.Forms.RadioButton CheckRulesRadBtn;
        private System.Windows.Forms.RadioButton ReviewReportRadBtn;
        private DevButton BrowseBtn;
        private RadioButton GenerateIGRadBtn;
        private GroupBox GenerateIGOptions;
        private Label EnterServNameLabel;
        private TextBox WrittenByInput;
        private Label label5;
        private TextBox IGVersionInput;
        private CheckBox AddBracketBox;
        private CheckBox GenerateIgsOptionRatBrn;
        private SplitContainer MainSplitContainer;
        private Label label7;
        private GroupBox ConverterOptions;
        private RadioButton CodeToSqlBtn;
        private RadioButton GetPackageReferencesBtn;
        private RadioButton DecryptTokenBtn;
        private PictureBox ImageResult;
        private RadioButton GenerateNdbJsonRadBtn;
        private RadioButton Blob2ImageBtn;
        private RadioButton Base2ImageBtn;
        private RadioButton JsonToCsharpBtn;
        private RadioButton JsonToXmlBtn;
        private RadioButton XmlToJsonBtn;
        private RadioButton SqlToCodeBtn;
        private GroupBox GenerateJsonOptions;
        private CheckBox OpenJsonBox;
        private GroupBox SqlCoverterOptions;
        private CheckBox RemoveWhiteSpaceskBox;
        private CheckBox RemoveCommentBox;
        private RadioButton GenApiBtn;
        private Label label8;
        private RadioButton GenMicroBtn;
        private Panel panel1;
        private RadioButton VbToCharpBtn;
        private GroupBox ClassConversionOpt;
        private GroupBox GetPropsOpt;
        private CheckBox BypassValidationsBtn;
        private CheckBox PrepareXmlBtn;
        private Label AddSuffixLabel;
        private ToolTip AddSuffixTip;
        private TextBox suffixTxtBox;
        private ToolTip CSharpConverterTip;
    }
}

