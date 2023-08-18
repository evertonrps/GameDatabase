using AutoMapper;
using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;


namespace  GameDatabase.API.Mapper;

public class GameMapper : Profile
{
    public GameMapper()
    {          
        //To Model            
        CreateMap<Game, GameModel>();

        //To Domain
        CreateMap<GameModel, Game>();
    }
}

