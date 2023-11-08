using Common.DataTransferObjects;
using Common.Domain;

namespace DataLayer.Services.Contracts;

public interface IUserService
{
    public Task<List<UserDTO>> GetAllUser();
    public Task<UserDTO> GetOneUser(string id);
    public Task<bool> UpdateUser(string id, AlterUserDTO user);
    public Task<bool> DeleteUser (string id);
}