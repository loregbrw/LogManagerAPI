namespace Application.Interfaces.Services.Core.Auth;

public interface IPasswordHasher
{

    string Hash(string password);

    bool Verify(string password, string hashedPassword);
}