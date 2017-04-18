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
using System.Resources;
using System.Windows.Forms;

namespace CSClock
{
    public partial class SelectLanguage : Form
    {
        //private const string className = "SelectLanguage.cs";     currently not needed


        Dictionary<string, string> btnLanguages = new Dictionary<string, string>
        {
            { "button1", "English" },
            { "button2", "Swedish" }
        };

        public SelectLanguage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (btnLanguages[ ((Button)sender).Name ] != Program.selectedLanguage)
            {
                if (MessageBox.Show(string.Format(Program.rm_Messages.GetString("changeLang_confirm_msg"), ((Button)sender).Text), "CSClock", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Program.configureForm.selectedLanguage = btnLanguages[ ((Button)sender).Name ];
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void SelectLanguage_Load(object sender, EventArgs e)
        {
            this.Location = Program.configureForm.Location;
            Program.rm_SelectLanguage = new ResourceManager(string.Format("CSClock.Languages.{0}.SelectLanguage", Program.selectedLanguage), Program.assembly);

            label1.Text = Program.rm_SelectLanguage.GetString("label1_text");
            this.Text = Program.rm_SelectLanguage.GetString("form_title");
            button1.Text = Program.rm_SelectLanguage.GetString("English");
            button2.Text = Program.rm_SelectLanguage.GetString("Swedish");
        }

        private void SelectLanguage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_SelectLanguage = null;
        }
    }
}
