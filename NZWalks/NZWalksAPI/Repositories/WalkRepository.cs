using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Walk> AddWalkAsyanc(Walk walk)
        {
            //Asign New Id
            walk.Id= Guid.NewGuid();   
            //Add a New Walk Context 
            await nZWalksDBContext.Walks.AddAsync(walk);
            //SaveChanges
            await nZWalksDBContext.SaveChangesAsync();
            //return walk
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id); ;

            if (walk == null)
            {
                return null;
            }

            //Delete the region
            nZWalksDBContext.Walks.Remove(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsyanc()
        {
            return await
                 nZWalksDBContext.Walks
                 .Include(x => x.Region)
                 .Include(x => x.WalkDifficulty)
                 .ToListAsync();
        }

        public async Task<Walk> GetWalkAsyanc(Guid id)
        {
            return await nZWalksDBContext.Walks
                    .Include(x => x.Region)
                   .Include(x => x.WalkDifficulty)
                   .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
           var getwalk= await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(getwalk==null)
            {
                return null;
            }
            getwalk.Name = walk.Name;   
            getwalk.Length = walk.Length;   
            getwalk.RegionId = walk.RegionId;   
            getwalk.WalkDifficultyId=walk.WalkDifficultyId;

            await nZWalksDBContext.SaveChangesAsync();
            return getwalk;
        }

    }
}
