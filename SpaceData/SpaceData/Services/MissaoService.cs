using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;
using SpaceData.Repositories;

namespace SpaceData.Services;

public class MissaoService(IMissaoRepository repo)
{
    public async Task<MissaoResponse> CriarAsync(MissaoRequest req)
    {
        var entity = MissaoMapper.ToEntity(req);
        entity.IdMissao = Guid.NewGuid().ToString();

        var saved = await repo.AddAsync(entity);
        return MissaoMapper.ToResponse(saved);
    }

    public async Task<MissaoResponse> ObterPorIdAsync(string id)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");
        return MissaoMapper.ToResponse(entity);
    }

    public async Task<IEnumerable<MissaoResponse>> ObterTodasAsync() =>
        (await repo.GetAllAsync()).Select(MissaoMapper.ToResponse);

    public async Task<MissaoResponse> AtualizarAsync(string id, MissaoRequest req)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");

        entity.NomeMissao = req.NomeMissao;
        entity.DtInicio = req.DtInicio;
        entity.DuracaoEstimada = req.DuracaoEstimada;
        entity.Descricao = req.Descricao;
        entity.Status = req.Status;

        var updated = await repo.UpdateAsync(entity);
        return MissaoMapper.ToResponse(updated);
    }

    public async Task DeletarAsync(string id)
    {
        if (!await repo.DeleteAsync(id))
            throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");
    }
}

