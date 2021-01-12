using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using YSP.Search.Core.Classes.Static;

namespace YSP.Search.Core.Test
{
    public class LimitConstraintTest
    {
        [Fact]
        public void GetLimitProcentTest()
        {
            //var actual = LimitConstraint.GetLimitProcent();
            //https://stackoverflow.com/questions/59678504/how-to-unit-test-private-method-in-net-core-application-which-does-not-support
            //Program.YandexSearch(new QueryRegionDTO { Name = "test", Region = new Region { Name = "������"} });
            var limitConstraintObject = new LimitConstraint();
            var method = limitConstraintObject.GetType().GetMethod("GetLimitProcent", BindingFlags.Static | BindingFlags.NonPublic);

            var actual = (decimal)method.Invoke(limitConstraintObject, null);

            var expected = 0;

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void GetLimitTest()
        {
            var actual = LimitConstraint.GetLimit(500);
            var expected = 0;

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void Megafon()
        {
            var n = 12345;
            var expected = 0;
            StringBuilder sb = new StringBuilder("Password is ", 17);
            for (int i = 0; i < 5 - n.ToString().Length; i++)
            {
                sb.Append("0");
            }
            sb.Append(n);
            string s = sb.ToString();
        }

        [Fact]
        public void Megafon2()
        {
            //var n = 2147483647;
            var n = 10;
            var strRes = ((long)(n - 1) + (n - 2)).ToString();
            var res = (int)(strRes[strRes.Length - 1] - '0');

            Test2(10);
        }

        private int Test2(int n)
        {
            int x = 1;
            int y = 1;

            for (int i = 2; i < n; i++)
            {
                y = x + y;
                x = y - x;
            }
            return y;
        }

       // �� ����� �������� ����, �� ������� ������� ���������� ����� � ������� ������� ������, �������� ������������ � ��.�� �� ������ ������ ��� ��� ��������, ����� ����� ���������� ����, ���� ���� ���������� �� � ���� �����, ��������, ����� ��� ������, ��� ���� ���.������������.

       //������� ���� ������ �������� ������� CheckAvailability ������� ��������� 2 ���������:  


       //schedule - ���������� ��������� � ������� "hh:mm-hh:mm" 24-h.���������� � ���� ������� �����, ��������� �� ������� ������ � ����� ���������� �������, ����������� "-" (��������, ["09:30-10:15"]).


       //currentTime - ������ � ������������ �������� � ������� hh:mm 24-h, ��� ������� ������� ����� ��������� ����������� �� ������ ����������.

       //���� �� ����� currentTime  �� ������������� ������� ����� � ������(���� ��������� ��������� ����), ������� ������ ���������� ������ "available". 
       // ���� � currentTime ��� ������, ������� ������ ���������� ������ � ��������, ����� ����� ��������.

       // ���� �����, ���������� � �������� ������� ������, ����� ������� ���������, ������� ����� ������ ���������� �������� "available".

       // �������:

       // CheckAvailability(["09:30-10:15", "12:20-15:50"], "11:00") --> "available"
       // CheckAvailability(["09:30-10:15", "12:20-15:50"], "10:00") --> "10:15"
        [Fact]
        public void Megafon3()
        {
            List<string> timeBuzy = new List<string>
            {
                "07:40-07:50", 
                "07:50-08:00",
                "09:30-10:15",
                "12:20-15:50",
                "23:20-00:50"
            };
            CheckAvailability(timeBuzy, "10:00");
            CheckAvailability(timeBuzy, "11:00");
            CheckAvailability(timeBuzy, "15:50");
            CheckAvailability(timeBuzy, "23:30");
            CheckAvailability(timeBuzy, "00:15");
        }

        public static string CheckAvailability(List<string> schedule, string currentTime)
        {
            string result = "available";
            DateTime needTime = DateTime.ParseExact(currentTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime startTime, endTime;
            List<(DateTime, DateTime)> intervals = new List<(DateTime, DateTime)>();
            foreach (var item in schedule)
            {
                var interval = item.Split('-');
                startTime = DateTime.ParseExact(interval[0], "HH:mm",
                                        CultureInfo.InvariantCulture);
                endTime = DateTime.ParseExact(interval[1], "HH:mm",
                                        CultureInfo.InvariantCulture);

                int contiguousInterval = intervals.FindIndex(x => x.Item2 == startTime);
                if (contiguousInterval < 0)
                    intervals.Add((startTime, endTime));
                else
                    intervals[contiguousInterval] = (intervals[contiguousInterval].Item1, endTime);
            }

            foreach (var tuple in intervals)
            {
                startTime = tuple.Item1;
                endTime = tuple.Item2;

                if (needTime == endTime)
                {
                    result = endTime.ToString("HH:mm");
                    break;
                }

                if (startTime > endTime) endTime = endTime.AddDays(1);

                if (needTime < startTime || needTime > endTime) continue;
                if (needTime > startTime && needTime < endTime)
                {
                    result = endTime.ToString("HH:mm");
                    break;
                }
            }

            //foreach (var item in schedule)
            //{
            //    var interval = item.Split('-');
            //    startTime = DateTime.ParseExact(interval[0], "HH:mm",
            //                            CultureInfo.InvariantCulture);
            //    endTime = DateTime.ParseExact(interval[1], "HH:mm",
            //                            CultureInfo.InvariantCulture);

            //    if(needTime == endTime)
            //    {
            //        result = interval[1];
            //        break;
            //    }

            //    if (startTime > endTime) endTime = endTime.AddDays(1);

            //    if (needTime < startTime || needTime > endTime) continue;
            //    if (needTime > startTime && needTime < endTime)
            //    {
            //        result = interval[1];
            //        break;
            //    }
            //}

            return result;
        }
    }
}
