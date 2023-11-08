using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class AliasesService : IAliasesService
{
    private IGenericRepository<Alias> _repository;

    public AliasesService(IGenericRepository<Alias> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<AliasDTO>> GetAllAliases()
    {
        throw new NotImplementedException();
    }

    public async Task<AliasDTO> GetOneAlias(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<AliasDTO> UpdateAlias(string id, AlterAliasDTO alias)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAlias(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<AliasDTO> AddAlias(AlterAliasDTO alias)
    {
        throw new NotImplementedException();
    }
}