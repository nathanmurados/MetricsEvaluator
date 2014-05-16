using System.Collections.Generic;
using MetricsUtility.Core.Services.Evaluators.Css;

namespace MetricsUtility.Core.ViewModels
{
    public class JavaScriptEvaluationResult
    {
        public string FileName { get; set; }
        public BlockContent[] PageInstances { get; set; }
        public int References { get; set; }
        public List<DetailedJavaScriptEvaluationResult> Block { get; set; }
        public List<DetailedJavaScriptEvaluationResult> Razor { get; set; }
    }
}