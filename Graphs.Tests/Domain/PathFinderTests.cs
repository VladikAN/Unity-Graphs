using Graphs.Domain;
using Xunit;

namespace Graphs.Tests.Domain
{
    public class PathFinderTests
    {
        [Fact]
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

            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal(A, result[0]);
            Assert.Equal(B, result[1]);
            Assert.Equal(D, result[2]);
        }

        [Fact]
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

            Assert.NotNull(result1);
            Assert.Equal(result1, result2);
            Assert.Equal(result2, result3);
        }

        [Fact]
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

            Assert.Null(result);
        }

        [Fact]
        public void Update_NewConnectors_Updated()
        {
            var A = new Waypoint("A");
            var B = new Waypoint("B");
            var C = new Waypoint("C");
            var A_B = new Connector(A, B, 2);
            var B_C = new Connector(B, C, 2);

            var finder = new PathFinder(new[] { A_B });
            finder.InitOrUpdate(new[] { A_B, B_C });

            var result = finder.Find(A, C);

            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal(A, result[0]);
            Assert.Equal(B, result[1]);
            Assert.Equal(C, result[2]);
        }
    }
}