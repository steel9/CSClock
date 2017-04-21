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
using Microsoft.Win32;
using System.Resources;
using System.Reflection;

namespace OnlineSetup
{
    static class Program
    {
        const string className = "Program.cs";

        public static string selectedLanguage = "English";
        static ResourceManager rm_Messages;

        public static bool antiExit = true;

        public static bool dev = false;
        public static bool portable = false;

        static string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");

        static string exePath;

        static string setupExePath;

        static string tempCSClockPath = Path.Combine(Path.GetTempPath(), "CSClock");
        static string tempLibPath = Path.Combine(tempCSClockPath, "lib");
        static string tempExePath = Path.Combine(tempCSClockPath, "CSClock.exe");

        static string logPath;

        static string startupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock.lnk");
        static string startmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock.lnk");

        static string devStartupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock Dev.lnk");
        static string devStartmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock Dev.lnk");

        public static Logger logger = null;

        public static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            rm_Messages = new ResourceManager(string.Format("OnlineSetup.Languages.{0}.Messages", selectedLanguage), assembly);

            if (!Directory.Exists(installDir))
            {
                Directory.CreateDirectory(installDir);
            }
            if (!Directory.Exists(tempCSClockPath))
            {
                Directory.CreateDirectory(tempCSClockPath);
            }
            if (!Directory.Exists(tempLibPath))
            {
                Directory.CreateDirectory(tempLibPath);
            }

            if (args != null && args.Length > 0)
            {
                if (args.Contains("-dev") && !args.Contains("-np"))
                {
                    dev = true;
                    installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock", "dev");
                }
                else if (!args.Contains("-np"))
                {
                    portable = true;
                    installDir = Path.GetDirectoryName(Application.ExecutablePath);
                }
            }
            exePath = Path.Combine(installDir, "CSClock.exe");
            setupExePath = Path.Combine(installDir, "Setup.exe");
            logPath = Path.Combine(installDir, "log.txt");

            if (File.Exists(logPath))
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
                if (!Directory.Exists(Path.Combine(installDir, "dev")))
                {
                    Directory.CreateDirectory(Path.Combine(installDir, "dev"));
                }
            }
            logger = new Logger("CSClock Online Setup", logPath, Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);

