using AutoMapper;

using MvcMovie.Models;
using MvcMovie.Models.Dto;
using MvcMovie.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using MvcMovie.models;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public HomeController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new();

            var response = await _villaService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
       
    }
}