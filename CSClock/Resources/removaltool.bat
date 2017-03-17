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



@REM %1 is the FULL path of the CSClock executable
@REM %2 is only the NAME of the executable, WITHOUT the file extension (exe)



@echo off
title CSClock Removal Tool

echo -------------------------------------------------------
echo CSClock Removal Tool
echo Please wait while the CSClock removal tool is running,
echo do not close this window until the removal is finished
echo -------------------------------------------------------
echo.

if "%1" == "" goto ERR1
if "%2" == "" goto ERR1


echo Closing CSClock...
powershell -Command "Stop-Process -Name %2 -ErrorAction SilentlyContinue"

echo Removing application...
powershell -Command "Remove-Item -Recurse -Force %1 -ErrorAction SilentlyContinue"

echo Removing data...
powershell -Command "Remove-Item -Recurse -Force \"%localappdata%\CSClock\" -ErrorAction SilentlyContinue"
powershell -Command "Remove-Item -Recurse -Force \"%appdata%\Microsoft\Windows\Start Menu\Programs\Startup\CSClock.lnk\" -ErrorAction SilentlyContinue"
powershell -Command "Remove-Item -Recurse -Force \"%appdata%\Microsoft\Windows\Start Menu\CSClock.lnk\" -ErrorAction SilentlyContinue"

echo.
echo.
echo Removal finished! Press any key to finish
pause>nul
(goto) 2>nul & del "%~f0"



:ERR1
echo.
echo ERROR: Missing arguments
echo Please launch the removal tool from the CSClock GUI or by launching CSClock.exe with the argument "-removal" (without quotation)
echo.
echo.
echo Press any key to exit
pause>nul