using StixVuln.Core.Authentication.Interfaces;
using StixVuln.Core.Authentication;
using Microsoft.EntityFrameworkCore;
using StixVuln.Infrastructure.Data;

namespace StixVuln.Infrastructure.Authenticaiton;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
}
