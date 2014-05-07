using System.Collections.Generic;

namespace MetricsEvaluationUtility.ViewModels
{
    public class JavaScriptEvaluationResult
    {
        public string FileName { get; set; }
        public List<int> PageInstances { get; set; }
        public int References { get; set; }
        public List<DetailedJavaScriptEvaluationResult> Block { get; set; }
        public List<DetailedJavaScriptEvaluationResult> Razor { get; set; }
    }
}