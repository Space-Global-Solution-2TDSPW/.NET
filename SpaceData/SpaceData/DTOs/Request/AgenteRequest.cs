using System.ComponentModel.DataAnnotations;
using SpaceData.Models;

namespace SpaceData.DTOs.Request;

public class AgenteRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 150 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de nascimento é obrigatória")]
    public DateOnly DtNascimento { get; set; }

    [Required(ErrorMessage = "Status é obrigatório")]
    public StatusAgente Status { get; set; }

    [Required(ErrorMessage = "Especialidade é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Especialidade deve ter entre 3 e 100 caracteres")]
    public string Especialidade { get; set; } = string.Empty;
}