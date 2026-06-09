using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers;

public class MissaoMapper
{
    public Missao MissaoRequestToEntity(MissaoRequest missaoRequest)
    {
        return new Missao
        {
            NomeMissao = missaoRequest.NomeMissao,
            DtInicio = missaoRequest.DtInicio,
            DuracaoEstimada = missaoRequest.DuracaoEstimada,
            Descricao = missaoRequest.Descricao,
            Status = missaoRequest.Status
        };
    }

    public MissaoResponse MissaoToResponse(Missao missao)
    {
        return new MissaoResponse(
            missao.IdMissao,
            missao.NomeMissao,
            missao.DtInicio,
            missao.DuracaoEstimada,
            missao.Descricao,
            missao.Status
        );
    }
}
