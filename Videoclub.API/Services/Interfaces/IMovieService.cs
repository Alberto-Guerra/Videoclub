using Videoclub.API.DTOs;

namespace Videoclub.API.Services.Interfaces;

public interface IMovieService
{
    IEnumerable<MovieDTO> GetAllMovies();
    void RentMovie(RentHistoryDTO rentHistoryDTO);
    void ReturnMovie(int movie_id);
    void AddMovie(MovieDTO movieDTO);
    void EditMovie(MovieDTO movieDTO);
    void DeleteMovie(int movie_id);
    bool MovieExist(int movie_id);
    bool MovieIsRented(int movie_id);
}
