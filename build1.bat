@echo off
echo Building...
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
msbuild /p:Configuration=Debug "%~dp0%\CSClock.sln"
echo Done
pause