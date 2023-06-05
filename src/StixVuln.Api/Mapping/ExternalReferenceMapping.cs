namespace StixVuln.Api.Mapping;
using Mapster;

using StixVuln.Api.DTO.Vulnerability;
using StixVuln.Core;

public class ExternalReferenceMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ExternalReferenceRequestDTO, ExternalReference>().MapToConstructor(true);
        config.NewConfig<ExternalReference, ExternalReferenceResponseDTO>();
    }
}