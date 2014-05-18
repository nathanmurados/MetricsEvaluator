﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace MetricsUtiltiy.Tests
{
    using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.ViewModels;

    /// <summary>
    /// Extract a razor fragment that is not immediately bounded by quotes.
    /// It is still within quotes, but there's some text between the quote and the razor fragment.
    /// This is not just an issue for extracting the fragment, but for how we modify the line.
    /// 
    /// For example: alert('text left @Viewbag.Variable');
    /// Will become: alert('text left' + ap2.ViewbagVariable);
    /// 
    /// For example: alert('text left @Viewbag.Variable right text');
    /// Will become: alert('text left' + ap2.ViewbagVariable + 'right text');
    /// 
    /// For example: alert('@Viewbag.Variable right text');
    /// Will become: alert(ap2.ViewbagVariable + 'right text');
    /// 
    /// </summary>
    [TestFixture]
    public class JsModuleLineEvaluatorTests
    {
        [Test]
        public void DeDuplicationTest()
        {
            // Just a quick test of the de-duplication of a list of objects based on an object property value.

            // Arrange
            List<JsModuleViewModel> totalRazorLines = new List<JsModuleViewModel>();
            totalRazorLines.Add(new JsModuleViewModel() {OriginalRazorText  = "'@serverVariable1'", JavaScriptName = "serverVariable1"});
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable1'", JavaScriptName = "serverVariable1" }); // Duplicate
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" });
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable3'", JavaScriptName = "serverVariable3" });
            totalRazorLines.Add(new JsModuleViewModel() { OriginalRazorText = "'@serverVariable2'", JavaScriptName = "serverVariable2" }); // Duplicate
            
            // Act
            totalRazorLines = totalRazorLines.Distinct().ToList();

            // Assert
            Assert.AreEqual(3, totalRazorLines.Count);
            Assert.AreEqual(totalRazorLines[0].JavaScriptName, "serverVariable1");
            Assert.AreEqual(totalRazorLines[1].JavaScriptName, "serverVariable2");
            Assert.AreEqual(totalRazorLines[2].JavaScriptName, "serverVariable3");
        }

        [Test]
        public void Extract_Razor_wth_text_to_left()
        {
            // Arrange


            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable');";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("@Viewbag.Variable'", result[0]);
        }

        [Test]
        public void Extract_Razor_wth_text_to_right()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('@Viewbag.Variable text right');";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("'@Viewbag.Variable", result[0]);
        }

        [Test]
        public void Extract_Razor_wth_text_eitherside()
        {
            // Arrange
            var evaluator = new JsModuleLineEvaluator();
            string input = "alert('text left @Viewbag.Variable text right');";

            // Act
            string[] result = evaluator.Evaluate(input);

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("@Viewbag.Variable", result[0]);
        }
        

    }
}
