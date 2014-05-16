namespace MetricsUtility.Core.Services.RefactorServices
{
    public class SeperatedJs
    {
        public string[] ReplacementLines { get; set; }
        public GeneratedJsViewModel[] ExtractedJsBlocks { get; set; }
    }
}