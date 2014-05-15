using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class PageBlockSplitResult
    {
        public List<string> Lines { get; set; }
        public int FirstOccurenceLineNumber { get; set; }
        public int AtSymbols { get; set; }
    }
}