using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class DtoService : IDtoService
{
    private IRentHistoryService _historyService;
    private ICategoryService _categoryService;

    public DtoService(IRentHistoryService historyService, ICategoryService categoryService)
    {
        _historyService = historyService;
        _categoryService = categoryService;
    }

    //converts a movie to a movieDTO
    public MovieDTO MovieToDto(Movie movie)
    {
        var dto = new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Category = movie.Category.Name,
            PhotoURL = movie.PhotoURL,
            State = movie.Available ? Constants.AVAILABLE_STRING : Constants.NOT_AVAILABLE_STRING,
            UserId = _historyService.GetUserIdFromMovieId(movie.Id),
            UsernameRented = _historyService.GetUsernameFromMovieId(movie.Id),
            RentDate = _historyService.GetRentDateFromMovieId(movie.Id)
        };
        return dto;
    }

    //converts a movieDTO to a movie
    public Movie DtoToMovie(MovieDTO dto)
    {
        var movie = new Movie
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            PhotoURL = dto.PhotoURL,
            Available = dto.State == Constants.AVAILABLE_STRING,
            RentHistories = _historyService.GetByMovieId(dto.Id),
            Category = _categoryService.GetCategoryByName(dto.Category)
        };
        return movie;
    }

    //converts a list of movies to a list of movieDTOs calling the movieToDto method
    public IEnumerable<MovieDTO> movieToDtoList(IEnumerable<Movie> dtoList)
    {
        List<MovieDTO> dtos = new List<MovieDTO>();
        foreach (Movie movie in dtoList)
        {
            dtos.Add(MovieToDto(movie));
        }
        return dtos;
    }

    //converts a userDTO to a user
    public User DtoToUser(UserDTO dto)
    {
        User user = new User
        {
            Username = dto.Username,
            Name = dto.Name,
            LastName = dto.LastName,
            Birthday = dto.Birthday
        };
        return user;
    }

    //converts a user to a userDTO
    public UserDTO UserToDto(User user)
    {
        UserDTO dto = new UserDTO
        {
            Username = user.Username,
            Name = user.Name,
            LastName = user.LastName,
            Birthday = user.Birthday
        };
        return dto;
    }

    //converts a RentHistory to a RentHistoryDTO
    public RentHistoryDTO RentHistoryToDto(RentHistory history)
    {
        RentHistoryDTO dto = new RentHistoryDTO
        {
            RentDate = history.RentDate,
            UserId = history.UserId,
            MovieId = history.MovieId,
            ReturnDate = history.ReturnDate
        };
        return dto;
    }

    //converts a RentHistoryDTO to a RentHistory
    public RentHistory DtoToRentHistory(RentHistoryDTO dto)
    {
        RentHistory history = new RentHistory
        {
            ReturnDate = dto.ReturnDate,
            RentDate = dto.RentDate
        };
        return history;
    }
}
