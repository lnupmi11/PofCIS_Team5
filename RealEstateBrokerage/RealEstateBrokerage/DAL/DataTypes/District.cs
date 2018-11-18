using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.DataTypes
{
    /// <summary>
    /// Class what represents district.
    /// </summary>
    public class District
    {
        /// <summary>
        /// Id of district.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Id of city where district is situated.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Name of district.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cityId"></param>
        /// <param name="name"></param>
        public District(int id, int cityId, string name)
        {
            Id = id;
            CityId = cityId;
            Name = name;
        }
        
        /// <summary>
        /// Method what write all data about district in string and returns it.
        /// </summary>
        /// <returns>String with information about district (id,cityId ,name). </returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, CityId, Name);
        }
    }
}
