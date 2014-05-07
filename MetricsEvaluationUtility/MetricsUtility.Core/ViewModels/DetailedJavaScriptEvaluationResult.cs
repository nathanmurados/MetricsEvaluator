using System.Collections.Generic;

namespace MetricsUtility.Core.ViewModels
{
    public class DetailedJavaScriptEvaluationResult
    {
        public string AttributeName { get; set; }
        public List<JavascriptOccurenceResult> InlineJavascriptTags { get; set; }
    }
}