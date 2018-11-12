using RealEstateBrokerage.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.IOTypes
{
    public class CitiesDB
    {
        private readonly string fileName;
        private List<City> allCities;
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
        public CitiesDB(string _fileName)
        {
            allCities = new List<City>();
            fileName = _fileName;
            ReadFromFile();
        }

        public void ReadFromFile()
        {
            string[] allLines = File.ReadAllLines(fileName);
            foreach (string line in allLines)
            {
                string[] lineElems = line.Split(' ');
                allCities.Add(new City(Int32.Parse(lineElems[0]), lineElems[1]));
            }
        }

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
