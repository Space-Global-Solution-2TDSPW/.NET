using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;
using SpaceData.Repositories;

namespace SpaceData.Services;

public class AgenteService(IAgenteRepository repo)
{
    public async Task<AgenteResponse> CriarAsync(AgenteRequest req)
    {
        var entity = AgenteMapper.ToEntity(req);
        entity.IdAgente = Guid.NewGuid().ToString();

        var saved = await repo.AddAsync(entity);
        return AgenteMapper.ToResponse(saved);
    }

    public async Task<AgenteResponse> ObterPorIdAsync(string id)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");
        return AgenteMapper.ToResponse(entity);
    }

    public async Task<IEnumerable<AgenteResponse>> ObterTodosAsync() =>
        (await repo.GetAllAsync()).Select(AgenteMapper.ToResponse);

    public async Task<AgenteResponse> AtualizarAsync(string id, AgenteRequest req)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");

        entity.Nome = req.Nome;
        entity.DtNascimento = req.DtNascimento;
        entity.Status = req.Status;
        entity.Especialidade = req.Especialidade;

        var updated = await repo.UpdateAsync(entity);
        return AgenteMapper.ToResponse(updated);
    }

    public async Task DeletarAsync(string id)
    {
        if (!await repo.DeleteAsync(id))
            throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");
    }
}
