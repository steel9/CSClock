/*
CSTime - a program which keeps track of your computer time
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Resources;

namespace CSTime
{
    static class Program
    {
        private const string className = "Program.cs";


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool debug = false;

        public static Logger logger;

        public static CSTime pctimeForm;
        public static Configure configureForm;
        public static NotifyIcon notifyIcon1;
        public static SetOvertime setOvertime;
        public static AddSubtractTime addSubtractTime;
        public static BalloonClickActions balloonClickAction = BalloonClickActions.None;

        public static ContextMenu contextMenu1;
        public static string selectedLanguage = "English"; //Must be spelled the same as the language folder

        public static Assembly assembly;

        public static ResourceManager rm_About = null;
        public static ResourceManager rm_AssemblyInfo = null;
        public static ResourceManager rm_Configure = null;
        public static ResourceManager rm_GUI = null;
        public static ResourceManager rm_Messages = null;
        public static ResourceManager rm_SelectLanguage = null;
        public static ResourceManager rm_SetOvertime = null;
        public static ResourceManager rm_AddSubtractTime = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            debug = true;
#endif
            string logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSTime");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            if (debug)
            {
                logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSTime", "debug");
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
            }

            
            logger = new Logger("CSTime", Path.Combine(logDir, "log.txt"),
                Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);

            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "CSTime", out createdNew))
            {
                if (createdNew || (args != null && args.Contains("-ignorerunning")))
                {
                    logger.Log(className, "createdNew=true", Logger.LogType.Info, true);
                    StartApplication(args);
                }
                else
                {

#if DEBUG
                    logger.Log(className, "createdNew=false, debug=true", Logger.LogType.Info, true);
                    StartApplication(args);
                    return;
#endif
                    logger.Log(className, "createdNew=false", Logger.LogType.Info, true);

                    //Bring the window of the already running instance of CSTime to the front
                    try
                    {
                        Process current = Process.GetCurrentProcess();
                        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                SetForegroundWindow(process.MainWindowHandle);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + "\r\n\r\n\r\nFull error details: " + ex, "CSTime", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        logger.Log(className, Convert.ToString(ex), Logger.LogType.Error);
                    }
                }
            }
        }

        static void StartApplication(string[] args)
        {
            assembly = Assembly.GetExecutingAssembly();


            logger.Log(className, "Starting CSTime", Logger.LogType.Info);

            if (Properties.Settings.Default.upgradeRequired && (args == null || !args.Contains("-donotupgradesettings")))
            {
                logger.Log(className, "Upgrading user settings...", Logger.LogType.Info);

                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.upgradeRequired = false;
                Properties.Settings.Default.Save();

                logger.Log(className, "Upgrade done", Logger.LogType.Info);
            }

            selectedLanguage = Properties.Settings.Default.selectedLanguage;
            rm_Messages = new ResourceManager(string.Format("CSTime.Languages.{0}.Messages", selectedLanguage), assembly);
            rm_GUI = new ResourceManager(string.Format("CSTime.Languages.{0}.GUI", selectedLanguage), assembly);

            if (args != null && args.Contains("-removal"))
            {
                Removal(true);
                return;
            }

            if (args != null && args.Contains("-reset"))
            {
                logger.Log(className, "Resetting CSTime", Logger.LogType.Info);

                Properties.Settings.Default.Reset();
                Properties.Settings.Default.upgradeRequired = false;
                Properties.Settings.Default.Save();

                logger.Log(className, "Reset completed, closing CSTime", Logger.LogType.Info);

                if ((args == null || !args.Contains("-deletelog")) || !File.Exists("log.txt"))
                {
                    return;
                }
            }

            if (args != null && args.Contains("-deletelog") && File.Exists("log.txt"))
            {
                File.Delete("log.txt");
                return;
            }

            pctimeForm = new CSTime();
            pctimeForm.startDateTime = DateTime.Now;

            LoadNotifyIcon();

            if (!Properties.Settings.Default.configured)
            {
                Properties.Settings.Default.overtimeDateTime = default(DateTime);
                Properties.Settings.Default.secondsElapsedDateTime = default(DateTime);
                Properties.Settings.Default.Save();
            }

            logger.Log(className, "executing LoadApplySettings()", Logger.LogType.Info);
            try
            {
                pctimeForm.LoadApplySettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + "\r\n\r\n\r\nFull error details: " + ex, "CSTime", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(className, Convert.ToString(ex), Logger.LogType.Error);
                Application.Exit();
                return;
            }

            if (args != null && args.Contains("-s"))
            {
                pctimeForm.startMinimized = true;
            }

            Application.Run(pctimeForm);
        }

        public static void Removal(bool confirmMsg)
        {
            if (!confirmMsg || MessageBox.Show(rm_Messages.GetString("removalQuestion"), "CSTime",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string path = Path.Combine(Path.GetTempPath(), "pcTimeRemovalTool.bat");

                StreamWriter sw = new StreamWriter(path);
                sw.Write(Properties.Resources.removaltool);
                sw.Close();

                Process.Start(path, "\"" + Application.ExecutablePath + "\" \"" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "\"");
            }
        }

        public static void Reload()
        {
            if (setOvertime != null)
            {
                setOvertime.Close();
                setOvertime = null;
            }
            if (addSubtractTime != null)
            {
                addSubtractTime.Close();
                addSubtractTime = null;
            }

            selectedLanguage = Properties.Settings.Default.selectedLanguage;
            rm_Messages = new ResourceManager(string.Format("CSTime.Languages.{0}.Messages", selectedLanguage), assembly);
            rm_GUI = new ResourceManager(string.Format("CSTime.Languages.{0}.GUI", selectedLanguage), assembly);

            LoadNotifyIcon();
            pctimeForm.LoadApplySettings();
            pctimeForm.GUI_Load();
        }

        public static void LoadNotifyIcon()
        {
            if (notifyIcon1 != null)
            {
                notifyIcon1.Dispose();
            }
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = Properties.Resources.Logo;
            contextMenu1 = new ContextMenu();
            if (Properties.Settings.Default.timerEnabled)
            {
                contextMenu1.MenuItems.Add(rm_GUI.GetString("pauseResume"), new EventHandler(PauseResumeTimer));
            }
            contextMenu1.MenuItems.Add(rm_GUI.GetString("quit"), new EventHandler(Quit));
            notifyIcon1.ContextMenu = contextMenu1;

            if (!debug)
            {
                notifyIcon1.Text = "CSTime";
            }
            else
            {
                notifyIcon1.Text = "CSTime DEBUG";
            }

            notifyIcon1.Visible = true;

            notifyIcon1.DoubleClick += new EventHandler(ShowGUI);
            notifyIcon1.BalloonTipClicked += new EventHandler(BalloonClickEvent);
        }

        static void ShowGUI(object sender, EventArgs e)
        {
            if (pctimeForm != null)
            {
                pctimeForm.BringToFront();
            }
            else
            {
                pctimeForm = new CSTime();
                pctimeForm.Show();
            }
        }

        static void BalloonClickEvent(object sender, EventArgs e)
        {
            switch (balloonClickAction)
            {
                case BalloonClickActions.SetOvertime:
                    setOvertime = new SetOvertime();
                    setOvertime.TopMost = pctimeForm.TopMost;
                    setOvertime.ShowDialog();
                    break;
            }

            balloonClickAction = BalloonClickActions.None;
        }

        public enum BalloonClickActions
        {
            SetOvertime,
            None
        }

        static void PauseResumeTimer(object sender, EventArgs e)
        {
            bool updateGUI;
            if (pctimeForm != null)
                updateGUI = true;
            else
                updateGUI = false;

            if (pctimeForm.timer.Enabled)
            {
                pctimeForm.PauseTimer(updateGUI);
                notifyIcon1.ShowBalloonTip(2500, "CSTime", rm_Messages.GetString("timerPausedNotification_text"), ToolTipIcon.Info);
            }
            else
            {
                pctimeForm.ResumeTimer(updateGUI);
                notifyIcon1.ShowBalloonTip(2500, "CSTime", rm_Messages.GetString("timerResumedNotification_text"), ToolTipIcon.Info);
            }
        }

        static void Quit(object sender, EventArgs e)
        {
            if (MessageBox.Show(rm_Messages.GetString("quitConfirmationDialog_text"), "CSTime", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
