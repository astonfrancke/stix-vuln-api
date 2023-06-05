namespace StixVuln.Core.Authentication;
public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; } // Hash
    public string IdentityId { get; private set; }
    public Role Role { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public User(
        string username,
        string password,
        string identityId,
        Role role)
    {
        Username = username;
        Password = password;
        IdentityId = identityId;
        Role = role;
    }
}
