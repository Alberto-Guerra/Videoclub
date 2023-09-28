using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IAuthService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    void VerifyPassWordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    string createToken(User user);
}
