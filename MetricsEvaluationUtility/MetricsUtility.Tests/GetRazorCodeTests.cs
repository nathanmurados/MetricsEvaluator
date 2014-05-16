using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;

    [TestFixture]
    public class GetRazorCodeTests
    {
        [Test]
        public void Extract_Razor_1()
        {
            // Arrange
            var evaluator = new GetJsToRefactor();
            string input = "var selectedMenu = '@ViewBag.MenuInstanceName';";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("'@ViewBag.MenuInstanceName'", result);
        }

        [Test]
        public void Extract_Razor_2()
        {
            throw new NotImplementedException("Would who ever works on this please discuss this with Nathan");

            // Arrange
            var evaluator = new GetJsToRefactor();
            string input = "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("Url.Action(\"Configure\", \"ConfigureMenu\")", result);
        }
        [Test]
        public void Extract_Razor_3()
        {
            // Arrange
            var evaluator = new GetJsToRefactor();
            string input = "$('#DecommisionReason').val('@decommisionReason');";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("'@decommisionReason'", result);
        }
        [Test]
        public void Extract_Razor_4()
        {
            // Arrange
            var evaluator = new GetJsToRefactor();
            string input = "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("@Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList))", result);
        }
        [Test]
        public void Extract_Razor_5()
        {
            // Arrange
            var evaluator = new GetJsToRefactor();
            
            // NB contains 2 fragments of razor
            string input = " data: \"{'docId1':'\" + '@ViewBag.docid' + \"','conditionType1':'\" + '@ViewBag.doctype' + \"'}\",";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("'@ViewBag.doctype'", result);
        }
        [Test]
        public void Extract_Razor_6()
        {
            // Arrange
            var evaluator = new GetJsToRefactor();
            string input = "$('#HiddenName').val('@Convert.ToString(stateWatcherVM.LName)');";

            // Act
            IEnumerable<string> result = evaluator.GetFragment(input);

            // Assert
            Assert.AreEqual("@Convert.ToString(stateWatcherVM.LName)'", result);
        }
        
    }
}
