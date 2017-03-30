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
using System.Xml;

namespace CSClock
{
    static class Stats
    {
        public static string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        public static string statisticsFile = Path.Combine(installDir, "statistics.xml");

        private static XmlDocument statXml_ = null; //do not call statXml_ in code, call statXml !
        public static XmlDocument statXml
        {
            get
            {
                XmlElement statElement;
                if (statXml_ != null)
                {
                    //Xml is already loaded
                    return statXml_;
                }
                else if (!File.Exists(statisticsFile))
                {
                    //Xml isn't created
                    statXml_ = new XmlDocument();
                    XmlDeclaration xmlDeclaration = statXml_.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = statXml_.DocumentElement;
                    statXml_.InsertBefore(xmlDeclaration, root);
                    statElement = statXml_.CreateElement(string.Empty, "Statistics", string.Empty);
                    XmlElement weekStatsElement = statXml_.CreateElement(string.Empty, "WeekStatistics", string.Empty);
                    XmlElement timeSpentElement = statXml_.CreateElement(string.Empty, "TimeSpent", string.Empty);
                    XmlElement daysTimeSpentElement = statXml_.CreateElement(string.Empty, "DaysTimeSpent", string.Empty);
                    XmlElement lastUpdateElement = statXml_.CreateElement(string.Empty, "LastUpdate", string.Empty);
                    XmlText timeSpentText = statXml_.CreateTextNode("0");
                    XmlText daysTimeSpentText = statXml_.CreateTextNode("0");
                    XmlText lastUpdateText = statXml_.CreateTextNode("-");
                    timeSpentElement.AppendChild(timeSpentText);
                    daysTimeSpentElement.AppendChild(daysTimeSpentText);
                    lastUpdateElement.AppendChild(lastUpdateText);
                    weekStatsElement.AppendChild(timeSpentElement);
                    weekStatsElement.AppendChild(daysTimeSpentElement);
                    weekStatsElement.AppendChild(lastUpdateElement);
                    statElement.AppendChild(weekStatsElement);
                    statXml_.AppendChild(statElement);
                    statXml_.Save(statisticsFile);
                 }
                 else
                 {
                    //Xml is created, but not loaded
                    statXml_ = new XmlDocument();
                    statXml_.Load(statisticsFile);
                 }
                return statXml_;
            }
            set
            {
                statXml_ = value;
            }
        }

        public static void UpdateStatistics(int secsElapsed)
        {
            //Week statistics
            int timeSpent = int.Parse(statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText);
            int daysTimeSpent_ = int.Parse(statXml.SelectSingleNode("//WeekStatistics/DaysTimeSpent").InnerText);
            timeSpent += secsElapsed;

            var lastUpd_ = statXml.SelectSingleNode("//WeekStatistics/LastUpdate").InnerText;
            ulong lastUpd;
            if (lastUpd_ == "-")
            {
                lastUpd = 0;
            }
            else
            {
                lastUpd = ulong.Parse(lastUpd_);
            }

            var currentDate = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            if (currentDate > lastUpd)
            {
                daysTimeSpent_ += 1;
            }

            statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText = timeSpent.ToString();
            statXml.SelectSingleNode("//WeekStatistics/LastUpdate").InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");
            statXml.Save(Path.GetFileName(statisticsFile));
        }

        public static int averageWeekTimeSpent()
        {
            CheckFixWeekStatDate();

            var timeSpent = int.Parse(statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText);
            var days = int.Parse(statXml.SelectSingleNode("//WeekStatistics/DaysTimeSpent").InnerText);

            try
            {
                return timeSpent / days;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        public static void CheckFixWeekStatDate()
        {
            var lastUpd_ = statXml.SelectSingleNode("//WeekStatistics/LastUpdate").InnerText;
            ulong lastUpd;
            if (lastUpd_ == "-")
            {
                lastUpd = 0;
            }
            else
            {
                lastUpd = ulong.Parse(lastUpd_);
            }

            var currentDate = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            var currentWeekDay = (ulong)DateTime.Now.DayOfWeek;

            if (currentDate - lastUpd > currentWeekDay)
            {
                ClearWeekStatistics();
            }
        }

        private static void ClearWeekStatistics()
        {
            statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText = "0";
            statXml.SelectSingleNode("//WeekStatistics/DaysTimeSpent").InnerText = "0";
            statXml.SelectSingleNode("//WeekStatistics/LastUpdate").InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");
            statXml.Save(Path.GetFileName(statisticsFile));
        }
    }
}
