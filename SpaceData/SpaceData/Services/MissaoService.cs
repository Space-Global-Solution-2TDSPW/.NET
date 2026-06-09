using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Mappers;

namespace SpaceData.Services;

public class MissaoService
{
    private readonly AppDbContext _context;
    private readonly MissaoMapper _missaoMapper;

    public MissaoService(AppDbContext context, MissaoMapper missaoMapper)
    {
        _context = context;
        _missaoMapper = missaoMapper;
    }

    public MissaoResponse CriarMissao(MissaoRequest missaoRequest)
    {
        var missao = _missaoMapper.MissaoRequestToEntity(missaoRequest);
        missao.IdMissao = Guid.NewGuid().ToString();

        _context.Missoes.Add(missao);
        _context.SaveChanges();

        return _missaoMapper.MissaoToResponse(missao);
    }

    public MissaoResponse ObterMissaoPorId(string id)
    {
        var missao = _context.Missoes.Find(id)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");

        return _missaoMapper.MissaoToResponse(missao);
    }

    public List<MissaoResponse> ObterTodasMissoes()
    {
        return _context.Missoes
            .AsNoTracking()
            .Select(m => _missaoMapper.MissaoToResponse(m))
            .ToList();
    }

    public MissaoResponse AtualizarMissao(string id, MissaoRequest missaoRequest)
    {
        var missao = _context.Missoes.Find(id)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");

        missao.NomeMissao = missaoRequest.NomeMissao;
        missao.DtInicio = missaoRequest.DtInicio;
        missao.DuracaoEstimada = missaoRequest.DuracaoEstimada;
        missao.Descricao = missaoRequest.Descricao;
        missao.Status = missaoRequest.Status;

        _context.SaveChanges();

        return _missaoMapper.MissaoToResponse(missao);
    }

    public void DeletarMissao(string id)
    {
        var missao = _context.Missoes.Find(id)
            ?? throw new KeyNotFoundException($"Missão não encontrada com ID: {id}");

        _context.Missoes.Remove(missao);
        _context.SaveChanges();
    }
}
