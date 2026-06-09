using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string IdAgente { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public DateOnly DtNascimento { get; set; }

    [Required]
    public StatusAgente Status { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Especialidade { get; set; } = string.Empty;

    public ICollection<AgenteMissao> AgenteMissoes { get; set; } = [];
}