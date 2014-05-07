using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;
using MetricsUtility.Core.ViewModels;
using Moq;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JavaScriptStorage
    {
        [Test]
        public void StoreCss()
        {
            var mockHumanInterface = new Mock<IHumanInterface>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockStorer = new Mock<IStorer>();
            var mockNamer = new Mock<ICssStatsFileNameEvaluator>();

            mockStorer.Setup(x => x.Store(It.IsAny<StringBuilder>(), It.IsAny<string>())).Callback<StringBuilder, string>((sb, title) =>
            {
                var str = sb.ToString();

                Assert.AreEqual(18, str.Count(x => x == ','));
                Assert.AreEqual(2, Regex.Matches(str, Environment.NewLine).Count);
            });

            var evaluator = new CssStatsStorer(mockStorer.Object, mockDateTimeProvider.Object, mockHumanInterface.Object, mockNamer.Object);

            var testData = new List<CssEvaluationResult>
            {
                new CssEvaluationResult
                {
                    Inline = new List<DetailedCssEvaluationResult>
                    {
                        new DetailedCssEvaluationResult{ Value = "<div style='display:none;'></div>"}
                    },
                    FileName = "test",
                    Page = new List<int>{3,5},
                    Razor = new List<DetailedCssEvaluationResult>
                    {
                        new DetailedCssEvaluationResult{ Value = "{ style = 'float:left;' }"}
                    },
                }
            };

            evaluator.Store(testData);
        }
    }
}