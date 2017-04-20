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
using Newtonsoft.Json;

namespace CSClock
{
    static class Stats
    {
        public static string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        public static string statisticsFile = Path.Combine(installDir, "statistics.json");

        public static uint secondsElapsed = 0;
        public static uint overtimeSecondsElapsed = 0;

        public static void UpdateStatistics()
        {
            if (File.Exists(statisticsFile))
            {
                StreamReader sr = new StreamReader(statisticsFile);
                Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
                sr.Close();

                statsDz.hoursSpent += (secondsElapsed / 1200);
                secondsElapsed = 0;

                statsDz.overtimeHoursSpent += (overtimeSecondsElapsed / 1200);
                overtimeSecondsElapsed = 0;

                if (statsDz.lastDaysSpentUpdate.Date != DateTime.Now.Date)
                {
                    statsDz.daysSpent++;
                    statsDz.lastDaysSpentUpdate = DateTime.Now;
                }

                StreamWriter sw = new StreamWriter(statisticsFile, false);
                sw.Write(JsonConvert.SerializeObject(statsDz));
                sw.Close();
            }
            else
            {
                Stat stats = new Stat();

                stats.startDateTime = DateTime.Now;

                stats.hoursSpent = (secondsElapsed / 1200);
                secondsElapsed = 0;

                stats.overtimeHoursSpent = (overtimeSecondsElapsed / 1200);
                overtimeSecondsElapsed = 0;

                stats.daysSpent++;
                stats.lastDaysSpentUpdate = DateTime.Now;

                StreamWriter sw = new StreamWriter(statisticsFile, false);
                sw.Write(JsonConvert.SerializeObject(stats));
                sw.Close();
            }
        }

        public static DateTime StartDateTime()
        {
            StreamReader sr = new StreamReader(statisticsFile);
            Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
            sr.Close();

            return statsDz.startDateTime;
        }

        public static ulong TotalHoursSpent()
        {
            StreamReader sr = new StreamReader(statisticsFile);
            Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
            sr.Close();

            return statsDz.hoursSpent;
        }

        public static ulong AverageHoursSpent()
        {
            StreamReader sr = new StreamReader(statisticsFile);
            Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
            sr.Close();

            return statsDz.hoursSpent / statsDz.daysSpent;
        }

        public static ulong TotalOvertimeHoursSpent()
        {
            StreamReader sr = new StreamReader(statisticsFile);
            Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
            sr.Close();

            return statsDz.overtimeHoursSpent;
        }

        public static ulong AverageOvertimeHoursSpent()
        {
            StreamReader sr = new StreamReader(statisticsFile);
            Stat statsDz = JsonConvert.DeserializeObject<Stat>(sr.ReadToEnd());
            sr.Close();

            return statsDz.overtimeHoursSpent / statsDz.daysSpent;
        }
    }
}
