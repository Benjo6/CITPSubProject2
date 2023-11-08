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

    public partial AlterResponseMovieDTO MovieToAlterResponseMovieDTO(Movie movie);
    public partial Movie AlterMovieDTOToMovie(AlterMovieDTO movie);
    public partial List<GetAllMovieDTO> MovieToGetAllMoviesDTO(List<Movie> movie);
    public partial List<GetAllPersonDTO> PersonToGetAllPersonsDTO(List<Person> person);
    public partial GetOneMovieDTO MovieToGetOneMovieDTO(Movie movie);
    public partial GetOnePersonDTO PersonToGetOnePersonDTO(Person person);
    public partial Person AlterPersonDTOToPerson(AlterPersonDTO person);

    public partial UpdatePersonDTO PersonToUpdatePersonDTO(Person person);

    public partial RatingHistoryDTO RatingHistoryToRatingHistoryDTO(RatingHistory ratingHistory);
    public partial RoleDTO RoleToRoleDTO(Role role);
    public partial SearchHistoryDTO SearchHistoryToSearchHistoryDTO(SearchHistory searchHistory);
    public partial UserDTO UserToUserDTO(User user);
}