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
using MagicVilla_webapi.models;
using System.Net;

namespace MagicVilla_webapi.Controllers
{   [Route("api/VillaAPI")]
    [ApiController]
    public class villaAPIController:ControllerBase{
        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public villaAPIController(IVillaRepository dbVilla,IMapper mapper){
           _dbVilla=dbVilla; 
           _mapper=mapper;
           this._response=new();
        }
        private readonly ILogging _logger;
           public villaAPIController(ILogging logger){
                _logger=logger;
            }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas(){ 
            try{
           IEnumerable<Villa> villaList=await _dbVilla.GetAllAsync();
           _response.Result=_mapper.Map<List<VillaDTO>>(villaList);
           _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
            }
            catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return _response;
        } 
        [HttpGet("{id:int}",Name ="GetVillas")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<ActionResult<APIResponse>> GetVillas(int id){
            try{
            if(id==0){
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var Villa=await _dbVilla.GetAsync(u=>u.id==id);
            if(Villa==null){
                _response.StatusCode=HttpStatusCode.NotFound;
                return NotFound(_response);
            }
             _response.Result=_mapper.Map<VillaDTO>(Villa);
           _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
            }
             catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return _response;
          
        }
        [HttpPost()]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO createDTO){
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            try{
            if(await _dbVilla.GetAsync(u=>u.name.ToLower()==createDTO.name.ToLower())!=null){
                ModelState.AddModelError("CustomError","VillaAlready Exist");
            }
            if(createDTO==null){
                return BadRequest(createDTO);
            }
            Villa Villa=_mapper.Map<Villa>(createDTO);
          
            //VillaDTO.id=VillaStore.VillaList.OrderByDescending(u=>u.id).FirstOrDefault().id+1;
        //    Villa model=new(){
        //      details=createDTO.details,
        //      name=createDTO.name,
        //      occupancy=createDTO.occupancy,
        //      rate=createDTO.rate,
        //      sqft=createDTO.sqft
        //    };
       await _dbVilla.CreateAsync(Villa);
       _response.Result=_mapper.Map<VillaDTO>(Villa);
           _response.StatusCode=HttpStatusCode.Created;
            return CreatedAtRoute("GetVillas",new {id=Villa.id},_response);
            }
             catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return _response;
        }  
       [HttpDelete("{id:int}",Name ="DeleteVilla")]
       public async Task<ActionResult<APIResponse>> DeleteVilla(int id){
         try{
         if(id==0){
                return BadRequest();
            }
            var villa=await _dbVilla.GetAsync(u=>u.id==id);
            if(villa==null){
                return NotFound();
            }
           await _dbVilla.RemoveAsync(villa);
          _response.StatusCode=HttpStatusCode.NoContent;
          _response.IsSuccess=true;
            return Ok(_response);
         }
          catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return  _response;
       }   
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id,[FromBody]VillaUpdateDTO updateDTO){
            try{
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
          _response.StatusCode=HttpStatusCode.NoContent;
          _response.IsSuccess=true;
            return Ok(_response);
        }
         catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return _response;
           
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