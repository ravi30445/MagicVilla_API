using System.Linq;
using MagicVilla_webapi.data;
using MagicVilla_webapi.Models;
using MagicVilla_webapi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_webapi.Controllers
{   [Route("api/VillaAPI")]
    [ApiController]
    public class villaAPIController:ControllerBase{
        private readonly ILogger<villaAPIController> _logger;
           public villaAPIController(ILogger<villaAPIController> logger){
                _logger=logger;
            }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas(){ 
            _logger.LogInformation("Getting all values");
            return Ok(VillaStore.VillaList);
        } 
        [HttpGet("{id:int}",Name ="GetVillas")]
        public ActionResult<VillaDTO> GetVillas(int id){
             _logger.LogError("Get villa error with id"+id);
            return Ok(VillaStore.VillaList.FirstOrDefault(u=>u.id==id));
        }
        [HttpPost()]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO VillaDTO){
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            if(VillaStore.VillaList.FirstOrDefault(u=>u.name.ToLower()==VillaDTO.name.ToLower())!=null){
                ModelState.AddModelError("CustomError","VillaAlready Exist");
            }
            if(VillaDTO==null){
                return BadRequest(VillaDTO);
            }
            if(VillaDTO.id >0){
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            VillaDTO.id=VillaStore.VillaList.OrderByDescending(u=>u.id).FirstOrDefault().id+1;
            VillaStore.VillaList.Add(VillaDTO);
            return CreatedAtRoute("GetVillas",VillaDTO);
        }  
       [HttpDelete("{id:int}",Name ="DeleteVilla")]
       public IActionResult DeleteVilla(int id){
         if(id==0){
                return BadRequest();
            }
            var villa=VillaStore.VillaList.FirstOrDefault(u=>u.id==id);
            if(villa==null){
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
       }   
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public IActionResult UpdateVilla(int id,[FromBody]VillaDTO villaDTO){
            if(villaDTO==null||id!=villaDTO.id){
                return BadRequest();
            }
            var villa=VillaStore.VillaList.FirstOrDefault(u=>u.id==id);
            villa.name=villaDTO.name;
            return NoContent();
        }
        // [HttpPatch("{id:int}",Name="UpdatePartialVilla")]
        // public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<>){

        // }
    }
}