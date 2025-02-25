using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.Mapper;

public class DeveloperMapper : Profile
{
    public DeveloperMapper()
    {
        //To Model            
        CreateMap<Developer, DeveloperOutputModel>();

        //To Domain
        CreateMap<DeveloperInputModel, Developer>()
            .ConvertUsing(x => Developer.Factory(x.Name, x.Founded, x.WebSite));
        ;
    }
}