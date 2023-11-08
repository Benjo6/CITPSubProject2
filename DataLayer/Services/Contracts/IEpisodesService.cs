using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IEpisodesService
{
    Task<List<EpisodeDTO>> GetAllEpisodes();
    Task<EpisodeDTO> GetOneEpisode(string id);
    Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode);
    Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode);
    Task<EpisodeDTO> DeleteEpisode(string id);
}