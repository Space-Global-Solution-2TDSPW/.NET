using System;
using SpaceData.Models;

namespace SpaceData.DTOs.Response;

public class MissaoResponse
{
    public string IdMissao { get; init; }
    public string NomeMissao { get; init; }
    public DateOnly DtInicio { get; init; }
    public int DuracaoEstimada { get; init; }
    public string Descricao { get; init; }
    public StatusMissao Status { get; init; }

    public MissaoResponse() { }

    public MissaoResponse(string idMissao, string nomeMissao, DateOnly dtInicio, int duracaoEstimada, string descricao, StatusMissao status)
    {
        IdMissao = idMissao;
        NomeMissao = nomeMissao;
        DtInicio = dtInicio;
        DuracaoEstimada = duracaoEstimada;
        Descricao = descricao;
        Status = status;
    }
}
