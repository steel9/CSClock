/*
CSClock - a program which keeps track of your computer time
Copyright (C) 2017  Viktor J

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace CSClock
{
    partial class Statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.l_totalHrsSpent = new System.Windows.Forms.Label();
            this.l_avgHrsSpent = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.l_startDate = new System.Windows.Forms.Label();
            this.l_totalOvertimeHrsSpent = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.l_avgOvertimeHrsSpent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 80);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total hours spent:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(270, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 373);
            this.label2.TabIndex = 8;
            // 
            // l_totalHrsSpent
            // 
            this.l_totalHrsSpent.AutoSize = true;
            this.l_totalHrsSpent.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_totalHrsSpent.Location = new System.Drawing.Point(14, 93);
            this.l_totalHrsSpent.Name = "l_totalHrsSpent";
            this.l_totalHrsSpent.Size = new System.Drawing.Size(62, 21);
            this.l_totalHrsSpent.TabIndex = 9;
            this.l_totalHrsSpent.Text = "No info";
            // 
            // l_avgHrsSpent
            // 
            this.l_avgHrsSpent.AutoSize = true;
            this.l_avgHrsSpent.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_avgHrsSpent.Location = new System.Drawing.Point(277, 93);
            this.l_avgHrsSpent.Name = "l_avgHrsSpent";
            this.l_avgHrsSpent.Size = new System.Drawing.Size(62, 21);
            this.l_avgHrsSpent.TabIndex = 11;
            this.l_avgHrsSpent.Text = "No info";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(277, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 80);
            this.label5.TabIndex = 10;
            this.label5.Text = "Average hours spent / day:";
            // 
            // l_startDate
            // 
            this.l_startDate.AutoSize = true;
            this.l_startDate.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_startDate.Location = new System.Drawing.Point(11, 397);
            this.l_startDate.Name = "l_startDate";
            this.l_startDate.Size = new System.Drawing.Size(106, 21);
            this.l_startDate.TabIndex = 12;
            this.l_startDate.Text = "Since: No info";
            // 
            // l_totalOvertimeHrsSpent
            // 
            this.l_totalOvertimeHrsSpent.AutoSize = true;
            this.l_totalOvertimeHrsSpent.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_totalOvertimeHrsSpent.Location = new System.Drawing.Point(14, 272);
            this.l_totalOvertimeHrsSpent.Name = "l_totalOvertimeHrsSpent";
            this.l_totalOvertimeHrsSpent.Size = new System.Drawing.Size(62, 21);
            this.l_totalOvertimeHrsSpent.TabIndex = 14;
            this.l_totalOvertimeHrsSpent.Text = "No info";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 80);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total overtime hours spent:";
            // 
            // l_avgOvertimeHrsSpent
            // 
            this.l_avgOvertimeHrsSpent.AutoSize = true;
            this.l_avgOvertimeHrsSpent.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_avgOvertimeHrsSpent.Location = new System.Drawing.Point(277, 272);
            this.l_avgOvertimeHrsSpent.Name = "l_avgOvertimeHrsSpent";
            this.l_avgOvertimeHrsSpent.Size = new System.Drawing.Size(62, 21);
            this.l_avgOvertimeHrsSpent.TabIndex = 16;
            this.l_avgOvertimeHrsSpent.Text = "No info";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(276, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(268, 80);
            this.label8.TabIndex = 15;
            this.label8.Text = "Average overtime hours spent / day:";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(12, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(533, 1);
            this.label6.TabIndex = 18;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 431);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.l_avgOvertimeHrsSpent);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.l_totalOvertimeHrsSpent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.l_startDate);
            this.Controls.Add(this.l_avgHrsSpent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.l_totalHrsSpent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Statistics";
            this.Text = "Statistics - CSClock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Statistics_FormClosing);
            this.Load += new System.EventHandler(this.Statistics_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label l_totalHrsSpent;
        private System.Windows.Forms.Label l_avgHrsSpent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label l_startDate;
        private System.Windows.Forms.Label l_totalOvertimeHrsSpent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label l_avgOvertimeHrsSpent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
    }
}