using System.Collections.Generic;

namespace MetricsUtility.Core.ViewModels
{
    public class CssEvaluationResult
    {
        public string FileName { get; set; }
        public List<int> Page { get; set; }
        public List<DetailedCssEvaluationResult> Inline { get; set; }
        public List<DetailedCssEvaluationResult> Razor { get; set; }
    }
}