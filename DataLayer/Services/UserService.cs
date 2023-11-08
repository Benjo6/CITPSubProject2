using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class UserService : IUserService
{
    private IGenericRepository<User> _repository;

    public UserService(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<List<UserDTO>> GetAllUser()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> GetOneUser(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUser(string id, AlterUserDTO user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUser(string id)
    {
        throw new NotImplementedException();
    }
}
