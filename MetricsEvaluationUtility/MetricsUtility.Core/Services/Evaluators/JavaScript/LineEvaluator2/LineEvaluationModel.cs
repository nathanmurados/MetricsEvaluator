using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    public class LineEvaluationModel
    {
        public Dictionary<char, int> Quotes { get; set; }

        public Dictionary<BracketType, BracketTracker> Brackets { get; set; }

        public bool HaltProcessing { get; set; }
    }
}