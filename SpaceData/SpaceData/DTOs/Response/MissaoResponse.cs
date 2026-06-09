using SpaceData.Models;

namespace SpaceData.DTOs.Response;

public record MissaoResponse(
    string IdMissao,
    string NomeMissao,
    DateOnly DtInicio,
    int DuracaoEstimada,
    string Descricao,
    StatusMissao Status
);