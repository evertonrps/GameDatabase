using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace  GameDatabase.API.Mapper;

public class PlatformMapper : Profile
{
    public PlatformMapper()
    {          
        //To Model            
        CreateMap<Platform, PlatformModel>();

        //To Domain
        CreateMap<PlatformModel, Platform>();
    }
}

