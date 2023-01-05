namespace NZWalksAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation
        public Region regions { get; set; }
        public WalkDifficulty walkDifficultys { get; set; }

    }
}
