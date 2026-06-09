using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers;

public class AgenteMissaoMapper
{
    public AgenteMissao AgenteMissaoRequestToEntity(AgenteMissaoRequest request)
    {
        return new AgenteMissao
        {
            IdAgente = request.IdAgente,
            IdMissao = request.IdMissao,
            RelatorioMissao = request.RelatorioMissao
        };
    }

    public AgenteMissaoResponse AgenteMissaoToResponse(AgenteMissao agenteMissao)
    {
        return new AgenteMissaoResponse(
            agenteMissao.IdAgenteMissao,
            agenteMissao.Agente?.Nome ?? "N/A",
            agenteMissao.Missao?.NomeMissao ?? "N/A",
            agenteMissao.RelatorioMissao
        );
    }
}
