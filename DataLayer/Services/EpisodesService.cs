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

    public async Task<List<EpisodeDTO>> GetAllEpisodes(int? page = 1, int? perPage = 10)
    {
        var getAll = await _repository.GetAll(page, perPage);
        return _mapper.ListEpisodeToListEpisodeDTO(getAll) ?? new List<EpisodeDTO>();
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