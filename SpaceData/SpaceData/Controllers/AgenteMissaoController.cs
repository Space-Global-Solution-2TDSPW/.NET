using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

[ApiController]
[Route("api/agentemissao")]
public class AgenteMissaoController : ControllerBase
{
    private readonly AgenteMissaoService _agenteMissaoService;

    public AgenteMissaoController(AgenteMissaoService agenteMissaoService)
    {
        _agenteMissaoService = agenteMissaoService;
    }

    /// <summary>Vincular agente a uma missão</summary>
    /// <remarks>Cria um vínculo entre um agente e uma missão. O agente e a missão devem estar previamente cadastrados</remarks>
    [HttpPost]
    public IActionResult CriarAgenteMissao([FromBody] AgenteMissaoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = _agenteMissaoService.CriarAgenteMissao(request);
            return CreatedAtAction(nameof(BuscarAgenteMissaoPorId), new { id = response.IdAgenteMissao }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Buscar vínculo por ID</summary>
    /// <remarks>Retorna os dados de um vínculo AgenteMissão a partir do seu ID</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpGet("{id}")]
    public IActionResult BuscarAgenteMissaoPorId([FromRoute] string id)
    {
        try
        {
            var response = _agenteMissaoService.ObterAgenteMissaoPorId(id);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Listar todos os vínculos</summary>
    /// <remarks>Retorna uma lista com todos os vínculos AgenteMissão cadastrados no sistema</remarks>
    [HttpGet]
    public IActionResult ListarAgenteMissoes()
    {
        var response = _agenteMissaoService.ObterTodosAgenteMissoes();
        return Ok(response);
    }

    /// <summary>Atualizar vínculo</summary>
    /// <remarks>Atualiza os dados de um vínculo AgenteMissão existente</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpPut("{id}")]
    public IActionResult AtualizarAgenteMissao([FromRoute] string id, [FromBody] AgenteMissaoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = _agenteMissaoService.AtualizarAgenteMissao(id, request);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Remover vínculo</summary>
    /// <remarks>Remove um vínculo AgenteMissão do sistema</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpDelete("{id}")]
    public IActionResult DeletarAgenteMissao([FromRoute] string id)
    {
        try
        {
            _agenteMissaoService.DeletarAgenteMissao(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
