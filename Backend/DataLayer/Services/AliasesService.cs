using Common;
using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class AliasesService : IAliasesService
{
    private readonly IGenericRepository<Alias> _repository;
    private readonly ObjectMapper _mapper;

    public AliasesService(IGenericRepository<Alias> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }
    
    public async Task<(List<AliasDTO>,Metadata)> GetAllAliases(Filter filter)
    {
        var (aliases, metadata) = await _repository.GetAll(filter);
        return (_mapper.ListAliasToListAliasDTO(aliases), metadata);
    }

    public async Task<AliasDTO> GetOneAlias(string id)
    {
        return _mapper.AliasToAliasDTO(await _repository.GetById(id));
    }

    public async Task<AliasDTO> UpdateAlias(string id, AlterAliasDTO alias)
    {
        var aliasDb = _mapper.AlterAliasDTOToAlias(alias);
        aliasDb.Id = id;
        return _mapper.AliasToAliasDTO(await _repository.GetById(id));
    }

    public async Task<bool> DeleteAlias(string id)
    {
        return await _repository.Delete(await _repository.GetById(id));
    }

    public async Task<AliasDTO> AddAlias(AlterAliasDTO alias)
    {
        return _mapper.AliasToAliasDTO( await _repository.Add( _mapper.AlterAliasDTOToAlias(alias)));
    }
}