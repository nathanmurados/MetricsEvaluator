using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;
    using MetricsUtility.Core.ViewModels;
    
    /// <summary>
    /// Testing ability to pull razor fragments from a line of javascript.
    /// </summary>
    [TestFixture]
    public class JsModuleLineEvaluatorTests
    {
        [Test]
        public void Extract_Razor_ViewBag()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "var selectedMenu = '@ViewBag.MenuInstanceName';";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("'@ViewBag.MenuInstanceName'", result[0]);
        }

        [Test]
        public void Extract_Razor_UrlAction()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("'@Url.Action(\"Configure\", \"ConfigureMenu\")'", result[0]);
        }
        [Test]
        public void Extract_Razor_JQuery_val()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "$('#DecommisionReason').val('@decommisionReason');";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("'@decommisionReason'", result[0]);
        }
        [Test]
        public void Extract_Razor_No_Quotes()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("@Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList))", result[0]);
        }
        [Test]
        public void Extract_Razor_2_Fragments()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();

            // NB contains 2 fragments of razor
            string input = " data: \"{'docId1':'\" + '@ViewBag.docid' + \"','conditionType1':'\" + '@ViewBag.doctype' + \"'}\",";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("'@ViewBag.docid'", result[0]);
            Assert.AreEqual("'@ViewBag.doctype'", result[1]);
        }
        [Test]
        public void Extract_Razor_ConertToString()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "$('#HiddenName').val('@Convert.ToString(stateWatcherVM.LName)');";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("'@Convert.ToString(stateWatcherVM.LName)'", result[0]);
        }


        /// <summary>
        /// Extract a razor fragment that is not immediately bounded by quotes.
        /// It is still within quotes, but there's some text between the quote and the razor fragment.
        /// This is not just an issue for extracting the fragment, but for how we modify the line.
        /// 
        /// For example: alert('text left @Viewbag.Variable');
        /// Will become: alert('text left' + ap2.ViewbagVariable);
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_to_left()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable');";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("@Viewbag.Variable'", result[0]);
        }
        
        /// <summary>
        /// Text to right. 
        /// For example: alert('@Viewbag.Variable right text');
        /// Will become: alert(ap2.ViewbagVariable + 'right text');/// 
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_to_right()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('@Viewbag.Variable text right');";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("'@Viewbag.Variable", result[0]);
        }

        /// <summary>
        /// Text either side.
        // For example: alert('text left @Viewbag.Variable right text');
        /// Will become: alert('text left' + ap2.ViewbagVariable + 'right text');
        /// </summary>
        [Test]
        public void Extract_Razor_wth_text_eitherside()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable text right');";

            // Act
            List<string> result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("@Viewbag.Variable", result[0]);
        }
    }
}
