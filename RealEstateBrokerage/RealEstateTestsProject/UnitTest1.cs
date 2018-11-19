using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateBrokerage.DAL.IOTypes;

namespace RealEstateTestsProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CityDbTest()
        {
            CitiesDB cities = new CitiesDB("CitiesData.txt");
            cities.ReadFromFile();
            Assert.AreEqual(cities.AllCities.Count, 8);
        }

        [TestMethod]
        public void DistrictsDbTest()
        {
            DistrictsDB districts = new DistrictsDB("DistrictsData.txt");
            districts.ReadFromFile();
            Assert.AreEqual(districts.AllDistricts.Count, 14);
        }

        [TestMethod]
        public void RealEstateDbTest()
        {
           RealEstateDB realEstate = new RealEstateDB("RealEstateData.txt");
           realEstate.ReadFromFile();
           Assert.AreEqual(realEstate.AllRealEstate.Count, 14);
        }
    }
}
