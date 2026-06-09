using SpaceData.Models;

namespace SpaceData.DTOs.Response;

public class AgenteResponse
{
    public string IdAgente { get; init; }
    public string Nome { get; init; } 
    public DateOnly DtNascimento { get; init; }
    public StatusAgente Status { get; init; }
    public string Especialidade { get; init; } 

    public AgenteResponse() { }

    public AgenteResponse(string idAgente, string nome, DateOnly dtNascimento, StatusAgente status, string especialidade)
    {
        IdAgente = idAgente;
        Nome = nome;
        DtNascimento = dtNascimento;
        Status = status;
        Especialidade = especialidade;
    }
}


