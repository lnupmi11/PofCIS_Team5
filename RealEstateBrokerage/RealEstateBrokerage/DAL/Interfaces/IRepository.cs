using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBrokerage.DAL.Interfaces
{
    /// <summary>
    /// Interface that implement repository patern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Method to add obkect in the table
        /// </summary>
        /// <param name="obj"></param>
        void Create(T obj);
        
        /// <summary>
        /// Method to remove object from table
        /// </summary>
        /// <param name="obj"></param>
        void Remove(T obj);

        /// <summary>
        /// Method to update object in table
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);

        /// <summary>
        /// Method to get object from table
        /// </summary>
        /// <param name="obj"></param>
        T Get(int id);

        /// <summary>
        /// Method to find object from table by some conditions
        /// </summary>
        /// <param name="obj"></param>
        T Find(Predicate<T> predicate);

        /// <summary>
        /// Method to get all objects from table
        /// </summary>
        /// <param name="obj"></param>
        IEnumerable<T> GetAll();
    }
}
