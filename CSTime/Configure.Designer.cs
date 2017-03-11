/*
CSTime - a program which keeps track of your computer time
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


namespace CSTime
{
    partial class Configure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configure));
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_maximumHoursMon = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursTue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursWed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursThu = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursFri = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursSat = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_maximumHoursSun = new System.Windows.Forms.NumericUpDown();
            this.button_apply = new System.Windows.Forms.Button();
            this.checkBox_enableTimer = new System.Windows.Forms.CheckBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.checkBox_pauseResumeTimerOnComputerLockUnlock = new System.Windows.Forms.CheckBox();
            this.checkBox_guiTopMost = new System.Windows.Forms.CheckBox();
            this.checkBox_startCSTimeWithWindows = new System.Windows.Forms.CheckBox();
            this.button_selectLanguage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.l_mon = new System.Windows.Forms.Label();
            this.l_tue = new System.Windows.Forms.Label();
            this.l_wed = new System.Windows.Forms.Label();
            this.l_thu = new System.Windows.Forms.Label();
            this.l_fri = new System.Windows.Forms.Label();
            this.l_sat = new System.Windows.Forms.Label();
            this.l_sun = new System.Windows.Forms.Label();
            this.button_moreless = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursTue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursWed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursFri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursSat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursSun)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maximum hours allowed\r\n(set to 0 to disable limitation)";
            // 
            // numericUpDown_maximumHoursMon
            // 
            this.numericUpDown_maximumHoursMon.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursMon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursMon.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursMon.Location = new System.Drawing.Point(18, 91);
            this.numericUpDown_maximumHoursMon.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursMon.Name = "numericUpDown_maximumHoursMon";
            this.numericUpDown_maximumHoursMon.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursMon.TabIndex = 1;
            // 
            // numericUpDown_maximumHoursTue
            // 
            this.numericUpDown_maximumHoursTue.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursTue.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursTue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursTue.Location = new System.Drawing.Point(72, 91);
            this.numericUpDown_maximumHoursTue.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursTue.Name = "numericUpDown_maximumHoursTue";
            this.numericUpDown_maximumHoursTue.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursTue.TabIndex = 2;
            // 
            // numericUpDown_maximumHoursWed
            // 
            this.numericUpDown_maximumHoursWed.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursWed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursWed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursWed.Location = new System.Drawing.Point(126, 91);
            this.numericUpDown_maximumHoursWed.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursWed.Name = "numericUpDown_maximumHoursWed";
            this.numericUpDown_maximumHoursWed.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursWed.TabIndex = 3;
            // 
            // numericUpDown_maximumHoursThu
            // 
            this.numericUpDown_maximumHoursThu.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursThu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursThu.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursThu.Location = new System.Drawing.Point(180, 91);
            this.numericUpDown_maximumHoursThu.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursThu.Name = "numericUpDown_maximumHoursThu";
            this.numericUpDown_maximumHoursThu.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursThu.TabIndex = 4;
            // 
            // numericUpDown_maximumHoursFri
            // 
            this.numericUpDown_maximumHoursFri.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursFri.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursFri.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursFri.Location = new System.Drawing.Point(234, 91);
            this.numericUpDown_maximumHoursFri.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursFri.Name = "numericUpDown_maximumHoursFri";
            this.numericUpDown_maximumHoursFri.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursFri.TabIndex = 5;
            // 
            // numericUpDown_maximumHoursSat
            // 
            this.numericUpDown_maximumHoursSat.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursSat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursSat.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursSat.Location = new System.Drawing.Point(288, 91);
            this.numericUpDown_maximumHoursSat.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursSat.Name = "numericUpDown_maximumHoursSat";
            this.numericUpDown_maximumHoursSat.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursSat.TabIndex = 6;
            // 
            // numericUpDown_maximumHoursSun
            // 
            this.numericUpDown_maximumHoursSun.DecimalPlaces = 2;
            this.numericUpDown_maximumHoursSun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_maximumHoursSun.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_maximumHoursSun.Location = new System.Drawing.Point(340, 91);
            this.numericUpDown_maximumHoursSun.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown_maximumHoursSun.Name = "numericUpDown_maximumHoursSun";
            this.numericUpDown_maximumHoursSun.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_maximumHoursSun.TabIndex = 7;
            // 
            // button_apply
            // 
            this.button_apply.BackColor = System.Drawing.Color.LightGray;
            this.button_apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_apply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_apply.Location = new System.Drawing.Point(112, 266);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(280, 23);
            this.button_apply.TabIndex = 8;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = false;
            this.button_apply.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_enableTimer
            // 
            this.checkBox_enableTimer.AutoSize = true;
            this.checkBox_enableTimer.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_enableTimer.Location = new System.Drawing.Point(18, 133);
            this.checkBox_enableTimer.Name = "checkBox_enableTimer";
            this.checkBox_enableTimer.Size = new System.Drawing.Size(172, 17);
            this.checkBox_enableTimer.TabIndex = 9;
            this.checkBox_enableTimer.Text = "Enable timer (recommended)";
            this.checkBox_enableTimer.UseVisualStyleBackColor = true;
            // 
            // button_reset
            // 
            this.button_reset.BackColor = System.Drawing.Color.OrangeRed;
            this.button_reset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_reset.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_reset.ForeColor = System.Drawing.Color.White;
            this.button_reset.Location = new System.Drawing.Point(12, 295);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(196, 37);
            this.button_reset.TabIndex = 10;
            this.button_reset.Text = "Reset";
            this.button_reset.UseVisualStyleBackColor = false;
            this.button_reset.Visible = false;
            this.button_reset.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox_pauseResumeTimerOnComputerLockUnlock
            // 
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.AutoSize = true;
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.Location = new System.Drawing.Point(31, 156);
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.Name = "checkBox_pauseResumeTimerOnComputerLockUnlock";
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.Size = new System.Drawing.Size(340, 17);
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.TabIndex = 11;
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.Text = "Pause/resume timer on computer lock/unlock (recommended)";
            this.checkBox_pauseResumeTimerOnComputerLockUnlock.UseVisualStyleBackColor = true;
            // 
            // checkBox_guiTopMost
            // 
            this.checkBox_guiTopMost.AutoSize = true;
            this.checkBox_guiTopMost.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_guiTopMost.Location = new System.Drawing.Point(18, 195);
            this.checkBox_guiTopMost.Name = "checkBox_guiTopMost";
            this.checkBox_guiTopMost.Size = new System.Drawing.Size(298, 17);
            this.checkBox_guiTopMost.TabIndex = 12;
            this.checkBox_guiTopMost.Text = "Set the Topmost property for the GUI window to true";
            this.checkBox_guiTopMost.UseVisualStyleBackColor = true;
            // 
            // checkBox_startCSTimeWithWindows
            // 
            this.checkBox_startCSTimeWithWindows.AutoSize = true;
            this.checkBox_startCSTimeWithWindows.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_startCSTimeWithWindows.Location = new System.Drawing.Point(18, 230);
            this.checkBox_startCSTimeWithWindows.Name = "checkBox_startCSTimeWithWindows";
            this.checkBox_startCSTimeWithWindows.Size = new System.Drawing.Size(249, 17);
            this.checkBox_startCSTimeWithWindows.TabIndex = 13;
            this.checkBox_startCSTimeWithWindows.Text = "Start CSTime with Windows (recommended)";
            this.checkBox_startCSTimeWithWindows.UseVisualStyleBackColor = true;
            // 
            // button_selectLanguage
            // 
            this.button_selectLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_selectLanguage.Image = ((System.Drawing.Image)(resources.GetObject("button_selectLanguage.Image")));
            this.button_selectLanguage.Location = new System.Drawing.Point(352, 217);
            this.button_selectLanguage.Name = "button_selectLanguage";
            this.button_selectLanguage.Size = new System.Drawing.Size(40, 40);
            this.button_selectLanguage.TabIndex = 14;
            this.toolTip1.SetToolTip(this.button_selectLanguage, "Languages");
            this.button_selectLanguage.UseVisualStyleBackColor = true;
            this.button_selectLanguage.Click += new System.EventHandler(this.button_selectLanguage_Click);
            // 
            // l_mon
            // 
            this.l_mon.AutoSize = true;
            this.l_mon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_mon.Location = new System.Drawing.Point(15, 75);
            this.l_mon.Name = "l_mon";
            this.l_mon.Size = new System.Drawing.Size(32, 13);
            this.l_mon.TabIndex = 15;
            this.l_mon.Text = "Mon";
            // 
            // l_tue
            // 
            this.l_tue.AutoSize = true;
            this.l_tue.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_tue.Location = new System.Drawing.Point(69, 75);
            this.l_tue.Name = "l_tue";
            this.l_tue.Size = new System.Drawing.Size(25, 13);
            this.l_tue.TabIndex = 16;
            this.l_tue.Text = "Tue";
            // 
            // l_wed
            // 
            this.l_wed.AutoSize = true;
            this.l_wed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wed.Location = new System.Drawing.Point(123, 75);
            this.l_wed.Name = "l_wed";
            this.l_wed.Size = new System.Drawing.Size(31, 13);
            this.l_wed.TabIndex = 17;
            this.l_wed.Text = "Wed";
            // 
            // l_thu
            // 
            this.l_thu.AutoSize = true;
            this.l_thu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_thu.Location = new System.Drawing.Point(177, 75);
            this.l_thu.Name = "l_thu";
            this.l_thu.Size = new System.Drawing.Size(27, 13);
            this.l_thu.TabIndex = 18;
            this.l_thu.Text = "Thu";
            // 
            // l_fri
            // 
            this.l_fri.AutoSize = true;
            this.l_fri.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_fri.Location = new System.Drawing.Point(231, 75);
            this.l_fri.Name = "l_fri";
            this.l_fri.Size = new System.Drawing.Size(20, 13);
            this.l_fri.TabIndex = 19;
            this.l_fri.Text = "Fri";
            // 
            // l_sat
            // 
            this.l_sat.AutoSize = true;
            this.l_sat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_sat.Location = new System.Drawing.Point(285, 75);
            this.l_sat.Name = "l_sat";
            this.l_sat.Size = new System.Drawing.Size(23, 13);
            this.l_sat.TabIndex = 20;
            this.l_sat.Text = "Sat";
            // 
            // l_sun
            // 
            this.l_sun.AutoSize = true;
            this.l_sun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_sun.Location = new System.Drawing.Point(337, 75);
            this.l_sun.Name = "l_sun";
            this.l_sun.Size = new System.Drawing.Size(27, 13);
            this.l_sun.TabIndex = 21;
            this.l_sun.Text = "Sun";
            // 
            // button_moreless
            // 
            this.button_moreless.BackColor = System.Drawing.Color.DarkGray;
            this.button_moreless.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_moreless.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_moreless.ForeColor = System.Drawing.Color.White;
            this.button_moreless.Location = new System.Drawing.Point(12, 266);
            this.button_moreless.Name = "button_moreless";
            this.button_moreless.Size = new System.Drawing.Size(94, 23);
            this.button_moreless.TabIndex = 22;
            this.button_moreless.Text = "More ↓";
            this.button_moreless.UseVisualStyleBackColor = false;
            this.button_moreless.Click += new System.EventHandler(this.button_moreless_Click);
            // 
            // button_remove
            // 
            this.button_remove.BackColor = System.Drawing.Color.Red;
            this.button_remove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_remove.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_remove.ForeColor = System.Drawing.Color.White;
            this.button_remove.Location = new System.Drawing.Point(214, 295);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(178, 37);
            this.button_remove.TabIndex = 23;
            this.button_remove.Text = "Remove CSTime from this user";
            this.button_remove.UseVisualStyleBackColor = false;
            this.button_remove.Visible = false;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 344);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_moreless);
            this.Controls.Add(this.l_sun);
            this.Controls.Add(this.l_sat);
            this.Controls.Add(this.l_fri);
            this.Controls.Add(this.l_thu);
            this.Controls.Add(this.l_wed);
            this.Controls.Add(this.l_tue);
            this.Controls.Add(this.l_mon);
            this.Controls.Add(this.button_selectLanguage);
            this.Controls.Add(this.checkBox_startCSTimeWithWindows);
            this.Controls.Add(this.checkBox_guiTopMost);
            this.Controls.Add(this.checkBox_pauseResumeTimerOnComputerLockUnlock);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.checkBox_enableTimer);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.numericUpDown_maximumHoursSun);
            this.Controls.Add(this.numericUpDown_maximumHoursSat);
            this.Controls.Add(this.numericUpDown_maximumHoursFri);
            this.Controls.Add(this.numericUpDown_maximumHoursThu);
            this.Controls.Add(this.numericUpDown_maximumHoursWed);
            this.Controls.Add(this.numericUpDown_maximumHoursTue);
            this.Controls.Add(this.numericUpDown_maximumHoursMon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configure";
            this.Text = "Configuration - CSTime";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configure_FormClosing);
            this.Load += new System.EventHandler(this.Configure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursTue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursWed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursFri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursSat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maximumHoursSun)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursMon;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursTue;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursWed;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursThu;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursFri;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursSat;
        private System.Windows.Forms.NumericUpDown numericUpDown_maximumHoursSun;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.CheckBox checkBox_enableTimer;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.CheckBox checkBox_pauseResumeTimerOnComputerLockUnlock;
        private System.Windows.Forms.CheckBox checkBox_guiTopMost;
        private System.Windows.Forms.CheckBox checkBox_startCSTimeWithWindows;
        private System.Windows.Forms.Button button_selectLanguage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label l_mon;
        private System.Windows.Forms.Label l_tue;
        private System.Windows.Forms.Label l_wed;
        private System.Windows.Forms.Label l_thu;
        private System.Windows.Forms.Label l_fri;
        private System.Windows.Forms.Label l_sat;
        private System.Windows.Forms.Label l_sun;
        private System.Windows.Forms.Button button_moreless;
        private System.Windows.Forms.Button button_remove;
    }
}