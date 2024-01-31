using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Reviewme
{
    partial class IGPropsResult
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
            this.NumProps = new System.Windows.Forms.TextBox();
            this.NumClasses = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GetOutput = new Dev.Assistant.Common.Controllers.DevButton();
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CopyReqWord = new Dev.Assistant.Common.Controllers.DevButton();
            this.PasteLabel = new System.Windows.Forms.Label();
            this.CopyExcel = new Dev.Assistant.Common.Controllers.DevButton();
            this.label4 = new System.Windows.Forms.Label();
            this.CopyResWord = new Dev.Assistant.Common.Controllers.DevButton();
            this.label5 = new System.Windows.Forms.Label();
            this.AddBracketBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // NumProps
            // 
            this.NumProps.BackColor = System.Drawing.SystemColors.Control;
            this.NumProps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumProps.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumProps.Location = new System.Drawing.Point(175, 155);
            this.NumProps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumProps.Name = "NumProps";
            this.NumProps.ReadOnly = true;
            this.NumProps.Size = new System.Drawing.Size(100, 19);
            this.NumProps.TabIndex = 24;
            // 
            // NumClasses
            // 
            this.NumClasses.BackColor = System.Drawing.SystemColors.Menu;
            this.NumClasses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumClasses.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumClasses.Location = new System.Drawing.Point(175, 87);
            this.NumClasses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumClasses.Name = "NumClasses";
            this.NumClasses.ReadOnly = true;
            this.NumClasses.Size = new System.Drawing.Size(100, 19);
            this.NumClasses.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(54, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "# of Classes:";
            // 
            // GetOutput
            // 
            this.GetOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GetOutput.BackColor = System.Drawing.SystemColors.Control;
            this.GetOutput.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GetOutput.BorderColor = System.Drawing.Color.Red;
            this.GetOutput.BorderRadius = 32;
            this.GetOutput.BorderSize = 1;
            this.GetOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetOutput.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GetOutput.ForeColor = System.Drawing.Color.Red;
            this.GetOutput.Location = new System.Drawing.Point(14, 458);
            this.GetOutput.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.GetOutput.Name = "GetOutput";
            this.GetOutput.Size = new System.Drawing.Size(260, 62);
            this.GetOutput.TabIndex = 18;
            this.GetOutput.Text = "Close";
            this.GetOutput.TextColor = System.Drawing.Color.Red;
            this.GetOutput.UseVisualStyleBackColor = false;
            this.GetOutput.Click += new System.EventHandler(this.GetOutput_Click);
            // 
            // ResultGridView
            // 
            this.ResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ResultGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ResultGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGridView.Location = new System.Drawing.Point(294, 48);
            this.ResultGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ResultGridView.MultiSelect = false;
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.ReadOnly = true;
            this.ResultGridView.Size = new System.Drawing.Size(783, 725);
            this.ResultGridView.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(55, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Your output:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(54, 157);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "# of Properties:";
            // 
            // CopyReqWord
            // 
            this.CopyReqWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyReqWord.BackColor = System.Drawing.SystemColors.Highlight;
            this.CopyReqWord.BackgroundColor = System.Drawing.SystemColors.Highlight;
            this.CopyReqWord.BorderColor = System.Drawing.Color.Empty;
            this.CopyReqWord.BorderRadius = 28;
            this.CopyReqWord.BorderSize = 0;
            this.CopyReqWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyReqWord.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CopyReqWord.ForeColor = System.Drawing.Color.White;
            this.CopyReqWord.Location = new System.Drawing.Point(15, 370);
            this.CopyReqWord.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.CopyReqWord.Name = "CopyReqWord";
            this.CopyReqWord.Size = new System.Drawing.Size(126, 55);
            this.CopyReqWord.TabIndex = 27;
            this.CopyReqWord.Text = "Copy Request";
            this.CopyReqWord.TextColor = System.Drawing.Color.White;
            this.CopyReqWord.UseVisualStyleBackColor = false;
            this.CopyReqWord.Click += new System.EventHandler(this.CopyWord_Click);
            // 
            // PasteLabel
            // 
            this.PasteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PasteLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PasteLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.PasteLabel.Location = new System.Drawing.Point(14, 576);
            this.PasteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasteLabel.Name = "PasteLabel";
            this.PasteLabel.Size = new System.Drawing.Size(261, 108);
            this.PasteLabel.TabIndex = 26;
            this.PasteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CopyExcel
            // 
            this.CopyExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyExcel.BackColor = System.Drawing.SystemColors.Control;
            this.CopyExcel.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CopyExcel.BorderColor = System.Drawing.Color.Green;
            this.CopyExcel.BorderRadius = 30;
            this.CopyExcel.BorderSize = 1;
            this.CopyExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyExcel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CopyExcel.ForeColor = System.Drawing.Color.Green;
            this.CopyExcel.Location = new System.Drawing.Point(15, 267);
            this.CopyExcel.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.CopyExcel.Name = "CopyExcel";
            this.CopyExcel.Size = new System.Drawing.Size(260, 62);
            this.CopyExcel.TabIndex = 27;
            this.CopyExcel.Text = "Copy with Formatting - Excel";
            this.CopyExcel.TextColor = System.Drawing.Color.Green;
            this.CopyExcel.UseVisualStyleBackColor = false;
            this.CopyExcel.Click += new System.EventHandler(this.CopyExcel_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(14, 344);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "Copy for Word:";
            // 
            // CopyResWord
            // 
            this.CopyResWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyResWord.BackColor = System.Drawing.SystemColors.Highlight;
            this.CopyResWord.BackgroundColor = System.Drawing.SystemColors.Highlight;
            this.CopyResWord.BorderColor = System.Drawing.Color.Empty;
            this.CopyResWord.BorderRadius = 28;
            this.CopyResWord.BorderSize = 0;
            this.CopyResWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyResWord.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CopyResWord.ForeColor = System.Drawing.Color.White;
            this.CopyResWord.Location = new System.Drawing.Point(144, 370);
            this.CopyResWord.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.CopyResWord.Name = "CopyResWord";
            this.CopyResWord.Size = new System.Drawing.Size(131, 55);
            this.CopyResWord.TabIndex = 27;
            this.CopyResWord.Text = "Copy Response";
            this.CopyResWord.TextColor = System.Drawing.Color.White;
            this.CopyResWord.UseVisualStyleBackColor = false;
            this.CopyResWord.Click += new System.EventHandler(this.CopyResWord_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(122, 346);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 32;
            this.label5.Text = "(Recommended)";
            // 
            // AddBracketBox
            // 
            this.AddBracketBox.AutoSize = true;
            this.AddBracketBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBracketBox.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddBracketBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.AddBracketBox.Location = new System.Drawing.Point(23, 434);
            this.AddBracketBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddBracketBox.Name = "AddBracketBox";
            this.AddBracketBox.Size = new System.Drawing.Size(249, 17);
            this.AddBracketBox.TabIndex = 34;
            this.AddBracketBox.Text = "Add empty () after data type. Ex: Double(10)";
            this.AddBracketBox.UseVisualStyleBackColor = true;
            // 
            // IGPropsResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 787);
            this.Controls.Add(this.AddBracketBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CopyResWord);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CopyExcel);
            this.Controls.Add(this.PasteLabel);
            this.Controls.Add(this.CopyReqWord);
            this.Controls.Add(this.NumProps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NumClasses);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetOutput);
            this.Controls.Add(this.ResultGridView);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1151, 826);
            this.Name = "IGPropsResult";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox NumProps;
        private System.Windows.Forms.TextBox NumClasses;
        private System.Windows.Forms.Label label1;
        private DevButton GetOutput;
        private System.Windows.Forms.DataGridView ResultGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevButton CopyReqWord;
        private System.Windows.Forms.Label PasteLabel;
        private DevButton CopyExcel;
        private System.Windows.Forms.Label label4;
        private DevButton CopyResWord;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox AddBracketBox;
    }
}