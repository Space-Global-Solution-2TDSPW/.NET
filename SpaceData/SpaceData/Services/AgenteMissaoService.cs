using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;

namespace SpaceData.Services;

public class AgenteMissaoService
{
    private readonly AppDbContext _context; 
    private readonly AgenteMissaoMapper _agenteMissaoMapper;

    public AgenteMissaoService(AppDbContext context, AgenteMissaoMapper agenteMissaoMapper)
    {
        _context = context;
        _agenteMissaoMapper = agenteMissaoMapper;
    }

    public AgenteMissaoResponse CriarAgenteMissao(AgenteMissaoRequest request)
    {
        _ = _context.Agentes.Find(request.IdAgente)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {request.IdAgente}");

        _ = _context.Missoes.Find(request.IdMissao)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {request.IdMissao}");

        var agenteMissao = _agenteMissaoMapper.AgenteMissaoRequestToEntity(request);
        agenteMissao.IdAgenteMissao = Guid.NewGuid().ToString();

        _context.AgenteMissoes.Add(agenteMissao);
        _context.SaveChanges();

        var saved = _context.AgenteMissoes
            .Include(am => am.Agente)
            .Include(am => am.Missao)
            .First(am => am.IdAgenteMissao == agenteMissao.IdAgenteMissao);

        return _agenteMissaoMapper.AgenteMissaoToResponse(saved);
    }

    public AgenteMissaoResponse ObterAgenteMissaoPorId(string id)
    {
        var agenteMissao = _context.AgenteMissoes
            .Include(am => am.Agente)
            .Include(am => am.Missao)
            .FirstOrDefault(am => am.IdAgenteMissao == id)
            ?? throw new KeyNotFoundException($"Vínculo AgenteMissao não encontrado com ID: {id}");

        return _agenteMissaoMapper.AgenteMissaoToResponse(agenteMissao);
    }

    public List<AgenteMissaoResponse> ObterTodosAgenteMissoes()
    {
        return _context.AgenteMissoes
            .Include(am => am.Agente)
            .Include(am => am.Missao)
            .AsNoTracking() 
            .Select(am => _agenteMissaoMapper.AgenteMissaoToResponse(am))
            .ToList();
    }

    public AgenteMissaoResponse AtualizarAgenteMissao(string id, AgenteMissaoRequest request)
    {
        var agenteMissao = _context.AgenteMissoes
            .Include(am => am.Agente)
            .Include(am => am.Missao)
            .FirstOrDefault(am => am.IdAgenteMissao == id)
            ?? throw new KeyNotFoundException($"Vínculo AgenteMissao não encontrado com ID: {id}");

        _ = _context.Agentes.Find(request.IdAgente)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {request.IdAgente}");

        _ = _context.Missoes.Find(request.IdMissao)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {request.IdMissao}");

        agenteMissao.IdAgente = request.IdAgente;
        agenteMissao.IdMissao = request.IdMissao;
        agenteMissao.RelatorioMissao = request.RelatorioMissao;

        _context.SaveChanges();

        // Reload navigation properties
        _context.Entry(agenteMissao).Reference(am => am.Agente).Load();
        _context.Entry(agenteMissao).Reference(am => am.Missao).Load();

        return _agenteMissaoMapper.AgenteMissaoToResponse(agenteMissao);
    }

    public void DeletarAgenteMissao(string id)
    {
        var agenteMissao = _context.AgenteMissoes.Find(id)
            ?? throw new KeyNotFoundException($"Vínculo AgenteMissao não encontrado com ID: {id}");

        _context.AgenteMissoes.Remove(agenteMissao);
        _context.SaveChanges();
    }
}
