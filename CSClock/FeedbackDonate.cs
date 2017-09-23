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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSClock
{
    public partial class FeedbackDonate : Form
    {
        public FeedbackDonate()
        {
            InitializeComponent();
        }

        private void FeedbackDonate_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.feedbackReminderShown = true;
            Properties.Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://steel9apps.wixsite.com/csclock/donate");
            Process.Start("https://docs.google.com/forms/d/1so-iTsCwOd-KcT0yQd4LqCEymGbQEyBAASYEu7cXP0I");
            Close();
        }
    }
}
