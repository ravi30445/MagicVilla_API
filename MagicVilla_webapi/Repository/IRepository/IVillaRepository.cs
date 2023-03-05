
using MagicVilla_webapi.Models;
using MagicVilla_webapi.Repository.IRepostiory;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepostiory
{
    public interface IVillaRepository:IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa entity);
      
    }
}