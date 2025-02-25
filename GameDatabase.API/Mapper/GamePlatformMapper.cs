using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.Mapper;

public class GamePlatformMapper : Profile
{
    public GamePlatformMapper()
    {
        //To Model            
        CreateMap<GamePlatform, GamePlatformModel>();

        //To Domain
        CreateMap<GamePlatformModel, GamePlatform>();
    }
}