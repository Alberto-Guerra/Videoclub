using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class MovieService : IMovieService
{
    private IMovieRepository _movieRepository;
    private IDtoService _dtoService;
    private IUserRepository _userRepository;
    private ICategoryRepository _categoryRepository;
    private IRentHistoryRepository _historyRepository;

    public MovieService(
        IMovieRepository movieRepository,
        IDtoService dtoService,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IRentHistoryRepository historyRepository
    )
    {
        _movieRepository = movieRepository;
        _dtoService = dtoService;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _historyRepository = historyRepository;
    }

    //gets all the movies in the database and then returns them converted to DTO
    public IEnumerable<MovieDTO> GetAllMovies()
    {
        IEnumerable<Movie> movies = _movieRepository.GetAll();
        return _dtoService.movieToDtoList(movies);
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
        _movieRepository.AddMovie(movie);
    }

    //check if the movie with the given id exists, looks for it and then removes it from the database
    public void DeleteMovie(int movie_id)
    {
        Movie? movie = _movieRepository.GetMovieById(movie_id);
        if (movie == null)
        {
            throw new InvalidOperationException("Cannot delete - Movie does not exist ");
        }
        _movieRepository.RemoveMovie(movie);
    }

    //checks the proper fields then updates them in case they are provided
    public void EditMovie(MovieDTO movieDTO)
    {
        if (string.IsNullOrWhiteSpace(movieDTO.Title))
        {
            throw new InvalidOperationException("Title cannot be empty");
        }
        Movie? movie = _movieRepository.GetMovieById(movieDTO.Id);
        if (movie == null)
        {
            throw new InvalidOperationException("Cannot edit - Movie does not exist ");
        }
        Category? category = _categoryRepository.GetCategoryByName(movieDTO.Category);
        if (category == null)
        {
            throw new InvalidOperationException("The category selected doesnt exist");
        }
        //use of the ?? operator to copy the value if there is one, or remain the same if there isnt
        movie.Title = movieDTO.Title ?? movie.Title;
        movie.Description = movieDTO.Description ?? movie.Description;
        movie.PhotoURL = movieDTO.PhotoURL ?? movie.PhotoURL;

        movie.Category = category;

        _movieRepository.UpdateMovie(movie);
    }

    //checks the proper fields, gets the movie and the user and then creates a rent history with the given information. It also updates the state of the movie
    public void RentMovie(RentHistoryDTO rentHistoryDTO)
    {
        Movie? movie = _movieRepository.GetMovieById(rentHistoryDTO.MovieId);
        User? user = _userRepository.GetById(rentHistoryDTO.UserId);
        if (movie == null)
        {
            throw new InvalidOperationException("Cannot rent - Movie does not exist");
        }
        if (user == null)
        {
            throw new InvalidOperationException("Cannot rent - User does not exist");
        }
        if (MovieIsRented(rentHistoryDTO.MovieId))
        {
            throw new InvalidOperationException("Cannot rent - Movie already rented");
        }
        RentHistory history = _dtoService.DtoToRentHistory(rentHistoryDTO);
        history.User = user;
        history.Movie = movie;
        movie.Available = false;
        _historyRepository.Add(history);
    }

    //checks the proper fields, gets the history of the movie to return, sets returnDate to the actual time and updates the state of the movie
    public void ReturnMovie(int movie_id)
    {
        Movie? movie = _movieRepository.GetMovieById(movie_id);
        if (movie == null)
        {
            throw new InvalidOperationException("Cannot return - Movie does not exist");
        }
        RentHistory? history = _historyRepository.GetRentedByMovieId(movie_id);
        if (history == null)
        {
            throw new InvalidOperationException("Cannot return - Movie is not rented");
        }
        history.ReturnDate = DateTime.Now;
        movie.Available = true;
        _historyRepository.Update(history);
    }

    //check if there is a movie with the given id
    public bool MovieExist(int movie_id)
    {
        return _movieRepository.GetMovieById(movie_id) != null;
    }

    //check if the movie with the given id is rented
    public bool MovieIsRented(int movie_id)
    {
        return _historyRepository.GetRentedByMovieId(movie_id) != null;
    }
}
