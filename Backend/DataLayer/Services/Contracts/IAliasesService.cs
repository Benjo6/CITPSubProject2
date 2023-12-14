using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IAliasesService
{
    public Task<List<AliasDTO>> GetAllAliases(Filter filter);
    public Task<AliasDTO> GetOneAlias(string id);
    public Task<AliasDTO> UpdateAlias(string id, AlterAliasDTO alias);
    public Task<bool> DeleteAlias(string id);
    public Task<AliasDTO> AddAlias(AlterAliasDTO alias);
}