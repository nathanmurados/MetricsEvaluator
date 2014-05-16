namespace MetricsUtility.Core.Services
{
    public interface IHasDateTimeProvider
    {
        IDateTimeProvider DateTimeProvider { get; }
    }
}