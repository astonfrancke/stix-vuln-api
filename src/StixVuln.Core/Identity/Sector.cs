namespace StixVuln.Core.Identity;
public class Sector
{
    public int SectorId { get; private set; }
    public string Name { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Sector() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Sector(string name)
    {
        Name = name;
    }
}
