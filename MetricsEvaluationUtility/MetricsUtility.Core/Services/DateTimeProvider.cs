using System;

namespace MetricsUtility.Core.Services
{
    public interface IHasDateTimeProvider
    {
        IDateTimeProvider DateTimeProvider { get; }
    }

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}