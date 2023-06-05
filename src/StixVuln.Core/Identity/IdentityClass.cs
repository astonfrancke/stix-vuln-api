namespace StixVuln.Core.Identity;
public class IdentityClass
{
    public int IdentityClassId { get; private set; }
    public string Name { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public IdentityClass() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public IdentityClass(string name)
    {
        Name = name;
    }
}
