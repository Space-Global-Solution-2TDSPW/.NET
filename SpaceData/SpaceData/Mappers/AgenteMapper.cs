using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers;

public class AgenteMapper
{
    public Agente AgenteRequestToEntity(AgenteRequest agenteRequest)
    {
        return new Agente
        {
            Nome = agenteRequest.Nome,
            DtNascimento = agenteRequest.DtNascimento,
            Status = agenteRequest.Status,
            Especialidade = agenteRequest.Especialidade
        };
    }

    public AgenteResponse AgenteToResponse(Agente agente)
    {
        return new AgenteResponse(
            agente.IdAgente,
            agente.Nome,
            agente.DtNascimento,
            agente.Status,
            agente.Especialidade
        );
    }
}
