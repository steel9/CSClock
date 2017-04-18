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
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

namespace CSClock
{
    public partial class AddSubtractTime : Form
    {
        private const string className = "AddSubtractTime.cs";


        private int secondsChange_ = 0;
        int secondsChange
        {
            get
            {
                return secondsChange_;
            }

            set
            {
                if (value <= -86400 || value >= 86400)
                {
                    secondsChange_ = 0;
                }
                else
                {
                    secondsChange_ = value;
                }

                if (secondsChange_ == 0)
                {
                    label1.Text = "+-";
                }
                else if (secondsChange_ < 0)
                {
                    label1.Text = "-";
                }
                else if (secondsChange_ > 0)
                {
                    label1.Text = "+";
                }
                TimeSpan tsFromSec_secondsChange = TimeSpan.FromSeconds(secondsChange_);
                label1.Text += string.Format("\r\n{0}h {1}m", tsFromSec_secondsChange.ToString(@"hh").Replace("-", ""),
                    tsFromSec_secondsChange.ToString(@"mm"));
            }
        }

        public AddSubtractTime()
        {
            InitializeComponent();

            this.TopMost = Program.CSClockForm.TopMost;
        }

        private void FwrdRwndTime_Load(object sender, EventArgs e)
        {
            Program.rm_AddSubtractTime = new ResourceManager(string.Format("CSClock.Languages.{0}.AddSubtractTime", Program.selectedLanguage),
                Program.assembly);
            this.Text = Program.rm_AddSubtractTime.GetString("windowTitle");
            label2.Text = Program.rm_AddSubtractTime.GetString("label2_text");
            this.Location = new Point(Program.CSClockForm.Location.X, Program.CSClockForm.Location.Y + (this.Size.Height + 20));
        }

        private void FwrdRwndTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.addSubtractTime = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secondsChange += 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            secondsChange += 3600;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            secondsChange -= 60;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            secondsChange -= 3600;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.logger.Log(className, string.Format("Adding {0} to seconds elapsed", secondsChange),
                Logger.LogType.Info);

            if (Program.CSClockForm.secondsElapsed + secondsChange < 86400
                && Program.CSClockForm.secondsElapsed + secondsChange > 0)
            {
                Program.CSClockForm.secondsElapsed += secondsChange;
            }
            else
            {
                Program.CSClockForm.secondsElapsed = 0;
            }
            this.Close();
        }
    }
}
