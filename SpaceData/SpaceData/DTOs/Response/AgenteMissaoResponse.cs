using System;
using SpaceData.Models;

namespace SpaceData.DTOs.Response;


public class AgenteMissaoResponse
{
    public string IdAgenteMissao { get; init; }
    public string NomeAgente { get; init; }
    public string NomeMissao { get; init; }
    public string RelatorioMissao { get; init; }

    public AgenteMissaoResponse() { }

    public AgenteMissaoResponse(string idAgenteMissao, string nomeAgente, string nomeMissao, string relatorioMissao)
    {
        IdAgenteMissao = idAgenteMissao;
        NomeAgente = nomeAgente;
        NomeMissao = nomeMissao;
        RelatorioMissao = relatorioMissao;
    }
}

