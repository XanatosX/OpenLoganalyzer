using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using OpenLoganalyzerLib.Core.LoaderCofiguration;
using OpenLoganalyzerTests.Helper;

namespace OpenLoganalyzerTests
{
    [TestClass]
    public class SimpleConfigurationLoaderTest
    {
        private string testFile;

        [TestInitialize()]
        public void Initialize()
        {
            testFile = testFile.CreateTestPath();
        }

        [TestMethod]
        public void TestBasicSetupUp()
        {
            SimpleConfigurationLoader simpleConfigurationLoader = new SimpleConfigurationLoader();
        
            Assert.IsTrue(simpleConfigurationLoader != null);
        }

        [TestMethod]
        public void TestBasicSaving()
        {
            SimpleConfigurationLoader simpleConfigurationLoader = new SimpleConfigurationLoader();

            Mock<ILoaderConfiguration> configuration = new Mock<ILoaderConfiguration>();
            configuration.Setup(x => x.LoaderType).Returns(OpenLoganalyzerLib.Core.Enum.LoaderTypeEnum.FileLoader);
            configuration.Setup(x => x.Filters).Returns(new System.Collections.Generic.Dictionary<string, string>());
            configuration.Setup(x => x.FilterNames).Returns(new System.Collections.Generic.List<string>());

            simpleConfigurationLoader.Save(configuration.Object, testFile);

            Assert.IsTrue(simpleConfigurationLoader != null);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete(testFile);
        }
    }
}
