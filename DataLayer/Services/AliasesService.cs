using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class AliasesService : IAliasesService
{
    private IGenericRepository<Alias> _repository;
    private ObjectMapper _mapper;

    public AliasesService(IGenericRepository<Alias> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }
    
    public async Task<List<AliasDTO>> GetAllAliases(int? page = 1, int? perPage = 10)
    {
        var aliases = await _repository.GetAll(page, perPage);
        var aliasesDTO = new List<AliasDTO>();
        aliases.ForEach(alias =>
        {
            aliasesDTO.Add(_mapper.AliasToAliasDTO(alias));
        });
        return aliasesDTO;
    }

    public async Task<AliasDTO> GetOneAlias(string id)
    {
        return _mapper.AliasToAliasDTO(await _repository.GetById(id));
    }

    public async Task<AliasDTO> UpdateAlias(string id, AlterAliasDTO alias)
    {
        var aliasDb = _mapper.AlterAliasDTOToAlias(alias);
        if(await _repository.Update(aliasDb))
        {
            return _mapper.AliasToAliasDTO(await _repository.GetById(id));
        }    
        else
            return new AliasDTO();
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