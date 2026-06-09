using System.ComponentModel.DataAnnotations;

namespace SpaceData.DTOs.Request;

public class AgenteMissaoRequest
{
    [Required(ErrorMessage = "ID do agente é obrigatório")]
    public string IdAgente { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID da missão é obrigatório")]
    public string IdMissao { get; set; } = string.Empty;

    public string RelatorioMissao { get; set; }
}
