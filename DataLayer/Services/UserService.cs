using Common.DataTransferObjects;
using Common.Domain;
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
        return _mapper.UserDTO(getAll) ?? new List<UserDTO>();
    }

    public async Task<UserDTO> GetOneUser(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.UserDTO(getOne);
    }

    public async Task<bool> UpdateUser(string id, AlterUserDTO user)
    {
        _ = await _repository.GetById(id);
        var old  = _mapper.AlterUserDTO(user);
        old.Id = id;
        await _repository.Update(ep);
        var updated = await _repository.GetById(old.Id);
        return _mapper.AlterUserDTO(updated);
    }

    public async Task<bool> DeleteUser(string id)
    {
       var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}
