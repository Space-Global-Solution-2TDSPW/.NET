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
