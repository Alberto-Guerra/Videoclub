using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Controllers;

[ApiController]
[Authorize]
[Route("/movies")]
public class MoviesController : Controller
{
    private IMovieService _movieService;
    private ICategoryService _categoryService;

    public MoviesController(IMovieService movieService, ICategoryService categoryService)
    {
        _movieService = movieService;
        _categoryService = categoryService;
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
        IEnumerable<Category> categories = _categoryService.GetCategories();
        return Ok(categories.Select(c => c.Name)); //Return the title of the categories
    }

    [HttpPost("add")]
    public ActionResult AddMovie(MovieDTO movie)
    {
        _movieService.AddMovie(movie);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public ActionResult RemoveMovie(int id)
    {
        _movieService.DeleteMovie(id);
        return Ok();
    }

    [HttpPut("rent")]
    public ActionResult RentMovie(RentHistoryDTO history)
    {
        _movieService.RentMovie(history);
        return Ok();
    }

    [HttpPost("return/{movie_id}")]
    public ActionResult ReturnMovie(int movie_id)
    {
        _movieService.ReturnMovie(movie_id);
        return Ok();
    }

    [HttpPut("edit")]
    public ActionResult EditMovie(MovieDTO movie)
    {
        _movieService.EditMovie(movie);
        return Ok();
    }
}
