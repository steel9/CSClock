﻿/*
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

namespace OnlineSetup
{
    static class Program
    {
        const string className = "Program.cs";

        static string installFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        static string exePath = Path.Combine(installFolder, "CSClock.exe");

        static string logPath = Path.Combine(installFolder, "setuplog.txt");

        static string startupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock.lnk");
        static string startmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock.lnk");

        static Logger logger = null;

        public static void Main(string[] args)
        {
            if (!Directory.Exists(installFolder))
            {
                Directory.CreateDirectory(installFolder);
            }
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }
            logger = new Logger("CSClock Online Setup", logPath, Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);
            if (args == null || args.Length == 0)
            {
                Install();
            }
            else if (args.Contains("-update"))
            {
                Update();
            }
            return;
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

        static void Install()
        {
            logger.Log("--INSTALLATION--", className, Logger.LogType.Info);

            //Check for Internet connection
            if (!CheckForInternetConnection())
            {
                logger.Log("No internet connection available, aborting installation", className, Logger.LogType.Info);
                MessageBox.Show("Network connection is needed to download CSClock", "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Download CSClock
            logger.Log("Downloading CSClock.exe", className, Logger.LogType.Info);
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", exePath);
            }
            catch (Exception ex)
            {
                logger.Log("Download error: " + ex.ToString(), className, Logger.LogType.Error);
                MessageBox.Show("Error when downloading CSClock: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Install updater
            logger.Log("Installing updater", className, Logger.LogType.Info);
            File.Copy(Application.ExecutablePath, Path.Combine(installFolder, "Updater.exe"), true);

            //Make shortcuts
            try
            {
                logger.Log("Creating startup shortcut", className, Logger.LogType.Info);
                CreateShortcut(exePath, startupShortcutPath, Path.GetDirectoryName(exePath), "-s -np");
                logger.Log("Creating start menu shortcut", className, Logger.LogType.Info);
                CreateShortcut(exePath, startmenuShortcutPath, Path.GetDirectoryName(exePath), "-np");
            }
            catch (MissingMethodException ex)
            {
                logger.Log("Error while creating shortcuts: " + ex.ToString(), className, Logger.LogType.Error);
                MessageBox.Show("Error while creating shortcuts" +
                    "Your .NET version does not support this function", "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                logger.Log("Error while creating shortcuts: " + ex.ToString(), className, Logger.LogType.Error);
                MessageBox.Show("Error while creating shortcuts: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Start CSClock
            logger.Log("Starting CSClock", className, Logger.LogType.Info);
            Process.Start(exePath);
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
            logger.Log("--UPDATE--", className, Logger.LogType.Info);

            //Check for Internet connection
            if (!CheckForInternetConnection())
            {
                logger.Log("No internet connection available, aborting update", className, Logger.LogType.Info);
                return;
            }

            //Check if update is needed
            logger.Log("Initializing WebClient", className, Logger.LogType.Info);
            WebClient webClient = new WebClient();
            logger.Log("Current version without dots --> int", className, Logger.LogType.Info);
            FileVersionInfo currVer = FileVersionInfo.GetVersionInfo(exePath);
            int currentVersion = int.Parse(string.Format("{0}{1}{2}", currVer.FileMajorPart.ToString(), currVer.FileMinorPart.ToString(),
                currVer.FileBuildPart.ToString()));
            logger.Log("Downloading VERSION file from master branch", className, Logger.LogType.Info);
            string latestVersionText = null;
            try
            {
                latestVersionText = webClient.DownloadString("https://raw.githubusercontent.com/steel9/CSClock/master/VERSION");
            }
            catch (Exception ex)
            {
                logger.Log("Error while downloading VERSION file from master branch, aborting update. Error: " + ex.ToString(), className, Logger.LogType.Error);
                return;
            }
            logger.Log("Parsing version", className, Logger.LogType.Info);
            string latestVersion_s = latestVersionText.Split('#')[0];
            logger.Log("Current version is: " + currentVersion, className, Logger.LogType.Info);
            logger.Log("Latest version is: " + latestVersion_s, className, Logger.LogType.Info);
            int latestVersion = int.Parse(latestVersion_s);

            if (currentVersion >= latestVersion)
            {
                //Update is not needed
                logger.Log("Update is NOT needed. Current version: '" + currentVersion.ToString() + "', latest version: '" +
                    latestVersion.ToString() + "'", className, Logger.LogType.Info);
                return;
            }

            //Close CSClock
            logger.Log("Closing CSClock", className, Logger.LogType.Info);
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
                logger.Log("NullReferenceException occurred, probably because CSClock is not running", className, Logger.LogType.Warning);
            }
            catch (Exception ex)
            {
                logger.Log("Error when closing CSClock, aborting update. Error: " + ex.ToString(), className, Logger.LogType.Error);
            }


            //Update CSClock
            logger.Log("Downloading latest CSClock.exe", className, Logger.LogType.Info);
            try
            {
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", exePath);
            }
            catch (Exception ex)
            {
                logger.Log("Error when downloading CSClock, aborting update. Error: " + ex.ToString(), className, Logger.LogType.Info);
            }

            //Start CSClock
            logger.Log("Starting CSClock", className, Logger.LogType.Info);
            Process.Start(exePath);
        }
    }
}