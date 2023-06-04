using System.Collections.ObjectModel;

namespace StixVuln.Core;
public class ExternalReference
{
    public int ExternalReferenceId { get; private set; }
    public string SourceName { get; private set; }
    public string? Description { get; private set; }
    public string? ExternalId { get; private set; }
    public string? Url { get; private set; }

    private readonly Dictionary<string, string> _hashes = new();
    public IReadOnlyDictionary<string, string> Hashes =>
        new ReadOnlyDictionary<string, string>(_hashes);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ExternalReference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ExternalReference(
        string sourceName,
        string? description,
        string? externalId,
        string? url,
        string? hash)
    /* TODO: Enforce:
     * Specifies a dictionary of hashes for the contents of the url. This SHOULD be provided when the url property is present.
     * Dictionary keys MUST come from one of the entries listed in the hash-algorithm-ov open vocabulary. */
    {
        if (string.IsNullOrEmpty(sourceName))
        {
            throw new ArgumentNullException("Name is required");
        }

        if (string.IsNullOrEmpty(description) &&
            string.IsNullOrEmpty(externalId) &&
            string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException("At least one of the description, url, or external_id properties must be present.");
        }

        if (!string.IsNullOrEmpty(url) && string.IsNullOrEmpty(hash))
        {
            throw new ArgumentNullException("Hash should be provided when the url property is present.");
        }

        SourceName = sourceName;
        Description = description;
        ExternalId = externalId;
        Url = url;

        if (!string.IsNullOrEmpty(url))
        {
            _hashes.Add("SHA256", hash);
            // TODO: Add support for algorithms
        }
    }
}
