namespace StixVuln.Core.Authentication.Interfaces;
public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}
