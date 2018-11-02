using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenLoganalyzer.Core.Factories;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Loader;

namespace OpenLoganalyzerTests
{
    [TestClass]
    public class LoaderFactoryTest
    {
        [TestMethod]
        public void TestBasicSetup()
        {
            ILoaderFactory factory = new LoaderFactory();
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void TestBasicSetup2()
        {
            ILoaderFactory factory = new LoaderFactory();
            Assert.IsNotNull(factory);
            Assert.IsNull(factory.GetLoader(null));
        }

        [TestMethod]
        public void TestFileLoaderReturn()
        {
            ILoaderFactory factory = new LoaderFactory();

            Mock<ILoaderConfiguration> testConfig = new Mock<ILoaderConfiguration>();
            testConfig.Setup(x => x.LoaderType).Returns(LoaderTypeEnum.FileLoader);

            ILoader loader = factory.GetLoader(testConfig.Object);
            Assert.AreEqual(typeof (FileLoader), loader.GetType());
        }
    }
}
