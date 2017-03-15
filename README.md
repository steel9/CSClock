# CSClock

**0.2.0-UNSTABLE. DO NOT DOWNLOAD FOR USAGE. FOR USAGE, PLEASE DOWNLOAD CSCLOCK FROM THE MASTER BRANCH, WHICH IS A LOT MORE STABLE THAN THIS UNSTABLE BRANCH.**

**A C# application which keeps track of the time you spend on your computer, by setting limits. It will then alert you when your computer time is out. Note that CSClock doesn't *prevent* you from using the computer after the limit is reached, but it *alerts* you.** 

0.2.0 news:
- Automatic updates
- CSClock now uses an installer. A portable version is planned before 0.2.0 release
- Finally, you can now fully minimize CSClock to the tray! It is not stuck in the taskbar anymore.

**The project was created in Visual Studio 2015.**
&nbsp;  
&nbsp;  
**It is recommended that you read the whole text before downloading CSClock.**
&nbsp;  
&nbsp;  
**PLEASE NOTE!**  
**CSClock is in alpha-state, and might contain major bugs!**  
  
**FEATURES:**  
- Easy and clean user interface  
- Overtime will deduct computer time the next day (experimental)  
- Automatic timer pause/resume on computer lock/unlock  
- You can add/subtract time from the time elapsed, if you for example left the computer unlocked, and the timer counted that time as   well. **NOTE!** Anyone having access to the computer can do this, so if you for example want to count your childrens' computer time, they can change the time elapsed.  
- Language support for both English and Swedish  
- Portable, no installation required (but it does create a Local AppData folder in order to save settings, timer data and log files)
  
**SCREENSHOT(S):**  
![Alt text](https://github.com/steel9/CSClock/blob/master/Screenshots/screenshot1.PNG?raw=true "Main form")
  
**SYSTEM REQUIREMENTS:**
  - .NET Framework 4.5.2  
  - Windows PowerShell, used by the removal tool. You can use CSClock without Windows PowerShell, but you can't use the removal tool. 
  
**HOW TO INSTALL:**  
Download Setup.exe, open it, done. Simple as that (finally).
  
**CONTACT:**  
steel9apps@gmail.com  
  
**KNOWN ISSUES:**  
**ISSUE 1:** In some cases where pressing any of the buttons in the About form, the computer will stop responding a few seconds.  
**ISSUE 2:** Settings gone after some updates  
  
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
   
**ISSUE 2:**  
**Windows Vista, 7, 8.x, 10:**  
This is probably due to some special changes in the application resetting the user data. The easiest solution is just to adjust the       settings by yourself again.  
  
**FEATURES TO BE ADDED:**  
- Automatic updater  
- More unified icons (the button icons)  
- More supported languages  

If you want, you can contribute! It is really appreciated!
