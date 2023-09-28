using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videoclub.API.DTOs;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Controllers;
[ApiController]
[Authorize]
[Route("/movies")]
public class MoviesController : Controller
{
    private IMovieService _movieService;
    private IUserService _userService;

    
    public MoviesController(IMovieService movieService, IUserService userService)
    {
        _movieService = movieService;
        _userService = userService;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<MovieDTO>> GetAllMovies()
    {

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);


    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<string>> GetAllCategories()
    {

        IEnumerable<string> categories = _movieService.GetAllCategories();

        return Ok(categories);


    }

    [HttpPost("add")]
    public ActionResult<IEnumerable<MovieDTO>> AddMovie(MovieDTO movie)
    {

        _movieService.AddMovie(movie);

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);
    }


    [HttpDelete("delete/{id}")]
    public ActionResult<IEnumerable<MovieDTO>> RemoveMovie(int id)
    {

        _movieService.DeleteMovie(id);

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);
    }

    [HttpPut("rent")]
    public  ActionResult<IEnumerable<MovieDTO>> RentMovie(RentHistoryDTO history)
    {

        _movieService.RentMovie(history);

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);
    }
    [HttpPost("return/{movie_id}")]
    public ActionResult<IEnumerable<MovieDTO>> ReturnMovie(int movie_id)
    {

        _movieService.ReturnMovie(movie_id);

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);
    }

    [HttpPut("edit")]
    public ActionResult<IEnumerable<MovieDTO>> EditMovie(MovieDTO movie)
    {

        _movieService.EditMovie(movie);

        IEnumerable<MovieDTO> movies = _movieService.GetAllMovies();

        return Ok(movies);
    }


}
