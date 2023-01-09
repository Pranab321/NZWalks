using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            var WalkDifficulty = await walkDifficultyRepository.GetAllWalkDifficultyAsync();
            var WalkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(WalkDifficulty);
            return Ok(WalkDifficultyDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var WalkDifficulty = await walkDifficultyRepository.GetWalkDifficultyAsync(id);
            var WalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDifficulty);
            if (WalkDifficultyDTO == null)
            {
                return NotFound();
            }
            return Ok(WalkDifficultyDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var addWalkDifficultyRequestDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            addWalkDifficultyRequestDomain = await walkDifficultyRepository.AddWalkDifficultyAsync(addWalkDifficultyRequestDomain);

            var addRegionRequestDTO = new Models.DTO.WalkDifficulty()
            {
                Id = addWalkDifficultyRequestDomain.Id,
                Code = addWalkDifficultyRequestDomain.Code
            };
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = addRegionRequestDTO.Id }, addRegionRequestDTO);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var UpdateWalkDifficultyRequestDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code,
            };
            UpdateWalkDifficultyRequestDomain = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, UpdateWalkDifficultyRequestDomain);

            if (UpdateWalkDifficultyRequestDomain == null)
            {
                return NotFound();
            }

            var UpdateWalkDifficultyRequestDomainDTO = new Models.DTO.WalkDifficulty
            {
                Code = UpdateWalkDifficultyRequestDomain.Code
            };
            return Ok(UpdateWalkDifficultyRequestDomainDTO);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            //Get walkDifficulty from database
            var walkDifficulty = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);

            //if null not Found

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            //Convert response back to DTO

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Code = walkDifficulty.Code,
            };
            //return Ok response


            return Ok(walkDifficultyDTO);

        }

    }
}
