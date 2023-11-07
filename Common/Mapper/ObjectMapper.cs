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
    public partial MovieDTO MovieToMovieDTO(Movie movie);
    public partial PersonDTO PersonToPersonDTO(Person person);
    public partial RatingHistoryDTO RatingHistoryToRatingHistoryDTO(RatingHistory ratingHistory);
    public partial RoleDTO RoleToRoleDTO(Role role);
    public partial SearchHistoryDTO SearchHistoryToSearchHistoryDTO(SearchHistory searchHistory);
    public partial UserDTO UserToUserDTO(User user);
}