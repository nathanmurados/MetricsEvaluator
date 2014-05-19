using System;

namespace MetricsUtility.Core.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}