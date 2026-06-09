using System.ComponentModel.DataAnnotations;
using SpaceData.Models;

namespace SpaceData.DTOs.Request;

public class MissaoRequest
{
    [Required(ErrorMessage = "Nome da missão é obrigatório")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 150 caracteres")]
    public string NomeMissao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de início é obrigatória")]
    public DateOnly DtInicio { get; set; }

    [Required(ErrorMessage = "Duração estimada é obrigatória")]
    [Range(1, 3650, ErrorMessage = "Duração deve estar entre 1 e 3650 dias")]
    public int DuracaoEstimada { get; set; }

    [StringLength(500, MinimumLength = 50, ErrorMessage = "Descrição deve ter entre 50 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status é obrigatório")]
    public StatusMissao Status { get; set; }
}