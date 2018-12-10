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
    /// Class that implenets interface IRepository for City entity.
    /// </summary>
    public class CityRepository : IRepository<City>
    {

        private Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }

        public void Create(City obj)
        {
            _context.Cities.Add(obj);
        }

        public City Find(Predicate<City> predicate)
        {
            return _context.Cities.Where(c => predicate(c)).FirstOrDefault();
        }

        public City Get(int id)
        {
            return _context.Cities.FirstOrDefault(city => city.Id == id);
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities;
        }

        public void Remove(City obj)
        {
            _context.Cities.Remove(obj);
        }

        public void Update(City obj)
        {
            _context.Cities.Remove(Get(obj.Id));
            _context.Cities.Add(obj);
        }
    }
}
