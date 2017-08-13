/*
CSClock - a program which keeps track of your computer time
Copyright (C) 2017  Viktor J

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/


using System;
using System.IO;

namespace CSClock
{
    public class Logger
    {
        public string applicationName_;
        public LogTimeDateOptions logTimeDateOption_;
        public string logPath_;
        public bool _24hFormat_;

        public Logger(string applicationName, string logPath, LogTimeDateOptions logTimeDateOption, bool _24hFormat)
        {
            applicationName_ = applicationName;
            logTimeDateOption_ = logTimeDateOption;
            logPath_ = logPath;
            _24hFormat_ = _24hFormat;

            if (!File.Exists(logPath_))
            {
                StreamWriter sw = new StreamWriter(logPath_, false);
                sw.Write("Log for " + applicationName + "\r\n\r\n");
                sw.Close();
            }
        }

        public void Log(string className, string logText, LogType logType,
            bool writeNewLineBefore = false)
        {
            string dateTime = null;
            if (!File.Exists(logPath_))
            {
                return;
            }

            switch (logTimeDateOption_)
            {
                case LogTimeDateOptions.HourMinute:
                    if (_24hFormat_)
                        dateTime = DateTime.Now.ToString("HH:mm");
                    else
                        dateTime = DateTime.Now.ToString("h:mm tt");
                    break;

                case LogTimeDateOptions.HourMinuteSecond:
                    if (_24hFormat_)
                        dateTime = DateTime.Now.ToString("HH:mm:ss");
                    else
                        dateTime = DateTime.Now.ToString("h:mm:ss tt");
                    break;

                case LogTimeDateOptions.YearMonthDayHourMinute:
                    if (_24hFormat_)
                        dateTime = DateTime.Now.ToString("yyyy-MM-dd") + " - " + DateTime.Now.ToString("HH:mm");
                    else
                        dateTime = DateTime.Now.ToString("yyyy-MM-dd") + " - " + DateTime.Now.ToString("h:mm tt");
                    break;

                case LogTimeDateOptions.YearMonthDayHourMinuteSecond:
                    if (_24hFormat_)
                        dateTime = DateTime.Now.ToString("yyyy-MM-dd") + " - " + DateTime.Now.ToString("HH:mm:ss");
                    else
                        dateTime = DateTime.Now.ToString("yyyy-MM-dd") + " - " + DateTime.Now.ToString("h:mm:ss tt");
                    break;
            }

            StreamWriter sw = new StreamWriter(logPath_, true);
            if (writeNewLineBefore)
            {
                sw.Write("\r\n");
            }
            sw.Write(dateTime + " | " + logType + " | " + className + " | " + logText + "\r\n");
            sw.Close();
        }

        public enum LogTimeDateOptions
        {
            YearMonthDayHourMinuteSecond,
            YearMonthDayHourMinute,
            HourMinuteSecond,
            HourMinute,
            None
        }

        public enum LogType
        {
            Info,
            Warning,
            Error,
            None
        }
    }
}