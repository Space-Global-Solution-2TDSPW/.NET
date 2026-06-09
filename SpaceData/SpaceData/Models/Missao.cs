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
    [Column("ID_MISSAO")]
    public string IdMissao { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    [Column("NM_MISSAO")]
    public string NomeMissao { get; set; }

    [Required]
    [Column("DT_INICIO")]
    public DateOnly DtInicio { get; set; }

    [Required]
    [Column("DURACAO_ESTIMADA")]
    public int DuracaoEstimada { get; set; } // em dias

    [Required, MinLength(50), MaxLength(300)]
    [Column("Descrição")]
    public string Descricao { get; set; }

    [Required]
    [Column("ST_MISSAO")]
    public StatusMissao Status { get; set; }

    public ICollection<AgenteMissao> AgenteMissoes { get; set; } = new List<AgenteMissao>();
}
