using Common;
using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly ObjectMapper _mapper;

    public UserService(IGenericRepository<User> repository, IAuthenticationRepository authenticationRepository)
    {
        _repository = repository;
        _authenticationRepository = authenticationRepository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<UserDTO>> GetAllUser(Filter filter)
    {
        var getAll = await _repository.GetAll(filter);
        return _mapper.ListUserToListUserDTO(getAll);
    }

    public async Task<UserDTO> GetOneUser(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.UserToUserDTO(getOne);
    }
    
    public async Task<UserDTO> GetUserByUsername(string username)
    {
        var getOne = await _authenticationRepository.GetUserByUsername(username);
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
