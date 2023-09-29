using Microsoft.EntityFrameworkCore;
using Videoclub.API.Context;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class DtoService : IDtoService
{
    private VideoclubContext _context;
    public DtoService(VideoclubContext context)
    {
        _context = context;
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
            State = movie.Available ? Constants.AVAILABLE_STRING : Constants.NOT_AVAILABLE_STRING
        };

        //checks if there is a rent history with null return date
        var historyIfRented = _context.RentHistories.Include(u => u.User).FirstOrDefault(r => r.ReturnDate == null && r.MovieId == dto.Id);
        if (historyIfRented != null)
        {
            dto.UsernameRented = historyIfRented.User.Username;
            dto.UserId = historyIfRented.User.Id;
            dto.RentDate = historyIfRented.RentDate;
        }

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
            Available = dto.State == Constants.AVAILABLE_STRING
        };
        movie.RentHistories = _context.RentHistories.Where(r => r.MovieId == movie.Id);
        movie.Category = _context.Categories.First(c => c.Name == dto.Category);

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

        user.RentHistories = _context.RentHistories.Where(r => r.UserId == user.Id);

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
