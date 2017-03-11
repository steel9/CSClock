# PCTime
**A C# application which keeps track of the time you spend on your computer.** 

**FEATURES:**  
- Easy and clean user interface  
- Automatic timer pause/resume on computer lock/unlock  
- Language support for both English and Swedish  
- Portable, no installation required (but it does create a Local AppData folder in order to save settings, timer data and log files)  
  
**SYSTEM REQUIREMENTS:**
  - .NET Framework 4.5.2  
  - Windows PowerShell, used by the removal tool. You can use PCTime without Windows PowerShell, but you can't use the removal tool.  
  
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
5. Locate 'PCTime.exe'  
6. Right click 'PCTime.exe' and select 'Kill process'. Confirm.  
**Windows 7 or earlier:**  
Do steps 1, 2, 5, 6  
   
**ISSUE 2:**  
**Windows Vista, 7, 8.x, 10:**  
This is probably due to some special changes in the application resetting the user data. The easiest solution is just to adjust the       settings by yourself again.  
