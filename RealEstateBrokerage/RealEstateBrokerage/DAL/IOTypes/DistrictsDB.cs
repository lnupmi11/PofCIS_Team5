using RealEstateBrokerage.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.IOTypes
{
    public class DistrictsDB
    {
        private readonly string fileName;
        private List<District> allDistricts;
        public List<District> AllDistricts
        {
            get
            {
                return allDistricts;
            }
            set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentOutOfRangeException("CitiesDB is empty");
                }
                allDistricts = value;
            }
        }
        public DistrictsDB(string _fileName)
        {
            allDistricts = new List<District>();
            fileName = _fileName;
            ReadFromFile();
        }

        public void ReadFromFile()
        {
            string[] allLines = File.ReadAllLines(fileName);
            foreach (string line in allLines)
            {
                string[] lineElems = line.Split(' ');
                allDistricts.Add(new District(Int32.Parse(lineElems[0]), Int32.Parse(lineElems[1]), lineElems[2]));
            }
        }

        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (District client in allDistricts)
                {
                    writer.WriteLine(client);
                }
            }
        }

        public District GetDistrictById(int cityId)
        {
            return allDistricts.Where(x => x.Id == cityId).FirstOrDefault();
        }

        public District GetDistrictByName(string name)
        {
            return allDistricts.Where(x => x.Name == name).FirstOrDefault();
        }

        public List<District> GetDistrictsByCityId(int id)
        {
            return allDistricts.Where(x => x.CityId == id).ToList();
        }
    }
}
