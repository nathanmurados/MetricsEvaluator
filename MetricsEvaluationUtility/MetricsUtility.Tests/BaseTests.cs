using System.IO;

namespace MetricsUtiltiy.Tests
{
    public abstract class BaseTests
    {
        public FileAndContents GetFileAndContent()
        {
            const string path = @"C:\Users\nathan.murados\Documents\Visual Studio 2010\Projects\MetricsEvaluationUtility\MetricsEvaluationUtiltiyTests\Assets\TestingResource.cshtml";

            return new FileAndContents
            {
                Path = path,
                Contents = File.ReadAllLines(path)
            };
        }
    }
}