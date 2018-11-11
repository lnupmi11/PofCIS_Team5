using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.DataTypes
{
    public class District
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        public District(int id, int cityId, string name)
        {
            Id = id;
            CityId = cityId;
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, CityId, Name);
        }
    }
}
