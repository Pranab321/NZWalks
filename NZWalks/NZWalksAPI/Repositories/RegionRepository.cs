using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext1;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this .nZWalksDBContext1 = nZWalksDBContext; 
        }
        public async Task<IEnumerable<Region>> GetAlAsyncl()
        {
            return await nZWalksDBContext1.Regions.ToListAsync();
        }
    }
}
