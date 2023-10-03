using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories.Interfaces;

public interface IMovieRepository
{
    void AddMovie(Movie movie);
    void RemoveMovie(Movie movie);
    void UpdateMovie(Movie movie);
    Movie? GetMovieById(int id);
    IEnumerable<Movie> GetAll();
}
