# CSClock
**A C# application which keeps track of the time you spend on your computer.** 

**The project was created in Visual Studio 2015.**

**FEATURES:**  
- Easy and clean user interface  
- Automatic timer pause/resume on computer lock/unlock  
- Language support for both English and Swedish  
- Portable, no installation required (but it does create a Local AppData folder in order to save settings, timer data and log files)

**SCREENSHOT(S):**  
![Alt text](https://github.com/steel9/CSClock/blob/master/Screenshots/screenshot1.PNG?raw=true "Main form")
  
**SYSTEM REQUIREMENTS:**
  - .NET Framework 4.5.2  
  - Windows PowerShell, used by the removal tool. You can use CSClock without Windows PowerShell, but you can't use the removal tool. 
  
**HOW TO INSTALL:**  
Download CSClock.exe, place it somewhere on your computer (but not in a folder requiring administrator permissions to write), then run it and follow the configuration instructions. If you for some reason don't trust me, check the source code then compile the exe yourself. **NOTE!** If you move or rename the exe, your settings and user data will be lost! This is because of how the built in settings manager works.  
  
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
