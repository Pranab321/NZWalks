using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkDifficultyRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;

        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDBContext.WalkDifficultys.AddAsync(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var getwalkDifficulty = await nZWalksDBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == id);
            if (getwalkDifficulty == null)
            {
                return null;
            }
            nZWalksDBContext.WalkDifficultys.Remove(getwalkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return getwalkDifficulty;   

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultyAsync()
        {
            return await nZWalksDBContext.WalkDifficultys.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            return await nZWalksDBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
           var getwalkDifficulty = await nZWalksDBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == id);
            if(getwalkDifficulty == null)
            {
                return null;
            }
            getwalkDifficulty.Code = walkDifficulty.Code;

            await nZWalksDBContext.SaveChangesAsync();

            return getwalkDifficulty;
        }
    }
}
