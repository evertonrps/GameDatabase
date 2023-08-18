using System;
using System.Threading.Tasks;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Repositories;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
public interface IGamePlatformRepository : IRepository<GamePlatform>
{
}

