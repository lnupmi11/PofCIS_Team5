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
    /// Class what implements logic of Districts table in Database.
    /// </summary>
    public class DistrictsDB
    {
        /// <summary>
        /// Name of file where stores information about cities.
        /// </summary>
        private readonly string fileName;
        
        /// <summary>
        /// Collection of districts.
        /// </summary>
        private List<District> allDistricts;
        
        /// <summary>
        /// Property what provide access to collection of districts.
        /// </summary>
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
        
        /// <summary>
        /// Constructor wich takes one parameter - name of file where stores information about districts.
        /// </summary>
        /// <param name="_fileName"></param>
        public DistrictsDB(string _fileName)
        {
            allDistricts = new List<District>();
            fileName = _fileName;
            ReadFromFile();
        }

        // <summary>
        /// Method wich read data about districts from file and write it in the collection of Districts. 
        /// </summary>
        public void ReadFromFile()
        {
            string[] allLines = File.ReadAllLines(fileName);
            foreach (string line in allLines)
            {
                string[] lineElems = line.Split(' ');
                allDistricts.Add(new District(Int32.Parse(lineElems[0]), Int32.Parse(lineElems[1]), lineElems[2]));
            }
        }

        /// <summary>
        /// Method wich write data about districts from collection in the file. 
        /// </summary>
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
    }
}
