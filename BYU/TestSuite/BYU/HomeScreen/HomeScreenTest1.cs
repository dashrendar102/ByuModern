using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
// Use the following to unit test private methods.
// If you don't alias the following using, it will conflict with the unit test framework.
using TestTools = Microsoft.VisualStudio.TestTools.UnitTesting;
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
            TestTools.PrivateObject obj = new TestTools.PrivateObject(page);
            string netId = "asdf"; // TODO need a test user/pass
            string pass = "asdf";
            obj.Invoke("DoLogin",new string[] {netId, pass});
            // TODO assert login succeeded
        }

        [TestMethod]
        public void Logout()
        {
        }
    }
}
