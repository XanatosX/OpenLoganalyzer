using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenLoganalyzerLib.Core.LoaderCofiguration;
using OpenLoganalyzerLib.Core.Enum;
using System.Collections.Generic;

namespace OpenLoganalyzerTests
{
    [TestClass]
    public class SimpleConfigurationTest
    {
        const string TEST_FILTER_CONTENT = "ASDF";
        const string TEST_FILE_PATH = @"C:\Test\File\Path";

        [TestMethod]
        public void TestBasicSetup1()
        {
            SimpleConfiguration config = new SimpleConfiguration(LoaderTypeEnum.FileLoader, new Dictionary<string, string>(), new Dictionary<string, string>());

            Assert.AreEqual(LoaderTypeEnum.FileLoader, config.LoaderType);
            Assert.AreEqual(0, config.FilterNames.Count);
            Assert.AreEqual(0, config.Filters.Count);
        }

        [TestMethod]
        public void TestBasicSetup2()
        {
            Dictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add(FilterTypeEnum.Caller.ToString(), TEST_FILTER_CONTENT);
            SimpleConfiguration config = new SimpleConfiguration(LoaderTypeEnum.FileLoader, filters, new Dictionary<string, string>());

            Assert.AreEqual(LoaderTypeEnum.FileLoader, config.LoaderType);

            Assert.AreEqual(1, config.FilterNames.Count);
            Assert.AreEqual(FilterTypeEnum.Caller.ToString(), config.FilterNames[0]);
            Assert.AreEqual(TEST_FILTER_CONTENT, config.Filters[FilterTypeEnum.Caller.ToString()]);
            Assert.AreEqual(1, config.Filters.Count);
        }

        [TestMethod]
        public void TestBasicSetup3()
        {
            Dictionary<string, string> additional = new Dictionary<string, string>();
            additional.Add(AdditionalSettingsEnum.FilePath.ToString(), TEST_FILE_PATH);
            SimpleConfiguration config = new SimpleConfiguration(LoaderTypeEnum.FileLoader, new Dictionary<string, string>(), additional);

            Assert.AreEqual(LoaderTypeEnum.FileLoader, config.LoaderType);
            Assert.AreEqual(0, config.FilterNames.Count);
            Assert.AreEqual(0, config.Filters.Count);

            Assert.AreEqual(TEST_FILE_PATH, config.GetAdditionalSetting(AdditionalSettingsEnum.FilePath));
            Assert.AreEqual(TEST_FILE_PATH, config.GetAdditionalSetting("FilePath"));
        }
    }
}
