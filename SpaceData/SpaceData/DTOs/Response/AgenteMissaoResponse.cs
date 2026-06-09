namespace SpaceData.DTOs.Response;

public record AgenteMissaoResponse(
    string IdAgenteMissao,
    string NomeAgente,
    string NomeMissao,
    string RelatorioMissao
);