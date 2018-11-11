using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.DataTypes
{
    public class RealEstate
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBaths { get; set; }
        public bool IsWithTerrace { get; set; }
        public bool IsWithViews { get; set; }
        public bool IsPenthouse { get; set; }
        public double Price { get; set; }

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

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", Id, CityId, DistrictId, NumOfRooms, NumOfBaths, IsWithTerrace,
                IsWithViews, IsPenthouse, Price);
        }
    }
}
