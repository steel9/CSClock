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
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;
using System.Linq;
using CSClock;
using System.Globalization;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace OnlineSetup
{
    static class Program
    {
        const string className = "Program.cs";

        public static bool dev = false;

        static string installFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");

        static string exePath = Path.Combine(installFolder, "CSClock.exe");
        static string devExePath = Path.Combine(installFolder, "dev", "CSClock.exe");

        static string tempExePath = Path.Combine(Path.GetTempPath(), "CSClock.exe");

        static string logPath = Path.Combine(installFolder, "setuplog.txt");
        static string devLogPath = Path.Combine(installFolder, "dev", "setuplog.txt");

        static string startupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock.lnk");
        static string startmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock.lnk");

        static string devStartupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock Dev.lnk");
        static string devStartmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock Dev.lnk");

        static Logger logger = null;

        public static void Main(string[] args)
        {
            /*
            if (args == null || args.Length == 0 || !args.Contains("-update"))
            {
                if (!IsAdmin() && MessageBox.Show("The setup requires administrator permissions to write the uninstaller to the registry. A warning will be displayed" +
                " saying that the developer is unknown, but that is because it costs money to be a verified publisher. Press OK now, and then 'Yes' on the warning" +
                " to install CSClock", "CSClock", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    ProcessStartInfo start = new ProcessStartInfo(Application.ExecutablePath);
                    start.Verb = "runas";
                    Process.Start(start);
                    return;
                }
                else if (!IsAdmin())
                {
                    return;
                }
            }
            */

            if (!Directory.Exists(installFolder))
            {
                Directory.CreateDirectory(installFolder);
            }

            if (args != null && args.Length > 0 && args.Contains("-dev"))
            {
                dev = true;
            }

            if (dev && File.Exists(devLogPath))
            {
                File.Delete(devLogPath);
            }
            else if (!dev && File.Exists(logPath))
            {
                File.Delete(logPath);
            }  

            if (dev)
            {
                if (!File.Exists("CSClock.exe"))
                {
                    MessageBox.Show("Please run the installer from the Build folder with CSClock.exe inside", "CSClock", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (!Directory.Exists(Path.Combine(installFolder, "dev")))
                {
                    Directory.CreateDirectory(Path.Combine(installFolder, "dev"));
                }

                logger = new Logger("CSClock Online Setup", devLogPath, Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);
            }
            else
            {
                logger = new Logger("CSClock Online Setup", logPath, Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);
            }

            if (args == null || args.Length == 0 || !args.Contains("-update"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Install installForm = new Install();
                installForm.Show();
            }
            else if (args.Contains("-update"))
            {
                Update();
            }
        }

        private static bool IsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static void Install()
        {
            logger.Log(className, "--INSTALLATION--", Logger.LogType.Info);
            if (dev)
            {
                logger.Log(className, "-DEV-", Logger.LogType.Info);
            }

            //Check for Internet connection
            if (!CheckForInternetConnection())
            {
                logger.Log("No internet connection available, aborting installation", className, Logger.LogType.Info);
                MessageBox.Show("Network connection is needed to download CSClock", "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Download CSClock
            if (!dev)
            {
                logger.Log(className, "Downloading CSClock.exe", Logger.LogType.Info);
            }
            else
            {
                logger.Log(className, "Copying CSClock.exe", Logger.LogType.Info);
            }

            try
            {
                if (!dev)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", tempExePath);
                }
                else
                {
                    File.Copy("CSClock.exe", tempExePath, true);
                }
            }
            catch (Exception ex)
            {
                if (!dev)
                {
                    logger.Log(className, "Download error: " + ex.ToString(), Logger.LogType.Error);
                    MessageBox.Show("Error when downloading CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    logger.Log(className, "Copy error: " + ex.ToString(), Logger.LogType.Error);
                    MessageBox.Show("Error when copying CSClock.exe from bin to temp dir: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            //Install CSClock
            logger.Log(className, "Installing CSClock", Logger.LogType.Info);
            try
            {
                if (!dev)
                {
                    File.Copy(tempExePath, exePath, true);
                }
                else
                {
                    File.Copy(tempExePath, devExePath, true);
                }
                File.Delete(tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log("CSClock installation error: " + ex.ToString(), className, Logger.LogType.Error);
                MessageBox.Show("Error when installing CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Install updater
            logger.Log(className, "Installing updater", Logger.LogType.Info);
            try
            {
                if (!dev)
                {
                    File.Copy(Application.ExecutablePath, Path.Combine(installFolder, "Setup.exe"), true);
                }
                else
                {
                    File.Copy(Application.ExecutablePath, Path.Combine(installFolder, "dev", "Setup.exe"), true);
                }
            }
            catch (Exception ex)
            {
                logger.Log(className, "Updater installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing updater: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Make shortcuts
            try
            {
                if (!dev)
                {
                    logger.Log(className, "Creating startup shortcut", Logger.LogType.Info);
                    CreateShortcut(exePath, startupShortcutPath, Path.GetDirectoryName(exePath), "-s -np");
                    logger.Log(className, "Creating start menu shortcut", Logger.LogType.Info);
                    CreateShortcut(exePath, startmenuShortcutPath, Path.GetDirectoryName(exePath), "-np");
                }
                else
                {
                    logger.Log(className, "Creating startup shortcut", Logger.LogType.Info);
                    CreateShortcut(exePath, devStartupShortcutPath, Path.GetDirectoryName(devExePath), "-s -np -dev");
                    logger.Log(className, "Creating start menu shortcut", Logger.LogType.Info);
                    CreateShortcut(exePath, devStartmenuShortcutPath, Path.GetDirectoryName(devExePath), "-np -dev");
                }
            }
            catch (MissingMethodException ex)
            {
                logger.Log(className, "Error while creating shortcuts: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error while creating shortcuts" +
                    "Your .NET version does not support this function", "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error while creating shortcuts: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error while creating shortcuts: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CreateUninstaller();

            //Start CSClock
            logger.Log(className, "Starting CSClock", Logger.LogType.Info);
            if (!dev)
            {
                Process.Start(exePath, "-np");
            }
            else
            {
                Process.Start(devExePath, "-np -dev");
            }
        }

        static void CreateUninstaller()
        {
            Guid uninstallGuid = new Guid("924c5816-7549-4556-ac2f-f1ab1af211b3");
            const string uninstallRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";

            using (RegistryKey parent = Registry.CurrentUser.OpenSubKey(
                         uninstallRegKeyPath, true))
            {
                if (parent == null)
                {
                    throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = uninstallGuid.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);

                        if (key == null)
                        {
                            throw new Exception(string.Format("Unable to create uninstaller '{0}\\{1}'", uninstallRegKeyPath,
                                guidText));
                        }

                        Version v = new Version(FileVersionInfo.GetVersionInfo(exePath).ProductVersion);
                        string exe;

                        if (!dev)
                        {
                            exe = exePath;
                            key.SetValue("DisplayName", "CSClock");
                        }
                        else
                        {
                            key.SetValue("DisplayName", "CSClock DEV");
                            exe = devExePath;
                        }
                        key.SetValue("ApplicationVersion", v.ToString());
                        key.SetValue("Publisher", "steel9");
                        key.SetValue("DisplayIcon", exe);
                        key.SetValue("DisplayVersion", v.ToString(2));
                        key.SetValue("URLInfoAbout", "https://steel9apps.wixsite.com/csclock");
                        key.SetValue("Contact", "steel9apps@gmail.com");
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", exe + " -removal");
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }
            }
        }

        static void CreateShortcut(string filePath, string shortcutPath,
            string workingDir, string arguments = "", string description = "", string hotkey = "")
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            string shortcutAddress = shortcutPath;
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutAddress);
            if (description != "")
            {
                shortcut.Description = description;
            }
            if (hotkey != "")
            {
                shortcut.Hotkey = hotkey;
            }
            if (arguments != "")
            {
                shortcut.Arguments = arguments;
            }
            shortcut.TargetPath = filePath;
            shortcut.WorkingDirectory = workingDir;
            shortcut.Save();
        }

        static void Update()
        {
            logger.Log(className, "--APP UPDATE--", Logger.LogType.Info);
            if (dev)
            {
                logger.Log(className, "-DEV-", Logger.LogType.Info);
            }

            //Check for Internet connection
            if (!CheckForInternetConnection())
            {
                logger.Log(className, "No internet connection available, aborting update", Logger.LogType.Info);
                return;
            }

            //Check if update is needed
            WebClient webClient = new WebClient();
            FileVersionInfo currVer = FileVersionInfo.GetVersionInfo(exePath);
            Version currentVersion = new Version(string.Format("{0}.{1}.{2}", currVer.FileMajorPart.ToString(), currVer.FileMinorPart.ToString(),
                currVer.FileBuildPart.ToString()));
            if (!dev)
            {
                logger.Log(className, "Downloading VERSION2 file from master branch", Logger.LogType.Info);
            }
            else
            {
                logger.Log(className, "Downloading VERSION2 file from development branch", Logger.LogType.Info);
            }
            string latestVersionText = null;
            try
            {
                if (!dev)
                {
                    latestVersionText = webClient.DownloadString("https://raw.githubusercontent.com/steel9/CSClock/master/VERSION2");
                }
                else
                {
                    latestVersionText = webClient.DownloadString("https://raw.githubusercontent.com/steel9/CSClock/development/VERSION2");
                }
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error while downloading VERSION file from master branch, aborting update. Error: " + ex.ToString(), Logger.LogType.Error);
                return;
            }
            logger.Log(className, "Parsing version", Logger.LogType.Info);
            string latestVersion_s = latestVersionText.Split(',')[0];
            Version latestVersion = new Version(latestVersion_s);
            logger.Log(className, "Current version is: " + currentVersion.ToString(), Logger.LogType.Info);
            logger.Log(className, "Latest version is: " + latestVersion.ToString(), Logger.LogType.Info);

            if (currentVersion >= latestVersion)
            {
                //Update is not needed
                logger.Log(className, "Update is NOT needed. Current version: '" + currentVersion.ToString() + "', latest version: '" +
                    latestVersion.ToString() + "'", Logger.LogType.Info);
                return;
            }
            else if (dev)
            {
                logger.Log(className, "App update is available: " + latestVersion.ToString() + "\r\nAutomatic updates are not available in development builds", Logger.LogType.Info);
                MessageBox.Show("App update is available: " + latestVersion.ToString() + "\r\nAutomatic updates are not available in development builds", "CSClock",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Close CSClock
            logger.Log(className, "Closing CSClock", Logger.LogType.Info);
            Process[] processes;
            string procName = "CSClock";
            processes = Process.GetProcessesByName(procName);
            try
            {
                foreach (Process proc in processes)
                {
                    proc.CloseMainWindow();
                    proc.WaitForExit();
                }
            }
            catch (NullReferenceException)
            {
                logger.Log(className, "NullReferenceException occurred", Logger.LogType.Warning);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when closing CSClock, aborting update. Error: " + ex.ToString(), Logger.LogType.Error);
                return;
            }


            //Update CSClock
            logger.Log(className, "Downloading latest CSClock.exe", Logger.LogType.Info);
            try
            {
                 webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when downloading CSClock, aborting update. Error: " + ex.ToString(), Logger.LogType.Info);
                return;
            }

            //Install CSClock
            logger.Log("Installing CSClock", className, Logger.LogType.Info);
            try
            {
                File.Copy(tempExePath, exePath, true);
                File.Delete(tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log(className, "CSClock installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Start CSClock
            logger.Log(className, "Starting CSClock", Logger.LogType.Info);
            if (!dev)
            {
                Process.Start(exePath);
            }
            else
            {
                Process.Start(devExePath);
            }
        }
    }
}
