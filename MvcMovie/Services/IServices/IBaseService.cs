using MvcMovie;
using MvcMovie.models;
using MvcMovie.Models;

namespace MvcMovie.Services.IServices
{
    public interface IBaseService{
        APIResponse responseModel{get;set;}
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
    
}