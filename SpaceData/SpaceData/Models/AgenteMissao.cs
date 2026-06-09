using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceData.Models;

[Table("T_AGENTE_MISSAO")]
public class AgenteMissao
{
    [Key]
    public string IdAgenteMissao { get; set; } = string.Empty;

    [Required]
    public string IdAgente { get; set; } = string.Empty;

    [ForeignKey(nameof(IdAgente))]
    public Agente Agente { get; set; } = null!;

    [Required]
    public string IdMissao { get; set; } = string.Empty;

    [ForeignKey(nameof(IdMissao))]
    public Missao Missao { get; set; } = null!;

    [Required, MaxLength(500)]
    public string RelatorioMissao { get; set; } = string.Empty;
}