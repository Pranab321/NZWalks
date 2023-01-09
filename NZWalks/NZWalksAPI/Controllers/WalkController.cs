using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            var walks = await walkRepository.GetAllWalkAsyanc();
            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkAsynk")]
        public async Task<IActionResult> GetWalkAsynk(Guid id)
        {
            var walks = await walkRepository.GetWalkAsyanc(id);
            var walkDTO = mapper.Map<Models.DTO.Walk>(walks);
            if(walkDTO == null)
            {
                return NotFound(); 
            }
            return Ok(walkDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody]Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Request(DTO) to Domain model
            var walksDomain = new Models.Domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
                RegionId = addWalkRequest.RegionId
            };

            // Pass details to Repository

            walksDomain = await walkRepository.AddWalkAsyanc(walksDomain);

            //Convert back to DTO
            var walksDTO = new Models.DTO.Walk
            {
                Id=walksDomain.Id,
                Name = walksDomain.Name,
                Length = walksDomain.Length,
                WalkDifficultyId = walksDomain.WalkDifficultyId,
                RegionId = walksDomain.RegionId
            };

            //Return CreateAt Action
            return CreatedAtAction(nameof(GetWalkAsynk), new { id = walksDTO.Id }, walksDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain model
            var walkDomain = new Models.Domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
                RegionId = updateWalkRequest.RegionId
            };

            // Update Walk using repository

            walkDomain = await walkRepository.UpdateWalkAsync(id, walkDomain);

            //if Null then Not Found
            if (walkDomain == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var walkDTO = new Models.DTO.Walk
            {
               
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
                RegionId = walkDomain.RegionId
            };


            //Return Ok response
            return Ok(walkDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeliteWalkAsync(Guid id)
        {
            //Get region from database
            var walksDomain = await walkRepository.DeleteAsync(id);

            //if null not Found

            if (walksDomain == null)
            {
                return NotFound();
            }

            //Convert response back to DTO

            var walkDTO = new Models.DTO.Walk
            {
                Id = walksDomain.Id,
                Name = walksDomain.Name,
                Length = walksDomain.Length,
                WalkDifficultyId = walksDomain.WalkDifficultyId,
                RegionId = walksDomain.RegionId
            };
            //return Ok response


            return Ok(walkDTO);
        }

    }
}
