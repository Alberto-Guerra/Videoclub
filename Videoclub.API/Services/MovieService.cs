using Microsoft.EntityFrameworkCore;
using Videoclub.API.Context;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class MovieService : IMovieService
{
    private VideoclubContext _context;
    private IDtoService _dtoService;
    private IUserService _userService;  
    public MovieService(VideoclubContext context, IDtoService dtoService, IUserService userService)
    {
        _context = context;
        _dtoService = dtoService;
        _userService = userService;
    }


    //gets all the movies in the database and then returns them converted to DTO
    public IEnumerable<MovieDTO> GetAllMovies()
    {
        IEnumerable <Movie> movies = _context.Movies.Include(m => m.Category).ToList();

        return _dtoService.movieToDtoList(movies);
    }

    //gets all the categories in the database and then returns them converted to string
    public IEnumerable<string> GetAllCategories()
    {
        IEnumerable<string> categories = _context.Categories.Select(c => c.Name);

        return categories;
    }

    //check the proper fields and then creates the movie and adds it to the database
    public void AddMovie(MovieDTO movieDTO)
    {
        if (MovieExist(movieDTO.Id))
        {
            throw new InvalidOperationException("Cannot add - Movie already exists ");
        }

        if (string.IsNullOrWhiteSpace(movieDTO.Title))
        {
            throw new InvalidOperationException("Title cant be empty");
        }

        Movie movie = _dtoService.DtoToMovie(movieDTO);

        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    //check if the movie with the given id exists, looks for it and then removes it from the database
    public void DeleteMovie(int movie_id)
    {
        if (!MovieExist(movie_id))
        {
            throw new InvalidOperationException("Cannot delete - Movie does not exist ");
        }

        Movie movie = _context.Movies.First(x => x.Id == movie_id);
        _context.Movies.Remove(movie);
        _context.SaveChanges();

    }

    //checks the proper fields then updates them in case they are provided
    public void EditMovie(MovieDTO movieDTO)
    {
        if (!MovieExist(movieDTO.Id))
        {
            throw new InvalidOperationException("Cannot edit - Movie does not exist ");
        }

        if (!CategoryExist(movieDTO.Category))
        {
            throw new InvalidOperationException("The category selected doesnt exist");
        }

        if(string.IsNullOrWhiteSpace(movieDTO.Title))
        {
            throw new InvalidOperationException("Title cannot be empty");
        }

        Movie movie = _context.Movies.First(x => x.Id == movieDTO.Id);

        //use of the ?? operator to copy the value if there is one, or remain the same if there isnt

        movie.Title = movieDTO.Title ?? movie.Title; 
        movie.Description = movieDTO.Description ?? movie.Description;
        movie.PhotoURL = movieDTO.PhotoURL ?? movie.PhotoURL;
        var category = _context.Categories.FirstOrDefault(x => x.Name == movieDTO.Category);
        if(category != null)
        {
            movie.Category = category;
        }

        _context.SaveChanges();
    }


    //checks the proper fields, gets the movie and the user and then creates a rent history with the given information. It also updates the state of the movie
    public void RentMovie(RentHistoryDTO rentHistoryDTO)
    {

        if (!MovieExist(rentHistoryDTO.MovieId))
        {
            throw new InvalidOperationException("Cannot rent - Movie does not exist");
        }

        if (!_userService.UserExist(rentHistoryDTO.UserId))
        {
            throw new InvalidOperationException("Cannot rent - User does not exist");
        }

        if (MovieIsRented(rentHistoryDTO.MovieId))
        {
            throw new InvalidOperationException("Cannot rent - Movie already rented");
        }

        Movie movie = _context.Movies.First(m => m.Id == rentHistoryDTO.MovieId);
        movie.Available = false;
        User user = _context.Users.First(m => m.Id == rentHistoryDTO.UserId);

        RentHistory history = _dtoService.DtoToRentHistory(rentHistoryDTO);

        history.User = user;
        history.Movie = movie;

        _context.RentHistories.Add(history);    

        _context.SaveChanges();
    }

    //checks the proper fields, gets the history of the movie to return, sets returnDate to the actual time and updates the state of the movie
    public void ReturnMovie(int movie_id)
    {
        if (!MovieExist(movie_id))
        {
            throw new InvalidOperationException("Cannot return - Movie does not exist");
        }

        if (!MovieIsRented(movie_id))
        {
            throw new InvalidOperationException("Cannot return - Movie is not rented");
        }

        //We get the first history with no return date and movie_id equals to given id
        RentHistory history = _context.RentHistories.First(r => r.MovieId == movie_id && r.ReturnDate == null);

        history.ReturnDate = DateTime.Now;

        Movie movie = _context.Movies.First(x => x.Id == movie_id);
        movie.Available = true;

        _context.SaveChanges();
    }

    //check if there is a movie with the given id
    public bool MovieExist(int movie_id)
    {
        return _context.Movies.AsNoTracking().Any(m => m.Id == movie_id);
    }
    //check if there is a category with the given name
    public bool CategoryExist(string name)
    {
        return _context.Categories.AsNoTracking().Any(c =>  c.Name == name);
    }
    //check if the movie with the given id is rented
    public bool MovieIsRented(int movie_id)
    {
        return _context.RentHistories.AsNoTracking().Any(r => r.MovieId == movie_id && r.ReturnDate == null);
    }





}
