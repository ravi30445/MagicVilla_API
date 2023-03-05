using MagicVilla_Utility;
using MvcMovie.Models;
using MvcMovie.Models.Dto;
using MvcMovie.Services.IServices;
namespace MvcMovie.Services
{
    public class VillaService:BaseService, IVillaService{
          private readonly IHttpClientFactory _clientFactory;
            private string villaUrl;

        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

        }
         public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Post,
                Data = dto,
                Url = villaUrl + "/api/v1/villaAPI",
                Token = token
            });
        }

      

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = villaUrl + "/api/v1/villaAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/v1/villaAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = villaUrl + "/api/v1/villaAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.Put,
                Data = dto,
                Url = villaUrl + "/api/v1/villaAPI/" + dto.id,
                Token = token
            }) ;
        
    }

        Task<T> IVillaService.CreateAsync<T>(VillaUpdateDTO dto, string token)
        {
            throw new NotImplementedException();
        }

        Task<T> IVillaService.CreateAsync<T>(int id, string token)
        {
            throw new NotImplementedException();
        }

        Task IVillaService.UpdateAsync<T>(VillaUpdateDTO model, string v)
        {
            throw new NotImplementedException();
        }
    }
}