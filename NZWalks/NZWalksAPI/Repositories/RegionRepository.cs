using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext1;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext1 = nZWalksDBContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDBContext1.AddAsync(region);
            await nZWalksDBContext1.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region = await nZWalksDBContext1.Regions.FirstOrDefaultAsync(x => x.Id == id); ;

            if(region == null)
            {
                return null;
            }

            //Delete the region
            nZWalksDBContext1.Regions.Remove(region);
            await nZWalksDBContext1.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAlAsyncl()
        {
            return await nZWalksDBContext1.Regions.ToListAsync();
        }
        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDBContext1.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var getregion = await nZWalksDBContext1.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(getregion == null)
            {
                return null;
            }
            getregion.Code=region.Code.ToString();  
            getregion.Name=region.Name.ToString();
            getregion.Area = region.Area;
            getregion.Long = region.Long;
            getregion.Lat = region.Lat;
            getregion.Population = region.Population;

            await nZWalksDBContext1.SaveChangesAsync();
            return getregion;
        }
    }
}
