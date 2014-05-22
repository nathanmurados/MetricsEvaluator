using System;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class UnhandledPatternException : Exception
    {
        public UnhandledPatternException(string offendingLine)
            : base(offendingLine)
        {

        }
    }
}