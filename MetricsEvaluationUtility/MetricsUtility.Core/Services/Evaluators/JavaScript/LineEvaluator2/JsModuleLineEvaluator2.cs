using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    public class JsModuleLineEvaluator2 : IJsModuleLineEvaluator
    {
        public EndOfrazorEvaluator EndOfrazorEvaluator { get; set; }

        public JsModuleLineEvaluator2()
        {
            EndOfrazorEvaluator = new EndOfrazorEvaluator();
        }

        public List<Fragment> Evaluate(string jsLine)
        {
            HandleDoubleAts(jsLine);

            var fragments = new List<Fragment>();
            while (jsLine.Contains("@"))
            {
                var firstFragment = GetFirstFragment(jsLine);
                fragments.Add(new Fragment { FragType = FragType.Default, Text = firstFragment });
                jsLine = jsLine.Replace(fragments.Last().Text, "");
            }

            return fragments;
        }

        private string GetFirstFragment(string jsLine)
        {
            var razorStart = jsLine.IndexOf("@", StringComparison.Ordinal);
            var razorEnd = EndOfrazorEvaluator.Evaluate(jsLine.Substring(razorStart));

            return razorEnd;
        }

        private void HandleDoubleAts(string jsLine)
        {
            if (jsLine.Contains("@@"))
            {
                throw new UnhandledPatternException(jsLine);
            }
        }
    }
}