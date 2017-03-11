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


using System;
using System.Resources;
using System.Windows.Forms;

namespace CSTime
{
    public partial class SetOvertime : Form
    {
        //private const string className = "SetOvertime.cs";          currently not needed
        public SetOvertime()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.CSTimeForm.maximumSecondsOvertime = (int)(numericUpDown_maximumMinutesOvertime.Value * 60);
            Properties.Settings.Default.maximumMinutesOvertime = numericUpDown_maximumMinutesOvertime.Value;
            Properties.Settings.Default.overtimeDateTime = DateTime.Now;
            Properties.Settings.Default.Save();
            Close();
        }

        private void SetOvertime_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_SetOvertime = null;
        }

        private void SetOvertime_Load(object sender, EventArgs e)
        {
            Program.rm_SetOvertime = new ResourceManager(string.Format("CSTime.Languages.{0}.SetOvertime", Program.selectedLanguage), Program.assembly);
            label1.Text = Program.rm_SetOvertime.GetString("label1_text");
            label2.Text = Program.rm_SetOvertime.GetString("label2_text");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
