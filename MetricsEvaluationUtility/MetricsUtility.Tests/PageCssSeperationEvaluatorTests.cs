using System.Linq;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Refactorers;
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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

            Assert.AreEqual(7, result.UpdatedContent.Count());
            Assert.AreEqual(1, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

            Assert.AreEqual(7, result.UpdatedContent.Count());
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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

            Assert.AreEqual(7, result.UpdatedContent.Count());
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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

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

            var obj = new PageCssSeperationEvaluator(new CssPageEvaluator());

            var result = obj.Evaluate(testData);

            // Assert.AreEqual(7, result.UpdatedContent.Count());
            Assert.AreEqual(2, result.ExtractedCssContent.Count());
            Assert.AreEqual("body{float:left;}", result.ExtractedCssContent[0]);
            Assert.AreEqual("div{float:left;}", result.ExtractedCssContent[1]);
        }
    }
}