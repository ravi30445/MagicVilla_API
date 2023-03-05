using System.Linq.Expressions;
using MagicVilla_webapi.data;
using MagicVilla_webapi.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_webapi.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
         private readonly ApplicationDbContext _db;
         internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.VillaNumbers.Include(u=>u.Villa).ToList();
            this.dbSet=_db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
           await dbSet.AddAsync(entity);
           await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T,bool>> filter = null, bool tracked = true,string? includeProperties=null)
        {
            IQueryable<T> query= dbSet;
            if(!tracked){
             query=query.AsNoTracking();
             }
            if(filter!=null){
              query=  query.Where(filter);
            }
            if(includeProperties!=null){
                foreach(var includeprop in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)){
                    query=query.Include(includeprop);    
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter = null,string? includeProperties=null)
        {
            IQueryable<T> query= dbSet;
            if(filter!=null){
              query=  query.Where(filter);
            }
             if(includeProperties!=null){
                foreach(var includeprop in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)){
                    query=query.Include(includeprop);    
                }
            }
            return await query.ToListAsync();
        }

        public async  Task RemoveAsync(T entity)
        {
              dbSet.Remove(entity);
              await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        Task<T> IRepository<T>.CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T> IRepository<T>.RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T> IRepository<T>.SaveAsync()
        {
            throw new NotImplementedException();
        }





        // Task<Villa> IVillaRepository.Get(Expression<Func<Villa, bool>> filter, bool tracked)
        // {
        //     throw new NotImplementedException();
        // }



        //    public async Task UpdateAsync(T entity)
        //     {
        //         entity.UpdatedDate = DateTime.Now;
        //         _db.Villa.Update(entity);
        //         await SaveAsync();

        //     }
    }
}