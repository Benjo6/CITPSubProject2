using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class EpisodesService : IEpisodesService
{
    private GenericRepository<Episode> _repository;
    private ObjectMapper _mapper;

    public EpisodesService(GenericRepository<Episode> repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<EpisodeDTO>> GetAllEpisodes()
    {
        var getAll = await _repository.GetAll();
        return _mapper.GetAllEpisodes(getAll) ?? new List<EpisodeDTO>();
    }

    public async Task<EpisodeDTO> GetOneEpisode(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.GetOneEpisode(getOne);
    }

    public async Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode)
    {
        _ = await _repository.GetById(id);
        var ep  = _mapper.AlterEpisodeDTO(episode);
        ep.Id = id;
        await _repository.Update(ep);
        var updatedep = await _repository.GetById(ep.Id);
        return _mapper.EpisodeDTO(updatedep);
    }

    public async Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode)
    {
        var addedepisode = await _repository.Add(_mapper.AlterMovieDTOToMovie(episode));
        return _mapper.AlterEpisodeDTO(addedepisode);
    }

    public async Task<bool> DeleteEpisode(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}