using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateBrokerage.DAL.DataTypes;
using RealEstateBrokerage.DAL.EF;
using RealEstateBrokerage.DAL.Interfaces;

namespace RealEstateBrokerage.DAL.Repositories
{
    /// <summary>
    /// Class that implement IUnitOfWork Interface
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// Context
        /// </summary>
        private Context db = new Context();
        
        /// <summary>
        /// City repository.
        /// </summary>
        private IRepository<City> cityRepository;

        /// <summary>
        /// District repository.
        /// </summary>
        private IRepository<District> districtRepository;

        /// <summary>
        /// RealEstate repository
        /// </summary>
        private IRepository<RealEstate> realEstateRepository;

        /// <summary>
        /// City repository property.
        /// </summary>
        public IRepository<City> Cities
        {
            get
            {
                if (cityRepository == null)
                    cityRepository = new CityRepository(db);
                return cityRepository;
            }
        }

        /// <summary>
        /// District repository property.
        /// </summary>
        public IRepository<District> Districts
        {
            get
            {
                if (districtRepository == null)
                    districtRepository = new DistrictRepository(db);
                return districtRepository;
            }
        }

        /// <summary>
        /// RealEstate repository property.
        /// </summary>
        public IRepository<RealEstate> RealEstates
        {
            get
            {
                if (realEstateRepository == null)
                    realEstateRepository = new RealEstateRepository(db);
                return realEstateRepository;
            }
        }

        /// <summary>
        /// Method that save changes in DB.
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Implementation of IDisposable pattern.
        /// </summary>
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
