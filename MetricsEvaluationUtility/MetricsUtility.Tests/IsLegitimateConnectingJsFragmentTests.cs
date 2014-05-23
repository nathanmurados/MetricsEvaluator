using System;
using MetricsUtility.Core.Services.RefactorServices;
using NUnit.Framework;

namespace MetricsUtiltiy.Tests
{
    [TestFixture]
    public class IsLegitimateConnectingJsFragmentTests
    {
        [Test]
        public void Test01()
        {
            Assert.IsTrue("+ 'text'".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test02()
        {
            Assert.IsTrue("'text' +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test03()
        {
            Assert.IsTrue(" + 'text' + ".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test04()
        {
            Assert.IsTrue(" + 'text' ".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test05()
        {
            Assert.IsTrue(" 'text' + ".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test07()
        {
            Assert.IsTrue(" + 'text' + ".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test08()
        {
            Assert.IsTrue(" + 'text'".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test09()
        {
            Assert.IsTrue(" 'text' +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test10()
        {
            Assert.IsTrue(" + 'text' +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test11()
        {
            Assert.IsTrue(" + \"text\"".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test12()
        {
            Assert.IsTrue(" \"text\" +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test13()
        {
            Assert.IsTrue(" + \"text\" +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test14()
        {
            Assert.Throws<NotImplementedException>(() => " + \"text' +".IsLegitimateConnectingJsFragment());
        }


        [Test]
        public void Test15()
        {
            Assert.IsFalse("text".IsLegitimateConnectingJsFragment());
        }

        [Test]
        public void Test16()
        {
            Assert.IsFalse(" + text".IsLegitimateConnectingJsFragment());
        }

        [Test]
        public void Test17()
        {
            Assert.IsFalse(" + text  '".IsLegitimateConnectingJsFragment());
        }

        [Test]
        public void Test18()
        {
            Assert.IsTrue("'some text ' + \" and some more text \" + ".IsLegitimateConnectingJsFragment());
        }
    }
}