

namespace Dev.Assistant.Dashboard.LogEvents
{
    partial class LogEventsHome
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
            this.AllEventBox = new System.Windows.Forms.TextBox();
            this.LogsTypeBox = new System.Windows.Forms.ComboBox();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.EventsBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EventStatusBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.OrderByBox = new System.Windows.Forms.ComboBox();
            this.DataBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ExcludeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AllEventBox
            // 
            this.AllEventBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllEventBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AllEventBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AllEventBox.Location = new System.Drawing.Point(19, 110);
            this.AllEventBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AllEventBox.Multiline = true;
            this.AllEventBox.Name = "AllEventBox";
            this.AllEventBox.ReadOnly = true;
            this.AllEventBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllEventBox.Size = new System.Drawing.Size(1113, 684);
            this.AllEventBox.TabIndex = 41;
            // 
            // LogsTypeBox
            // 
            this.LogsTypeBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LogsTypeBox.FormattingEnabled = true;
            this.LogsTypeBox.Location = new System.Drawing.Point(952, 24);
            this.LogsTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LogsTypeBox.Name = "LogsTypeBox";
            this.LogsTypeBox.Size = new System.Drawing.Size(180, 22);
            this.LogsTypeBox.TabIndex = 46;
            this.LogsTypeBox.SelectedIndexChanged += new System.EventHandler(this.LogsTypeBox_SelectedIndexChanged);
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(90, 44);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(200, 23);
            this.DatePicker.TabIndex = 47;
            this.DatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 48;
            this.label1.Text = "Date:";
            // 
            // EventsBox
            // 
            this.EventsBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EventsBox.FormattingEnabled = true;
            this.EventsBox.Location = new System.Drawing.Point(406, 24);
            this.EventsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EventsBox.Name = "EventsBox";
            this.EventsBox.Size = new System.Drawing.Size(201, 22);
            this.EventsBox.TabIndex = 52;
            this.EventsBox.SelectedIndexChanged += new System.EventHandler(this.EventsBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(318, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 51;
            this.label3.Text = "Event:";
            // 
            // EventStatusBox
            // 
            this.EventStatusBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EventStatusBox.FormattingEnabled = true;
            this.EventStatusBox.Location = new System.Drawing.Point(406, 67);
            this.EventStatusBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EventStatusBox.Name = "EventStatusBox";
            this.EventStatusBox.Size = new System.Drawing.Size(201, 22);
            this.EventStatusBox.TabIndex = 54;
            this.EventStatusBox.SelectedIndexChanged += new System.EventHandler(this.EventStatusBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(318, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 53;
            this.label6.Text = "Event Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(664, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 17);
            this.label4.TabIndex = 55;
            this.label4.Text = "Data:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(907, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 56;
            this.label5.Text = "Type:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(639, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 58;
            this.label7.Text = "Order By:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // OrderByBox
            // 
            this.OrderByBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.OrderByBox.FormattingEnabled = true;
            this.OrderByBox.Location = new System.Drawing.Point(709, 24);
            this.OrderByBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OrderByBox.Name = "OrderByBox";
            this.OrderByBox.Size = new System.Drawing.Size(155, 22);
            this.OrderByBox.TabIndex = 57;
            this.OrderByBox.SelectedIndexChanged += new System.EventHandler(this.OrderByBox_SelectedIndexChanged);
            // 
            // DataBox
            // 
            this.DataBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DataBox.FormattingEnabled = true;
            this.DataBox.Location = new System.Drawing.Point(709, 67);
            this.DataBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DataBox.Name = "DataBox";
            this.DataBox.Size = new System.Drawing.Size(155, 22);
            this.DataBox.TabIndex = 59;
            this.DataBox.SelectedIndexChanged += new System.EventHandler(this.DataBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(895, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 15);
            this.label8.TabIndex = 61;
            this.label8.Text = "Exclude:";
            // 
            // ExcludeText
            // 
            this.ExcludeText.Location = new System.Drawing.Point(952, 67);
            this.ExcludeText.Name = "ExcludeText";
            this.ExcludeText.Size = new System.Drawing.Size(180, 23);
            this.ExcludeText.TabIndex = 60;
            this.ExcludeText.TextChanged += new System.EventHandler(this.ExcludeText_TextChanged);
            // 
            // LogEventsHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ExcludeText);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.OrderByBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EventStatusBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EventsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.LogsTypeBox);
            this.Controls.Add(this.AllEventBox);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1154, 570);
            this.Name = "LogEventsHome";
            this.Size = new System.Drawing.Size(1154, 818);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox AllEventBox;
        private ComboBox LogsTypeBox;
        private DateTimePicker DatePicker;
        private Label label1;
        private ComboBox EventsBox;
        private Label label3;
        private ComboBox EventStatusBox;
        private Label label6;
        private Label label4;
        private Label label5;
        private Label label7;
        private ComboBox OrderByBox;
        private ComboBox DataBox;
        private Label label8;
        private TextBox ExcludeText;
    }
}

