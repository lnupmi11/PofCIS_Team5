using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateBrokerage.DAL.Interfaces;
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.EF;

namespace RealEstateBrokerage.DAL.Repositories
{
    /// <summary>
    /// Class that implenets interface IRepository for District entity.
    /// </summary>
    public class DistrictRepository : IRepository<District>
    {
        private Context _context;

        public DistrictRepository(Context context)
        {
            _context = context;
        }

        public void Create(District obj)
        {
            _context.Districts.Add(obj);
        }

        public District Find(Predicate<District> predicate)
        {
            return _context.Districts.Where(d => predicate(d)).FirstOrDefault();
        }

        public District Get(int id)
        {
            return _context.Districts.FirstOrDefault(district => district.Id == id);
        }

        public IEnumerable<District> GetAll()
        {
            return _context.Districts;
        }

        public void Remove(District obj)
        {
            _context.Districts.Remove(obj);
        }

        public void Update(District obj)
        {
            _context.Districts.Remove(Get(obj.Id));
            _context.Districts.Add(obj);
        }
    }
}
