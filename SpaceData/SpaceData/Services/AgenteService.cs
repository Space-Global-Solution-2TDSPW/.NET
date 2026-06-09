using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;

namespace SpaceData.Services;

public class AgenteService
{
    private readonly AppDbContext _context;
    private readonly AgenteMapper _agenteMapper;

    public AgenteService(AppDbContext context, AgenteMapper agenteMapper)
    {
        _context = context;
        _agenteMapper = agenteMapper;
    }

    public AgenteResponse CriarAgente(AgenteRequest agenteRequest)
    {
        var agente = _agenteMapper.AgenteRequestToEntity(agenteRequest);
        agente.IdAgente = Guid.NewGuid().ToString();

        _context.Agentes.Add(agente);
        _context.SaveChanges();

        return _agenteMapper.AgenteToResponse(agente);
    }

    public AgenteResponse ObterAgentePorId(string id)
    {
        var agente = _context.Agentes.Find(id)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");

        return _agenteMapper.AgenteToResponse(agente);
    }

    public List<AgenteResponse> ObterTodosAgentes()
    {
        return _context.Agentes
            .AsNoTracking()
            .Select(a => _agenteMapper.AgenteToResponse(a))
            .ToList();
    }

    public AgenteResponse AtualizarAgente(string id, AgenteRequest agenteRequest)
    {
        var agente = _context.Agentes.Find(id)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");

        agente.Nome = agenteRequest.Nome;
        agente.DtNascimento = agenteRequest.DtNascimento;
        agente.Status = agenteRequest.Status;
        agente.Especialidade = agenteRequest.Especialidade;

        _context.SaveChanges();

        return _agenteMapper.AgenteToResponse(agente);
    }

    public void DeletarAgente(string id)
    {
        var agente = _context.Agentes.Find(id)
            ?? throw new KeyNotFoundException($"Agente não encontrado com ID: {id}");

        _context.Agentes.Remove(agente);
        _context.SaveChanges();
    }
}
