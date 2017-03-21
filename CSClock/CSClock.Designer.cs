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
    partial class CSClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSClock));
            this.label_configRequired = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_home = new System.Windows.Forms.Panel();
            this.l_exclM = new System.Windows.Forms.Label();
            this.label_timeRemaining = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_timeElapsed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_fwrdRwndTime = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label_version = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_about = new System.Windows.Forms.Button();
            this.restartEveryDayTimer = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button_quit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel_home.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_configRequired
            // 
            this.label_configRequired.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_configRequired.Location = new System.Drawing.Point(12, 9);
            this.label_configRequired.Name = "label_configRequired";
            this.label_configRequired.Size = new System.Drawing.Size(380, 62);
            this.label_configRequired.TabIndex = 0;
            this.label_configRequired.Text = "Configuration is required";
            this.label_configRequired.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(12, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button1, "Configure");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel_home
            // 
            this.panel_home.Controls.Add(this.l_exclM);
            this.panel_home.Controls.Add(this.label_timeRemaining);
            this.panel_home.Controls.Add(this.label3);
            this.panel_home.Controls.Add(this.label_timeElapsed);
            this.panel_home.Controls.Add(this.label1);
            this.panel_home.Location = new System.Drawing.Point(12, 9);
            this.panel_home.Name = "panel_home";
            this.panel_home.Size = new System.Drawing.Size(380, 62);
            this.panel_home.TabIndex = 3;
            this.panel_home.Visible = false;
            // 
            // l_exclM
            // 
            this.l_exclM.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_exclM.ForeColor = System.Drawing.Color.Red;
            this.l_exclM.Location = new System.Drawing.Point(146, 0);
            this.l_exclM.Name = "l_exclM";
            this.l_exclM.Size = new System.Drawing.Size(63, 59);
            this.l_exclM.TabIndex = 4;
            this.l_exclM.Text = "!";
            this.l_exclM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.l_exclM.Visible = false;
            // 
            // label_timeRemaining
            // 
            this.label_timeRemaining.AutoSize = true;
            this.label_timeRemaining.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_timeRemaining.Location = new System.Drawing.Point(268, 30);
            this.label_timeRemaining.Name = "label_timeRemaining";
            this.label_timeRemaining.Size = new System.Drawing.Size(70, 21);
            this.label_timeRemaining.TabIndex = 3;
            this.label_timeRemaining.Text = "00:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(215, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Time remaining:";
            // 
            // label_timeElapsed
            // 
            this.label_timeElapsed.AutoSize = true;
            this.label_timeElapsed.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_timeElapsed.Location = new System.Drawing.Point(32, 30);
            this.label_timeElapsed.Name = "label_timeElapsed";
            this.label_timeElapsed.Size = new System.Drawing.Size(70, 21);
            this.label_timeElapsed.TabIndex = 1;
            this.label_timeElapsed.Text = "00:00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time elapsed:";
            // 
            // button_fwrdRwndTime
            // 
            this.button_fwrdRwndTime.Image = ((System.Drawing.Image)(resources.GetObject("button_fwrdRwndTime.Image")));
            this.button_fwrdRwndTime.Location = new System.Drawing.Point(58, 74);
            this.button_fwrdRwndTime.Name = "button_fwrdRwndTime";
            this.button_fwrdRwndTime.Size = new System.Drawing.Size(56, 40);
            this.button_fwrdRwndTime.TabIndex = 7;
            this.button_fwrdRwndTime.UseVisualStyleBackColor = true;
            this.button_fwrdRwndTime.Click += new System.EventHandler(this.button_fwrdRwndTime_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(120, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(272, 40);
            this.button3.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button3, "Pause/resume");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label_version.Location = new System.Drawing.Point(14, 128);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(48, 13);
            this.label_version.TabIndex = 4;
            this.label_version.Text = "CSClock";
            // 
            // button_about
            // 
            this.button_about.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_about.Location = new System.Drawing.Point(317, 118);
            this.button_about.Name = "button_about";
            this.button_about.Size = new System.Drawing.Size(75, 23);
            this.button_about.TabIndex = 5;
            this.button_about.Text = "About";
            this.button_about.UseVisualStyleBackColor = true;
            this.button_about.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // restartEveryDayTimer
            // 
            this.restartEveryDayTimer.Enabled = true;
            this.restartEveryDayTimer.Interval = 1000;
            this.restartEveryDayTimer.Tick += new System.EventHandler(this.reloadEveryDayTimer_Tick);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button_quit
            // 
            this.button_quit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_quit.Location = new System.Drawing.Point(249, 118);
            this.button_quit.Name = "button_quit";
            this.button_quit.Size = new System.Drawing.Size(62, 23);
            this.button_quit.TabIndex = 6;
            this.button_quit.Text = "Quit";
            this.button_quit.UseVisualStyleBackColor = true;
            this.button_quit.Click += new System.EventHandler(this.button_quit_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(120, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Statistics";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CSClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 150);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_fwrdRwndTime);
            this.Controls.Add(this.button_quit);
            this.Controls.Add(this.button_about);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.panel_home);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_configRequired);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSClock";
            this.Text = "CSClock";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.CSClock_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.CSClock_Load);
            this.panel_home.ResumeLayout(false);
            this.panel_home.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_configRequired;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Panel panel_home;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label label_timeRemaining;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label_timeElapsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_version;
        public System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_about;
        private System.Windows.Forms.Timer restartEveryDayTimer;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button_quit;
        private System.Windows.Forms.Button button_fwrdRwndTime;
        private System.Windows.Forms.Label l_exclM;
        private System.Windows.Forms.Button button2;
    }
}

