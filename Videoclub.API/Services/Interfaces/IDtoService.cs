using Videoclub.API.DTOs;
using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IDtoService
{
    public MovieDTO MovieToDto(Movie movie);

    public Movie DtoToMovie(MovieDTO dto);

    public IEnumerable<MovieDTO> movieToDtoList(IEnumerable<Movie> dtoList);

    public User DtoToUser(UserDTO dto);
    public UserDTO UserToDto(User user);

    public RentHistoryDTO RentHistoryToDto(RentHistory history);
    public RentHistory DtoToRentHistory(RentHistoryDTO dto);
}
