using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexagonVisualisatorDAL.Models;
using System.Collections.Generic;
using System.Windows.Media;

namespace UnitTestProject1
{
    [TestClass]
    public class HexagonTests
    {
        [TestMethod]
        public void Test_PointsProperty()
        {
            Hexagon Hexagon = new Hexagon();
            List<Point> points = new List<Point>
            {
                new Point(67, 89),
                new Point(67, 45),
                new Point(56, 23),
                new Point(10, 9),
                new Point(100, 145),
                new Point(23, 76)
            };
            Hexagon.Vertexes = points;
            Assert.AreEqual(Hexagon.Vertexes, points);
        }

        [TestMethod]
        public void Test_HexagonColorProperty()
        {
            Hexagon Hexagon = new Hexagon();
            Hexagon.Color = Colors.Red;
            Assert.AreEqual(Hexagon.Color, Colors.Red);
        }
    }
}
