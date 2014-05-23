using System;
using System.Collections.Generic;
using System.Text;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    public class EndOfrazorEvaluator
    {
        public string Evaluate(string substring)
        {
            var model = new LineEvaluationModel
            {
                Quotes = new Dictionary<char, int>(),
                Brackets = new Dictionary<BracketType, BracketTracker>(),
                HaltProcessing = false
            };

            var sb = new StringBuilder();

            foreach (var character in substring)
            {
                if (character.IsQuote())
                {
                    EvaluateQuote(model, character);
                    if (model.HaltProcessing)
                    {
                        return sb.ToString();
                    }
                }

                if (character.IsBracket())
                {
                    EvaluateBracket(model, character);
                    if (model.HaltProcessing)
                    {
                        sb.Append(character);
                        return sb.ToString();
                    }
                }

                if (character.IsPotentialTerminator() && !model.HasAnyUnmatchedBrackets())
                {
                    return sb.ToString();
                }
                
                sb.Append(character);
            }
            throw new UncaughtPatternException();
        }

        private static void EvaluateBracket(LineEvaluationModel model, char character)
        {
            if (!character.IsBracket()) throw new ArgumentException();

            var bracketInfo = character.GetBracketInfo();

            var isFirstInstance = model.AddBracketIfIsFirstInstance(bracketInfo);

            if (!isFirstInstance)
            {
                model.Brackets[bracketInfo.Type].AmountOpen += bracketInfo.IsOpener ? 1 : 0;
                model.Brackets[bracketInfo.Type].AmountClosed += bracketInfo.IsOpener ? 0 : 1;

                if (bracketInfo.IsCloser && !model.HasAnyUnmatchedBrackets() && !model.HasUnmatchedQuotes())
                {
                    model.HaltProcessing = true;
                }
            }
        }

        private static void EvaluateQuote(LineEvaluationModel model, char quote)
        {
            if (!quote.IsQuote()) throw new ArgumentException();

            if (!model.Quotes.ContainsKey(quote))
            {
                if (!model.HasAnyUnmatchedBrackets())
                {
                    model.HaltProcessing = true;
                    return;
                }
                
                model.Quotes.Add(quote, 1);
                return;
            }

            if (!model.HasAnyUnmatchedBrackets())
            {
                model.HaltProcessing = true;
                return;
            }

            model.Quotes[quote]++;
        }
    }
}