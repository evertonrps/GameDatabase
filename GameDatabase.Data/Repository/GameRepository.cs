using GameDatabase.Data.Context;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;

namespace GameDatabase.Data.Repository;

public class GameRepository : Repository<Game>, IGameRepository
{
    private readonly GameDatabaseContext _context;
//    public IUnitOfWork UnitOfWork;

    public GameRepository(GameDatabaseContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Game> RecuperaGamesCompletos()
    {
        // return _context.Games.Include(c => c.GamePlatform).ThenInclude(c => c.Platform).ToList();
        throw new NotImplementedException();
    }

    public dynamic ObterGameCompletoPorID(int id)
    {
        //var gamePlataformass = (from gp in _context.Set<GamePlatform>() where gp.GameId == id select gp).ToList();
        //
        // var result = (from g in _context.Set<Game>()
        //         join gp in _context.Set<GamePlatform>() on g.Id equals gp.GameId
        //         join d in _context.Set<Developer>() on g.DeveloperId equals d.Id
        //        // join p in _context.Set<Platform>() on gp.PlatformId equals p.Id
        //        // join pt in _context.Set<PlatformType>() on p.PlatformTypeId equals pt.Id
        //         where g.Id == id
        //         select new
        //         {
        //             Id = g.Id,
        //             Title = g.Title,
        //             Description = g.Description,
        //             DeveloperId = g.DeveloperId,
        //             GamePlatform = gamePlataformass,
        //             PlatformTypeId = p.PlatformTypeId
        //         }
        //     ).FirstOrDefault();
        //
        // return result;
        //return _context.Games.Include(c => c.GamePlatform).ThenInclude(c => c.FirstOrDefault().PlatformId).Where(x => x.Id == id).FirstOrDefault();
        throw new NotImplementedException();
    }
}