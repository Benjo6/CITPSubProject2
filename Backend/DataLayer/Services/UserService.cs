using Common;
using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;
    private readonly ObjectMapper _mapper;

    public UserService(IGenericRepository<User> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<(List<UserDTO>, Metadata)> GetAllUser(Filter filter)
    {
        var (getAll, metadata) = await _repository.GetAll(filter);
        return (_mapper.ListUserToListUserDTO(getAll), metadata);
    }

    public async Task<UserDTO> GetOneUser(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.UserToUserDTO(getOne);
    }

    public async Task<bool> UpdateUser(string id, AlterUserDTO user)
    {
        var mappedUser = _mapper.AlterUserDTOToUser(user);
        mappedUser.Id = id;
        await _repository.Update(mappedUser);
        _ = await _repository.GetById(mappedUser.Id);
        return true;
    }

    public async Task<bool> DeleteUser(string id)
    {
       var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}
