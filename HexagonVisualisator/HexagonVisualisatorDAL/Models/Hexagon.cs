using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonVisualisatorDAL.Models
{
    /// <summary>
    /// Class what represents Hexagon.
    /// </summary>
    [Serializable]
    public class Hexagon
    {
        /// <summary>
        /// List of all Vertexes.
        /// </summary>
        public List<Point> Vertexes { get; set; }

        /// <summary>
        /// Hexagon center.
        /// </summary>
        public Point HexagonCenter { get; set; }

        /// <summary>
        /// Length of side of hexagon.
        /// </summary>
        public int SideLength { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Hexagon() { }

        /// <summary>
        /// Constructor what build hexagon with equels sides from center of hexagon and one of it's vertex.
        /// </summary>
        /// <param name="center"> Center of hexagon. </param>
        /// <param name="vertex"> One of vertexes of hexagon. </param>
        /// <param name="sideLength"> Length of side of hexagon. </param>
        public Hexagon(Point center, Point vertex, int sideLength)
        {
            Vertexes = new List<Point>();
            HexagonCenter = center;
            SideLength = sideLength;
            double x = vertex.X;
            double y = vertex.Y;
            Vertexes.Add(vertex);
            for (int side = 0; side < 6; side++)
            {
                x += SideLength * Math.Cos(side * 2 * Math.PI / 6);
                y += SideLength * Math.Sin(side * 2 * Math.PI / 6);
                Vertexes.Add(new Point(x, y));
            }
        }

    }
}
