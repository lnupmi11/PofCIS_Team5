using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
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
        /// Saves hexagon color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Hexagon()
        {
            Vertexes = new List<Point>();
            Color = Color.FromArgb(255, 20, 20, 90);
        }

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


        /// <summary>
        /// Method that checks if point is in polygon.
        /// </summary>
        /// <param name="point"> point that is going to be checked </param>
        public bool IsInPolygon(Point point)
        {
            var coef = Vertexes.Skip(1).Select((p, i) =>
                                            (point.Y - Vertexes[i].Y) * (p.X - Vertexes[i].X)
                                          - (point.X - Vertexes[i].X) * (p.Y - Vertexes[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method to compute the centroid of a polygon. This does NOT work for a complex polygon.
        /// </summary>
        /// <param name="poly">points that define the polygon</param>
        /// <returns>centroid point, or PointF.Empty if something wrong</returns>
        public Point GetCentroid()
        {
            double accumulatedArea = 0.0f;
            double centerX = 0.0f;
            double centerY = 0.0f;

            for (int i = 0, j = Vertexes.Count - 1; i < Vertexes.Count; j = i++)
            {
                double temp = Vertexes[i].X * Vertexes[j].Y - Vertexes[j].X * Vertexes[i].Y;
                accumulatedArea += temp;
                centerX += (Vertexes[i].X + Vertexes[j].X) * temp;
                centerY += (Vertexes[i].Y + Vertexes[j].Y) * temp;
            }

            if (Math.Abs(accumulatedArea) < 1E-7f)
                return null;  // Avoid division by zero

            accumulatedArea *= 3f;
            return new Point(centerX / accumulatedArea, centerY / accumulatedArea);
        }

    }
}
