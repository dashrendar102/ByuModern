using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BYU;

namespace TestSuite
{
    // URL for assert class descriptions http://msdn.microsoft.com/library/ms182530.aspx

    [TestClass]
    public class HomeScreenTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Login()
        {
            HubPage page = new BYU.HubPage();
            //string netId = "asdf"; // TODO need a test user/pass
            //string pass = "asdf";
            // TODO assert login succeeded
        }

        [TestMethod]
        public void Logout()
        {
        }
    }
}
