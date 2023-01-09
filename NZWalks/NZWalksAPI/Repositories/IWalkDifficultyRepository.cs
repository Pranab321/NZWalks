using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultyAsync();
        Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id,WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
    }
}
