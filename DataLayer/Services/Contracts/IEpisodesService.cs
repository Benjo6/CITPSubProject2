using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IEpisodesService
{
    Task<List<EpisodeDTO>> GetAllEpisodes(Filter filter);
    Task<EpisodeDTO> GetOneEpisode(string id);
    Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode);
    Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode);
    Task<bool> DeleteEpisode(string id);
}