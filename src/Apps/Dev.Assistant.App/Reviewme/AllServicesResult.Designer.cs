using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Reviewme
{
    partial class AllServicesResult
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
            this.NumSerivces = new System.Windows.Forms.TextBox();
            this.GetOutput = new Dev.Assistant.Common.Controllers.DevButton();
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PasteLabel = new System.Windows.Forms.Label();
            this.CopyExcel = new Dev.Assistant.Common.Controllers.DevButton();
            this.Notes = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // NumSerivces
            // 
            this.NumSerivces.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumSerivces.Location = new System.Drawing.Point(163, 72);
            this.NumSerivces.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NumSerivces.Name = "NumSerivces";
            this.NumSerivces.Size = new System.Drawing.Size(100, 26);
            this.NumSerivces.TabIndex = 24;
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
            this.GetOutput.Location = new System.Drawing.Point(15, 462);
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
            this.label3.Location = new System.Drawing.Point(42, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "# of Services:";
            // 
            // PasteLabel
            // 
            this.PasteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PasteLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PasteLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.PasteLabel.Location = new System.Drawing.Point(14, 527);
            this.PasteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasteLabel.Name = "PasteLabel";
            this.PasteLabel.Size = new System.Drawing.Size(261, 135);
            this.PasteLabel.TabIndex = 26;
            this.PasteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CopyExcel
            // 
            this.CopyExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyExcel.BackColor = System.Drawing.SystemColors.Control;
            this.CopyExcel.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CopyExcel.BorderColor = System.Drawing.Color.Green;
            this.CopyExcel.BorderRadius = 32;
            this.CopyExcel.BorderSize = 1;
            this.CopyExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyExcel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CopyExcel.ForeColor = System.Drawing.Color.Green;
            this.CopyExcel.Location = new System.Drawing.Point(15, 374);
            this.CopyExcel.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.CopyExcel.Name = "CopyExcel";
            this.CopyExcel.Size = new System.Drawing.Size(260, 62);
            this.CopyExcel.TabIndex = 27;
            this.CopyExcel.Text = "Copy to Excel";
            this.CopyExcel.TextColor = System.Drawing.Color.Green;
            this.CopyExcel.UseVisualStyleBackColor = false;
            this.CopyExcel.Click += new System.EventHandler(this.CopyExcel_Click);
            // 
            // Notes
            // 
            this.Notes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Notes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Notes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Notes.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Notes.Location = new System.Drawing.Point(15, 666);
            this.Notes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            this.Notes.Size = new System.Drawing.Size(260, 107);
            this.Notes.TabIndex = 31;
            this.Notes.Text = "";
            // 
            // AllServicesResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 787);
            this.Controls.Add(this.Notes);
            this.Controls.Add(this.CopyExcel);
            this.Controls.Add(this.PasteLabel);
            this.Controls.Add(this.NumSerivces);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GetOutput);
            this.Controls.Add(this.ResultGridView);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1151, 826);
            this.Name = "AllServicesResult";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox NumSerivces;
        private DevButton GetOutput;
        private System.Windows.Forms.DataGridView ResultGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PasteLabel;
        private DevButton CopyExcel;
        private System.Windows.Forms.RichTextBox Notes;
    }
}