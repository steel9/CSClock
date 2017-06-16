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
using System.IO;
using System.Linq;
using System.Reflection;
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

        private List<string> licensePaths = new List<string>();

        public Licenses()
        {
            InitializeComponent();
        }

        private void Licenses_Load(object sender, EventArgs e)
        {
            Program.rm_LicensesForm = new ResourceManager(string.Format("CSClock.Languages.{0}.LicensesForm", Program.selectedLanguage), Program.assembly);

            this.Text = Program.rm_LicensesForm.GetString("form_title");
            l_lcOf.Text = Program.rm_LicensesForm.GetString("l_lcOf_startText");

            foreach (string licenseFile in Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(x => x.StartsWith("CSClock.Licenses") && !x.StartsWith("CSClock.Licenses.ExtraInfo") && !x.EndsWith("resources")))
            {
                listBox1.Items.Add(
                    licenseFile
                    .Replace("CSClock.Licenses.", "")
                    .Replace("_LICENSE", "")
                    .Replace(".txt", "")
                    .Replace(".htm", "")
                    );
                licensePaths.Add(licenseFile);
            }
        }

        private void Licenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_LicensesForm = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (licensePaths[listBox1.SelectedIndex].EndsWith(".htm"))
            {
                richTextBox1.Visible = false;
                webBrowser1.Visible = true;

                var stream = Program.assembly.GetManifestResourceStream(licensePaths[listBox1.SelectedIndex]);
                webBrowser1.DocumentStream = stream;
            }
            else
            {
                webBrowser1.Navigate("about:blank");
                webBrowser1.Visible = false;
                richTextBox1.Visible = true;

                var stream = Program.assembly.GetManifestResourceStream(licensePaths[listBox1.SelectedIndex]);
                using (var sr = new StreamReader(stream))
                {
                    richTextBox1.Text = sr.ReadToEnd();
                }
            }

            l_lcOf.Text = "License of: " + listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedLicense = listBox1.Items[listBox1.SelectedIndex].ToString();

            foreach (string infoFile_ in Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(x => x.StartsWith("CSClock.Licenses.ExtraInfo")
                && !x.EndsWith("resources")))
            {
                var infoFile = infoFile_
                    .Replace("CSClock.Licenses.ExtraInfo.", "")
                    .Replace(".txt", "");

                if (infoFile == selectedLicense)
                {
                    var stream = Program.assembly.GetManifestResourceStream(infoFile_);
                    using (var sr = new StreamReader(stream))
                    {
                        MessageBox.Show(sr.ReadToEnd(), selectedLicense, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
