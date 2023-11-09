using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class EpisodesService : IEpisodesService
{
    private IGenericRepository<Episode> _repository;
    private ObjectMapper _mapper;

    public EpisodesService(IGenericRepository<Episode> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<EpisodeDTO>> GetAllEpisodes()
    {
        var getAll = await _repository.GetAll();
        List<EpisodeDTO> list = new List<EpisodeDTO>();
        foreach (var item in getAll)
        {
            list.Add(_mapper.EpisodeToEpisodeDTO(item));
        }
        return list;
    }

    public async Task<EpisodeDTO> GetOneEpisode(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.EpisodeToEpisodeDTO(getOne);
    }

    public async Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode)
    {
        var ep = await _repository.GetById(id);
        ep = _mapper.AlterEpisodeDTOToEpisode(episode);
        await _repository.Update(ep);
        var updatedep = await _repository.GetById(ep.Id);
        return _mapper.EpisodeToEpisodeDTO(updatedep);
    }

    public async Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode)
    {
        var addedepisode = await _repository.Add(_mapper.AlterEpisodeDTOToEpisode(episode));
        return _mapper.EpisodeToEpisodeDTO(addedepisode);
    }

    public async Task<bool> DeleteEpisode(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}