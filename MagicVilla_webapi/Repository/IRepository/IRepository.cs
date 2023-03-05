using MagicVilla_webapi.Models;
using System.Linq.Expressions;

namespace MagicVilla_webapi.Repository.IRepostiory
{
    public interface IRepository<T> where T:class
    {
         Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter=null,string? includeProperties=null);
         Task<T> GetAsync(Expression<Func<T,bool>> filter=null,bool tracked=true,string includeProperties=null);
        Task<T> CreateAsync(T entity);
        Task<T> RemoveAsync(T entity);
       // Task<T> UpdateAsync(T entity);
        Task<T> SaveAsync();
    }
}