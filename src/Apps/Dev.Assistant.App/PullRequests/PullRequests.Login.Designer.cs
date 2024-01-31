using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.PrReview
{
    partial class PrReviewLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrReviewLogin));
            this.EnterCriteriaLabel = new System.Windows.Forms.Label();
            this.GoBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.PatInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnterCriteriaLabel
            // 
            this.EnterCriteriaLabel.AutoSize = true;
            this.EnterCriteriaLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.EnterCriteriaLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EnterCriteriaLabel.Location = new System.Drawing.Point(0, 0);
            this.EnterCriteriaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterCriteriaLabel.Name = "EnterCriteriaLabel";
            this.EnterCriteriaLabel.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.EnterCriteriaLabel.Size = new System.Drawing.Size(222, 35);
            this.EnterCriteriaLabel.TabIndex = 5;
            this.EnterCriteriaLabel.Text = "Enter your Personal Access Tokens (PAT):";
            this.EnterCriteriaLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // GoBtn
            // 
            this.GoBtn.AutoSize = true;
            this.GoBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.GoBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.GoBtn.BorderColor = System.Drawing.Color.Empty;
            this.GoBtn.BorderRadius = 27;
            this.GoBtn.BorderSize = 0;
            this.GoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GoBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.GoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GoBtn.ForeColor = System.Drawing.Color.White;
            this.GoBtn.Location = new System.Drawing.Point(80, 30);
            this.GoBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(373, 53);
            this.GoBtn.TabIndex = 2;
            this.GoBtn.Text = "Save and Go";
            this.GoBtn.TextColor = System.Drawing.Color.White;
            this.GoBtn.UseVisualStyleBackColor = false;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // PatInput
            // 
            this.PatInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PatInput.Location = new System.Drawing.Point(0, 42);
            this.PatInput.Margin = new System.Windows.Forms.Padding(10);
            this.PatInput.Name = "PatInput";
            this.PatInput.Size = new System.Drawing.Size(533, 23);
            this.PatInput.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(533, 112);
            this.label3.TabIndex = 35;
            this.label3.Text = "Login\r\nby personal token";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semilight", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(0, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.label5.Size = new System.Drawing.Size(533, 145);
            this.label5.TabIndex = 34;
            this.label5.Text = resources.GetString("label5.Text");
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(8, 8);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.splitContainer1.Size = new System.Drawing.Size(1136, 560);
            this.splitContainer1.SplitterDistance = 552;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(25, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(502, 206);
            this.label1.TabIndex = 37;
            this.label1.Text = "PR Review";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(25, 206);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.label4.Size = new System.Drawing.Size(502, 354);
            this.label4.TabIndex = 36;
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(25, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(533, 560);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 37;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.EnterCriteriaLabel);
            this.splitContainer3.Panel1.Controls.Add(this.PatInput);
            this.splitContainer3.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label6);
            this.splitContainer3.Panel2.Controls.Add(this.GoBtn);
            this.splitContainer3.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer3.Panel2.Padding = new System.Windows.Forms.Padding(80, 30, 80, 10);
            this.splitContainer3.Size = new System.Drawing.Size(533, 280);
            this.splitContainer3.SplitterDistance = 65;
            this.splitContainer3.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Location = new System.Drawing.Point(80, 151);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.label6.Size = new System.Drawing.Size(373, 50);
            this.label6.TabIndex = 36;
            this.label6.Text = " ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrReviewLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PrReviewLogin";
            this.Size = new System.Drawing.Size(1154, 577);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Label EnterCriteriaLabel;
        private DevButton GoBtn;
        private TextBox PatInput;
        private Label label3;
        private Label label5;
        private SplitContainer splitContainer1;
        private Label label1;
        private Label label4;
        private Label label6;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
    }
}

