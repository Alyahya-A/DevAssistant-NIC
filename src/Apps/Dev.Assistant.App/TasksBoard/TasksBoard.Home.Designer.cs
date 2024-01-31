
using Dev.Assistant.Common.Controllers;

namespace Dev.Assistant.App.TasksBoard
{
    partial class TasksBoardHome
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
            this.NumServicesMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UpdateBtn = new Dev.Assistant.Common.Controllers.DevButton();
            this.devButton1 = new Dev.Assistant.Common.Controllers.DevButton();
            this.devButton2 = new Dev.Assistant.Common.Controllers.DevButton();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
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
            // UpdateBtn
            // 
            this.UpdateBtn.BackColor = System.Drawing.Color.DarkCyan;
            this.UpdateBtn.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.UpdateBtn.BorderColor = System.Drawing.Color.Empty;
            this.UpdateBtn.BorderRadius = 31;
            this.UpdateBtn.BorderSize = 0;
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Location = new System.Drawing.Point(451, 252);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(252, 64);
            this.UpdateBtn.TabIndex = 25;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.TextColor = System.Drawing.Color.White;
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // devButton1
            // 
            this.devButton1.BackColor = System.Drawing.Color.DarkCyan;
            this.devButton1.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.devButton1.BorderColor = System.Drawing.Color.Empty;
            this.devButton1.BorderRadius = 31;
            this.devButton1.BorderSize = 0;
            this.devButton1.FlatAppearance.BorderSize = 0;
            this.devButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.devButton1.ForeColor = System.Drawing.Color.White;
            this.devButton1.Location = new System.Drawing.Point(440, 117);
            this.devButton1.Name = "devButton1";
            this.devButton1.Size = new System.Drawing.Size(252, 64);
            this.devButton1.TabIndex = 26;
            this.devButton1.Text = "Update";
            this.devButton1.TextColor = System.Drawing.Color.White;
            this.devButton1.UseVisualStyleBackColor = false;
            this.devButton1.Click += new System.EventHandler(this.devButton1_Click);
            // 
            // devButton2
            // 
            this.devButton2.BackColor = System.Drawing.Color.DarkCyan;
            this.devButton2.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.devButton2.BorderColor = System.Drawing.Color.Empty;
            this.devButton2.BorderRadius = 31;
            this.devButton2.BorderSize = 0;
            this.devButton2.FlatAppearance.BorderSize = 0;
            this.devButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.devButton2.ForeColor = System.Drawing.Color.White;
            this.devButton2.Location = new System.Drawing.Point(71, 252);
            this.devButton2.Name = "devButton2";
            this.devButton2.Size = new System.Drawing.Size(252, 64);
            this.devButton2.TabIndex = 25;
            this.devButton2.Text = "Get main Contracts";
            this.devButton2.TextColor = System.Drawing.Color.White;
            this.devButton2.UseVisualStyleBackColor = false;
            this.devButton2.Click += new System.EventHandler(this.devButton2_Click);
            // 
            // TasksBoardHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.devButton1);
            this.Controls.Add(this.devButton2);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NumServicesMsg);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1154, 568);
            this.Name = "TasksBoardHome";
            this.Size = new System.Drawing.Size(1154, 568);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevButton AddBtn;
        private System.Windows.Forms.TextBox MSNames;
        private DevButton ClearBtn;
        private System.Windows.Forms.Label NumServicesMsg;
        private DevButton DeleteBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox ExcludActiveRadBtn;
        private System.Windows.Forms.CheckBox checkBox2;
        private DevButton UpdateBtn;
        private DevButton devButton1;
        private DevButton devButton2;
        private PageSetupDialog pageSetupDialog1;
        private ColorDialog colorDialog1;
    }
}

