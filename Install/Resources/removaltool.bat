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
title CSClock uninstallation finish

echo Finishing CSClock uninstallation
powershell -Command "Stop-Process -Name Setup -ErrorAction SilentlyContinue"
powershell -Command "Remove-Item -Recurse -Force \"%localappdata%\CSClock\" -ErrorAction SilentlyContinue"
(goto) 2>nul & del "%~f0"