using MvcMovie.Models.Dto;
namespace MvcMovie.Services.IServices
{
    public interface IVillaService{
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id,string token);
        Task<T> CreateAsync<T>(VillaCreateDTO dto,string token);
        Task<T> CreateAsync<T>(VillaUpdateDTO dto,string token);
        Task<T> CreateAsync<T>(int id,string token);
        Task UpdateAsync<T>(VillaUpdateDTO model, string v);
    }
    
}