using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class EpisodesService : IEpisodesService
{
    private IGenericRepository<Episode> _repository;

    public EpisodesService(IGenericRepository<Episode> repository)
    {
        _repository = repository;
    }

    public async Task<List<EpisodeDTO>> GetAllEpisodes()
    {
        throw new NotImplementedException();
    }

    public async Task<EpisodeDTO> GetOneEpisode(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode)
    {
        throw new NotImplementedException();
    }

    public async Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode)
    {
        throw new NotImplementedException();
    }

    public async Task<EpisodeDTO> DeleteEpisode(string id)
    {
        throw new NotImplementedException();
    }
}