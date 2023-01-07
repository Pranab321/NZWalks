using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //var regions = new List<Region>()
            //{
            //    new  Region
            //    {
            //      Id = Guid.NewGuid(),
            //      Name="Wellington",
            //      Code="WLG",
            //      Area=222555,
            //      Lat=102.222,
            //      Long=235.25,
            //      Population=500000
            //    }, 
            //    new  Region
            //    {
            //      Id = Guid.NewGuid(),
            //      Name = "Auckland",
            //      Code = "AULK",
            //      Area = 345345,
            //      Lat = 45.254,
            //      Long = 235.25,
            //      Population = 500000
            //    }
            //};
            var regions = await _regionRepository.GetAlAsyncl();

            //return DTO regions

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(regions =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id=regions.Id,
            //        Code=regions.Code,  
            //        Name=regions.Name,  
            //        Lat=regions.Lat,
            //        Long=regions.Long,
            //        Area=regions.Area,
            //        Population=regions.Population

            //    };
            //    regionsDTO.Add(regionDTO);  
            //});

            var regionsDTO =  mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);

        }

    }
}
