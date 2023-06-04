namespace StixVuln.Api.Mapping;
using Mapster;

using StixVuln.Api.DTO;
using StixVuln.Core;

public class ExternalReferenceMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ExternalReferenceRequestDTO, ExternalReference>();
    }
}