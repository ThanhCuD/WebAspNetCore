using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSite.Models
{
    public interface IGenericRepository<T>  where T: BaseEntity
    {
        Task<T> GetByID(Guid id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
