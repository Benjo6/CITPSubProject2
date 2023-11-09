using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IEpisodesService
{
    Task<List<EpisodeDTO>> GetAllEpisodes(int? page = 1, int? perPage = 10);
    Task<EpisodeDTO> GetOneEpisode(string id);
    Task<EpisodeDTO> UpdateEpisode(string id, AlterEpisodeDTO episode);
    Task<EpisodeDTO> AddEpisode(AlterEpisodeDTO episode);
    Task<bool> DeleteEpisode(string id);
}