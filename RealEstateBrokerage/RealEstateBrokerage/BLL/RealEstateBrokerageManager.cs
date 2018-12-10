using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.Interfaces;
using RealEstateBrokerage.DAL.EF;
using RealEstateBrokerage.DAL.Repositories;

namespace RealEstateBrokerage.BLL
{
    public class RealEstateBrokerageManager
    {
        
        public UnitOfWork UnitOfWork { get; set; }

        private List<RealEstate> _serchResult;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public RealEstateBrokerageManager()
        {
            UnitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Method which returns data about district by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public District GetDistrictById(int cityId)
        {
            return UnitOfWork.Districts.GetAll().Where(x => x.Id == cityId).FirstOrDefault();
        }
        
        /// <summary>
        /// Method which returns data about district by name from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public District GetDistrictByName(string name)
        {
            return UnitOfWork.Districts.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }
        
        /// <summary>
        /// Method which returns list of districts from one city by cityId from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<District> GetDistrictsByCityId(int id)
        {
            return UnitOfWork.Districts.GetAll().Where(x => x.CityId == id).ToList();
        }

        /// <summary>
        /// Method which returns data about city by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public City GetCityById(int cityId)
        {
            return UnitOfWork.Cities.GetAll().Where(x => x.Id == cityId).FirstOrDefault();
        }

        /// <summary>
        /// Method which returns data about city by name from database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public City GetCityByName(string name)
        {
            return UnitOfWork.Cities.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Method which returns data about realEstate by id from database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public RealEstate GetRealEstateById(int cityId)
        {
            return UnitOfWork.RealEstates.GetAll().Where(x => x.Id == cityId).FirstOrDefault();
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
                int id = UnitOfWork.Cities.GetAll().Max(c => c.Id);
                city = new City(id + 1, currCity);
                UnitOfWork.Cities.Create(city);
                UnitOfWork.Save();
            }
            District distric = GetDistrictByName(currDistrict);
            if(distric == null)
            {
                int id = UnitOfWork.Cities.GetAll().Max(dis => dis.Id);
                distric = new District(id + 1, city.Id, currDistrict );
                UnitOfWork.Districts.Create(distric);
                UnitOfWork.Save();
            }
            int accId = UnitOfWork.RealEstates.GetAll().Max(item => item.Id);
            UnitOfWork.RealEstates.Create(new DAL.DataTypes.RealEstate(accId + 1, city.Id, distric.Id, rooms, baths, terrace, views, penthouse, price));
            UnitOfWork.Save();
        }

        /// <summary>
        /// Method which returns list of flats from one city by cityId from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RealEstate> GetRealEstateByCityId(int id)
        {
            return UnitOfWork.RealEstates.GetAll().Where(x => x.CityId == id).ToList();
        }

        /// <summary>
        /// Method which returns list of flats from one district by districtId from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RealEstate> GetRealEstateByDistrictId(int id)
        {
            return UnitOfWork.RealEstates.GetAll().Where(x => x.DistrictId == id).ToList();
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
            _serchResult = UnitOfWork.RealEstates.GetAll().Where(x => x.Price >= minPrice && x.Price <= maxPrice
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
            RealEstate realEstate = UnitOfWork.RealEstates.GetAll().Where(item => item.Id == id).FirstOrDefault();
            UnitOfWork.RealEstates.Remove(realEstate);
            UnitOfWork.Save();
        }
    }
}
