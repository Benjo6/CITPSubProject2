using Common.DataTransferObjects;
using Common.Domain;
using Riok.Mapperly.Abstractions;

namespace Common.Mapper;

[Mapper]
public partial class ObjectMapper
{
    public partial AliasDTO AliasToAliasDTO(Alias alias);
    public partial BookmarkMovieDTO BookmarkMovieToBookmarkMovieDTO(BookmarkMovie bookmarkMovie);

    public partial BookmarkPersonalityDTO BookmarkPersonalityToBookmarkPersonalityDTO(
        BookmarkPersonality bookmarkPersonality);
    public partial EpisodeDTO EpisodeToEpisodeDTO(Episode episode);
    public partial AddAndUpdateMovieDTO MovieToAddAndUpdateMovieDTO(Movie movie);
    public partial Movie CreateMovieDTOToMovie(CreateMovieDTO movie);
    public partial List<GetAllMovieDTO> MovieToGetAllMoviesDTO(List<Movie> movie);
    public partial GetOneMovieDTO MovieToGetOneMovieDTO(Movie movie);
    public partial RatingHistoryDTO RatingHistoryToRatingHistoryDTO(RatingHistory ratingHistory);
    public partial RoleDTO RoleToRoleDTO(Role role);
    public partial SearchHistoryDTO SearchHistoryToSearchHistoryDTO(SearchHistory searchHistory);
    public partial UserDTO UserToUserDTO(User user);
}