using System.Collections.Generic;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Core.Services.Evaluators;
using Moq;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class DirectoryGroupEvaluatorTests
    {
        [Test]
        public void GroupsShouldBeEqual()
        {
            var mockFolderperGroupEvaluator = new Mock<IFoldersPerGroupEvaluator>();
            mockFolderperGroupEvaluator.Setup(x => x.Evaluate(It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            var mockDirEvaluator = new Mock<IDirectoryDescendentFilesEvaluator>();
            mockDirEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(new List<string> { "file1" });

            var obj = new DirectoryGroupEvaluator(mockFolderperGroupEvaluator.Object, mockDirEvaluator.Object);

            var dirs = new[]
            {
                "dir1a", "dir1b", "dir1c", 
                "dir2a", "dir2b", "dir2c", 
                "dir3a", "dir3b", "dir3c", 
                "dir4a", "dir4b", "dir4c"
            };

            var result = obj.Evaluate(4, dirs);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(3, result[1].Files.Count);
            Assert.AreEqual(3, result[2].Files.Count);
            Assert.AreEqual(3, result[3].Files.Count);
        }

        [Test]
        public void GroupFourShouldBeLess()
        {
            var mockFolderperGroupEvaluator = new Mock<IFoldersPerGroupEvaluator>();
            mockFolderperGroupEvaluator.Setup(x => x.Evaluate(It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            var mockDirEvaluator = new Mock<IDirectoryDescendentFilesEvaluator>();
            mockDirEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(new List<string> { "file1" });

            var obj = new DirectoryGroupEvaluator(mockFolderperGroupEvaluator.Object, mockDirEvaluator.Object);

            var dirs = new[]
            {
                "dir1a", "dir1b", "dir1c", 
                "dir2a", "dir2b", "dir2c", 
                "dir3a", "dir3b", "dir3c", 
                "dir4a", "dir4b"
            };

            var result = obj.Evaluate(4, dirs);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(3, result[1].Files.Count);
            Assert.AreEqual(3, result[2].Files.Count);
            Assert.AreEqual(2, result[3].Files.Count);
        }


        [Test]
        public void GroupFourShouldBeMore()
        {
            var mockFolderperGroupEvaluator = new Mock<IFoldersPerGroupEvaluator>();
            mockFolderperGroupEvaluator.Setup(x => x.Evaluate(It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            var mockDirEvaluator = new Mock<IDirectoryDescendentFilesEvaluator>();
            mockDirEvaluator.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(new List<string> { "file1" });

            var obj = new DirectoryGroupEvaluator(mockFolderperGroupEvaluator.Object, mockDirEvaluator.Object);

            var dirs = new[]
            {
                "dir1a", "dir1b", "dir1c", 
                "dir2a", "dir2b", "dir2c", 
                "dir3a", "dir3b", "dir3c", 
                "dir4a", "dir4b", "dir4c", "dir3d", 
            };

            var result = obj.Evaluate(4, dirs);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(3, result[1].Files.Count);
            Assert.AreEqual(3, result[2].Files.Count);
            Assert.AreEqual(4, result[3].Files.Count);
        }
    }
}
