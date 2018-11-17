using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskImplementation implementation = new TaskImplementation();
            implementation.OpenConnection();
            implementation.ShowListOfEmployeeFirstAndLastNamesWhichStartsOn('A');
            implementation.ShowInformationAboutEmployeeWithId(8);
            implementation.ShowListOfEmployeeFirstAndLastNamesFromCity("London");
            implementation.ShowListOfEmployeeFirstAndLastNamesWithAgeMoreThan(55);
            implementation.ShowMaxMinAndAvgAgeOfEmployeeOfCity("London");
            implementation.CloseConnection();
            Console.ReadLine();
        }
    }
}
