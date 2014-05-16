namespace MetricsUtility.Core.Services
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}