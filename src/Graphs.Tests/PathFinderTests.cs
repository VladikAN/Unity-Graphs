using Graphs.Domain;
using NUnit.Framework;

namespace Graphs.Tests
{
    [TestFixture]
    public class PathFinderTests
    {
        [Test]
        public void Find_ValidGraph_FondedWay()
        {
            var A = new Waypoint("A");
            var B = new Waypoint("B");
            var C = new Waypoint("C");
            var D = new Waypoint("D");

            var A_B = new Connector(A, B, 2);
            var A_C = new Connector(A, C, 1);
            var B_D = new Connector(B, D, 1);
            var C_D = new Connector(C, D, 3);
            var connectors = new[] { A_B, A_C, B_D, C_D };

            var finder = new PathFinder(connectors);
            var result = finder.Find(A, D);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(D, result[2]);
        }

        [Test]
        public void Find_RunMultipleTimes_SameResult()
        {
            var A = new Waypoint("A");
            var B = new Waypoint("B");
            var C = new Waypoint("C");

            var A_B = new Connector(A, B, 2);
            var B_C = new Connector(A, C, 1);
            var connectors = new[] { A_B, B_C };

            var finder = new PathFinder(connectors);
            var result1 = finder.Find(A, C);
            var result2 = finder.Find(A, C);
            var result3 = finder.Find(A, C);

            Assert.IsNotNull(result1);
            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result2, result3);
        }

        [Test]
        public void Find_PathBlocked_NullResult()
        {
            var A = new Waypoint("A");
            var B = new Waypoint("B", true);
            var C = new Waypoint("C");

            var A_B = new Connector(A, B, 2);
            var B_C = new Connector(B, C, 1);
            var connectors = new[] { A_B, B_C };

            var finder = new PathFinder(connectors);
            var result = finder.Find(A, C);

            Assert.IsNull(result);
        }

        [Test]
        public void Update_NewConnectors_Updated()
        {
            var A = new Waypoint("A");
            var B = new Waypoint("B");
            var C = new Waypoint("C");
            var A_B = new Connector(A, B, 2);
            var B_C = new Connector(B, C, 2);

            var finder = new PathFinder(new[] { A_B });
            finder.Update(new[] { A_B, B_C });

            var result = finder.Find(A, C);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(C, result[2]);
        }
    }
}