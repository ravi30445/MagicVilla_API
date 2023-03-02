using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_webapi.data;
using MagicVilla_webapi.Logging;
using MagicVilla_webapi.Models;
using MagicVilla_webapi.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Repository.IRepostiory;

namespace MagicVilla_webapi.Controllers
{   [Route("api/VillaAPI")]
    [ApiController]
    public class villaAPIController:ControllerBase{

        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public villaAPIController(IVillaRepository dbVilla,IMapper mapper){
           _dbVilla=dbVilla; 
           _mapper=mapper;
        }
        private readonly ILogging _logger;
           public villaAPIController(ILogging logger){
                _logger=logger;
            }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas(){ 
           IEnumerable<Villa> villaList=await _dbVilla.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        } 
        [HttpGet("{id:int}",Name ="GetVillas")]
        public async  Task<ActionResult<VillaDTO>> GetVillas(int id){
             _logger.Log("Get villa error with id"+id,"error");
             var villa=await _dbVilla.GetAsync(u=>u.id==id);
            return Ok(_mapper.Map<VillaDTO>(villa));
        }
        [HttpPost()]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO createDTO){
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            if(await _dbVilla.GetAsync(u=>u.name.ToLower()==createDTO.name.ToLower())!=null){
                ModelState.AddModelError("CustomError","VillaAlready Exist");
            }
            if(createDTO==null){
                return BadRequest(createDTO);
            }
            Villa model=_mapper.Map<Villa>(createDTO);
          
            //VillaDTO.id=VillaStore.VillaList.OrderByDescending(u=>u.id).FirstOrDefault().id+1;
        //    Villa model=new(){
        //      details=createDTO.details,
        //      name=createDTO.name,
        //      occupancy=createDTO.occupancy,
        //      rate=createDTO.rate,
        //      sqft=createDTO.sqft
        //    };
       await _dbVilla.CreateAsync(model);
            return CreatedAtRoute("GetVillas",new {id=model.id},model);
        }  
       [HttpDelete("{id:int}",Name ="DeleteVilla")]
       public async Task<IActionResult> DeleteVilla(int id){
         if(id==0){
                return BadRequest();
            }
            var villa=await _dbVilla.GetAsync(u=>u.id==id);
            if(villa==null){
                return NotFound();
            }
           await _dbVilla.RemoveAsync(villa);
          
            return NoContent();
       }   
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody]VillaUpdateDTO updateDTO){
            if(updateDTO==null||id!=updateDTO.id){
                return BadRequest();
            }
            Villa model=_mapper.Map<Villa>(updateDTO);
            // var villa=VillaStore.VillaList.FirstOrDefault(u=>u.id==id);
            // villa.name=villaDTO.name;
        //       Villa model=new(){
        //      details=updateDTO.details,
        //      id=updateDTO.id,
        //      name=updateDTO.name,
        //      occupancy=updateDTO.occupancy,
        //      rate=updateDTO.rate,
        //      sqft=updateDTO.sqft
        //    };
          await _dbVilla.UpdateAsync(model);
         
            return NoContent();
        }
        [HttpPatch("{id:int}",Name="UpdatePartialVilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDTO> patchDTO){
                if (patchDTO==null || id==0){
                    return BadRequest();
                }
                var villa=await _dbVilla.GetAsync(u=>u.id==id);
                // villa.name="new name";
                // _db.SaveChanges();
            VillaUpdateDTO villaDTO=_mapper.Map<VillaUpdateDTO>(villa);
            //  VillaUpdateDTO VillaDTO=new(){
            //     details=villa.details,
            //     id=villa.id,
            //     name=villa.name,
            //     occupancy=villa.occupancy,
            //     rate=villa.rate,
            //     sqft=villa.sqft
            // };
                 if(villa==null){
                    return BadRequest();
                 }

                 patchDTO.ApplyTo(villaDTO,ModelState);
                Villa model=_mapper.Map<Villa>(villaDTO);
        //            Villa model=new Villa(){
        //      details=VillaDTO.details,
        //      id=VillaDTO.id,
        //      name=VillaDTO.name,
        //      occupancy=VillaDTO.occupancy,
        //      rate=VillaDTO.rate,
        //      sqft=VillaDTO.sqft
        //    };
          await _dbVilla.UpdateAsync(model);
          
                 if(ModelState.IsValid){
                    return BadRequest(ModelState);
                 }
            return NoContent();
        }    
    }
}