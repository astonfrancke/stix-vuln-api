namespace StixVuln.SharedKernel;

public abstract class StixDomainObject
{
    public string Id { get; }
    public virtual string Type { get; } = "";
    public string SpecVersion { get; } = "2.1";
    public DateTime Created { get; init; }
    public DateTime Modified { get; set; }
    public string CreatedByRef { get; init; }

    protected StixDomainObject()
    {
        Id = $"{Type}--{Guid.NewGuid()}";
    }
}
