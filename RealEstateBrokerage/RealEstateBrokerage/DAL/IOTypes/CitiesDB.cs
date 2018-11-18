using RealEstateBrokerage.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.IOTypes
{
    
    /// <summary>
    /// Class what implements logic of City table in Database.
    /// </summary>
    public class CitiesDB
    {
        /// <summary>
        /// Name of file where stores information about cities.
        /// </summary>
        private readonly string fileName;
        
        /// <summary>
        /// Collection of cities.
        /// </summary>
        private List<City> allCities;
        
        /// <summary>
        /// Property what provide access to collection of cities.
        /// </summary>
        public List<City> AllCities
        {
            get
            {
                return allCities;
            }
            set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentOutOfRangeException("CitiesDB is empty");
                }
                allCities = value;
            }
        }

        /// <summary>
        /// Constructor wich takes one parameter - name of file where stores information about cities.
        /// </summary>
        /// <param name="_fileName"></param>
        public CitiesDB(string _fileName)
        {
            allCities = new List<City>();
            fileName = _fileName;
            ReadFromFile();
        }

        /// <summary>
        /// Method wich read data about cities from file and write it in the collection of Cities. 
        /// </summary>
        public void ReadFromFile()
        {
            string[] allLines = File.ReadAllLines(fileName);
            foreach (string line in allLines)
            {
                string[] lineElems = line.Split(' ');
                allCities.Add(new City(Int32.Parse(lineElems[0]), lineElems[1]));
            }
        }

        /// <summary>
        /// Method wich write data about cities from collection in the file. 
        /// </summary>
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (City client in allCities)
                {
                    writer.WriteLine(client);
                }
            }
        }
    }
}
