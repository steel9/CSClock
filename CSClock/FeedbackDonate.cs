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
