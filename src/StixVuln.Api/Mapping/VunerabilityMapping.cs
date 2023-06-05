using Mapster;

using StixVuln.Api.DTO.Vulnerability;
using StixVuln.Core.Vulnerability;

namespace StixVuln.Api.Mapping;

public class VunerabilityMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Vulnerability, VulnerabilityResponseDTO>();
    }
}
