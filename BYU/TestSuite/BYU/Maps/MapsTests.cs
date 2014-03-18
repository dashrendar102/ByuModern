using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Common;

namespace TestSuite
{
    [TestClass]
    public class MapsTests
    {
        [TestMethod]
        public void TestMapControl()
        {
            //This throws some kind of threading exception I can't figure out. This test will fail.
            ByuMap map = new ByuMap();

            var buildingsTask = map.GetBuildings();

            //Give the control 10 seconds to get maps
            Assert.IsTrue(buildingsTask.Wait(TimeSpan.FromSeconds(10)));

            var buildings = buildingsTask.Result.ToArray();

            Assert.IsTrue(buildings.Count() > 100);

            CollectionAssert.AllItemsAreNotNull(buildings);
            CollectionAssert.AllItemsAreUnique(buildings);

            var talmageBuilding = buildings.SingleOrDefault(bme => bme.Name.Contains("Talmage"));
            Assert.IsNotNull(talmageBuilding);

            map.SelectEntity(talmageBuilding);
        }
    }
}
