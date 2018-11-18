using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.DataTypes
{
    /// <summary>
    /// Class what represents realEstate.
    /// </summary>
    public class RealEstate
    {
        /// <summary>
        /// Id of realEstate.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of city.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Id of district.
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Number of rooms in the flat.
        /// </summary>
        public int NumOfRooms { get; set; }

        /// <summary>
        /// Number of batths in the flat.
        /// </summary>
        public int NumOfBaths { get; set; }

        /// <summary>
        /// Contain information if flat is with terrace.
        /// </summary>
        public bool IsWithTerrace { get; set; }

        /// <summary>
        /// Contain information if flat is with views.
        /// </summary>
        public bool IsWithViews { get; set; }

        /// <summary>
        /// Contain information if flat is pent house.
        /// </summary>
        public bool IsPenthouse { get; set; }

        /// <summary>
        /// Contain information about monthly price of flat.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// COnstructor with parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cityId"></param>
        /// <param name="districtId"></param>
        /// <param name="numOfRooms"></param>
        /// <param name="numOfBaths"></param>
        /// <param name="isWithTerrace"></param>
        /// <param name="isWithViews"></param>
        /// <param name="isPenthouse"></param>
        /// <param name="price"></param>
        public RealEstate(int id, int cityId, int districtId, int numOfRooms, int numOfBaths, bool isWithTerrace, bool isWithViews, bool isPenthouse, double price)
        {
            Id = id;
            CityId = cityId;
            DistrictId = districtId;
            NumOfRooms = numOfRooms;
            NumOfBaths = numOfBaths;
            IsWithTerrace = isWithTerrace;
            IsWithViews = isWithViews;
            IsPenthouse = isPenthouse;
            Price = price;
        }

        /// <summary>
        /// Method what write all data about realEstate in string and returns it.
        /// </summary>
        /// <returns>String with information about realEstate.</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", Id, CityId, DistrictId, NumOfRooms, NumOfBaths, IsWithTerrace,
                IsWithViews, IsPenthouse, Price);
        }
    }
}
