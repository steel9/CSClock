using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;
using System.Linq;

namespace OnlineSetup
{
    public static class Program
    {
        static string installFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        static string exePath = Path.Combine(installFolder, "CSClock.exe");

        static string startupShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "CSClock.lnk");
        static string startmenuShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "CSClock.lnk");

        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Install();
            }
            else if (args.Contains("-update"))
            {
                Update();
            }
        }

        static void Install()
        {
            //Install CSClock
            if (!Directory.Exists(installFolder))
            {
                Directory.CreateDirectory(installFolder);
            }

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when downloading CSClock: " + ex.Message, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Install updater
            File.Copy(Application.ExecutablePath, Path.Combine(installFolder, "Updater.exe"), true);

            //Make shortcuts
            try
            {
                CreateShortcut(Application.ExecutablePath, startupShortcutPath, Path.GetDirectoryName(Application.ExecutablePath), "-s");
                CreateShortcut(Application.ExecutablePath, startmenuShortcutPath, Path.GetDirectoryName(Application.ExecutablePath));
            }
            catch (MissingMethodException ex)
            {
                MessageBox.Show("Error while creating shortcuts" +
                    "Your .NET version does not support this function", "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating shortcuts: " + ex.Message, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Start CSClock
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
            //Check if update is needed
            WebClient webClient = new WebClient();
            int currentVersion = int.Parse(Application.ProductVersion.Replace(".", ""));
            string latestVersionText = webClient.DownloadString("https://raw.githubusercontent.com/steel9/CSClock/unstable/VERSION"); //should not be /unstable/
            string latestVersion_s = latestVersionText.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
            int latestVersion = int.Parse(latestVersion_s);

            if (currentVersion >= latestVersion)
            {
                //Update is not needed
                return;
            }

            //Close CSClock
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
            catch (NullReferenceException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Error when closing CSClock: " + ex.Message, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Update CSClock
            try
            {
                webClient.DownloadFile("https://github.com/steel9/CSClock/raw/master/CSClock.exe", exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when downloading CSClock: " + ex.Message, "CSClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Start CSClock
            Process.Start(exePath);
        }
    }
}
