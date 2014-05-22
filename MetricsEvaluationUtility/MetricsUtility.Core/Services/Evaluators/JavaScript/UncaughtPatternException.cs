namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class UncaughtPatternException : UnhandledPatternException
    {
        public UncaughtPatternException(): base("This razor is completely unrecognised")
        {

        }
    }
}