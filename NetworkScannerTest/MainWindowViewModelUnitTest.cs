using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkScanner;
using System.Linq;

namespace NetworkScannerTest
{
    [TestClass]
    public class MainWindowViewModelUnitTest
    {
        private MainWindowViewModel vm;

        public MainWindowViewModelUnitTest()
        {
            IoC.Setup();
            vm = new MainWindowViewModel();
        }

        [TestMethod]
        public void TitleShouldBeNetworkScanner()
        {
            // Arrange
            var title = "Network Scanner";
            // Assert
            Assert.AreEqual(vm.Title, title);
        }
    }
}
