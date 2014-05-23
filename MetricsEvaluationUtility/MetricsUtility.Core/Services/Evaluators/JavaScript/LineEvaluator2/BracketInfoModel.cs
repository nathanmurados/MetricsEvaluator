namespace MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2
{
    public class BracketInfoModel
    {
        public BracketType Type { get; set; }
        public bool IsOpener { get; set; }
        public bool IsCloser { get { return !IsOpener; } }
    }
}