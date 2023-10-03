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

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //creates the hash of the password with the hmacsha512 algorithm
    public void CreatePasswordHash(
        string password,
        out byte[] passwordHash,
        out byte[] passwordSalt
    )
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    //creates a jwt token given an user
    public string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim> //creates the claims of the token
        {
            new Claim("name", user.Username),
            new Claim("id", user.Id.ToString()),
        };
        //takes secret phrase from appsettings.json and creates a key with it
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
        );
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //creates the token with all the information and also the issuer, audience from appsettings.json
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

    //computes the hash of the given password and compare it with the stored hash
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
