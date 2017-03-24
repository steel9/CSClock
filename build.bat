@echo off
echo Building...
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
msbuild /p:Configuration=Release "%~dp0%\CSClock.sln"
cd "%~dp0%"
mkdir Build
copy /Y "CSClock\bin\Debug\CSClock.exe" "Build\CSClock.exe"
copy /Y "Install\bin\Debug\Install.exe" "Build\Install.exe"
echo.
echo.
echo.
echo DONE, check the Build folder.
echo Press any key to exit
pause>nul