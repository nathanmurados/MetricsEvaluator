namespace MetricsUtility.Core.Services.Refactorers
{
    public class SeperatedJsViewModel
    {
        public string[] StripedContent { get; set; }
        public GeneratedJsViewModel[] ExtractedJsBlocks { get; set; }
    }
}