using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Core.ViewModels
{
    public class SeperatedCssViewModel
    {
        public string[] StripedContent { get; set; }
        public GeneratedCssViewModel[] ExtractedCssBlocks { get; set; }
    }
}