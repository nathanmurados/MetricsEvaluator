using System.Collections.Generic;

namespace MetricsEvaluationUtility.ViewModels
{
    public class DetailedJavaScriptEvaluationResult
    {
        public string AttributeName { get; set; }
        public List<JavascriptOccurenceResult> InlineJavascriptTags { get; set; }
    }
}