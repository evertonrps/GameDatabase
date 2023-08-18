using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Infra.Data.Repositories;
public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
{
    public DeveloperRepository(GameDatabaseContext context)
    {
    }
}

