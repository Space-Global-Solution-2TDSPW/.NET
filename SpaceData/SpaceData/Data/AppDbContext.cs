using Microsoft.EntityFrameworkCore;
using SpaceData.Models;

namespace SpaceData.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Agente> Agentes => Set<Agente>();
    public DbSet<Missao> Missoes => Set<Missao>();
    public DbSet<AgenteMissao> AgenteMissoes => Set<AgenteMissao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agente>(entity =>
        {
            entity.HasKey(a => a.IdAgente);
            entity.Property(a => a.IdAgente).HasColumnName("ID_AGENTE");
            entity.Property(a => a.Nome).IsRequired().HasMaxLength(150).HasColumnName("NM_AGENTE");
            entity.Property(a => a.DtNascimento).HasColumnName("DT_NASCIMENTO");
            entity.Property(a => a.Status).HasConversion<string>().HasColumnName("ST_AGENTE");
            entity.Property(a => a.Especialidade).IsRequired().HasMaxLength(100).HasColumnName("ESPECIALIDADE");
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
                  .HasForeignKey(am => am.IdAgente)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(am => am.Missao)
                  .WithMany(m => m.AgenteMissoes)
                  .HasForeignKey(am => am.IdMissao)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}