using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Infra.Data.Repositories;
public class PlatformRepository : Repository<Platform>, IPlatformRepository
{
    public PlatformRepository(GameDatabaseContext context)
    {
    }
}

