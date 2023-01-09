using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
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
        public async Task<IActionResult> GetAllRegionsAsync()
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

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            var regionsDTO = mapper.Map<Models.DTO.Region>(region);
            if (regionsDTO == null)
            {
                return NotFound();
            }
            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to dowain model

            var region = new Models.Domain.Region
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Area = addRegionRequest.Area,
                Population = addRegionRequest.Population
            };

            // Pass details to Repository
            region = await _regionRepository.AddAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Id= region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id },regionDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await _regionRepository.DeleteAsync(id);  

            //if null not Found

            if(region == null)
            {
                return NotFound();  
            }

            //Convert response back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population
            };
            //return Ok response


            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequist updateRegionRequist)
        {
            // Convert DTO to Domain model
            var region = new Models.Domain.Region
            {
                Code = updateRegionRequist.Code,
                Name = updateRegionRequist.Name,
                Lat = updateRegionRequist.Lat,
                Long = updateRegionRequist.Long,
                Area = updateRegionRequist.Area,
                Population = updateRegionRequist.Population
            };

            // Update Region using repository

            region = await _regionRepository.UpdateAsync(id,region);

            //if Null then Not Found
            if (region == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population
            };


            //Return Ok response
            return Ok(regionDTO);
 
        }

    }
}
