using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateBrokerage.BLL;
using RealEstateBrokerage.DAL.IOTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTestsProject
{
    [TestClass]
    public class BLLTests
    {
        [TestMethod]
        public void DistricByIdTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            string district = rebm.GetDistrictById(1).Name;
            Assert.AreEqual(district, "Soho");
        }

        [TestMethod]
        public void DistricByNameTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            int districtId = rebm.GetDistrictByName("Soho").Id;
            Assert.AreEqual(districtId, 1);
        }

        [TestMethod]
        public void DistricByCityIdTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            string district = rebm.GetDistrictsByCityId(1).FirstOrDefault().Name;
            Assert.AreEqual(district, "Soho");
        }

        [TestMethod]
        public void CityByIdTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            string city = rebm.GetCityById(1).Name;
            Assert.AreEqual(city, "NewYork");
        }

        [TestMethod]
        public void CityByNameTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            int cityId = rebm.GetCityByName("NewYork").Id;
            Assert.AreEqual(cityId, 1);
        }

        [TestMethod]
        public void RealEstateByCityIdTest()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            double price = rebm.GetRealEstateById(1).Price;
            Assert.AreEqual(price, 350);
        }

        [TestMethod]
        public void RealEstateByDistrictId()
        {
            RealEstateBrokerageManager rebm = new RealEstateBrokerageManager();
            double price = rebm.GetRealEstateByDistrictId(1).FirstOrDefault().Price;
            Assert.AreEqual(price, 350);
        }
    }
}
