using System.IO;

namespace MetricsUtiltiy.Tests
{
    using System.Reflection;

    public class AvailableTestingResources
    {
        public const string TestingResource = "TestingResource.cshtml";
        public const string GetComplianceMatrix = "GetComplianceMatrix.cshtml";
    }

    public class AssetRetriever
    {
        public static FileAndContents GetFileAndContent(string availableTestingResource)
        {
            var path = "MetricsUtiltiy.Tests.Assets." + availableTestingResource;
            var asm = Assembly.GetExecutingAssembly();
            string fileText;
            using (var s = asm.GetManifestResourceStream(path))
            using (TextReader r = new StreamReader(s))
            {
                fileText = r.ReadToEnd();
            }
            return new FileAndContents
            {
                Path = path,
                Contents = fileText.Split(new[] { '\n' })
            };
        }
    }
}