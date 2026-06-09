using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;
using SpaceData.Repositories;

namespace SpaceData.Services;

public class AgenteMissaoService(
    IAgenteMissaoRepository repo,
    IAgenteRepository agenteRepo,
    IMissaoRepository missaoRepo)
{
    public async Task<AgenteMissaoResponse> CriarAsync(AgenteMissaoRequest req)
    {
        if (!await agenteRepo.ExistsAsync(req.IdAgente))
            throw new KeyNotFoundException($"Agente não encontrado com ID: {req.IdAgente}");

        if (!await missaoRepo.ExistsAsync(req.IdMissao))
            throw new KeyNotFoundException($"Missão não encontrada com ID: {req.IdMissao}");

        var entity = AgenteMissaoMapper.ToEntity(req);
        entity.IdAgenteMissao = Guid.NewGuid().ToString();

        await repo.AddAsync(entity);

        var full = await repo.GetByIdAsync(entity.IdAgenteMissao);
        return AgenteMissaoMapper.ToResponse(full!);
    }

    public async Task<AgenteMissaoResponse> ObterPorIdAsync(string id)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Vínculo AgenteMissão não encontrado com ID: {id}");
        return AgenteMissaoMapper.ToResponse(entity);
    }

    public async Task<IEnumerable<AgenteMissaoResponse>> ObterTodosAsync() =>
        (await repo.GetAllAsync()).Select(AgenteMissaoMapper.ToResponse);

    public async Task<AgenteMissaoResponse> AtualizarAsync(string id, AgenteMissaoRequest req)
    {
        var entity = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Vínculo AgenteMissão não encontrado com ID: {id}");

        if (!await agenteRepo.ExistsAsync(req.IdAgente))
            throw new KeyNotFoundException($"Agente não encontrado com ID: {req.IdAgente}");

        if (!await missaoRepo.ExistsAsync(req.IdMissao))
            throw new KeyNotFoundException($"Missão não encontrada com ID: {req.IdMissao}");

        entity.IdAgente = req.IdAgente;
        entity.IdMissao = req.IdMissao;
        entity.RelatorioMissao = req.RelatorioMissao;

        await repo.UpdateAsync(entity);

        var full = await repo.GetByIdAsync(entity.IdAgenteMissao);
        return AgenteMissaoMapper.ToResponse(full!);
    }

    public async Task DeletarAsync(string id)
    {
        if (!await repo.DeleteAsync(id))
            throw new KeyNotFoundException($"Vínculo AgenteMissão não encontrado com ID: {id}");
    }
}

