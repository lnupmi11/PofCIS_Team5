using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonVisualisatorDAL.Models
{
    /// <summary>
    /// Class what represents data about Point
    /// </summary>
    [Serializable]
    public class Point
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Point() { }
        
        /// <summary>
        /// Hold X cordinate of Point.
        /// </summary>
        public double X { get; set; }
        
        /// <summary>
        /// Hold Y cordinate of Point. 
        /// </summary>
        public double Y { get; set; }
    }
}
