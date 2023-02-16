using MagicVilla_webapi.Models;
using MagicVilla_webapi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_webapi.Controllers
{   [Route("api/VillaAPI")]
    [ApiController]
    public class villaAPIController:ControllerBase{
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas(){
            return new List<VillaDTO> {
                 new VillaDTO{id=1,name="pool view"},
                 new VillaDTO {id=2,name="beach view"}   
            };
        }   
    }
}