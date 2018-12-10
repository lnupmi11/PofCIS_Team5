using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateBrokerage.DAL.DataTypes;

namespace RealEstateBrokerage.DAL.EF
{
    /// <summary>
    /// Class that implement context of our database
    /// </summary>
    public class Context: DbContext
    {
        /// <summary>
        /// Cities table
        /// </summary>
        public DbSet<City> Cities { get; set; }
        
        /// <summary>
        /// Ditricts table
        /// </summary>
        public DbSet<District> Districts { get; set; }
        
        /// <summary>
        /// RealEstates table
        /// </summary>
        public DbSet<RealEstate> RealEstates { get; set; }

        public Context() : base("DbRealEstateBrokerage") { }
    }
}
