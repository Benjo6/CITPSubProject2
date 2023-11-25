using Common;
using Common.DataTransferObjects;
using Common.Domain;
using Common.Mapper;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class EpisodesService : IEpisodesService
{
    private readonly IGenericRepository<Episode> _repository;
    private readonly ObjectMapper _mapper;

    public EpisodesService(IGenericRepository<Episode> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<EpisodeDTO>> GetAllEpisodes(Filter filter)
    {
        var getAll = await _repository.GetAll(filter);
        return _mapper.ListEpisodeToListEpisodeDTO(getAll);
    }

    public async Task<EpisodeDTO> GetOneEpisode(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.EpisodeToEpisodeDTO(getOne);
    }

    public async Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode)
    {
        var mappedEpisode = _mapper.AlterEpisodeDTOToEpisode(episode);
        mappedEpisode.Id = id;
        await _repository.Update(mappedEpisode);
        var updatedEpisode = await _repository.GetById(mappedEpisode.Id);
        return _mapper.EpisodeToEpisodeDTO(updatedEpisode);
    }

    public async Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode)
    {
        var addedEpisode = await _repository.Add(_mapper.AlterEpisodeDTOToEpisode(episode));
        return _mapper.EpisodeToEpisodeDTO(addedEpisode);
    }

    public async Task<bool> DeleteEpisode(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}