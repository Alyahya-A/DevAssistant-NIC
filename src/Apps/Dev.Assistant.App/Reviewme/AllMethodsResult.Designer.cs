using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Reviewme
{
    partial class ResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultForm));
            this.label2 = new System.Windows.Forms.Label();
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NameSpace = new System.Windows.Forms.TextBox();
            this.FuncNum = new System.Windows.Forms.TextBox();
            this.ServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FileType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GetOutput = new Dev.Assistant.Common.Controllers.DevButton();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(57, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Your output:";
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
            this.ResultGridView.Location = new System.Drawing.Point(531, 48);
            this.ResultGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.Size = new System.Drawing.Size(548, 631);
            this.ResultGridView.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(56, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name Space:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(56, 223);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "# of Funcations:";
            // 
            // NameSpace
            // 
            this.NameSpace.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameSpace.Location = new System.Drawing.Point(177, 87);
            this.NameSpace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NameSpace.Name = "NameSpace";
            this.NameSpace.Size = new System.Drawing.Size(325, 26);
            this.NameSpace.TabIndex = 10;
            // 
            // FuncNum
            // 
            this.FuncNum.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FuncNum.Location = new System.Drawing.Point(177, 220);
            this.FuncNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FuncNum.Name = "FuncNum";
            this.FuncNum.Size = new System.Drawing.Size(325, 26);
            this.FuncNum.TabIndex = 11;
            // 
            // ServiceName
            // 
            this.ServiceName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ServiceName.Location = new System.Drawing.Point(177, 155);
            this.ServiceName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.Size = new System.Drawing.Size(325, 26);
            this.ServiceName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(56, 157);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Service Name:";
            // 
            // FileType
            // 
            this.FileType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileType.Location = new System.Drawing.Point(177, 280);
            this.FileType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FileType.Name = "FileType";
            this.FileType.Size = new System.Drawing.Size(325, 26);
            this.FileType.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(56, 283);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "File Type";
            // 
            // GetOutput
            // 
            this.GetOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GetOutput.BackColor = System.Drawing.SystemColors.Control;
            this.GetOutput.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GetOutput.BorderColor = System.Drawing.Color.Red;
            this.GetOutput.BorderRadius = 23;
            this.GetOutput.BorderSize = 1;
            this.GetOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetOutput.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GetOutput.ForeColor = System.Drawing.Color.Red;
            this.GetOutput.Location = new System.Drawing.Point(430, 712);
            this.GetOutput.Margin = new System.Windows.Forms.Padding(117, 3, 4, 3);
            this.GetOutput.Name = "GetOutput";
            this.GetOutput.Size = new System.Drawing.Size(276, 45);
            this.GetOutput.TabIndex = 6;
            this.GetOutput.Text = "Close";
            this.GetOutput.TextColor = System.Drawing.Color.Red;
            this.GetOutput.UseVisualStyleBackColor = false;
            this.GetOutput.Click += new System.EventHandler(this.GetOutput_Click);
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 787);
            this.Controls.Add(this.FileType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ServiceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FuncNum);
            this.Controls.Add(this.NameSpace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetOutput);
            this.Controls.Add(this.ResultGridView);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1151, 826);
            this.Name = "ResultForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Output";
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView ResultGridView;
        private DevButton GetOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NameSpace;
        private System.Windows.Forms.TextBox FuncNum;
        private System.Windows.Forms.TextBox ServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FileType;
        private System.Windows.Forms.Label label5;
    }
}