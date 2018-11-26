using System;
using ADO.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShowInformationAboutEmployeeWithId()
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            string result = implementation.ShowInformationAboutEmployeeWithId(8);
            implementation.CloseConnection();
            result = result.Substring(0, result.IndexOf('\n'));
            Assert.IsTrue(result == "LastName: Callahan ");
        }

        [TestMethod]
        public void ShowListOfEmployeeFirstAndLastNamesWhichStartsOn()
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            string result = implementation.ShowListOfEmployeeFirstAndLastNamesWhichStartsOn('A');
            implementation.CloseConnection();
            result = result.Substring(0, result.IndexOf('\n'));
            Assert.IsTrue(result == "Andrew - Fuller");
        }

        [TestMethod]
        public void ShowListOfEmployeeFirstAndLastNamesFromCity()
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            string result = implementation.ShowListOfEmployeeFirstAndLastNamesFromCity("London");
            implementation.CloseConnection();
            result = result.Substring(0, result.IndexOf('\n'));
            Assert.IsTrue(result == "Steven - Buchanan");
        }

        [TestMethod]
        public void ShowListOfEmployeeFirstAndLastNamesWithAgeMoreThan()
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            string result = implementation.ShowListOfEmployeeFirstAndLastNamesWithAgeMoreThan(55);
            implementation.CloseConnection();
            result = result.Substring(0, result.IndexOf('\n'));
            Assert.IsTrue(result == "Steven - Buchanan - 63");
        }

        [TestMethod]
        public void ShowCountEmployeesOfCity()
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            string result = implementation.ShowCountEmployeesOfCity("London");
            implementation.CloseConnection();
            result = result.Substring(0, result.IndexOf('\n'));
            Assert.IsTrue(result == "London - 4");
        }
    }
}
