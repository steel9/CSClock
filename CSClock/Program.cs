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
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Net;
using Squirrel;

namespace CSClock
{
    static class Program
    {
        private const string className = "Program.cs";


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool debug = false;
        public static bool dev = false;
        public static bool portable = true;

        public static Logger logger;

        public static CSClock CSClockForm;
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
        public static ResourceManager rm_LicensesForm = null;
        public static ResourceManager rm_Statistics = null;

        public static bool properExitLast = true;

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

            Application.ThreadException += new
                ThreadExceptionEventHandler(CSClock.Form1_UIThreadException);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new
                UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            if (args != null && args.Length > 0)
            {
                if (args.Contains("-dev"))
                {
                    dev = true;
                }

                if (args.Contains("-np"))
                {
                    portable = false;
                }
            }

            string logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");

            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            if (debug)
            {
                logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "debug");
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
            }

            assembly = Assembly.GetExecutingAssembly();
            rm_Messages = new ResourceManager(string.Format("CSClock.Languages.{0}.Messages", selectedLanguage), assembly);

            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "CSClock", out createdNew))
            {
                if (createdNew || (args != null && args.Contains("-ignorerunning")))
                {
                    if (!Properties.Settings.Default.properExit)
                    {
                        properExitLast = false;
                    }
                    else
                    {
                        Properties.Settings.Default.properExit = false;
                        Properties.Settings.Default.Save();
                    }

                    logger = new Logger("CSClock", Path.Combine(logDir, "log.txt"),
                        Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);
                    logger.Log(className, "createdNew=true", Logger.LogType.Info, true);

                    StartApplication(args);
                }
                else
                {
                    logger = new Logger("CSClock", Path.Combine(logDir, "log.txt"),
                        Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);

#if DEBUG
                    logger.Log(className, "createdNew=false, debug=true", Logger.LogType.Info, true);
                    StartApplication(args);
                    return;
#endif
                    logger.Log(className, "createdNew=false", Logger.LogType.Info, true);

                    //Bring the window of the already running instance of CSClock to the front
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
                        MessageBox.Show("Error: " + ex.Message + "\r\n\r\n\r\nFull error details: " + ex, "CSClock", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        logger.Log(className, Convert.ToString(ex), Logger.LogType.Error);
                    }
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "Ouch! An application error occurred :(\r\nPlease contact me (steel9) " +
                    "and tell me what you did when this happened";
                MessageBox.Show(errorMsg, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Ouch! An error occurred. Sorry about that :(\r\nCould not write the error to the event log. Reason: "
                        + exc.Message, "CSClock: Fatal Non-UI Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        static async void StartApplication(string[] args)
        {
            logger.Log(className, "Starting CSClock", Logger.LogType.Info);

            if ((args == null || args.Length == 0 || !args.Contains("-disup")) && Properties.Settings.Default.autoUpdate)
            {
                Properties.Settings.Default.properExit = true;
                Properties.Settings.Default.Save();
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/steel9/CSClock"))
                {
                    await mgr.Result.UpdateApp();
                }
                Properties.Settings.Default.properExit = false;
                Properties.Settings.Default.Save();
            }

            selectedLanguage = Properties.Settings.Default.selectedLanguage;
            rm_GUI = new ResourceManager(string.Format("CSClock.Languages.{0}.GUI", selectedLanguage), assembly);

            if (args != null && args.Length > 0 && args.Contains("-disup"))
            {
                Properties.Settings.Default.autoUpdate = false;
                Properties.Settings.Default.Save();
            }
            else if (args != null && args.Length > 0 && args.Contains("-enup"))
            {
                Properties.Settings.Default.autoUpdate = true;
                Properties.Settings.Default.Save();
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/steel9/CSClock"))
                {
                    await mgr.Result.UpdateApp();
                }
            }

            if (args != null && args.Length > 0 && args.Contains("-uninstall"))
            {
                if (!portable)
                {
                    Uninstall();
                }
                else
                {
                    MessageBox.Show("Uninstallation not available nor needed in portable version", "CSClock", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                return;
            }

            if (args != null && args.Length > 0 && args.Contains("-reset"))
            {
                logger.Log(className, "Resetting CSClock", Logger.LogType.Info);

                Properties.Settings.Default.Reset();
                //Properties.Settings.Default.upgradeRequired = false;
                Properties.Settings.Default.Save();

                logger.Log(className, "Reset completed, closing CSClock", Logger.LogType.Info);

                if (((args == null && args.Length > 0) || !args.Contains("-deletelogs")) || (!File.Exists("log.txt") && !File.Exists("setuplog.txt")))
                {
                    Properties.Settings.Default.properExit = true;
                    Properties.Settings.Default.Save();
                    return;
                }
            }

            if (args != null && args.Length > 0 && args.Contains("-deletelogs") && (File.Exists("log.txt") || File.Exists("setuplog.txt")))
            {
                File.Delete("log.txt");
                File.Delete("setuplog.txt");
                Properties.Settings.Default.properExit = true;
                Properties.Settings.Default.Save();
                return;
            }

            CSClockForm = new CSClock();
            CSClockForm.startDateTime = DateTime.Now;

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
                CSClockForm.LoadApplySettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + "\r\n\r\n\r\nFull error details: " + ex, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(className, Convert.ToString(ex), Logger.LogType.Error);
                Properties.Settings.Default.properExit = true;
                Properties.Settings.Default.Save();
                Application.Exit();
                return;
            }

            if (args != null && args.Contains("-s"))
            {
                CSClockForm.startMinimized = true;
            }

            Application.Run(CSClockForm);
        }

        public static void Uninstall()
        {
            Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "Setup.exe"), "-uninstall");
        }

        public static void Reload()
        {
            CSClockForm.Save();

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
            rm_Messages = new ResourceManager(string.Format("CSClock.Languages.{0}.Messages", selectedLanguage), assembly);
            rm_GUI = new ResourceManager(string.Format("CSClock.Languages.{0}.GUI", selectedLanguage), assembly);

            LoadNotifyIcon();
            CSClockForm.LoadApplySettings();
            CSClockForm.GUI_Load();
        }

        public static void LoadNotifyIcon()
        {
            if (notifyIcon1 != null)
            {
                notifyIcon1.Dispose();
            }
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = Properties.Resources.Logo;
            notifyIcon1.DoubleClick += ShowGUI;
            contextMenu1 = new ContextMenu();
            contextMenu1.MenuItems.Add(rm_GUI.GetString("showGUI"), new EventHandler(ShowGUI));
            if (Properties.Settings.Default.timerEnabled)
            {
                contextMenu1.MenuItems.Add(rm_GUI.GetString("pauseResume"), new EventHandler(PauseResumeTimer));
            }
            contextMenu1.MenuItems.Add(rm_GUI.GetString("quit"), new EventHandler(Quit));
            notifyIcon1.ContextMenu = contextMenu1;

            if (!debug)
            {
                notifyIcon1.Text = "CSClock";
            }
            else
            {
                notifyIcon1.Text = "CSClock DEBUG";
            }

            notifyIcon1.Visible = true;

            notifyIcon1.DoubleClick += new EventHandler(ShowGUI);
            notifyIcon1.BalloonTipClicked += new EventHandler(BalloonClickEvent);
        }

        static void ShowGUI(object sender, EventArgs e)
        {
            if (CSClockForm != null)
            {
                CSClockForm.Show();
                CSClockForm.BringToFront();
            }
            else
            {
                CSClockForm = new CSClock();
                CSClockForm.Show();
            }
        }

        static void BalloonClickEvent(object sender, EventArgs e)
        {
            switch (balloonClickAction)
            {
                case BalloonClickActions.SetOvertime:
                    setOvertime = new SetOvertime();
                    setOvertime.TopMost = CSClockForm.TopMost;
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
            if (CSClockForm != null)
                updateGUI = true;
            else
                updateGUI = false;

            if (CSClockForm.timer.Enabled)
            {
                CSClockForm.PauseTimer(updateGUI);
                notifyIcon1.ShowBalloonTip(2500, "CSClock", rm_Messages.GetString("timerPausedNotification_text"), ToolTipIcon.Info);
            }
            else
            {
                CSClockForm.ResumeTimer(updateGUI);
                notifyIcon1.ShowBalloonTip(2500, "CSClock", rm_Messages.GetString("timerResumedNotification_text"), ToolTipIcon.Info);
            }
        }

        static void Quit(object sender, EventArgs e)
        {
            if (MessageBox.Show(rm_Messages.GetString("quitConfirmationDialog_text"), "CSClock", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Properties.Settings.Default.properExit = true;
                Properties.Settings.Default.Save();
                Application.Exit();
            }
        }
    }
}
