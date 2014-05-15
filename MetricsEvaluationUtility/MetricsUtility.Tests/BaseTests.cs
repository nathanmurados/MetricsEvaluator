using System.IO;

namespace MetricsUtiltiy.Tests
{
    public abstract class BaseTests
    {
        public FileAndContents GetFileAndContent()
        {
            const string path = @"C:\Users\Michael.Brown\Documents\GitHub\MetricsEvaluator\MetricsEvaluationUtility\MetricsUtility.Tests\Assets\TestingResource.cshtml";

            return new FileAndContents
            {
                Path = path,
                Contents = File.ReadAllLines(path)
            };
        }
    }
}