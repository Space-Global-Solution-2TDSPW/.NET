using System.ComponentModel.DataAnnotations;

namespace SpaceData.DTOs.Request;

public class AgenteMissaoRequest
{
    [Required(ErrorMessage = "ID do agente é obrigatório")]
    public string IdAgente { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID da missão é obrigatório")]
    public string IdMissao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Relatório da missão é obrigatório")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Relatório deve ter entre 1 e 500 caracteres")]
    public string RelatorioMissao { get; set; } = string.Empty;
}