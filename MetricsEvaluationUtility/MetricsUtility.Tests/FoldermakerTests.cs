using MetricsUtility.Clients.Wpf.Services;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class FoldermakerTests
    {
        [Test]
        public void Test1()
        {
            var obj = new DirectoryMimicker();

            const string refactorPath = @"C:\code\proj\views";
            const string generatedFilesPath = @"C:\code\proj\script";
            const string file = @"C:\code\proj\views\somefile.cshtml";

            var result = obj.Mimick(refactorPath, generatedFilesPath, file);

            Assert.AreEqual(generatedFilesPath, result);
        }

        [Test]
        public void Test2()
        {
            var obj = new DirectoryMimicker();

            const string refactorPath = @"C:\code\proj\views";
            const string generatedFilesPath = @"C:\code\proj\script";
            const string file = @"C:\code\proj\views\subDir1\somefile.cshtml";

            var result = obj.Mimick(refactorPath, generatedFilesPath, file);

            Assert.AreEqual(@"C:\code\proj\script\subDir1", result);
        }

        [Test]
        public void Test3()
        {
            var obj = new DirectoryMimicker();

            const string refactorPath = @"C:\code\proj\views";
            const string generatedFilesPath = @"C:\code\proj\script";
            const string file = @"C:\code\proj\views\subDir1\subDir2\somefile.cshtml";

            var result = obj.Mimick(refactorPath, generatedFilesPath, file);

            Assert.AreEqual(@"C:\code\proj\script\subDir1\subDir2", result);
        }
    }
}