using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class UserService : IUserService
{
    private IGenericRepository<User> _repository;
    private ObjectMapper _mapper;

    public UserService(IGenericRepository<User> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<UserDTO>> GetAllUser()
    {
        var getAll = await _repository.GetAll();
        var users = new List<UserDTO>();
        foreach (var item in getAll)
        {
            users.Add(_mapper.UserToUserDTO(item));
        }
        return users;
    }

    public async Task<UserDTO> GetOneUser(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.UserToUserDTO(getOne);
    }

    public async Task<bool> UpdateUser(string id, AlterUserDTO user)
    {
        var ep = _mapper.AlterUserDTOToUser(user);
        await _repository.Update(ep);
        var updated = await _repository.GetById(ep.Id);
        return true;
    }

    public async Task<bool> DeleteUser(string id)
    {
       var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}
