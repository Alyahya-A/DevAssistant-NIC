using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.Reviewme
{
    partial class Result
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
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.CloseForm = new Dev.Assistant.Common.Controllers.DevButton();
            this.CheckSpllingBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NewRBtn = new System.Windows.Forms.RadioButton();
            this.CurrentRBtn = new System.Windows.Forms.RadioButton();
            this.NotFoundInNew = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NotFoundInCurrent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultGridView
            // 
            this.ResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGridView.Location = new System.Drawing.Point(342, 14);
            this.ResultGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.Size = new System.Drawing.Size(638, 824);
            this.ResultGridView.TabIndex = 0;
            // 
            // CloseForm
            // 
            this.CloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseForm.BackColor = System.Drawing.SystemColors.Control;
            this.CloseForm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CloseForm.BorderColor = System.Drawing.Color.Red;
            this.CloseForm.BorderRadius = 32;
            this.CloseForm.BorderSize = 1;
            this.CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseForm.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseForm.ForeColor = System.Drawing.Color.Red;
            this.CloseForm.Location = new System.Drawing.Point(50, 777);
            this.CloseForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(241, 61);
            this.CloseForm.TabIndex = 3;
            this.CloseForm.Text = "Close";
            this.CloseForm.TextColor = System.Drawing.Color.Red;
            this.CloseForm.UseVisualStyleBackColor = false;
            this.CloseForm.Click += new System.EventHandler(this.Close_Click);
            // 
            // CheckSpllingBtn
            // 
            this.CheckSpllingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckSpllingBtn.BackColor = System.Drawing.SystemColors.Control;
            this.CheckSpllingBtn.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CheckSpllingBtn.BorderColor = System.Drawing.Color.DarkCyan;
            this.CheckSpllingBtn.BorderRadius = 32;
            this.CheckSpllingBtn.BorderSize = 1;
            this.CheckSpllingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckSpllingBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckSpllingBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.CheckSpllingBtn.Location = new System.Drawing.Point(50, 708);
            this.CheckSpllingBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CheckSpllingBtn.Name = "CheckSpllingBtn";
            this.CheckSpllingBtn.Size = new System.Drawing.Size(241, 61);
            this.CheckSpllingBtn.TabIndex = 4;
            this.CheckSpllingBtn.Text = "Check Splling";
            this.CheckSpllingBtn.TextColor = System.Drawing.Color.DarkCyan;
            this.CheckSpllingBtn.UseVisualStyleBackColor = false;
            this.CheckSpllingBtn.Click += new System.EventHandler(this.CheckSpllingBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.NewRBtn);
            this.groupBox1.Controls.Add(this.CurrentRBtn);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(50, 614);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(241, 88);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Which Model?";
            // 
            // NewRBtn
            // 
            this.NewRBtn.AutoSize = true;
            this.NewRBtn.Checked = true;
            this.NewRBtn.Location = new System.Drawing.Point(148, 43);
            this.NewRBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NewRBtn.Name = "NewRBtn";
            this.NewRBtn.Size = new System.Drawing.Size(49, 19);
            this.NewRBtn.TabIndex = 1;
            this.NewRBtn.TabStop = true;
            this.NewRBtn.Text = "New";
            this.NewRBtn.UseVisualStyleBackColor = true;
            // 
            // CurrentRBtn
            // 
            this.CurrentRBtn.AutoSize = true;
            this.CurrentRBtn.Location = new System.Drawing.Point(28, 43);
            this.CurrentRBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CurrentRBtn.Name = "CurrentRBtn";
            this.CurrentRBtn.Size = new System.Drawing.Size(65, 19);
            this.CurrentRBtn.TabIndex = 0;
            this.CurrentRBtn.Text = "Current";
            this.CurrentRBtn.UseVisualStyleBackColor = true;
            // 
            // NotFoundInNew
            // 
            this.NotFoundInNew.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NotFoundInNew.Location = new System.Drawing.Point(18, 58);
            this.NotFoundInNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NotFoundInNew.Multiline = true;
            this.NotFoundInNew.Name = "NotFoundInNew";
            this.NotFoundInNew.ReadOnly = true;
            this.NotFoundInNew.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NotFoundInNew.Size = new System.Drawing.Size(317, 240);
            this.NotFoundInNew.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Fields from Input1 are not found in Input2:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 315);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fields from Input2 are not found in Input1:";
            // 
            // NotFoundInCurrent
            // 
            this.NotFoundInCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NotFoundInCurrent.Location = new System.Drawing.Point(18, 339);
            this.NotFoundInCurrent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NotFoundInCurrent.Multiline = true;
            this.NotFoundInCurrent.Name = "NotFoundInCurrent";
            this.NotFoundInCurrent.ReadOnly = true;
            this.NotFoundInCurrent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NotFoundInCurrent.Size = new System.Drawing.Size(317, 243);
            this.NotFoundInCurrent.TabIndex = 8;
            // 
            // Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 852);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NotFoundInCurrent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NotFoundInNew);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CheckSpllingBtn);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.ResultGridView);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1010, 891);
            this.Name = "Result";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultGridView;
        private DevButton CloseForm;
        private DevButton CheckSpllingBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton NewRBtn;
        private System.Windows.Forms.RadioButton CurrentRBtn;
        private System.Windows.Forms.TextBox NotFoundInNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NotFoundInCurrent;
    }
}