using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.Constants;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public static partial class StringExtensions
    {
        public static string Remove(this string str, string toRemove)
        {
            return toRemove.Length == 0 ? str : str.Replace(toRemove, "");
        }

        public static bool ContainsSingleQuote(this string str)
        {
            return str.Any(x => x == '\'');
        }

        public static bool ContainsDoubleQuote(this string str)
        {
            return str.Any(x => x == '\"');
        }

        public static int Count(this string str, char character)
        {
            return str.ToCharArray().Count(x => x == character);
        }

        public static int IndexOfc(this string str, string str2)
        {
            return str.IndexOf(str2, System.StringComparison.Ordinal);
        }

        public static bool QuotesComeBeforeRazorVariables(this string str, char charcterThatShouldComeFirst, List<JsModuleViewModel> razorVariables)
        {
            var firstIndexOfRazorVar = razorVariables.Where(x => str.IndexOfc(x.OriginalRazorText) > -1).Min(x => str.IndexOfc(x.OriginalRazorText));

            return str.LastIndexOf(charcterThatShouldComeFirst) < firstIndexOfRazorVar;
        }

        public static bool QuotesComeAfterRazorVariables(this string str, char charcterThatShouldComeFirst, List<JsModuleViewModel> razorVariables)
        {
            var firstIndexOfRazorVar = razorVariables.Where(x => str.IndexOfc(x.OriginalRazorText) > -1).Min(x => str.IndexOfc(x.OriginalRazorText));

            return str.IndexOf(charcterThatShouldComeFirst) > firstIndexOfRazorVar;
        }

        public static bool IsLegitimateConnectingJsFragment(this string str)
        {
            if (str.StartsWith("'") || str.StartsWith("\"") || str.StartsWith("+") || str.StartsWith(" "))
            {
                var isWithinSingleQuote = false;
                var isWithinDoubleQuote = false;

                var sb = new StringBuilder();

                foreach (var c in str)
                {
                    if (c == '+' && !isWithinDoubleQuote && !isWithinSingleQuote)
                    {
                        continue;
                    }

                    switch (c)
                    {
                        case '\'':
                            if (isWithinDoubleQuote) { throw new NotImplementedException(); }
                            isWithinSingleQuote = !isWithinSingleQuote;
                            if (!isWithinSingleQuote)
                            {
                                sb.Clear();
                                continue;
                            }

                            break;
                        case '\"':
                            if (isWithinSingleQuote) { throw new NotImplementedException(); }
                            isWithinDoubleQuote = !isWithinDoubleQuote;
                            if (!isWithinDoubleQuote)
                            {
                                sb.Clear();
                                continue;
                            }
                            break;
                    }
                    sb.Append(c);
                }

                var fragment = sb.ToString().Trim();

                return
                    (fragment == string.Empty && str != string.Empty)
                    ||
                    (
                        fragment.StartsWith(fragment.Last().AsString())
                        && fragment.Any(x => CharacterConstants.Quotes.Any(y => y == x))
                        && fragment.Length != 1
                    );
            }
            return false;
        }
    }
}