using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSite.Models
{
    public class GennericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext appDbContext;
        private DbSet<T> dbset;
        public GennericRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            dbset = appDbContext.Set<T>();
        }
        public async Task Add(T entity)
        {
            dbset.Add(entity);
            await appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            dbset.Remove(entity);
            await appDbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.AsEnumerable();
        }

        public async Task<T> GetByID(Guid id)
        {
            return await dbset.SingleOrDefaultAsync(_ => _.Id == id);
        }

        public async Task Update(T entity)
        {
            dbset.Update(entity);
            await appDbContext.SaveChangesAsync();
        }
    }
}
