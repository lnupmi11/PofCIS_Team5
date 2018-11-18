using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.DataTypes
{
    /// <summary>
    /// Class what represents city.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Id number of city.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Method what write all data about city in string and returns it.
        /// </summary>
        /// <returns>String with information about city (id, name). </returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", Id, Name);
        }
    }
}
