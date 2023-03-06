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
{   [Route("api/VillaNumberAPI")]
    [ApiController]
    public class villaNumberAPIController:ControllerBase{
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public villaNumberAPIController(IVillaNumberRepository dbVillaNumber,IMapper mapper,IVillaRepository dbVilla){
           _dbVillaNumber=dbVillaNumber; 
           _mapper=mapper; 
           this._response=new();
           _dbVilla=dbVilla;
        }
        private readonly ILogging _logger;
           public villaNumberAPIController(ILogging logger){
                _logger=logger;
            }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(){ 
            try{
           IEnumerable<VillaNumber> villaNumberList=await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
           _response.Result=_mapper.Map<List<VillaNumberDTO>>(villaNumberList);
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
        public async  Task<ActionResult<APIResponse>> GetVillaNumber(int id){
            try{
            if(id==0){
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var VillaNumber=await _dbVillaNumber.GetAsync(u=>u.VillaNo==id);
            if(VillaNumber==null){
                _response.StatusCode=HttpStatusCode.NotFound;
                return NotFound(_response);
            }
             _response.Result=_mapper.Map<VillaDTO>(VillaNumber);
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
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody]VillaNumberCreateDTO createDTO){
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            try{
            if(await _dbVillaNumber.GetAsync(u=>u.VillaNo==createDTO.VillaNo)!=null){
                ModelState.AddModelError("ErrorMessage","Villa number Already Exist");
                return BadRequest(ModelState);
            }
            if(await _dbVilla.GetAsync(u=>u.id==createDTO.VillaID)==null){
                    ModelState.AddModelError("ErrorMessage","Villa ID is invalid");
                return BadRequest(ModelState);
            }
            if(createDTO==null){
                return BadRequest(createDTO);
            }
            VillaNumber VillaNumber=_mapper.Map<VillaNumber>(createDTO);
          
            //VillaDTO.id=VillaStore.VillaList.OrderByDescending(u=>u.id).FirstOrDefault().id+1;
        //    Villa model=new(){
        //      details=createDTO.details,
        //      name=createDTO.name,
        //      occupancy=createDTO.occupancy,
        //      rate=createDTO.rate,
        //      sqft=createDTO.sqft
        //    };
       await _dbVillaNumber.CreateAsync(VillaNumber);
       _response.Result=_mapper.Map<VillaNumberDTO>(VillaNumber);
           _response.StatusCode=HttpStatusCode.Created;
            return CreatedAtRoute("GetVillas",new {id=VillaNumber.VillaNo},_response);
            }
             catch(Exception ex){
                _response.IsSuccess=false;
                _response.ErrorMessage=new List<string>(){ex.ToString()};
            }
            return _response;
        }  
       [HttpDelete("{id:int}",Name ="DeleteVilla")]
       public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id){
         try{
         if(id==0){
                return BadRequest();
            }
            var villaNumber=await _dbVillaNumber.GetAsync(u=>u.VillaNo==id);
            if(villaNumber==null){
                return NotFound();
            }
           await _dbVillaNumber.RemoveAsync(villaNumber);
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
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id,[FromBody]VillaNumberUpdateDTO updateDTO){
            try{
            if(updateDTO==null||id!=updateDTO.VillaNo){
                return BadRequest();
            }
             if(await _dbVilla.GetAsync(u=>u.id==updateDTO.VillaID)==null){
                    ModelState.AddModelError("ErrorMessage","Villa ID is invalid");
                return BadRequest(ModelState);
            }
            VillaNumber model=_mapper.Map<VillaNumber>(updateDTO);
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
          await _dbVillaNumber.UpdateAsync(model);
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
        public async Task<IActionResult> UpdatePartialVillaNumber(int id,JsonPatchDocument<VillaUpdateDTO> patchDTO){
                
                    if (patchDTO==null || id==0){
                    return BadRequest();
                }
                var villaNumber=await _dbVillaNumber.GetAsync(u=>u.VillaNo==id);
                // villa.name="new name";
                // _db.SaveChanges();
            VillaUpdateDTO villaDTO=_mapper.Map<VillaUpdateDTO>(villaNumber);
            //  VillaUpdateDTO VillaDTO=new(){
            //     details=villa.details,
            //     id=villa.id,
            //     name=villa.name,
            //     occupancy=villa.occupancy,
            //     rate=villa.rate,
            //     sqft=villa.sqft
            // };
                 if(villaNumber==null){
                    return BadRequest();
                 }

                 patchDTO.ApplyTo(villaDTO,ModelState);
                VillaNumber model=_mapper.Map<VillaNumber>(villaDTO);
        //            Villa model=new Villa(){
        //      details=VillaDTO.details,
        //      id=VillaDTO.id,
        //      name=VillaDTO.name,
        //      occupancy=VillaDTO.occupancy,
        //      rate=VillaDTO.rate,
        //      sqft=VillaDTO.sqft
        //    };
          await _dbVillaNumber.UpdateAsync(model);
          
                 if(ModelState.IsValid){
                    return BadRequest(ModelState);
                 }
            return NoContent();
        }    
    }
}