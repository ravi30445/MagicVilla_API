using MagicVilla_VillaAPI.Repository.IRepostiory;
using MagicVilla_webapi.data;
using MagicVilla_webapi.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MagicVilla_webapi.Repository;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
          public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
          
        }

        // public async Task CreateAsync(Villa entity)
        // {
        //    await _db.Villa.AddAsync(entity);
        //    await SaveAsync();
        // }

        // public async Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        // {
        //     IQueryable<Villa> query= _db.Villa;
        //     if(!tracked){
        //      query=query.AsNoTracking();
        //      }
        //     if(filter!=null){
        //       query=  query.Where(filter);
        //     }
        //     return await query.FirstOrDefaultAsync();
        // }

        // public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        // {
        //     IQueryable<Villa> query= _db.Villa;
        //     if(filter!=null){
        //       query=  query.Where(filter);
        //     }
        //     return await query.ToListAsync();
        // }

        // public async  Task RemoveAsync(Villa entity)
        // {
        //       _db.Villa.Remove(entity);
        //       await SaveAsync();
        // }

        // public async Task SaveAsync()
        // {
        //     await _db.SaveChangesAsync();
        // }

        // Task<Villa> IVillaRepository.Get(Expression<Func<Villa, bool>> filter, bool tracked)
        // {
        //     throw new NotImplementedException();
        // }

        

     
    }
}