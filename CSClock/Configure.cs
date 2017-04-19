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


using System;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Drawing;
using System.Diagnostics;

namespace CSClock
{
    public partial class Configure : Form
    {
        private const string className = "Configure.cs";


        string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock.lnk");
        public string selectedLanguage = null;
        private bool expanded = false;

        public Configure()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox_enableTimer.Checked)
                {
                    if (MessageBox.Show(Program.rm_Messages.GetString("notEnableTimerConfirmation_text"),
                        "CSClock", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                Properties.Settings.Default.maximumHoursMon = numericUpDown_maximumHoursMon.Value;
                Properties.Settings.Default.maximumHoursTue = numericUpDown_maximumHoursTue.Value;
                Properties.Settings.Default.maximumHoursWed = numericUpDown_maximumHoursWed.Value;
                Properties.Settings.Default.maximumHoursThu = numericUpDown_maximumHoursThu.Value;
                Properties.Settings.Default.maximumHoursFri = numericUpDown_maximumHoursFri.Value;
                Properties.Settings.Default.maximumHoursSat = numericUpDown_maximumHoursSat.Value;
                Properties.Settings.Default.maximumHoursSun = numericUpDown_maximumHoursSun.Value;
                Properties.Settings.Default.timerEnabled = checkBox_enableTimer.Checked;
                Properties.Settings.Default.pauseResumeTimerOnComputerLockUnlock = checkBox_pauseResumeTimerOnComputerLockUnlock.Checked;
                Properties.Settings.Default.configured = true;
                Properties.Settings.Default.guiTopMost = checkBox_guiTopMost.Checked;
                if (selectedLanguage != null)
                {
                    Properties.Settings.Default.selectedLanguage = selectedLanguage;
                }

                Properties.Settings.Default.Save();

                if (checkBox_startCSClockWithWindows.Checked && !File.Exists(shortcutPath) && !Program.debug)
                {
                    try
                    {
                        CreateShortcut(Application.ExecutablePath, shortcutPath, Path.GetDirectoryName(Application.ExecutablePath), "-s");
                    }
                    catch (MissingMethodException ex)
                    {
                        Program.logger.Log(className, "Error while creating the shortcut in the startup folder: " +
                            "Your .NET version does not support this function. Please make a shortcut manually for CSClock.exe in the startup folder (" +
                            Environment.GetFolderPath(Environment.SpecialFolder.Startup) + ")\r\n\r\nError message: " + ex.ToString(), Logger.LogType.Error);
                        MessageBox.Show("Error while creating the shortcut in the startup folder: " +
                            "Your .NET version does not support this function. Please make a shortcut manually for CSClock.exe in the startup folder (" +
                            Environment.GetFolderPath(Environment.SpecialFolder.Startup) + ")\n\nError message: " + ex.Message + "\n\nSee log.txt for more details",
                            "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        Program.logger.Log(className, "Error while creating a shortcut in the startup folder: " + ex.ToString(),
                            Logger.LogType.Error);
                        MessageBox.Show("Error while creating a shortcut in the startup folder: " + ex.Message + "\n\nSee log.txt for more details",
                            "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!checkBox_startCSClockWithWindows.Checked && File.Exists(shortcutPath) && !Program.debug)
                {
                    try
                    {
                        File.Delete(shortcutPath);
                    }
                    catch (Exception ex)
                    {
                        Program.logger.Log(className, "Error while deleting the shortcut in the startup folder: " + ex.ToString(),
                            Logger.LogType.Error);
                        MessageBox.Show("Error while deleting the shortcut in the startup folder: " + ex.Message + "\n\nSee log.txt for more details",
                            "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Program.Reload();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\n\r\n\r\nFull error details" + ex, "CSClock", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Program.logger.Log(className, Convert.ToString(ex), Logger.LogType.Error);
            }
        }

        private void CreateShortcut(string filePath, string shortcutPath,
            string workingDir, string arguments = "", string description = "", string hotkey = "")
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            string shortcutAddress = shortcutPath;
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutAddress);
            if (description != "")
            {
                shortcut.Description = description;
            }
            if (hotkey != "")
            {
                shortcut.Hotkey = hotkey;
            }
            if (arguments != "")
            {
                shortcut.Arguments = arguments;
            }
            shortcut.TargetPath = filePath;
            shortcut.WorkingDirectory = workingDir;
            shortcut.Save();
        }

        private void Configure_Load(object sender, EventArgs e)
        {
            this.Location = Program.CSClockForm.Location;
            this.Size = new Size(420, 342);

            Program.rm_Configure = new ResourceManager(string.Format("CSClock.Languages.{0}.Configure", Program.selectedLanguage), Program.assembly);

            //Load strings
            this.Text = Program.rm_Configure.GetString("form_title");
            toolTip1.SetToolTip(button_selectLanguage, Program.rm_Configure.GetString("button_selectLanguage_toolTip"));
            toolTip1.SetToolTip(button_remove, Program.rm_Configure.GetString("button_remove_toolTip"));
            label1.Text = Program.rm_Configure.GetString("label1_text");
            l_mon.Text = Program.rm_Configure.GetString("l_mon_text");
            l_tue.Text = Program.rm_Configure.GetString("l_tue_text");
            l_wed.Text = Program.rm_Configure.GetString("l_wed_text");
            l_thu.Text = Program.rm_Configure.GetString("l_thu_text");
            l_fri.Text = Program.rm_Configure.GetString("l_fri_text");
            l_sat.Text = Program.rm_Configure.GetString("l_sat_text");
            l_sun.Text = Program.rm_Configure.GetString("l_sun_text");
            checkBox_enableTimer.Text = Program.rm_Configure.GetString("checkBox_enableTimer_text");
            checkBox_pauseResumeTimerOnComputerLockUnlock.Text = Program.rm_Configure.GetString("checkBox_pauseResumeTimerOnComputerLockUnlock_text");
            checkBox_guiTopMost.Text = Program.rm_Configure.GetString("checkBox_guiTopMost_text");
            checkBox_startCSClockWithWindows.Text = Program.rm_Configure.GetString("checkBox_startCSClockWithWindows_text");
            button_apply.Text = Program.rm_Configure.GetString("button_apply_text");
            button_reset.Text = Program.rm_Configure.GetString("button_reset_text");
            button_remove.Text = Program.rm_Configure.GetString("button_remove_text");
            button_moreless.Text = Program.rm_Configure.GetString("button_moreless_text__more");
            //

            numericUpDown_maximumHoursMon.Value = Properties.Settings.Default.maximumHoursMon;
            numericUpDown_maximumHoursTue.Value = Properties.Settings.Default.maximumHoursTue;
            numericUpDown_maximumHoursWed.Value = Properties.Settings.Default.maximumHoursWed;
            numericUpDown_maximumHoursThu.Value = Properties.Settings.Default.maximumHoursThu;
            numericUpDown_maximumHoursFri.Value = Properties.Settings.Default.maximumHoursFri;
            numericUpDown_maximumHoursSat.Value = Properties.Settings.Default.maximumHoursSat;
            numericUpDown_maximumHoursSun.Value = Properties.Settings.Default.maximumHoursSun;
            checkBox_enableTimer.Checked = Properties.Settings.Default.timerEnabled;
            checkBox_pauseResumeTimerOnComputerLockUnlock.Checked = Properties.Settings.Default.pauseResumeTimerOnComputerLockUnlock;
            checkBox_guiTopMost.Checked = Properties.Settings.Default.guiTopMost;

            checkBox_startCSClockWithWindows.Checked = File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                    "CSClock.lnk"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Program.rm_Messages.GetString("resetConfirmation"),
                "CSClock", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.logger.Log(className, "Resetting CSClock...", Logger.LogType.Info);
                Program.CSClockForm.timer.Stop();
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.upgradeRequired = false;
                Properties.Settings.Default.Save();

                if (File.Exists("log.txt") && MessageBox.Show("Do you also want to delete the log?",
                    "CSClock", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete("log.txt");
                }

                Environment.Exit(0);
            }
        }

        private void button_selectLanguage_Click(object sender, EventArgs e)
        {
            SelectLanguage selectLanguageForm = new SelectLanguage();
            selectLanguageForm.ShowDialog();
        }

        private void Configure_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_Configure = null;
        }

        private void button_moreless_Click(object sender, EventArgs e)
        {
            expanded = !expanded;
            if (expanded)
            {
                this.Size = new Size(420, 383);
                button_moreless.Text = Program.rm_Configure.GetString("button_moreless_text__less");
                button_reset.Visible = true;
                button_remove.Visible = true;
            }
            else
            {
                this.Size = new Size(420, 342);
                button_moreless.Text = Program.rm_Configure.GetString("button_moreless_text__more");
                button_reset.Visible = false;
                button_remove.Visible = false;
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            Program.Uninstall(true);
        }
    }
}
