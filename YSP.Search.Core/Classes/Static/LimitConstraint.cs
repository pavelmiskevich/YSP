using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YSP.Search.Core.Classes.Static
{
    /// <summary>
    /// Limit Constraint
    /// </summary>
    public class LimitConstraint
    {
        /// <summary>
        /// Limit of hours
        /// </summary>
        private class YandexHourLimit
        {
            public decimal Procent { get; set; }
            public int Hour { get; set; }

            public static List<YandexHourLimit> GetList()
            {
                #region YandexHourLimitList
                List<YandexHourLimit> result = new List<YandexHourLimit>();

                result.Add(new YandexHourLimit { Hour = 0, Procent = 50 });
                result.Add(new YandexHourLimit { Hour = 1, Procent = 60 });
                result.Add(new YandexHourLimit { Hour = 2, Procent = 60 });
                result.Add(new YandexHourLimit { Hour = 3, Procent = 60 });
                result.Add(new YandexHourLimit { Hour = 4, Procent = 60 });
                result.Add(new YandexHourLimit { Hour = 5, Procent = 50 });
                result.Add(new YandexHourLimit { Hour = 6, Procent = 30 });
                result.Add(new YandexHourLimit { Hour = 7, Procent = 20 });
                result.Add(new YandexHourLimit { Hour = 8, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 9, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 10, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 11, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 12, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 13, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 14, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 15, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 16, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 17, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 18, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 19, Procent = 10 });
                result.Add(new YandexHourLimit { Hour = 20, Procent = 20 });
                result.Add(new YandexHourLimit { Hour = 21, Procent = 30 });
                result.Add(new YandexHourLimit { Hour = 22, Procent = 40 });
                result.Add(new YandexHourLimit { Hour = 23, Procent = 40 });

                return result;
                #endregion
            }
        }

        private static decimal GetLimitProcent()
        {
            return (YandexHourLimit.GetList().FirstOrDefault(n => n.Hour.Equals(DateTime.Now.TimeOfDay.Hours)).Procent / 100);
        }

        public static int GetLimit(int dayLimit)
        {
            return (int)(dayLimit * GetLimitProcent());
        }
    }
}
