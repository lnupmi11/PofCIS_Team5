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
        /// <summary>
        /// City table in DB.
        /// </summary>
        public CitiesDB Cities { get; }
        /// <summary>
        /// Districts table in DB.
        /// </summary>
        public DistrictsDB Districts { get; }
        /// <summary>
        /// RealEstate table in DB.
        /// </summary>
        public RealEstateDB RealEstate { get; }
        /// <summary>
        /// List what contain information about flats after search.
        /// </summary>
        private List<RealEstate> _serchResult;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public RealEstateBrokerageManager()
        {
            Cities = new CitiesDB("../../DAL/InputData/CitiesData.txt");
            Districts = new DistrictsDB("../../DAL/InputData/DistrictsData.txt");
            RealEstate = new RealEstateDB("../../DAL/InputData/RealEstateData.txt");
        }

        /// <summary>
        /// Method which returns data about district by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public District GetDistrictById(int cityId)
        {
            return Districts.AllDistricts.Where(x => x.Id == cityId).FirstOrDefault();
        }
        
        /// <summary>
        /// Method which returns data about district by name from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public District GetDistrictByName(string name)
        {
            return Districts.AllDistricts.Where(x => x.Name == name).FirstOrDefault();
        }
        
        /// <summary>
        /// Method which returns list of districts from one city by cityId from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<District> GetDistrictsByCityId(int id)
        {
            return Districts.AllDistricts.Where(x => x.CityId == id).ToList();
        }

        /// <summary>
        /// Method which returns data about city by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public City GetCityById(int cityId)
        {
            return Cities.AllCities.Where(x => x.Id == cityId).FirstOrDefault();
        }

        /// <summary>
        /// Method which returns data about city by name from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public City GetCityByName(string name)
        {
            return Cities.AllCities.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Method which returns data about realEstate by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public RealEstate GetRealEstateById(int cityId)
        {
            return RealEstate.AllRealEstate.Where(x => x.Id == cityId).FirstOrDefault();
        }

        /// <summary>
        /// Method wich add to RealEstate list new  item.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="rooms"></param>
        /// <param name="baths"></param>
        /// <param name="views"></param>
        /// <param name="terrace"></param>
        /// <param name="penthouse"></param>
        /// <param name="currCity"></param>
        /// <param name="currDistrict"></param>
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

        /// <summary>
        /// Method which returns list of flats from one city by cityId from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RealEstate> GetRealEstateByCityId(int id)
        {
            return RealEstate.AllRealEstate.Where(x => x.CityId == id).ToList();
        }

        /// <summary>
        /// Method which returns list of flats from one district by districtId from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RealEstate> GetRealEstateByDistrictId(int id)
        {
            return RealEstate.AllRealEstate.Where(x => x.DistrictId == id).ToList();
        }

        /// <summary>
        /// Method what implements search of flat by criteria.
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="cityId"></param>
        /// <param name="districtId"></param>
        /// <param name="views"></param>
        /// <param name="terrace"></param>
        /// <param name="penthouse"></param>
        /// <returns></returns>
        public List<RealEstate> SearchRealEstates(double minPrice, double maxPrice, int cityId, int districtId, bool views, bool terrace, bool penthouse)
        {
            _serchResult = RealEstate.AllRealEstate.Where(x => x.Price >= minPrice && x.Price <= maxPrice
            && x.CityId == cityId && x.DistrictId == districtId && x.IsWithViews == views
            && x.IsWithTerrace == terrace && x.IsPenthouse == penthouse).ToList();

            return _serchResult;
        }

        /// <summary>
        /// Method what delete record from list of RealEstates.
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteById(int id)
        {
            RealEstate realEstate = RealEstate.AllRealEstate.Where(item => item.Id == id).FirstOrDefault();
            RealEstate.AllRealEstate.Remove(realEstate);
            RealEstate.WriteToFile();
        }
    }
}
