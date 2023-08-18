using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Infra.Data.Repositories;
public class GamePlatformRepository : Repository<GamePlatform>, IGamePlatformRepository
{
    public GamePlatformRepository(GameDatabaseContext context)
    {
    }
}

