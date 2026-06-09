using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

[ApiController]
[Route("api/agente")]
public class AgenteController : ControllerBase
{
    private readonly AgenteService _agenteService;

    public AgenteController(AgenteService agenteService)
    {
        _agenteService = agenteService;
    }

    /// <summary>Cadastrar agente</summary>
    /// <remarks>Cria um novo cadastro de agente espacial no sistema</remarks>
    [HttpPost]
    public IActionResult CriarAgente([FromBody] AgenteRequest agenteRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = _agenteService.CriarAgente(agenteRequest);
        return CreatedAtAction(nameof(BuscarAgentePorId), new { id = response.IdAgente }, response);
    }

    /// <summary>Buscar agente por ID</summary>
    /// <remarks>Retorna os dados de um agente a partir do seu ID</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpGet("{id}")]
    public IActionResult BuscarAgentePorId([FromRoute] string id)
    {
        try
        {
            var response = _agenteService.ObterAgentePorId(id);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Listar todos os agentes</summary>
    /// <remarks>Retorna uma lista com todos os agentes cadastrados no sistema</remarks>
    [HttpGet]
    public IActionResult ListarAgentes()
    {
        var response = _agenteService.ObterTodosAgentes();
        return Ok(response);
    }

    /// <summary>Atualizar agente</summary>
    /// <remarks>Atualiza os dados de um agente existente</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpPut("{id}")]
    public IActionResult AtualizarAgente([FromRoute] string id, [FromBody] AgenteRequest agenteRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = _agenteService.AtualizarAgente(id, agenteRequest);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Deletar agente</summary>
    /// <remarks>Remove o cadastro de um agente do sistema</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpDelete("{id}")]
    public IActionResult DeletarAgente([FromRoute] string id)
    {
        try
        {
            _agenteService.DeletarAgente(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
