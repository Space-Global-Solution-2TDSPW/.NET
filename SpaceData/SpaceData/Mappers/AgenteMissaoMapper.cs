using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers
{
    public static class AgenteMissaoMapper
    {
        public static AgenteMissao ToEntity(AgenteMissaoRequest r) => new()
        {
            IdAgente = r.IdAgente,
            IdMissao = r.IdMissao,
            RelatorioMissao = r.RelatorioMissao
        };

        public static AgenteMissaoResponse ToResponse(AgenteMissao am) => new(
            am.IdAgenteMissao,
            am.Agente?.Nome ?? "N/A",
            am.Missao?.NomeMissao ?? "N/A",
            am.RelatorioMissao
        );
    }
}
