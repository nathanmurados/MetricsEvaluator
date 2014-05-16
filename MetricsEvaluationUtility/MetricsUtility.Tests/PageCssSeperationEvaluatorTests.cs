using System.Linq;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Refactorers;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class PageCssSeperationEvaluatorTests
    {
        private static SeperatedCssViewModel Run(string[] testData)
        {
            var obj = new PageCssSeperationEvaluator(new CssBlockContentEvaluator(), new CssFileNameEvaluator(new SolutionRelativeDirectoryEvaluator()));
            return obj.Evaluate(testData, @"C:\dir1\dir2\dir3", @"C:\dir1\dir2\dir3\Content\BlockCss\Search", @"LoggingResultGrid.cshtml");
        }

        [Test]
        public void VerySimple()
        {
            var testData = new[]
            {
                "<style type=\"text/css\">",
                "   body{float:left;}",
                "</style>",
            };

            var result = Run(testData);

            Assert.AreEqual(1, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[0]);
        }

        [Test]
        public void VerySimpleDirty()
        {
            var testData = new[]
            {
                "<style>",
                "   body{float:left;}",
                "</style>",
            };

            var result = Run(testData);

            Assert.AreEqual(1, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[0]);
        }


        [Test]
        public void CommonFormat()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <style type=\"text/css\">",
                "         body{float:left;}",
                "      </style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[2]);
        }

        [Test]
        public void CorrectReferenceToNewCssFile()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <style type=\"text/css\">",
                "         body{float:left;}",
                "      </style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[2]);
            Assert.AreEqual("   </head>", result.StripedContent[3]);
            Assert.AreEqual("   <body>", result.StripedContent[4]);
            Assert.AreEqual("      <div>some text</div>", result.StripedContent[5]);
            Assert.AreEqual("   <body>", result.StripedContent[6]);
            Assert.AreEqual("</html>", result.StripedContent[7]);
        }

        [Test]
        public void CorrectReferenceToTwoCssFiles()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <style type=\"text/css\">",
                "         body{float:left;}",
                "      </style>",
                "      <style type=\"text/css\">",
                "         body{float:right;}",
                "      </style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[2]);
            Assert.AreEqual("   </head>", result.StripedContent[3]);
            Assert.AreEqual("   <body>", result.StripedContent[4]);
            Assert.AreEqual("      <div>some text</div>", result.StripedContent[5]);
            Assert.AreEqual("   <body>", result.StripedContent[6]);
            Assert.AreEqual("</html>", result.StripedContent[7]);
        }

        [Test]
        public void CorrectReferenceToTwoCssFiles2()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <style type=\"text/css\">",
                "         body{float:left;}",
                "      </style>",
                "<!--some text-->",
                "      <style type=\"text/css\">",
                "         body{float:right;}",
                "      </style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);



            Assert.AreEqual("<html>", result.StripedContent[0]);
            Assert.AreEqual("   <head>", result.StripedContent[1]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.StripedContent[2]);
            Assert.AreEqual("<!--some text-->", result.StripedContent[3]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid_fragment1.css\" rel=\"stylesheet\" />", result.StripedContent[4]);
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
                "      <style type=\"text/css\">body{float:left;}</style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };


            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
        }

        [Test]
        public void CssAndClosingTagOnSameLine()
        {
            var testData = new[]
            {
                "<html>",
                "   <head>",
                "      <style type=\"text/css\">",
                "           body{float:left;}</style>",
                "   </head>",
                "   <body>",
                "      <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            Assert.AreEqual(8, result.StripedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
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
                "      <style type=\"text/css\">",
                "           body{float:left;}</style><div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.ReplacementLines.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
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
                "      <style type=\"text/css\">body{float:left;}" +
                "   </style><div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.ReplacementLines.Count());
            Assert.AreEqual(1, result.ExtractedCssBlocks.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
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
                "      <style type=\"text/css\">",
                "           body{float:left;}",
                "           ",
                "           div{float:left;}",
                "       </style>",
                "       <div>some text</div>",
                "   <body>",
                "</html>"
            };

            var result = Run(testData);

            // Assert.AreEqual(7, result.ReplacementLines.Count());
            Assert.AreEqual(2, result.ExtractedCssBlocks[0].Lines.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssBlocks[0].Lines[0]);
            Assert.AreEqual("div{float:left;}", result.ExtractedCssBlocks[0].Lines[1]);
        }
    }
}