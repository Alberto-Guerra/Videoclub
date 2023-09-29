using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Videoclub.API.Context;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Controllers;

[Route("/auth")]
public class UsersController : Controller
{

    private IAuthService _authService;
    private IUserService _userService;  
    private IDtoService _dtoService;

    public UsersController(IAuthService authService, IUserService userService, IDtoService dtoService) 
    {
        _authService = authService;
        _userService = userService;
        _dtoService = dtoService;


    }

    [HttpGet("users")]
    public ActionResult<IEnumerable<User>> getAllUsers()
    {
        return Ok(_userService.getAllUsers());
    }

    [HttpPost("register")]
    public ActionResult<string> Register([FromBody] UserDTO userDTO)
    {

        _userService.checkUserForRegister(userDTO);

        _authService.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = _dtoService.DtoToUser(userDTO);  

        _userService.addUser(user, passwordHash, passwordSalt);

        return Ok(user.Username);

    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] UserDTO userDTO)
    {

        User user = _userService.getUserByUsername(userDTO.Username);

        _authService.VerifyPassWordHash(userDTO.Password, user.PasswordHash, user.PasswordSalt);

        string token = _authService.createToken(user);
           
        return Ok(token);

    }


    
}
