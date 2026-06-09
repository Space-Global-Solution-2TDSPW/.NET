using Microsoft.EntityFrameworkCore;
using SpaceData.Models;

namespace SpaceData.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Agente> Agentes => Set<Agente>();
    public DbSet<Missao> Missoes => Set<Missao>();
    public DbSet<AgenteMissao> AgenteMissoes => Set<AgenteMissao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agente>(entity =>
        {
            entity.HasKey(a => a.IdAgente);
            entity.Property(a => a.Nome).IsRequired().HasMaxLength(150);
            entity.Property(a => a.Especialidade).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Status).HasConversion<string>();
        });

        modelBuilder.Entity<Missao>(entity =>
        {
            entity.HasKey(m => m.IdMissao);
            entity.Property(m => m.NomeMissao).IsRequired().HasMaxLength(150);
            entity.Property(m => m.Descricao).HasMaxLength(500);
            entity.Property(m => m.Status).HasConversion<string>();
        });

        modelBuilder.Entity<AgenteMissao>(entity =>
        {
            entity.HasKey(am => am.IdAgenteMissao);

            entity.HasOne(am => am.Agente)
                  .WithMany(a => a.AgenteMissoes)
                  .HasForeignKey(am => am.IdAgente);

            entity.HasOne(am => am.Missao)
                  .WithMany(m => m.AgenteMissoes)
                  .HasForeignKey(am => am.IdMissao);
        });
    }
}
