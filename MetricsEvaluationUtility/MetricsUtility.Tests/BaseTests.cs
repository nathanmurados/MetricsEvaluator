using System.IO;

namespace MetricsUtiltiy.Tests
{
    using System.Reflection;

    public abstract class BaseTests
    {
        public FileAndContents GetFileAndContent()
        {
            const string path = "MetricsUtiltiy.Tests.Assets.TestingResource.cshtml";
            Assembly asm = Assembly.GetExecutingAssembly();
            string fileText = null;
            using (Stream s = asm.GetManifestResourceStream("MetricsUtiltiy.Tests.Assets.TestingResource.cshtml"))
            using (TextReader r = new StreamReader(s))
            {
                fileText = r.ReadToEnd();
            }
                return new FileAndContents
            {
                Path =path,
                Contents =fileText.Split(new char[] {'\n'})
            };
        }
    }
}