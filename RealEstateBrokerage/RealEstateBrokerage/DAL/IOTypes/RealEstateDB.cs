using RealEstateBrokerage.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.IOTypes
{
    public class RealEstateDB
    {
        private readonly string fileName;
        private List<RealEstate> allRealEstate;
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
        public RealEstateDB(string _fileName)
        {
            allRealEstate = new List<RealEstate>();
            fileName = _fileName;
            ReadFromFile();
        }

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
