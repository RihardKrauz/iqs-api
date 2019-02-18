using Iqs.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity 
    {
        IQueryable<T> GetAll();
        Task<T> GetById(long Id);
        Task<T> Create(T entity);
        void Update(long Id, T item);
        Task Delete(long Id);
    }
}
