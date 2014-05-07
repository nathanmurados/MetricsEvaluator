using System;

namespace MetricsEvaluationUtility.Services.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison stringComparison)
        {
            return source.IndexOf(toCheck, stringComparison) >= 0;
        }
    }
}