using System;

namespace MongoDB.Training.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime NewDateTimeUTC(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        }
    }
}
