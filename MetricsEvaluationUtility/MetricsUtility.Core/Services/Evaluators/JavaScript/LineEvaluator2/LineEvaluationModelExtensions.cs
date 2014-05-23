using System;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    internal static class LineEvaluationModelExtensions
    {
        public static bool HasUnmatchedQuotes(this LineEvaluationModel model)
        {
            return model.Brackets.Any(x => x.Value.AmountOpen > x.Value.AmountClosed);
        }
        
        public static bool HasAnyUnmatchedBrackets(this LineEvaluationModel model)
        {
            return model.Brackets.Any() && model.Brackets.Any(bracketTracker => bracketTracker.Value.AmountClosed != bracketTracker.Value.AmountOpen);
        }

        public static bool AddBracketIfIsFirstInstance(this LineEvaluationModel model, BracketInfoModel bracketInfo)
        {
            if (model.Brackets.ContainsKey(bracketInfo.Type)) return false;

            var bracketTracker = new BracketTracker
            {
                AmountOpen = bracketInfo.IsOpener ? 1 : 0,
                AmountClosed = bracketInfo.IsOpener ? 0 : 1,
            };

            if (bracketTracker.AmountOpen == 0 && bracketTracker.AmountClosed > 0)
            {
                throw new ArgumentException(string.Format("Attempted to add a closing {0} without an opening equivalent", bracketInfo.Type));
            }

            model.Brackets.Add(bracketInfo.Type, bracketTracker);
            return true;
        }
    }
}