using System;
using System.Collections.Generic;
using System.Linq;
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

            result = HandleRazorDirectlyWithinQuotes(line);
            if (result.Completed) return result.ConvertedLine;

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

                    if (parts.Where(x => !x.Contains(vm.OriginalRazorText)).All(x => x.IsLegitimateConnectingJsFragment()))
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

        private Result HandleRazorDirectlyWithinQuotes(string line)
        {
            var updatesMade = false;
            foreach (var vm in RazorVariables)
            {
                if (line.Contains(vm.OriginalRazorText))
                {
                    var instances = line.Split(new[] { vm.OriginalRazorText }, StringSplitOptions.RemoveEmptyEntries);

                    if (instances.All(x => x.IsLegitimateConnectingJsFragment()))
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

        private Result HandleRazorEmbeddedInSingleString(string line)
        {
            foreach (var quote in CharacterConstants.Quotes)
            {
                if (line.Count(quote) == 2 && line.StartsWith(quote.AsString()) && line.EndsWith(quote.AsString()))
                {
                    line = RazorVariables.Aggregate(line, (current, vm) => current.Replace(vm.OriginalRazorText, string.Format("{0} + {1} + {0}", quote, vm.GetAp2Name())));
                    var weirdEnding = string.Format(" + {0}{0}", quote);
                    if (line.EndsWith(weirdEnding))
                    {
                        line = line.Replace(weirdEnding, "");
                    }
                    return new Result
                    {
                        Completed = true,
                        ConvertedLine = line
                    };
                }
            }
            return new Result();
        }
    }
}