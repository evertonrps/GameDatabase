using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace  GameDatabase.API.Mapper;

public class DeveloperMapper : Profile
{
    public DeveloperMapper()
    {          
        //To Model            
        CreateMap<Developer, DeveloperModel>();

        //To Domain
        CreateMap<DeveloperModel, Developer>()
            .ConvertUsing(x => Developer.Factory(x.Name, x.Founded, x.WebSite));
        ;
        
    }
}

