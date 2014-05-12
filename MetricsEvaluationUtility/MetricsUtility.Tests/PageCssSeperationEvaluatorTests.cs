using System.ComponentModel;
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

            Assert.AreEqual(8, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
            Assert.AreEqual("<link href=\"~/Content/BlockCss/Search/LoggingResultGrid.css\" rel=\"stylesheet\" />", result.UpdatedContent[2]);
        }

        private SeperatedCssViewModel Run(string[] testData)
        {
            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator(), new CssFileNameEvaluator(new SolutionRelativeFilenameEvaluator()));
            return obj.Evaluate(testData, @"C:\dir1\dir2\dir3\", @"C:\dir1\dir2\dir3\Content\BlockCss\Search\", @"LoggingResultGrid.cshtml");
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

            Assert.AreEqual(8, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
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

            Assert.AreEqual(8, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
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

            // Assert.AreEqual(7, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
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

            // Assert.AreEqual(7, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
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

            // Assert.AreEqual(7, result.UpdatedContent.Count());
            Assert.AreEqual(2, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
            Assert.AreEqual("div{float:left;}", result.ExtractedCssContent[1]);
        }
    }
}