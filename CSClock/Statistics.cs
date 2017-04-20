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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Stats.UpdateStatistics();

            this.Location = Program.CSClockForm.Location;

            l_startDate.Text = "Since " + Stats.StartDateTime().Date.ToString("yyyy-MM-dd");
            l_totalHrsSpent.Text = Stats.TotalHoursSpent().ToString();
            l_avgHrsSpent.Text = Stats.AverageHoursSpent().ToString();
        }
    }
}
