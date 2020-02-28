using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiJwtExample.DataAccess.Abstract;

namespace NetCoreWebApiJwtExample.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCoreTaskGenericRepository<TEntity,TContext>:IRepositoryTask<TEntity> where TEntity:class where TContext:DbContext,new ()
    {
        public async Task<List<TEntity>> GetAll()
        {
            await using (var context = new TContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }

        public async Task<TEntity> GetById(int id)
        {
           await using (var context = new TContext())
           {
               return await context.Set<TEntity>().FindAsync(id);
           }
        }

        public async Task Create(TEntity entity)
        {
           await using (var context = new TContext())
           {
               await context.Set<TEntity>().AddAsync(entity);
               await context.SaveChangesAsync();
           }
        }
        
        public async Task Update(TEntity entity)
        {
            await using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            await using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
