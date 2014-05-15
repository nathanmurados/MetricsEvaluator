using System.Linq;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Refactorers;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class PagJsSeperationEvaluatorTests
    {
        private static SeperatedJsViewModel Run(string[] testData)
        {
            var obj = new PageJsSeperationEvaluator(new JsPageEvaluator(), new JsFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()));
            return obj.Evaluate(testData, @"C:\dir1\dir2\dir3", @"C:\dir1\dir2\dir3\Content\BlockJs\Search", @"LoggingResultGrid.cshtml");
        }

        [Test]
        public void VerySimple()
        {
            var testData = new[]
            {
                "<script type=\"text/javascript\">",
                "   body{float:left;}",
                "</script>",
            };

            var result = Run(testData);

            Assert.AreEqual(1, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[0]);
        }


        [Test]
        public void TwoReferences()
        {
            var testData = new[]
            {
                "<script src=\"~/Scripts/ThirdPartyFeeds.js\" type=\"text/javascript\"></script>",
                "<script type=\"text/javascript\">",
                "   body{float:left;}",
                "</script>",
            };

            var result = Run(testData);

            Assert.AreEqual(2, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[1]);
        }

        [Test]
        public void HasAtSigns()
        {
            var testData = new[]
            {
                "<script src=\"~/Scripts/ThirdPartyFeeds.js\" type=\"text/javascript\"></script>",
                "<script type=\"text/javascript\">",
                "   body{float:left;} = @something",
                "</script>",
            };

            var result = Run(testData);

            Assert.AreEqual(4, result.StripedContent.Count());
            Assert.AreEqual(0, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("<script src=\"~/Scripts/ThirdPartyFeeds.js\" type=\"text/javascript\"></script>", testData[0]);
            Assert.AreEqual("<script type=\"text/javascript\">", testData[1]);
            Assert.AreEqual("   body{float:left;} = @something", testData[2]);
            Assert.AreEqual("</script>", testData[3]);
        }

        [Test]
        public void VerySimpleDirty()
        {
            var testData = new[]
            {
                "<script>",
                "   body{float:left;}",
                "</script>",
            };

            var result = Run(testData);

            Assert.AreEqual(1, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[0]);
            //<script src="@Url.Content("~/Scripts/ThirdPartyFeeds.js")" type="text/javascript"></script>

        }


        [Test]
        public void CommonFormat()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">",
                "         body{float:left;}",
                "      </script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[2]);
        }

        [Test]
        public void CorrectReferenceToNewCssFile()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">",
                "         body{float:left;}",
                "      </script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[2]);
            Assert.AreEqual("   </head>", result.StripedContent[3]);
            Assert.AreEqual("   <body>", result.StripedContent[4]);
            Assert.AreEqual("      <div>some text</div>", result.StripedContent[5]);
            Assert.AreEqual("   <body>", result.StripedContent[6]);
            Assert.AreEqual("</html>", result.StripedContent[7]);
        }

        [Test]
        public void CorrectReferenceToTwoJsFiles()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">",
                "         body{float:left;}",
                "      </script>",
                "",
                "      <script type=\"text/javascript\">",
                "         body{float:right;}",
                "      </script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[2]);
            Assert.AreEqual("", result.StripedContent[3]);
            Assert.AreEqual("   </head>", result.StripedContent[4]);
            Assert.AreEqual("   <body>", result.StripedContent[5]);
            Assert.AreEqual("      <div>some text</div>", result.StripedContent[6]);
            Assert.AreEqual("   <body>", result.StripedContent[7]);
            Assert.AreEqual("</html>", result.StripedContent[8]);
        }

        [Test]
        public void CorrectReferenceToTwoSeperateJsFiles()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">",
                "         body{float:left;}",
                "      </script>",
                "      <!-- A reason not to join these two scripts together in holy matrimony-->",
                "      <script type=\"text/javascript\">",
                "         body{float:right;}",
                "      </script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid.js\" type=\"text/javascript\"></script>", result.StripedContent[2]);
            Assert.AreEqual("      <!-- A reason not to join these two scripts together in holy matrimony-->", result.StripedContent[3]);
            Assert.AreEqual("<script src=\"~/Content/BlockJs/Search/LoggingResultGrid_fragment1.js\" type=\"text/javascript\"></script>", result.StripedContent[4]);
            Assert.AreEqual("   </head>", result.StripedContent[5]);
            Assert.AreEqual("   <body>", result.StripedContent[6]);
            Assert.AreEqual("      <div>some text</div>", result.StripedContent[7]);
            Assert.AreEqual("   <body>", result.StripedContent[8]);
            Assert.AreEqual("</html>", result.StripedContent[9]);
        }

        [Test]
        public void CssAndOpenCloseTagsOnSameLine()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">body{float:left;}</script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };


            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("      body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
        }

        [Test]
        public void CssAndClosingTagOnSameLine()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <script type=\"text/javascript\">",
                "           body{float:left;}</script>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("           body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
        }

        [Test]
        public void CssClosingTagAndHtmlOnSameLine()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "   </head>",
                "   <body>",
                "      <script type=\"text/javascript\">",
                "           body{float:left;}</script><div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("           body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
        }

        [Test]
        public void OpenTagAndCssOnSameLine()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "   </head>",
                "   <body>",
                "      <script type=\"text/javascript\">body{float:left;}" +
                "   </script><div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedJsBlocks.Count());
            Assert.AreEqual("      body{float:left;}   ", result.ExtractedJsBlocks[0].Lines[0]);
        }

        [Test]
        public void EmptyLinesInCss()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "   </head>",
                "   <body>",
                "      <script type=\"text/javascript\">",
                "           body{float:left;}",
                "           ",
                "           div{float:left;}",
                "       </script>",
                "       <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.StripedContent.Count());
            Assert.AreEqual(2, result.ExtractedJsBlocks[0].Lines.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedJsBlocks[0].Lines[0]);
            Assert.AreEqual("div{float:left;}", result.ExtractedJsBlocks[0].Lines[1]);
        }
    }
}