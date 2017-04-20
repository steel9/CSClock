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
using System.Resources;
using System.Windows.Forms;

namespace CSClock
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            this.TopMost = Program.CSClockForm.TopMost;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            Program.rm_Statistics = new ResourceManager(string.Format("CSClock.Languages.{0}.Statistics", Program.selectedLanguage), Program.assembly);

            Stats.UpdateStatistics();

            this.Location = Program.CSClockForm.Location;

            l_startDate.Text = Program.rm_Statistics.GetString("l_since_startText") + " " + Stats.StartDateTime().Date.ToString("yyyy-MM-dd");

            label1.Text = Program.rm_Statistics.GetString("l_totalHrsSpent_text");
            l_totalHrsSpent.Text = Math.Round(Stats.TotalHoursSpent(), 3).ToString();

            label5.Text = Program.rm_Statistics.GetString("l_avgHrsSpent_text");
            l_avgHrsSpent.Text = Math.Round(Stats.AverageHoursSpent(), 3).ToString();

            label4.Text = Program.rm_Statistics.GetString("l_totalOvertimeHrsSpent_text");
            l_totalOvertimeHrsSpent.Text = Math.Round(Stats.TotalOvertimeHoursSpent(), 3).ToString();

            label8.Text = Program.rm_Statistics.GetString("l_avgOvertimeHrsSpent_text");
            l_avgOvertimeHrsSpent.Text = Math.Round(Stats.AverageOvertimeHoursSpent(), 3).ToString();
        }

        private void Statistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_Statistics = null;
        }
    }
}
