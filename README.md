# CSClock DEV
**DEVELOPMENT BRANCH - THIS BRANCH CONTAINS UNFINISHED FUNCTIONS. THE APPLICATION MAY OR MAY NOT WORK. DO NOT DOWNLOAD DEVELOPMENT BUILDS FOR USAGE.**   
   
**A C# application which keeps track of the time you spend on your computer, by setting limits. It will then alert you when your computer time is out. Note that CSClock doesn't *prevent* you from using the computer after the limit is reached, but it *alerts* you.** 

If everything works as it should, CSClock should automatically update at next app start (if an Internet connection is available). You should not have to update manually. In some cases, the update will not be installed immediately due to caching. To update manually, download the latest CSClock.exe and replace the old CSClock.exe with the new one (%localappdata%\CSClock\CSClock.exe). **NOTE:** The portable version (if you only download CSClock.exe and run it) does NOT contain the automatic updater. The portable version also still stores the user data in the CSClock folder in local appdata.   
   
**3rd party libraries used:**  
- Json.NET by Newtonsoft - License is located in "3rd-party-licenses/Json.NET-LICENSE.md"  
- CustomSettingsProvider by CodeChimp, fixed and translated to C# by gpgemini  
  
**NEWS:**  
See NEWS.md  
   
**Development software:** Microsoft Visual Studio 2015 Community, upgrade to 2017 is planned later  
**Programming language:** C#  
**Program type:** Windows Forms Application (not Windows 10 universal app)  
**Font:** Segoe UI  
  
**PLEASE NOTE!**  
**CSClock is in alpha-state, and might contain major bugs!**  
  
**FEATURES:**  
- Easy and clean user interface  
- Overtime will deduct computer time the next day (experimental)  
- Automatic timer pause/resume on computer lock/unlock  
- You can add/subtract time from the time elapsed, if you for example left the computer unlocked, and the timer counted that time as   well. **NOTE!** Anyone having access to the computer can do this, so if you for example want to count your childrens' computer time, they can change the time elapsed.  
- Language support for both English and Swedish  
- Very easy to install
  
**SCREENSHOT(S):**  
![Alt text](https://github.com/steel9/CSClock/blob/master/Screenshots/screenshot1.PNG?raw=true "Main form")
  
**SYSTEM REQUIREMENTS:**
  - .NET Framework 4.5.2  
  - Windows PowerShell, used by the removal tool. You can use CSClock without Windows PowerShell, but you can't use the removal tool. 
  
**HOW TO INSTALL:**  
FOR INSTALLATION: Download Install.exe, open it, press 'More info' on the SmartScreen warning, then press 'Run anyway'.   
FOR NON-INSTALLATION: You can still just download and run CSClock.exe, but it does NOT auto update, and it still stores settings in its local appdata folder.

**HOW TO UNINSTALL:**  
**NOTE!** The removal tool won't work if you have parenthesis in the file name for CSClock.  
Option 1: Start CSClock with the parameter -removal (more info below under "Start parameters"), then confirm.  
Option 2: Open CSClock, press the gear, press "More", then press the removal button, then confirm.  
   
**START PARAMETERS:**   
To run CSClock with a start parameter, open the CSClock install folder (default in "%localappdata%\CSClock" (without quotation)), then SHIFT+RIGHT-CLICK on an empty location in the folder, then press "Open command prompt here" or something similar, then run CSClock with the parameter(s), for example "CSClock.exe -deletelogs" (without quotation), which removes the log files.   
   
-removal&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;| Extract & run the removal tool.   
-reset&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;| Reset the user data of CSClock.   
-deletelogs&nbsp;| Deletes the setup & application log files.   
-disup&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;| Disable automatic updates (not recommended, but it's for users who want to run old versions).   
-enup&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;| Enable automatic updates (default).   
&nbsp;  
&nbsp;  
**CONTACT:**  
steel9apps@gmail.com  
  
**KNOWN ISSUES:**  
**ISSUE 1:** CSClock might freeze, for example by pressing buttons in the about form. I believe this is caused by some software that conflicts with CSClock.  
  
**WHAT TO DO IF THESE ISSUES OCCUR:**  
**ISSUE 1:**  
**Windows 8.x or 10 systems:**  
1. Press CTRL+ALT+DEL.  
2. Select Task Manager.  
3. If there are no tabs in the task manager, press 'More information'.  
4. Navigate to the 'Information' tab.  
5. Locate 'CSClock.exe'  
6. Right click 'CSClock.exe' and select 'Kill process'. Confirm.  
**Windows 7 or earlier:**  
Do steps 1, 2, 5, 6  
  
**FEATURES TO BE ADDED:**  
- More unified icons (the button icons)  
- More supported languages  
- Statistics   
  
If you want, you can contribute! It is really appreciated!
