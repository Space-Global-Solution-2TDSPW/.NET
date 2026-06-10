using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Models;

namespace SpaceData.Mappers
{
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
}
