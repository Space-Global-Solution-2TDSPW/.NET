using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SpaceData.Models;

[Table("T_AGENTE_MISSAO")]
public class AgenteMissao
{
    [Key]
    [Column("ID_AGENTE_MISSAO")]
    public string IdAgenteMissao { get; set; }

    [ForeignKey(nameof(Agente))]
    [Column("ID_AGENTE")]
    public string IdAgente { get; set; }

    [ForeignKey(nameof(Missao))]
    [Column("ID_MISSAO")]
    public string IdMissao { get; set; }

    [Required, MaxLength(500)]
    [Column("DESCRICAO")]
    public string RelatorioMissao { get; set; }

    public Agente Agente { get; set; } = null!;
    public Missao Missao { get; set; } = null!;
}
