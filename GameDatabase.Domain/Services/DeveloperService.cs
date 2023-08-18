using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GameDatabase.Domain.Services;

public class DeveloperService : IDeveloperService
{
    private readonly IDeveloperRepository _developerRepository;
    private readonly ILogger<DeveloperService> _logger;

    public DeveloperService(IDeveloperRepository developerRepository, ILogger<DeveloperService> logger)
    {
        _developerRepository = developerRepository;
        _logger = logger;
    }
    public async Task<IEnumerable<Developer>> GetAll()
    {
        return await _developerRepository.GetAll();
    }

    public async Task<Developer> CreateDeveloper(Developer developer)
    {
        try
        {  await _developerRepository.Add(developer);
           await _developerRepository.SaveChanges();
           return developer;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao criar novo developer");
            return default;
        }
    }

    public async Task<Developer> GetById(int id)
    {
        try
        {
            return await _developerRepository.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Erro ao bucar developer");
            return default;
        }
    }
}