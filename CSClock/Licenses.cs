﻿/*
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
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSClock
{
    public partial class Licenses : Form
    {
        private string licensesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "Licenses");
        private string devLicensesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "dev", "Licenses");

        private Dictionary<string, string> btnsLicenses = new Dictionary<string, string>
        {
            { "btn_CSClock", "CSClock-LICENSE" },
            { "btn_JsonNET", "Json.NET-LICENSE.md" },
            { "btn_CustomSettingsProvider", "CustomSettingsProvider-LICENSE.htm" }
        };

        private Dictionary<string, string> btnsLicensesOf = new Dictionary<string, string>
        {
            { "btn_CSClock", "CSClock" },
            { "btn_JsonNET", "Json.NET by Newtonsoft" },
            { "btn_CustomSettingsProvider", "CustomSettingsProvider by CodeChimp" }
        };

        public Licenses()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (!btnsLicenses[((Button)sender).Name].EndsWith(".htm"))
            {
                webBrowser1.Navigate("about:blank");
                webBrowser1.Visible = false;
                richTextBox1.Visible = true;

                StreamReader sr;
                if (!Program.dev)
                {
                    sr = new StreamReader(Path.Combine(licensesDir, btnsLicenses[((Button)sender).Name]));
                }
                else
                {
                    sr = new StreamReader(Path.Combine(devLicensesDir, btnsLicenses[((Button)sender).Name]));
                }
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.Visible = false;
                webBrowser1.Visible = true;
                if (!Program.dev)
                {
                    webBrowser1.Navigate(Path.Combine(licensesDir, btnsLicenses[((Button)sender).Name]));
                }
                else
                {
                    webBrowser1.Navigate(Path.Combine(devLicensesDir, btnsLicenses[((Button)sender).Name]));
                }
            }

            l_lcOf.Text = Program.rm_LicensesForm.GetString("l_lcOf_startText") + " " +
                btnsLicensesOf[((Button)sender).Name];
        }

        private void Licenses_Load(object sender, EventArgs e)
        {
            Program.rm_LicensesForm = new ResourceManager(string.Format("CSClock.Languages.{0}.LicensesForm", Program.selectedLanguage), Program.assembly);

            this.Text = Program.rm_LicensesForm.GetString("form_title");
            label2.Text = Program.rm_LicensesForm.GetString("l_3rdpartylibraries_text");
            label3.Text = Program.rm_LicensesForm.GetString("l_instructions_text");
            l_lcOf.Text = Program.rm_LicensesForm.GetString("l_lcOf_startText");
        }

        private void Licenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_LicensesForm = null;
        }
    }
}
