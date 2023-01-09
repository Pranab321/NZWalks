using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalkAsyanc();
        Task<Walk> GetWalkAsyanc(Guid id);
        Task<Walk> AddWalkAsyanc(Walk walk);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);

    }
}
