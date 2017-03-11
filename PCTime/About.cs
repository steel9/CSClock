/*
PCTime - a program which keeps track of your computer time
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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace PCTime
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            Program.rm_About = new ResourceManager(string.Format("PCTime.Languages.{0}.About", Program.selectedLanguage), Program.assembly);

            if (Program.rm_AssemblyInfo == null)
            {
                Program.rm_AssemblyInfo = new ResourceManager(string.Format("PCTime.Languages.{0}.AssemblyInfo", Program.selectedLanguage),
                    Program.assembly);
            }

            this.Text = String.Format("{0} {1}", Program.rm_About.GetString("form_title_About"), AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = Program.rm_AssemblyInfo.GetString("Description");
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void About_Load(object sender, EventArgs e)
        {
            button_openLog.Text = Program.rm_About.GetString("button_openLog_text");
            button_exportLog.Text = Program.rm_About.GetString("button_exportLog_text");
            button_contact.Text = Program.rm_About.GetString("button_contact_text");
            button_openGitHubPage.Text = Program.rm_About.GetString("button_openGitHubPage_text");
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.rm_About = null;
        }

        private void button_contact_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:steel9apps@gmail.com");
        }

        private void button_openLog_Click(object sender, EventArgs e)
        {
            if (!Program.debug)
            {
                Process.Start(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "PCTime", "log.txt"));
            }
            else
            {
                Process.Start(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "PCTime", "debug", "log.txt"));
            }
        }

        private void button_exportLog_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string text;
            StreamReader sr = new StreamReader(Program.logger.logPath_);
            text = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile());
            sw.Write(text);
            sw.Close();

            MessageBox.Show("Log exported to: " + saveFileDialog1.FileName, "PCTime", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_openGitHubPage_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/steel9/PCTime");
        }
    }
}
