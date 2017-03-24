@REM 		CSClock - a program which keeps track of your computer time
@REM 		Copyright (C) 2017  Viktor J

@REM 		This program is free software: you can redistribute it and/or modify
@REM 		it under the terms of the GNU General Public License as published by
@REM 		the Free Software Foundation, either version 3 of the License, or
@REM 		(at your option) any later version.

@REM 		This program is distributed in the hope that it will be useful,
@REM 		but WITHOUT ANY WARRANTY; without even the implied warranty of
@REM 		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
@REM		GNU General Public License for more details.

@REM 		You should have received a copy of the GNU General Public License
@REM		along with this program.  If not, see <http://www.gnu.org/licenses/>.


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