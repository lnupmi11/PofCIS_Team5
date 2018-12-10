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
    /// Class that implenets interface IRepository for RealEstateRepository entity.
    /// </summary>
    class RealEstateRepository : IRepository<RealEstate>
    {
        private Context _context;

        public RealEstateRepository(Context context)
        {
            _context = context;
        }

        public void Create(RealEstate obj)
        {
            _context.RealEstates.Add(obj);
        }

        public RealEstate Find(Predicate<RealEstate> predicate)
        {
            return _context.RealEstates.Where(r => predicate(r)).FirstOrDefault();
        }

        public RealEstate Get(int id)
        {
            return _context.RealEstates.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<RealEstate> GetAll()
        {
            return _context.RealEstates;
        }

        public void Remove(RealEstate obj)
        {
            _context.RealEstates.Remove(obj);
        }

        public void Update(RealEstate obj)
        {
            _context.RealEstates.Remove(Get(obj.Id));
            _context.RealEstates.Add(obj);
        }
    }
}
