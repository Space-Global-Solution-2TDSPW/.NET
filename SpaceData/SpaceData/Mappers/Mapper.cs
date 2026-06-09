using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers;

public static class AgenteMapper
{
    public static Agente ToEntity(AgenteRequest r) => new()
    {
        Nome = r.Nome,
        DtNascimento = r.DtNascimento,
        Status = r.Status,
        Especialidade = r.Especialidade
    };

    public static AgenteResponse ToResponse(Agente a) => new(
        a.IdAgente,
        a.Nome,
        a.DtNascimento,
        a.Status,
        a.Especialidade
    );
}

public static class MissaoMapper
{
    public static Missao ToEntity(MissaoRequest r) => new()
    {
        NomeMissao = r.NomeMissao,
        DtInicio = r.DtInicio,
        DuracaoEstimada = r.DuracaoEstimada,
        Descricao = r.Descricao,
        Status = r.Status
    };

    public static MissaoResponse ToResponse(Missao m) => new(
        m.IdMissao,
        m.NomeMissao,
        m.DtInicio,
        m.DuracaoEstimada,
        m.Descricao,
        m.Status
    );
}

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