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
    public MovieDTO MovieToDto(Movie movie)
    {
        var dto = new MovieDTO();

        dto.Id = movie.Id;
        dto.Title = movie.Title;
        dto.Description = movie.Description;
        dto.Category = movie.Category.Name;
        dto.PhotoURL = movie.PhotoURL;
        dto.State = movie.Available ? Constants.AVAILABLE_STRING : Constants.NOT_AVAILABLE_STRING;

        //Checks if there is a rent history with null return date
        var historyIfRented = _context.RentHistories.Include(u => u.User).FirstOrDefault(r => r.ReturnDate == null && r.movie_id == dto.Id); 
        if (historyIfRented != null)
        {
            dto.UsernameRented = historyIfRented.User.Username;
            dto.Userid = historyIfRented.User.Id;
            dto.rent_date = historyIfRented.RentDate;
        }

        return dto;





    }

    public Movie DtoToMovie(MovieDTO dto)
    {
        var movie = new Movie();

        movie.Id = dto.Id;
        movie.Title = dto.Title;
        movie.Description = dto.Description;
        movie.PhotoURL = dto.PhotoURL;
        movie.Available = dto.State == Constants.AVAILABLE_STRING;
        movie.RentHistories = _context.RentHistories.Where(r => r.movie_id == movie.Id);

        movie.Category = _context.Categories.First(c  => c.Name == dto.Category);

        return movie;

    }

    public IEnumerable<MovieDTO> movieToDtoList(IEnumerable<Movie> dtoList)
    {
        List<MovieDTO> dtos = new List<MovieDTO>();

        foreach (Movie movie in dtoList)
        {
            dtos.Add(MovieToDto(movie));
        }

        return dtos;
    }

    public User DtoToUser(UserDTO dto)
    {
        User user = new User();

        user.Username = dto.Username;
        user.Name = dto.Name;
        user.LastName = dto.LastName;
        user.Birthday = dto.Birthday;

        user.RentHistories = _context.RentHistories.Where(r => r.user_id  == user.Id);

        return user;
    }

    public UserDTO UserToDto(User user)
    {
        UserDTO dto = new UserDTO();

        dto.Username = user.Username;
        dto.Name = user.Name;
        dto.LastName = user.LastName;
        dto.Birthday = user.Birthday;

        return dto;
    }

    public RentHistoryDTO RentHistoryToDto(RentHistory history)
    {
        RentHistoryDTO dto = new RentHistoryDTO();
        
        dto.RentDate = history.RentDate;
        dto.user_id = history.user_id;
        dto.movie_id = history.movie_id;
        dto.ReturnDate = history.ReturnDate;

        return dto;


    }

    public RentHistory DtoToRentHistory(RentHistoryDTO dto)
    {
        RentHistory history = new RentHistory();

        history.ReturnDate = dto.ReturnDate;
        history.RentDate = dto.RentDate;

        return history;

    }
}
