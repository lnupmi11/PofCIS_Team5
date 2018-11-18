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
    /// Class what implements logic of RealEstate table in Database.
    /// </summary>
    public class RealEstateDB
    {
        /// <summary>
        /// Name of file where stores information about realEstates.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// Collection of realEstates.
        /// </summary>
        private List<RealEstate> allRealEstate;

        /// <summary>
        /// Property what provide access to collection of realEstates.
        /// </summary>
        public List<RealEstate> AllRealEstate
        {
            get
            {
                return allRealEstate;
            }
            set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentOutOfRangeException("CitiesDB is empty");
                }
                allRealEstate = value;
            }
        }

        /// <summary>
        /// Constructor wich takes one parameter - name of file where stores information about realEstates.
        /// </summary>
        /// <param name="_fileName"></param>
        public RealEstateDB(string _fileName)
        {
            allRealEstate = new List<RealEstate>();
            fileName = _fileName;
            ReadFromFile();
        }

        /// <summary>
        /// Method wich read data about cities from file and write it in the collection of RealEstates. 
        /// </summary>
        public void ReadFromFile()
        {
            string[] allLines = File.ReadAllLines(fileName);
            foreach (string line in allLines)
            {
                string[] lineElems = line.Split(' ');
                allRealEstate.Add(new RealEstate(Int32.Parse(lineElems[0]), Int32.Parse(lineElems[1]), Int32.Parse(lineElems[2]),
                    Int32.Parse(lineElems[3]), Int32.Parse(lineElems[4]), Boolean.Parse(lineElems[5]), Boolean.Parse(lineElems[6]),
                    Boolean.Parse(lineElems[7]), Double.Parse(lineElems[8])));
            }
        }

        /// <summary>
        /// Method wich write data about realEstates from collection in the file. 
        /// </summary>
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (RealEstate client in allRealEstate)
                {
                    writer.WriteLine(client);
                }
            }
        }
    }
}
