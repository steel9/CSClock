using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSClock
{
    static class Stats
    {
        public static string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSClock");
        public static string statisticsFile = Path.Combine(installDir, "statistics.xml");

        private static XmlDocument statXml_ = null; //do not call statXml_ in code, call statXml !
        static XmlDocument statXml
        {
            get
            {
                XmlDocument statXml_ = new XmlDocument();
                XmlElement statElement;
                if (statXml_ != null)
                {
                    //Xml is already loaded
                    return statXml_;
                }
                else if (!File.Exists(statisticsFile))
                {
                    //Xml isn't created
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
                    weekStatsElement.AppendChild(daysTimeSpentText);
                    weekStatsElement.AppendChild(lastUpdateElement);
                    statElement.AppendChild(weekStatsElement);
                    statXml_.AppendChild(statElement);
                    statXml_.Save(statisticsFile);
                 }
                 else
                 {
                    //Xml is created, but not loaded
                    statXml_.Load(statisticsFile);
                 }
                return statXml_;
            }
            set
            {
                statXml_ = value;
            }
        }

        static void UpdateStatistics(int secsElapsed)
        {
            int timeSpent = int.Parse(statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText);
            timeSpent += secsElapsed;
            statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText = timeSpent.ToString();
        }

        static int averageWeekTimeSpent()
        {
            var timeSpent = int.Parse(statXml.SelectSingleNode("//WeekStatistics/TimeSpent").InnerText);
            var days = int.Parse(statXml.SelectSingleNode("//WeekStatistics/DaysTimeSpent").InnerText);
            return timeSpent / days;
        }
    }
}
