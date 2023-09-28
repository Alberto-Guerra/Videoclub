using Videoclub.API.DTOs;
using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IMovieService
{
    IEnumerable<MovieDTO> GetAllMovies();
    IEnumerable<string> GetAllCategories();
    void RentMovie(RentHistoryDTO rentHistoryDTO);
    void ReturnMovie(int movie_id);
    void AddMovie(MovieDTO movieDTO);
    void EditMovie(MovieDTO movieDTO);
    void DeleteMovie(int movie_id);

    bool MovieExist(int movie_id);
    bool MovieIsRented(int movie_id);

    public bool CategoryExist(string name);
}
