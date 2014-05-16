namespace MetricsUtility.Core.Services.Refactorers
{
    public class SeperatedJs
    {
        public string[] ReplacementLines { get; set; }
        public GeneratedJsViewModel[] ExtractedJsBlocks { get; set; }
    }
}