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
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CSClock
{
    static class UpdUpdater
    {
        private const string className = "UpdUpdater.cs";

        static string installFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        static string exePath = Path.Combine(installFolder, "AppUpdater.exe");
        static string tempExePath = Path.Combine(Path.GetTempPath(), "AppUpdater.exe");

        static string logPath = Path.Combine(installFolder, "updupdaterlog.txt");

        static Logger logger = null;

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

        public static void UpdUpdate()
        {
            logger = new Logger("CSClock App Updater Updater", logPath, Logger.LogTimeDateOptions.YearMonthDayHourMinuteSecond, true);

            logger.Log(className, "--UPDUPDATE (UPDATER UPDATE)--", Logger.LogType.Info);

            //Check for Internet connection
            if (!CheckForInternetConnection())
            {
                logger.Log(className, "No internet connection available, aborting update", Logger.LogType.Info);
                return;
            }

            //Check if update is needed
            WebClient webClient = new WebClient();
            FileVersionInfo currVer;
            try
            {
                currVer = FileVersionInfo.GetVersionInfo(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while getting current updater version: " + ex.Message, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(className, "Error while getting current updater version: " + ex.ToString(), Logger.LogType.Error);
                currVer = null;
                goto Update;
            }
            decimal currentVersion = decimal.Parse(string.Format("{0}.{1}{2}", currVer.FileMajorPart.ToString(), currVer.FileMinorPart.ToString(),
                currVer.FileBuildPart.ToString()), CultureInfo.InvariantCulture);
            logger.Log(className, "Downloading VERSION file from master branch", Logger.LogType.Info);
            string latestVersionText = null;
            try
            {
                 latestVersionText = webClient.DownloadString("https://raw.githubusercontent.com/steel9/CSClock/master/VERSION2");
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error while downloading VERSION file from master branch, aborting update. Error: " + ex.ToString(), Logger.LogType.Error);
                return;
            }
            logger.Log(className, "Parsing version", Logger.LogType.Info);
            string latestVersion_s = latestVersionText.Split(',', '#')[1];
            decimal latestVersion = decimal.Parse(latestVersion_s);
            logger.Log(className, "Current version is: " + currentVersion.ToString(), Logger.LogType.Info);
            logger.Log(className, "Latest version is: " + latestVersion.ToString(), Logger.LogType.Info);

            if (currentVersion >= latestVersion)
            {
                //Update is not needed
                logger.Log(className, "Update is NOT needed. Current version: '" + currentVersion.ToString() + "', latest version: '" +
                    latestVersion.ToString() + "'", Logger.LogType.Info);
                return;
            }

            Update:
            //Close CSClock
            logger.Log("Closing updater", className, Logger.LogType.Info);
            Process[] processes;
            string procName = "Updater";
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
                logger.Log(className, "Error when closing app updater, aborting updater update. Error: " + ex.ToString(), Logger.LogType.Error);
            }


            //Update CSClock
            logger.Log(className, "Downloading latest Install.exe", Logger.LogType.Info);
            try
            {
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/Install.exe", tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log(className, "Error when downloading CSClock, aborting update. Error: " + ex.ToString(), Logger.LogType.Info);
            }

            //Install CSClock
            logger.Log(className, "Installing UpdUpdater", Logger.LogType.Info);
            try
            {
                File.Copy(tempExePath, exePath, true);
                File.Delete(tempExePath);
            }
            catch (Exception ex)
            {
                logger.Log(className, "UpdUpdater installation error: " + ex.ToString(), Logger.LogType.Error);
                MessageBox.Show("Error when installing UpdUpdater: " + ex.Message, "CSClock Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

