using System;
using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.Services.RefactorServices;
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

        public List<Fragment> Evaluate(string line)
        {
            var jsLine = string.Copy(line);

            HandleDoubleAts(jsLine);

            var fragments = new List<Fragment>();
            while (jsLine.Contains("@"))
            {
                var lastFragment = GetFirstFragment(jsLine);
                fragments.Add(new Fragment
                {
                    FragType = GetFragType(jsLine, lastFragment),
                    Text = lastFragment
                });

                var start = jsLine.IndexOf(lastFragment, StringComparison.Ordinal);
                var end = start + lastFragment.Length;

                jsLine = jsLine.Substring(0, start) + jsLine.Substring(end);
                
                //throw new NotImplementedException();
            }

            return fragments;
        }

        private static FragType GetFragType(string line, string fragment)
        {
            var x = line.IsWithinQuotes(fragment);

            if(!x.IsWithinQuote)return FragType.Default;

            return x.Quote == '\'' ? FragType.SingleQuotes : FragType.DoubleQuotes;
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