            if (args == null || args.Length == 0 || (!args.Contains("-update") && !args.Contains("-uninstall")))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Install installForm = new Install();
                Application.Run(installForm);
            }
            else if (args.Contains("-update"))
            {
                try
                {
                    Update();
                }
                catch (Exception ex)
                {
                    logger.Log(className, "Update error: " + ex.ToString(), Logger.LogType.Error);
                    MessageBox.Show("Error while updating CSClock, see log.txt for more details. Error: " + ex.Message, "CSClock", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else if (args.Contains("-uninstall"))
            {
                try
                {
                    Uninstall();
                }
                catch (Exception ex)
                {
                    logger.Log(className, "Uninstall error: " + ex.ToString(), Logger.LogType.Error);
                    MessageBox.Show("Error while uninstalling CSClock, see log.txt for more details. Error: " + ex.Message, "CSClock", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
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

        static void CreateUninstallerReg()
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
                        if (!dev)
                        {
                            key.SetValue("DisplayName", "CSClock");
                        }
                        else
                        {
                            key.SetValue("DisplayName", "CSClock DEV");
                        }
                        key.SetValue("ApplicationVersion", v.ToString());
                        key.SetValue("Publisher", "steel9");
                        key.SetValue("DisplayIcon", exePath);
                        key.SetValue("DisplayVersion", v.ToString(3));
                        key.SetValue("URLInfoAbout", "https://steel9apps.wixsite.com/csclock");
                        key.SetValue("Contact", "steel9apps@gmail.com");
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", setupExePath + " -uninstall");
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
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the application or the command line.",
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

            WebClient webClient = new WebClient();

            logger.Log(className, "Downloading licenses", Logger.LogType.Info);
            string licensesDir = Path.Combine(installDir, "licenses");
            if (!Directory.Exists(licensesDir))
            {
                Directory.CreateDirectory(licensesDir);
            }
            webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/LICENSE", Path.Combine(licensesDir, "CSClock-LICENSE"));
            webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/3rd-party-licenses/Json.NET-LICENSE.md", Path.Combine(licensesDir,
                    "Json.NET-LICENSE.md"));
            webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/3rd-party-licenses/CustomSettingsProvider-LICENSE.htm",
                Path.Combine(licensesDir, "CustomSettingsProvider-LICENSE.htm"));

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

            logger.Log(className, "Downloading libraries", Logger.LogType.Info);
            try
            {
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/lib/Newtonsoft.Json.dll",
                    Path.Combine(tempLibPath, "Newtonsoft.Json.dll"));
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when downloading libraries, aborting update. Error: " + ex.ToString(), Logger.LogType.Info);
                return;
            }

            //Install CSClock
            logger.Log(className, "Installing CSClock", Logger.LogType.Info);
            try
            {
                File.Copy(tempExePath, exePath, true);
                File.Delete(tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log("CSClock installation error: " + ex.ToString(), className, Logger.LogType.Error);
                MessageBox.Show("Error when installing CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Install libraries
            logger.Log(className, "Installing libraries", Logger.LogType.Info);
            try
            {
                File.Copy(Path.Combine(tempLibPath, "Newtonsoft.Json.dll"), Path.Combine(installDir, "Newtonsoft.Json.dll"), true);
                Directory.Delete(tempCSClockPath, true);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Libraries installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing libraries: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Directory.Delete(tempCSClockPath, true);
                return;
            }

            //Install updater
            logger.Log(className, "Installing updater", Logger.LogType.Info);
            try
            {
                if (!dev)
                {
                    File.Copy(Application.ExecutablePath, Path.Combine(installDir, "Setup.exe"), true);
                }
                else
                {
                    File.Copy(Application.ExecutablePath, Path.Combine(installDir, "dev", "Setup.exe"), true);
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
                    CreateShortcut(exePath, devStartupShortcutPath, Path.GetDirectoryName(exePath), "-s -np -dev");
                    logger.Log(className, "Creating start menu shortcut", Logger.LogType.Info);
                    CreateShortcut(exePath, devStartmenuShortcutPath, Path.GetDirectoryName(exePath), "-np -dev");
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

            CreateUninstallerReg();

            //Start CSClock
            logger.Log(className, "Starting CSClock", Logger.LogType.Info);
            if (!dev)
            {
                Process.Start(exePath, "-np");
            }
            else
            {
                Process.Start(exePath, "-np -dev");
            }

            antiExit = false;
            Application.Exit();
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

            WebClient webClient = new WebClient();

            logger.Log(className, "Downloading licenses", Logger.LogType.Info);
            string licensesDir = Path.Combine(installDir, "licenses");
            if (!Directory.Exists(licensesDir))
            {
                Directory.CreateDirectory(licensesDir);
            }
            if (!File.Exists(Path.Combine(licensesDir, "CSClock-LICENSE")))
            {
                webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/LICENSE", Path.Combine(licensesDir, "CSClock-LICENSE"));
            }
            if (!File.Exists(Path.Combine(licensesDir, "Json.NET-LICENSE.md")))
            {
                webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/3rd-party-licenses/Json.NET-LICENSE.md", Path.Combine(licensesDir,
                    "Json.NET-LICENSE.md"));
            }
            if (!File.Exists(Path.Combine(licensesDir, "CustomSettingsProvider-LICENSE.htm")))
            {
                webClient.DownloadFile("https://raw.githubusercontent.com/steel9/CSClock/master/3rd-party-licenses/CustomSettingsProvider-LICENSE.htm",
                    Path.Combine(licensesDir, "CustomSettingsProvider-LICENSE.htm"));
            }


            //Check if update is needed
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
                logger.Log(className, "Error while downloading VERSION2 file from master branch, updating anyway. Error: " + ex.ToString(), Logger.LogType.Error);
                goto Update;
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


            Update:

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

            logger.Log(className, "Downloading libraries", Logger.LogType.Info);
            try
            {
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/lib/Newtonsoft.Json.dll",
                    Path.Combine(tempLibPath, "Newtonsoft.Json.dll"));
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when downloading libraries, aborting update. Error: " + ex.ToString(), Logger.LogType.Info);
                return;
            }

            //Install CSClock
            logger.Log(className, "Installing CSClock", Logger.LogType.Info);
            try
            {
                File.Copy(tempExePath, exePath, true);
            }
            catch (Exception ex)
            {
                logger.Log(className, "CSClock installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Directory.Delete(tempCSClockPath);
                return;
            }

            //Install libraries
            logger.Log(className, "Installing libraries", Logger.LogType.Info);
            try
            {
                File.Copy(Path.Combine(tempLibPath, "Newtonsoft.Json.dll"), Path.Combine(installDir, "Newtonsoft.Json.dll"), true);
                Directory.Delete(tempCSClockPath);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Libraries installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing libraries: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Directory.Delete(tempCSClockPath);
                return;
            }

            if (!portable)
            {
                //Add uninstaller to uninstall list in Windows if not added
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\CSClock"))
                {
                    if (key == null)
                    {
                        CreateUninstallerReg();
                    }
                }
            }

            //Start CSClock
            logger.Log(className, "Starting CSClock", Logger.LogType.Info);
            Process.Start(exePath);
        }

        private static void RemoveUninstallerFromReg()
        {
            Guid uninstallGuid = new Guid("924c5816-7549-4556-ac2f-f1ab1af211b3");
            const string uninstallRegKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(uninstallRegKeyPath, true))
            {
                if (key == null)
                {
                    return;
                }
                try
                {
                    string guidText = uninstallGuid.ToString("B");
                    RegistryKey child = key.OpenSubKey(guidText);
                    if (child != null)
                    {
                        child.Close();
                        key.DeleteSubKey(guidText);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred removing uninstall information from the registry.  The service was uninstalled will still show up in the add/remove program list.  To remove it manually delete the entry HKLM\\" +
                        uninstallRegKeyPath + "\\" + uninstallGuid, ex);
                }
            }
        }

        static void Uninstall(bool confirmMsg = true)
        {
            if (!confirmMsg || MessageBox.Show(rm_Messages.GetString("removalQuestion"), "CSClock",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                logger.Log(className, "--UNINSTALL--", Logger.LogType.Info);
                if (dev)
                {
                    logger.Log(className, "-DEV-", Logger.LogType.Info);
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
                    logger.Log(className, "Error when closing CSClock, aborting uninstallation. Error: " + ex.ToString(), Logger.LogType.Error);
                    return;
                }

                if (!dev)
                {
                    File.Delete(startupShortcutPath);
                    File.Delete(startmenuShortcutPath);

                    File.Delete(Path.Combine(installDir, "CSClock.exe"));
                    File.Delete(Path.Combine(installDir, "log.txt"));
                    File.Delete(Path.Combine(installDir, "setuplog.txt"));
                    File.Delete(Path.Combine(installDir, "updupdaterlog.txt"));
                    File.Delete(Path.Combine(installDir, "exc.txt"));
                    File.Delete(Path.Combine(installDir, "statistics.xml"));

                    RemoveUninstallerFromReg();
                }
                else
                {
                    File.Delete(devStartupShortcutPath);
                    File.Delete(devStartmenuShortcutPath);

                    File.Delete(Path.Combine(installDir, "dev", "CSClock.exe"));
                    File.Delete(Path.Combine(installDir, "dev", "log.txt"));
                    File.Delete(Path.Combine(installDir, "dev", "setuplog.txt"));
                    File.Delete(Path.Combine(installDir, "dev", "updupdaterlog.txt"));
                    File.Delete(Path.Combine(installDir, "dev", "exc.txt"));
                    File.Delete(Path.Combine(installDir, "dev", "statistics.xml"));

                    if (!File.Exists(Path.Combine(installDir, "CSClock.exe")))
                    {
                        RemoveUninstallerFromReg();
                    }
                }

                MessageBox.Show("CSClock is now uninstalled (almost). Press OK to finish", "CSClock Setup", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                string finisherScriptPath = Path.Combine(Path.GetTempPath(), "CSClockRemovalFinish.bat");
                StreamWriter sw = new StreamWriter(finisherScriptPath);
                sw.Write(Properties.Resources.removalfinisher);
                sw.Close();

                Process.Start(finisherScriptPath);
            }
        }
    }
}
