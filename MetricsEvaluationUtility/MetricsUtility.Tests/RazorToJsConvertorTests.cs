using System.Collections.Generic;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    internal class Comparison
    {
        public string Expected { get; set; }
        public string Actual { get; set; }
    }

    internal class ConversionResult
    {
        public string Expected { get; set; }
        public string Converted { get; set; }
    }

    [TestFixture]
    public class RazorToJsConvertorTests
    {
        private ConversionResult RunTest(Comparison comparison)
        {
            var obj = new RazorToJsConvertor { RazorVariables = new List<JsModuleViewModel> { new JsModuleViewModel { JavaScriptName = "ViewbagVariable", OriginalRazorText = "@Viewbag.Variable" }, new JsModuleViewModel { JavaScriptName = "ViewbagVariable2", OriginalRazorText = "@Viewbag.Variable2" } } };
        
            return new ConversionResult
            {
                Expected = comparison.Expected,
                Converted = obj.Convert(comparison.Actual)
            };
        }

        [Test]
        public void Test00()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text @Viewbag.Variable'",
                Expected = "'some text ' + ap2.ViewbagVariable"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test01()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text @Viewbag.Variable with more'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' with more'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test02()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text @Viewbag.Variable @Viewbag.Variable2'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' ' + ap2.ViewbagVariable2"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test03()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + @Viewbag.Variable",
                Expected = "'some text ' + ap2.ViewbagVariable"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test03B()
        {
            var result = RunTest(new Comparison
            {
                Actual = "@Viewbag.Variable + ' some text'",
                Expected = "ap2.ViewbagVariable + ' some text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test04()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable'",
                Expected = "'some text ' + ap2.ViewbagVariable"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test04B()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable' + ' some text ' + '@Viewbag.Variable'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' some text ' + ap2.ViewbagVariable"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test05()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text @Viewbag.Variable and more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test06()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + @Viewbag.Variable + ' and more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }


        [Test]
        public void Test06B()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + \" and some more text \" + @Viewbag.Variable + ' and more text'",
                Expected = "'some text ' + \" and some more text \" + ap2.ViewbagVariable + ' and more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test07()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable' + ' and more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test08()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text @Viewbag.Variable and more text @Viewbag.Variable2 and even more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text ' + ap2.ViewbagVariable2 + ' and even more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test09()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + @Viewbag.Variable + ' and more text ' + @Viewbag.Variable2 + ' and even more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text ' + ap2.ViewbagVariable2 + ' and even more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test10()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable' + ' and more text ' + '@Viewbag.Variable2' + ' and even more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + ' and more text ' + ap2.ViewbagVariable2 + ' and even more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test11()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + @Viewbag.Variable + \" and more text\"",
                Expected = "'some text ' + ap2.ViewbagVariable + \" and more text\""
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test12()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable' + \" and more text\"",
                Expected = "'some text ' + ap2.ViewbagVariable + \" and more text\""
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test13()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + @Viewbag.Variable + \" and more text \" + @Viewbag.Variable2 + ' and even more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + \" and more text \" + ap2.ViewbagVariable2 + ' and even more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }

        [Test]
        public void Test14()
        {
            var result = RunTest(new Comparison
            {
                Actual = "'some text ' + '@Viewbag.Variable' + \" and more text \" + '@Viewbag.Variable2' + ' and even more text'",
                Expected = "'some text ' + ap2.ViewbagVariable + \" and more text \" + ap2.ViewbagVariable2 + ' and even more text'"
            });
            Assert.AreEqual(result.Expected, result.Converted);
        }
    }
}