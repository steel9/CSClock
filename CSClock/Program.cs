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

namespace CSClock
{
    static class Program
    {
        private const string className = "Program.cs";

        public static string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool debug = false;
        public static bool dev = false;

        public static Logger logger;

        public static CSClock CSClockForm;
        public static Configure configureForm;
        public static NotifyIcon notifyIcon1;
        public static SetOvertime setOvertime;
        public static AddSubtractTime addSubtractTime;
        public static BalloonClickActions balloonClickAction = BalloonClickActions.None;
        public static Statistics statForm;

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

        private static bool portable = true;

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

            if (args != null && args.Length > 0 && args.Contains("-np"))
            {
                portable = false;
            }
            else
            {
                if (!Properties.Settings.Default.portableWarnHasAppeared)
                {
                    MessageBox.Show("You are running the (partly) portable version of CSClock. The portable version does not auto-update. To use the auto-updater, " +
                        "please download CSClock with the installer, or download Install.exe, depending on where you download it. If you are using the installed " +
                        "version but this message appears, it might be because you have not launched CSClock.exe with the start parameter \"-np\" (without quotation)." +
                        "\r\nThis message will not appear again", "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Properties.Settings.Default.portableWarnHasAppeared = true;
                    Properties.Settings.Default.Save();
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

            logger = new Logger("CSClock", Path.Combine(logDir, "log.txt"),
                Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);

            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "CSClock", out createdNew))
            {
                if (createdNew || (args != null && args.Contains("-ignorerunning")))
                {
                    if (args != null && args.Length > 0)
                    {
                        if (args.Contains("-dev"))
                        {
                            dev = true;
                        }

                        if (!portable && !debug && !args.Contains("-disup") && Properties.Settings.Default.autoUpdate)
                        {
                            UpdUpdater.UpdUpdate();
                            AppUpdate();
                        }
                    }

                    if (!dev)
                    {
                        logger.Log(className, "createdNew=true", Logger.LogType.Info, true);
                    }
                    else
                    {
                        logger.Log(className, "createdNew=true, dev=true", Logger.LogType.Info, true);
                    }
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

        static void AppUpdate()
        {
            try
            {
                string updaterPath;
                if (!dev)
                {
                    updaterPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "AppUpdater.exe");
                }
                else
                {
                    updaterPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "dev", "AppUpdater.exe");
                }
                ProcessStartInfo start =
                    new ProcessStartInfo();
                start.FileName = updaterPath;
                if (!dev)
                {
                    start.Arguments = "-update";
                }
                else
                {
                    start.Arguments = "-update -dev";
                }
                start.WindowStyle = ProcessWindowStyle.Hidden;
                var updateProc = Process.Start(start);
                updateProc.WaitForExit();
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when running auto-updater at app launch: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when running auto-updater: " + ex.Message + "\r\n\r\nSee '" + Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), @"CSClock\log.txt") + "' for more details", "CSClock",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void StartApplication(string[] args)
        {
            assembly = Assembly.GetExecutingAssembly();


            logger.Log(className, "Starting CSClock", Logger.LogType.Info);

            if (Properties.Settings.Default.upgradeRequired && ((args == null && args.Length > 0) || !args.Contains("-donotupgradesettings")))
            {
                logger.Log(className, "Upgrading user settings...", Logger.LogType.Info);

                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.upgradeRequired = false;
                Properties.Settings.Default.Save();

                logger.Log(className, "Upgrade done", Logger.LogType.Info);
            }

            selectedLanguage = Properties.Settings.Default.selectedLanguage;
            rm_Messages = new ResourceManager(string.Format("CSClock.Languages.{0}.Messages", selectedLanguage), assembly);
            rm_GUI = new ResourceManager(string.Format("CSClock.Languages.{0}.GUI", selectedLanguage), assembly);

            if (args != null && args.Length > 0)
            {
                if (args.Contains("-disup"))
                {
                    if (MessageBox.Show("Disabling auto-update is NOT recommended, as CSClock is in alpha-state, which might mean BUGS. If you turn off automatic updates, you will NOT get bug fixes and improvements. CSClock will also only auto-update at application launch (when auto-updates are enabled). Are you sure you want to disable auto-updating?",
                        "CSClock", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Properties.Settings.Default.autoUpdate = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (args.Contains("-enup"))
                {
                    Properties.Settings.Default.autoUpdate = true;
                    Properties.Settings.Default.Save();

                    //Update
                    UpdUpdater.UpdUpdate();
                    AppUpdate();
                }

                if (args.Contains("-removal"))
                {
                    Removal(true);
                    return;
                }

                if (args.Contains("-reset"))
                {
                    logger.Log(className, "Resetting CSClock", Logger.LogType.Info);

                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.upgradeRequired = false;
                    Properties.Settings.Default.Save();

                    logger.Log(className, "Reset completed, closing CSClock", Logger.LogType.Info);

                    if (!args.Contains("-deletelogs") || (!File.Exists("log.txt") && !File.Exists("setuplog.txt")))
                    {
                        return;
                    }
                }

                if (args.Contains("-deletelogs") && (File.Exists("log.txt") || File.Exists("setuplog.txt")))
                {
                    File.Delete("log.txt");
                    File.Delete("setuplog.txt");
                    return;
                }
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
                Application.Exit();
                return;
            }

            if (args != null && args.Contains("-s"))
            {
                CSClockForm.startMinimized = true;
            }

            Application.Run(CSClockForm);
        }

        public static void Removal(bool confirmMsg)
        {
            if (!confirmMsg || MessageBox.Show(rm_Messages.GetString("removalQuestion"), "CSClock",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string path = Path.Combine(Path.GetTempPath(), "CSClockRemovalTool.bat");

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
                Application.Exit();
            }
        }
    }
}
