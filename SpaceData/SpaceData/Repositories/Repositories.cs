using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.Models;

namespace SpaceData.Repositories;

// ── Interfaces ────────────────────────────────────────────────────────────────

public interface IAgenteRepository
{
    Task<Agente?> GetByIdAsync(string id);
    Task<IEnumerable<Agente>> GetAllAsync();
    Task<Agente> AddAsync(Agente agente);
    Task<Agente> UpdateAsync(Agente agente);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}

public interface IMissaoRepository
{
    Task<Missao?> GetByIdAsync(string id);
    Task<IEnumerable<Missao>> GetAllAsync();
    Task<Missao> AddAsync(Missao missao);
    Task<Missao> UpdateAsync(Missao missao);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}

public interface IAgenteMissaoRepository
{
    Task<AgenteMissao?> GetByIdAsync(string id);
    Task<IEnumerable<AgenteMissao>> GetAllAsync();
    Task<AgenteMissao> AddAsync(AgenteMissao agenteMissao);
    Task<AgenteMissao> UpdateAsync(AgenteMissao agenteMissao);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}

// ── Implementações ────────────────────────────────────────────────────────────

public class AgenteRepository(AppDbContext ctx) : IAgenteRepository
{
    public async Task<Agente?> GetByIdAsync(string id) =>
        await ctx.Agentes.FirstOrDefaultAsync(a => a.IdAgente == id);

    public async Task<IEnumerable<Agente>> GetAllAsync() =>
        await ctx.Agentes.AsNoTracking().ToListAsync();

    public async Task<Agente> AddAsync(Agente a) { ctx.Agentes.Add(a); await ctx.SaveChangesAsync(); return a; }

    public async Task<Agente> UpdateAsync(Agente a) { ctx.Agentes.Update(a); await ctx.SaveChangesAsync(); return a; }

    public async Task<bool> DeleteAsync(string id)
    {
        var a = await ctx.Agentes.FindAsync(id);
        if (a is null) return false;
        ctx.Agentes.Remove(a);
        await ctx.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(string id) =>
        ctx.Agentes.AnyAsync(a => a.IdAgente == id);
}

public class MissaoRepository(AppDbContext ctx) : IMissaoRepository
{
    public async Task<Missao?> GetByIdAsync(string id) =>
        await ctx.Missoes.FirstOrDefaultAsync(m => m.IdMissao == id);

    public async Task<IEnumerable<Missao>> GetAllAsync() =>
        await ctx.Missoes.AsNoTracking().ToListAsync();

    public async Task<Missao> AddAsync(Missao m)
    {
        ctx.Missoes.Add(m);
        await ctx.SaveChangesAsync();
        return m;
    }

    public async Task<Missao> UpdateAsync(Missao m)
    {
        ctx.Missoes.Update(m);
        await ctx.SaveChangesAsync();
        return m;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var m = await ctx.Missoes.FindAsync(id);
        if (m is null) return false;
        ctx.Missoes.Remove(m);
        await ctx.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(string id) =>
        ctx.Missoes.AnyAsync(m => m.IdMissao == id);
}

public class AgenteMissaoRepository(AppDbContext ctx) : IAgenteMissaoRepository
{
    private IQueryable<AgenteMissao> WithIncludes() =>
        ctx.AgenteMissoes
            .Include(am => am.Agente)
            .Include(am => am.Missao);

    public async Task<AgenteMissao?> GetByIdAsync(string id) =>
        await WithIncludes().FirstOrDefaultAsync(am => am.IdAgenteMissao == id);

    public async Task<IEnumerable<AgenteMissao>> GetAllAsync() =>
        await WithIncludes().AsNoTracking().ToListAsync();

    public async Task<AgenteMissao> AddAsync(AgenteMissao am)
    {
        ctx.AgenteMissoes.Add(am);
        await ctx.SaveChangesAsync();
        return am;
    }

    public async Task<AgenteMissao> UpdateAsync(AgenteMissao am)
    {
        ctx.AgenteMissoes.Update(am);
        await ctx.SaveChangesAsync();
        return am;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var am = await ctx.AgenteMissoes.FindAsync(id);
        if (am is null) return false;
        ctx.AgenteMissoes.Remove(am);
        await ctx.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(string id) =>
        ctx.AgenteMissoes.AnyAsync(am => am.IdAgenteMissao == id);
}