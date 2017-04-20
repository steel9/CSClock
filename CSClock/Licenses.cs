using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

            l_lcOf.Text = "License of: " + btnsLicensesOf[((Button)sender).Name];
        }
    }
}
