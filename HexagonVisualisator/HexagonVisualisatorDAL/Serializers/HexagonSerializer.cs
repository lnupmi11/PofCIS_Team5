using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using HexagonVisualisatorDAL.Models;

namespace HexagonVisualisatorDAL.Serializers
{
    /// <summary>
    /// XML-Serializer for class Hexagon. 
    /// </summary>
    public class HexagonSerializer
    {
        /// <summary>
        /// Method what serialize list of Hexagons data.
        /// </summary>
        /// <param name="hexagons"> Hexagons data to serialize.</param>
        /// <param name="fileName"> Path of file, where serialize data will be stored. </param>
        public void Serialize(List<Hexagon> hexagons, string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Hexagon>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, hexagons);
            }
        }

        /// <summary>
        /// Method what deserialize data from file into list of Hexagons.
        /// </summary>
        /// <param name="fileName"> Path of file, where serialize data is stored. </param>
        /// <returns> List of hexagons </returns>
        public List<Hexagon> Deserialize(string fileName)
        {
            List<Hexagon> hexagons;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Hexagon>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                hexagons = (List<Hexagon>)formatter.Deserialize(fs);
            }
            return hexagons;
        }
    }
}
