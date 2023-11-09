using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IAliasesService
{
    public Task<List<AliasDTO>> GetAllAliases(int? page = 1, int? perPage = 10);
    public Task<AliasDTO> GetOneAlias(string id);
    public Task<AliasDTO> UpdateAlias(string id, AlterAliasDTO alias);
    public Task<bool> DeleteAlias(string id);
    public Task<AliasDTO> AddAlias(AlterAliasDTO alias);
}