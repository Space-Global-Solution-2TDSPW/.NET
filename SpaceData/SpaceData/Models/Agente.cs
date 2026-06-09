using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceData.Models;

public enum StatusAgente
{
    Ativo,
    Inativo,
    EmMissao,
    Aposentado
}

[Table("T_AGENTE")]
public class Agente
{
    [Key]
    [Column("ID_AGENTE")]
    public string IdAgente { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(100)]
    [Column("NM_AGENTE")]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [Column("DtNascimento")]
    public DateOnly DtNascimento { get; set; }

    [Required]
    [Column("ST_AGENTE")]
    public StatusAgente Status { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    [Column("ESPECIALIDADE")]
    public string Especialidade { get; set; } = string.Empty;

    public ICollection<AgenteMissao> AgenteMissoes { get; set; } = new List<AgenteMissao>();
}
