using System;

namespace LiteDB
{
    internal static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Truncate DateTime in milliseconds
        /// </summary>
        public static DateTimeOffset Truncate(this DateTimeOffset dt)
        {
            if (dt == DateTimeOffset.MaxValue || dt == DateTimeOffset.MinValue)
            {
                return dt;
            }

            return new DateTimeOffset(dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute, dt.Second, dt.Millisecond,
                dt.Offset);
        }

        public static int MonthDifference(this DateTimeOffset startDate, DateTimeOffset endDate)
        {
            // https://stackoverflow.com/a/1526116/3286260

            int compMonth = (endDate.Month + endDate.Year * 12) - (startDate.Month + startDate.Year * 12);
            double daysInEndMonth = (endDate - endDate.AddMonths(1)).Days;
            double months = compMonth + (startDate.Day - endDate.Day) / daysInEndMonth;

            return Convert.ToInt32(Math.Truncate(months));
        }

        public static int YearDifference(this DateTimeOffset startDate, DateTimeOffset endDate)
        {
            // https://stackoverflow.com/a/28444291/3286260

            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day)// BUT the end day is less than the start day
            {
                years--;
            }
            else if (endDate.Month < startDate.Month)// if the end month is less than the start month
            {
                years--;
            }

            return years;
        }
    }
}