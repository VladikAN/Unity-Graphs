using Graphs.Core.Domain;
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
    }
}