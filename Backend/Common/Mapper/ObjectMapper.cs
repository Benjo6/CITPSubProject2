using Common.DataTransferObjects;
using Common.Domain;
using Riok.Mapperly.Abstractions;

namespace Common.Mapper;

[Mapper]
public partial class ObjectMapper
{
    /*
     * Alias
     */
    public partial AliasDTO AliasToAliasDTO(Alias alias);
    public partial Alias AlterAliasDTOToAlias(AlterAliasDTO alias);
    public partial List<AliasDTO> ListAliasToListAliasDTO(List<Alias> movie);

    /*
     * BookmarkMovie
     */
    public partial BookmarkMovieDTO BookmarkMovieToBookmarkMovieDTO(BookmarkMovie bookmarkMovie);
    public partial List<BookmarkMovieDTO> ListBookmarkMovieToListBookmarkMovieDTO(List<BookmarkMovie> bookmarkMovie);

    public partial BookmarkMovie BookmarkMovieDTOToBookmarkMovie(BookmarkMovieDTO bookmarkMovie);
    public partial BookmarkMovie AlterBookmarkMovieDTOToBookmarkMovie (AlterBookmarkMovieDTO bookmarkMovie);
    /*
     * BookmarkPersonality
     */
    public partial BookmarkPersonalityDTO BookmarkPersonalityToBookmarkPersonalityDTO(
        BookmarkPersonality bookmarkPersonality);
    public partial BookmarkPersonality BookmarkPersonalityDTOToBookmarkPersonality(
        BookmarkPersonalityDTO bookmarkPersonality);
    public partial BookmarkPersonality AlterBookmarkPersonalityDTOToBookmarkPersonality(AlterBookmarkPersonalityDTO bookmarkPersonality);

    public partial List<BookmarkPersonalityDTO> ListBookmarkPersonalityToListBookmarkPersonalityDTO(
     List<BookmarkPersonality> bookmarkPersonalities);
    /*
     * Episode
     */
    public partial List<EpisodeDTO> ListEpisodeToListEpisodeDTO(List<Episode> episode);
    public partial EpisodeDTO EpisodeToEpisodeDTO(Episode episode);

    public partial Episode EpisodeDTOTOEpisode(EpisodeDTO episode);
    public partial Episode AlterEpisodeDTOToEpisode(AlterEpisodeDTO episode);
    /* 
     * Movie
     */
    public partial AlterResponseMovieDTO MovieToAlterResponseMovieDTO(Movie movie);
    public partial Movie AlterMovieDTOToMovie(AlterMovieDTO movie);
    public partial List<GetAllMovieDTO> ListMovieToListGetAllMoviesDTO(List<Movie> movies);
    public partial GetOneMovieDTO MovieToGetOneMovieDTO(Movie movie);
    /*
     * Person
     */
    public partial List<GetAllPersonDTO> ListPersonToListGetAllPersonsDTO(List<Person> person);
    public partial GetOnePersonDTO PersonToGetOnePersonDTO(Person person);
    public partial Person AlterPersonDTOToPerson(AlterPersonDTO person);
    public partial UpdatePersonDTO PersonToUpdatePersonDTO(Person person);

    /*
     * RatingHistory
     */
    public partial RatingHistoryDTO RatingHistoryToRatingHistoryDTO(RatingHistory ratingHistory);
    public partial RatingHistory RatingHistoryDTOToRatingHistory(RatingHistoryDTO ratingHistory);
    /*
     * Role
     */
    public partial RoleDTO RoleToRoleDTO(Role role);
    public partial Role RoleDTOToRole(RoleDTO role);
    /*
     * SearchHistory
     */
    public partial List<SearchHistoryDTO> ListSearchToListSearchDTO(List<SearchHistory> searchHistory);

    public partial SearchHistoryDTO SearchHistoryToSearchHistoryDTO(SearchHistory searchHistory);
    public partial SearchHistory SearchHistoryDTOToSearchHistory(SearchHistoryDTO searchHistory);
    /*
     * User
     */     
    public partial List<UserDTO> ListUserToListUserDTO(List<User> user);

    public partial UserDTO UserToUserDTO(User user);
    public partial User AlterUserDTOToUser(AlterUserDTO user);
    public partial User UserDTOToUser(UserDTO user);
}

