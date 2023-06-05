namespace StixVuln.Core.Authentication.Interfaces;
public interface IUserRepository
{
    public Task<User?> GetUserByUsername(string username);
}
