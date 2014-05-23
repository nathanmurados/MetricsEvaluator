using System.Collections.Generic;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Evaluators.JavaScript.LineEvaluator2;
using NUnit.Framework;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class JsModuleLineEvaluatorTestsNathan
    {
        private static IJsModuleLineEvaluator GetEvaluator()
        {
            return new JsModuleLineEvaluator2();
        }
        
        [Test]
        public void Quoted_ViewBag()
        {
            var evaluator = GetEvaluator();
            const string input = "var selectedMenu = '@ViewBag.MenuInstanceName';";

            List<Fragment> result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@ViewBag.MenuInstanceName", result[0].Text);
        }

        [Test]
        public void Double_Quoted_ViewBag()
        {
            var evaluator = GetEvaluator();
            const string input = "$(completionStatus).val(\"@Model.OtherDetails.PageCompletionStatus\");";

            var result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@Model.OtherDetails.PageCompletionStatus", result[0].Text);
        }


        [Test]
        public void Single_Quoted_UrlAction()
        {
            var evaluator = GetEvaluator();
            const string input = "var addPageUrl = '@Url.Action(\"Configure\", \"ConfigureMenu\")';";

            var result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@Url.Action(\"Configure\", \"ConfigureMenu\")", result[0].Text);
        }


        [Test]
        public void Double_Quoted_UrlAction_Simple()
        {
            var evaluator = GetEvaluator();
            const string input = "var url = \"@x(\"y\")\";";

            var result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@x(\"y\")", result[0].Text);
        }

        [Test]
        public void Double_Quoted_UrlAction()
        {
            var evaluator = GetEvaluator();
            const string input = "var url = \"@Url.Action(\"LoadTrendAnalysisChart\", \"WidgetGallery\", new { communityId = \"_Id\", startYear = \"_Sdate\", endYear = \"_eDate\", trendParametr = \"_trend\" })\";";

            var result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@Url.Action(\"LoadTrendAnalysisChart\", \"WidgetGallery\", new { communityId = \"_Id\", startYear = \"_Sdate\", endYear = \"_eDate\", trendParametr = \"_trend\" })", result[0].Text);
        }

        [Test]
        public void Extract_Razor_BlockRazor_Exception()
        {
            // Arrange
            var evaluator = GetEvaluator();
            string input = "@{ column++;}";

            // Act
            var result = evaluator.Evaluate(input);
            //Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("@{ column++;}", result[0].Text);
        }


        [Test]
        public void Quoted_JQuery_val()
        {
            var evaluator = GetEvaluator();
            string input = "$('#DecommisionReason').val('@decommisionReason');";

            List<Fragment> result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@decommisionReason", result[0].Text);
        }
        [Test]
        public void Extract_Razor_Not_Quoted()
        {
            var evaluator = GetEvaluator();
            string input = "globalFunction = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList));";

            List<Fragment> result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Unquoted);
            Assert.AreEqual("@Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.GlobalFunctionVmList))", result[0].Text);
        }

        [Test]
        public void Quoted_But_Contains_Spaces_1()
        {
            var evaluator = GetEvaluator();
            string input = "if (\"@(Model.OtherDetails.ApplicantContactDetails == null)\" != \"true\") {";

            List<Fragment> result = evaluator.Evaluate(input);

            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@(Model.OtherDetails.ApplicantContactDetails == null)", result[0].Text);
        }

        [Test]
        public void Two_Quoted_Fragments()
        {
            var evaluator = GetEvaluator();

            string input = " data: \"{'docId1':'\" + '@ViewBag.docid' + \"','conditionType1':'\" + '@ViewBag.doctype' + \"'}\",";

            List<Fragment> result = evaluator.Evaluate(input);

            Assert.AreEqual(2, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            //Assert.IsTrue(result[1].FragType == FragType.Quoted);
            Assert.AreEqual("@ViewBag.docid", result[0].Text);
            Assert.AreEqual("@ViewBag.doctype", result[1].Text);
        }

        [Test]
        public void Quoted_ConvertToString()
        {
            var evaluator = GetEvaluator();
            string input = "$('#HiddenName').val('@Convert.ToString(stateWatcherVM.LName)');";

            List<Fragment> result = evaluator.Evaluate(input);
            // Assert
            Assert.AreEqual(1, result.Count);
            //Assert.IsTrue(result[0].FragType == FragType.Quoted);
            Assert.AreEqual("@Convert.ToString(stateWatcherVM.LName)", result[0].Text);
        }

        [Test]
        public void With_text_to_left_Exception()
        {
            var evaluator = GetEvaluator();
            const string input = "alert('text left @Viewbag.Variable');";

            var result = evaluator.Evaluate(input);

            //Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            Assert.AreEqual("@Viewbag.Variable", result[0].Text);
        }
        
        [Test]
        public void With_text_to_right_Exception()
        {
            var evaluator = GetEvaluator();
            string input = "alert('@Viewbag.Variable text right');";

            List<Fragment> result = evaluator.Evaluate(input);

            //Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            Assert.AreEqual("@Viewbag.Variable", result[0].Text);
        }

        [Test]
        public void With_text_eitherside_Exception()
        {
            var evaluator = GetEvaluator();
            const string input = "alert('text left @Viewbag.Variable text right');";

            var result = evaluator.Evaluate(input);

            //Assert.Throws<UnhandledPatternException>(() => evaluator.Evaluate(input));

            Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(FragType.RequiresManualCheck, result[0].FragType);
            Assert.AreEqual("@Viewbag.Variable", result[0].Text);
        }
    }
}
