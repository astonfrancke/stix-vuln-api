namespace StixVuln.Api.DTO;

public record ExternalReferenceRequestDTO(
    string SourceName,
    string Description,
    string ExternalId,
    string Url,
    string Hash
    );
