using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    public static class CharacterExtensions
    {
        private static readonly List<BracketModel> BracketDefintions = new List<BracketModel>
        {
            new BracketModel {BracketType=BracketType.Brace,Opener = '{', Closer = '}'},
            new BracketModel {BracketType=BracketType.Parentheses,Opener = '(', Closer = ')'},
            new BracketModel {BracketType=BracketType.Chevron,Opener = '<', Closer = '>'},
            new BracketModel {BracketType=BracketType.Crotchet,Opener = '[', Closer = ']'}
        };

        public static bool IsPotentialTerminator(this char character)
        {
            return character == ' ' || character == ';';
        }

        public static bool IsQuote(this char character)
        {
            return character == '\'' || character == '\"';
        }

        public static bool IsSingleQuote(this char character)
        {
            return character == '\'';
        }

        public static bool IsDoubleQuote(this char character)
        {
            return character == '\"';
        }

        public static BracketInfoModel GetBracketInfo(this char character)
        {
            var result = new BracketInfoModel { Type = BracketType.NotABracket };

            foreach (var bracketDefintion in BracketDefintions)
            {
                if (bracketDefintion.Opener == character)
                {
                    result.IsOpener = true;
                    result.Type = bracketDefintion.BracketType;
                }
                else if (bracketDefintion.Closer == character)
                {
                    result.IsOpener = false;
                    result.Type = bracketDefintion.BracketType;
                }
            }

            return result;
        }

        public static bool IsBracket(this char character)
        {
            return BracketDefintions.Any(x => x.Opener == character || x.Closer == character);
        }

        public static string AsString(this char character)
        {
            return character.ToString(CultureInfo.InvariantCulture);
        }
    }
}