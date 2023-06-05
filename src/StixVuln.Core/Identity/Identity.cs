using StixVuln.SharedKernel;

namespace StixVuln.Core.Identity;
public class Identity : StixDomainObject
{
    public override string Type => "identity";
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ContactInformation { get; private set; }
    public IdentityClass IdentityClass { get; set; }

    private readonly List<Role> _roles = new();
    public IEnumerable<Role> Roles => _roles.AsReadOnly();

    private readonly List<Sector> _sectors = new();
    public IEnumerable<Sector> Sectors => _sectors.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Identity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Identity(
        string name,
        string description,
        string contactInformation,
        string identityClass,
        List<string> roles,
        List<string> sectors)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Name is required");
        }

        Name = name;
        Description = description;
        ContactInformation = contactInformation;
        IdentityClass = new IdentityClass(identityClass);

        if (roles != null)
        {
            foreach (var role in roles)
            {
                _roles.Add(new Role(role));
            }
        }

        if (sectors != null)
        {
            foreach (var sector in sectors)
            {
                _sectors.Add(new Sector(sector));
            }
        }
    }
}
