using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceData.Models;

public enum StatusMissao
{
    Planejada,
    EmAndamento,
    Concluida,
    Cancelada,
    Suspensa
}

[Table("T_MISSAO")]
public class Missao
{
    [Key]
    public string IdMissao { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(150)]
    public string NomeMissao { get; set; } = string.Empty;

    [Required]
    public DateOnly DtInicio { get; set; }

    [Required]
    [Range(1, 3650)]
    public int DuracaoEstimada { get; set; }

    [Required, MinLength(50), MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public StatusMissao Status { get; set; }

    public ICollection<AgenteMissao> AgenteMissoes { get; set; } = [];
}