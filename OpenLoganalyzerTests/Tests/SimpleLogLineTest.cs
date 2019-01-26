using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenLoganalyzerLib.Core;
using OpenLoganalyzerLib.Core.Loglines;
using System.Collections.Generic;
using System.Threading;

namespace OpenLoganalyzerTests
{
    [TestClass]
    public class SimpleLogLineTest
    {
        const string THROWNBY = "Test class";
        const string MESSAGE = "Test message";
        const string SEVERITY = "Critical!";

        const string ADDITIONALKEY = "TestKey";
        const string ADDITIONALVALUE = "TestValue";

        [TestMethod]
        public void TestBasicSetup1()
        {
            DateTime dt = DateTime.Now;


            SimpleLogline logline = new SimpleLogline(THROWNBY, dt, MESSAGE, SEVERITY, new Dictionary<string, string>());

            Assert.AreEqual(THROWNBY, logline.ThrownBy);
            Assert.AreEqual(MESSAGE, logline.Message);
            Assert.AreEqual(SEVERITY, logline.Severity);
            Assert.AreEqual(dt, logline.LogTime);
            Assert.AreEqual(0, logline.AdditionalFilters.Count);
        }

        [TestMethod]
        public void TestBasicSetup2()
        {
            DateTime dt = DateTime.Now;


            SimpleLogline logline = new SimpleLogline(THROWNBY, dt, MESSAGE, SEVERITY, new Dictionary<string, string>());

            Thread.Sleep(2000);

            Assert.AreEqual(dt, logline.LogTime);
        }

        [TestMethod]
        public void TestBasicSetup3()
        {
            DateTime dt = DateTime.Now;

            Dictionary<string, string> testAdditional = new Dictionary<string, string>
            {
                { ADDITIONALKEY, ADDITIONALVALUE }
            };

            SimpleLogline logline = new SimpleLogline(THROWNBY, dt, MESSAGE, SEVERITY, testAdditional);

            Assert.AreEqual(ADDITIONALVALUE, logline.GetAdditionalContent(ADDITIONALKEY));
            Assert.AreEqual(ADDITIONALKEY, logline.AdditionalFilters[0]);
        }
    }
}
