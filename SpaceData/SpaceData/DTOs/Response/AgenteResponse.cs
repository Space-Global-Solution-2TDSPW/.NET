using SpaceData.Models;

namespace SpaceData.DTOs.Response;

public record AgenteResponse(
    string IdAgente,
    string Nome,
    DateOnly DtNascimento,
    StatusAgente Status,
    string Especialidade
);