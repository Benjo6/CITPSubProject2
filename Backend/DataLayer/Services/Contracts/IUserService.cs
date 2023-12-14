using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IUserService
{
    public Task<List<UserDTO>> GetAllUser(Filter filter);
    public Task<UserDTO> GetOneUser(string id);
    public Task<bool> UpdateUser(string id, AlterUserDTO user);
    public Task<bool> DeleteUser (string id);
}