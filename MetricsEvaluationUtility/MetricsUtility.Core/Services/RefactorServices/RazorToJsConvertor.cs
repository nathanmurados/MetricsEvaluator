using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.Constants;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{


    internal class Result
    {
        public string ConvertedLine { get; set; }
        public bool Completed { get; set; }
    }

    public class RazorToJsConvertor
    {
        public List<JsModuleViewModel> RazorVariables { get; set; }

        public string Convert(string line)
        {

            RazorVariables = RazorVariables.OrderByDescending(x => x.OriginalRazorText.Length).ToList();

            if (!line.ContainsSingleQuote() && !line.ContainsDoubleQuote())
            {
                return RazorVariables.Aggregate(line, (current, vm) => current.Replace(vm.OriginalRazorText, vm.GetAp2Name()));
            }

            var result = HandleRazorEmbeddedInSingleString(line);
            if (result.Completed) return result.ConvertedLine;

            //result = HandleRazorDirectlyWithinQuotes(line);
            //if (result.Completed) return result.ConvertedLine;

            result = HandleRazorBetweenClosedStrings(line);
            if (result.Completed) return result.ConvertedLine;

            throw new NotImplementedException();
        }

        private Result HandleRazorBetweenClosedStrings(string line)
        {
            bool updatesMade = false;
            foreach (var vm in RazorVariables)
            {
                if (line.Contains(vm.OriginalRazorText))
                {
                    line = line
                            .Replace(string.Format("\"{0}\"", vm.OriginalRazorText), vm.OriginalRazorText)
                            .Replace(string.Format("'{0}'", vm.OriginalRazorText), vm.OriginalRazorText);

                    var parts = line.Split(new[] { vm.OriginalRazorText }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Where(x => !x.Contains(vm.OriginalRazorText)).All(x => x.IsLegitimateConnectingJsFragment())
                        ||
                        parts.Where(x => !x.Contains(vm.OriginalRazorText)).All(x => x.Count('\'') % 2 != 1 && x.Count('\"') % 2 != 1)
                    )
                    {
                        line = line.Replace(vm.OriginalRazorText, vm.GetAp2Name());
                        updatesMade = true;
                    }
                }
            }

            return new Result
            {
                Completed = updatesMade,
                ConvertedLine = updatesMade ? line : ""
            };
        }

        private Result HandleRazorEmbeddedInSingleString(string originalLine)
        {
            //var line = string.Copy(originalLine);

            //foreach (var quote in CharacterConstants.Quotes)
            //{
            //    if (line.Count(quote) == 2 && line.StartsWith(quote.AsString()) && line.EndsWith(quote.AsString()))
            //    {
            //        line = RazorVariables.Aggregate(line, (current, vm) => current.Replace(vm.OriginalRazorText, string.Format("{0} + {1} + {0}", quote, vm.GetAp2Name())));
            //        var weirdEnding = string.Format(" + {0}{0}", quote);
            //        if (line.EndsWith(weirdEnding))
            //        {
            //            line = line.Replace(weirdEnding, "");
            //        }
            //        return new Result
            //        {
            //            Completed = true,
            //            ConvertedLine = line
            //        };
            //    }
            //}

            var line = string.Copy(originalLine);
            var changesMade = false;

            foreach (var vm in RazorVariables)
            {
                var isWithinQuoteInfo = line.IsWithinQuotes(vm.OriginalRazorText);

                if (isWithinQuoteInfo.IsWithinQuote)
                {
                    line = line.Replace(vm.OriginalRazorText, string.Format("{0} + {1} + {0}", isWithinQuoteInfo.Quote, vm.GetAp2Name()));
                    line = line.Replace(string.Format(" + {0}{0}", isWithinQuoteInfo.Quote), "");
                    line = line.Replace(string.Format("{0}{0} + ", isWithinQuoteInfo.Quote), "");
                    changesMade = true;
                }
            }

            return new Result
            {
                Completed = changesMade,
                ConvertedLine = line
            };
        }
    }

    public static partial class StringExtensions
    {
        public static WithinQuoteInfo IsWithinQuotes(this string line, string item)
        {
            // alert('test ' + ' more text ' + ' test @variable2')
            var sb = new StringBuilder();

            bool isWithinQuote = false;
            char lastQuote = ' ';

            var itemStart = line.IndexOf(item, StringComparison.Ordinal);
            var itemEnt = itemStart + item.Length;

            for (int i = 0; i < itemStart; i++)
            {
                var c = line[i];
                sb.Append(c);

                if (isWithinQuote && c.IsQuote() && c == lastQuote)
                {
                    sb.Clear();
                    isWithinQuote = false;
                }
                else if (!isWithinQuote && c.IsQuote())
                {
                    lastQuote = c;
                    isWithinQuote = true;
                }
            }

            return new WithinQuoteInfo
            {
                IsWithinQuote = isWithinQuote,
                Quote = isWithinQuote ? lastQuote : ' '
            };
        }
    }

    public class WithinQuoteInfo
    {
        public char Quote { get; set; }
        public bool IsWithinQuote { get; set; }
    }
}