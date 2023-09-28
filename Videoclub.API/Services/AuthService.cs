using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //Creates the hash of the password with the hmacsha512 algorithm
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())   
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    //Creates a jwt token given an user
    public string createToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("name", user.Username),
            new Claim("id", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)); //Takes secret phrase from appsettings.json

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var token = new JwtSecurityToken(
          claims: claims,
          expires: DateTime.Now.AddDays(1),
          issuer: _configuration.GetSection("AppSettings:Issuer").Value,
          audience: _configuration.GetSection("AppSettings:Audiencie").Value,
          signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    //Computes the hash of the given password and compare it with the stored hash
    public void VerifyPassWordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if (!hash.SequenceEqual(passwordHash))
            {
                throw new InvalidOperationException("Incorrect Password");
            }
            
        }
    }
}
