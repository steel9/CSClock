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
            { "btn_JsonNET", "Json.NET-LICENSE.md" }
        };

        private Dictionary<string, string> btnsLicensesOf = new Dictionary<string, string>
        {
            { "btn_CSClock", "CSClock" },
            { "btn_JsonNET", "Json.NET by Newtonsoft" }
        };

        public Licenses()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
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

            l_lcOf.Text = Program.rm_LicensesForm.GetString("l_lcOf_startText") + " " +
                btnsLicensesOf[((Button)sender).Name];
        }

        private void Licenses_Load(object sender, EventArgs e)
        {
            Program.rm_LicensesForm = new ResourceManager(string.Format("CSClock.Languages.{0}.LicensesForm", Program.selectedLanguage), Program.assembly);

            label2.Text = Program.rm_LicensesForm.GetString("l_3rdpartylibraries_text");
            label3.Text = Program.rm_LicensesForm.GetString("l_instructions_text");
            l_lcOf.Text = Program.rm_LicensesForm.GetString("l_lcOf_startText");
        }
    }
}
