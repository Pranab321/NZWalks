using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class WalkssProfile : Profile
    {
        public WalkssProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
                        .ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                       .ReverseMap();
        }
    }
}
