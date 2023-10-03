using Microsoft.EntityFrameworkCore;
using Videoclub.API.Context;
using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private VideoclubContext _context;

    public MovieRepository(VideoclubContext context)
    {
        _context = context;
    }

    public void AddMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    public IEnumerable<Movie> GetAll()
    {
        return _context.Movies.Include(m => m.Category).ToList();
    }

    public Movie? GetMovieById(int id)
    {
        return _context.Movies.Find(id);
    }

    public void RemoveMovie(Movie movie)
    {
        _context.Movies.Remove(movie);
        _context.SaveChanges();
    }

    public void UpdateMovie(Movie movie)
    {
        _context.Movies.Update(movie);
        _context.SaveChanges();
    }
}
