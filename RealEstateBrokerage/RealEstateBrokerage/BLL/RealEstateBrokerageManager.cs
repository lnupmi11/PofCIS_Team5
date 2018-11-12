using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.IOTypes;

namespace RealEstateBrokerage.BLL
{
    public class RealEstateBrokerageManager
    {
        public CitiesDB Cities { get; }
        public DistrictsDB Districts { get; }
        public RealEstateDB RealEstate { get; }

        private List<RealEstate> _serchResult;

        public RealEstateBrokerageManager()
        {
            Cities = new CitiesDB("../../DAL/InputData/CitiesData.txt");
            Districts = new DistrictsDB("../../DAL/InputData/DistrictsData.txt");
            RealEstate = new RealEstateDB("../../DAL/InputData/RealEstateData.txt");
        }

        public District GetDistrictById(int cityId)
        {
            return Districts.AllDistricts.Where(x => x.Id == cityId).FirstOrDefault();
        }

        public District GetDistrictByName(string name)
        {
            return Districts.AllDistricts.Where(x => x.Name == name).FirstOrDefault();
        }

        public List<District> GetDistrictsByCityId(int id)
        {
            return Districts.AllDistricts.Where(x => x.CityId == id).ToList();
        }

        public City GetCityById(int cityId)
        {
            return Cities.AllCities.Where(x => x.Id == cityId).FirstOrDefault();
        }

        public City GetCityByName(string name)
        {
            return Cities.AllCities.Where(x => x.Name == name).FirstOrDefault();
        }

        public RealEstate GetRealEstateById(int cityId)
        {
            return RealEstate.AllRealEstate.Where(x => x.Id == cityId).FirstOrDefault();
        }

        internal void AddNewAccommodation(double price, int rooms, int baths, bool views, bool terrace, bool penthouse, string currCity, string currDistrict)
        {
            City city = GetCityByName(currCity);
            if (city == null)
            {
                int id = Cities.AllCities.Max(c => c.Id);
                city = new City(id + 1, currCity);
                Cities.AllCities.Add(city);
                Cities.WriteToFile();
            }
            District distric = GetDistrictByName(currDistrict);
            if(distric == null)
            {
                int id = Districts.AllDistricts.Max(dis => dis.Id);
                distric = new District(id + 1, city.Id, currDistrict );
                Districts.AllDistricts.Add(distric);
                Districts.WriteToFile();
            }
            int accId = RealEstate.AllRealEstate.Max(item => item.Id);
            RealEstate.AllRealEstate.Add(new DAL.DataTypes.RealEstate(accId + 1, city.Id, distric.Id, rooms, baths, terrace, views, penthouse, price));
            RealEstate.WriteToFile();
        }

        public List<RealEstate> GetRealEstateByCityId(int id)
        {
            return RealEstate.AllRealEstate.Where(x => x.CityId == id).ToList();
        }

        public List<RealEstate> GetRealEstateByDistrictId(int id)
        {
            return RealEstate.AllRealEstate.Where(x => x.DistrictId == id).ToList();
        }

        public List<RealEstate> SearchRealEstates(double minPrice, double maxPrice, int cityId, int districtId, bool views, bool terrace, bool penthouse)
        {
            _serchResult = RealEstate.AllRealEstate.Where(x => x.Price >= minPrice && x.Price <= maxPrice
            && x.CityId == cityId && x.DistrictId == districtId && x.IsWithViews == views
            && x.IsWithTerrace == terrace && x.IsPenthouse == penthouse).ToList();

            return _serchResult;
        }

        internal void DeleteById(int id)
        {
            RealEstate realEstate = RealEstate.AllRealEstate.Where(item => item.Id == id).FirstOrDefault();
            RealEstate.AllRealEstate.Remove(realEstate);
            RealEstate.WriteToFile();
        }
    }
}
