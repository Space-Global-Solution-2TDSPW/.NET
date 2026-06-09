using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

// ── AgenteMissao ──────────────────────────────────────────────────────────────

[ApiController]
[Route("api/agentemissao")]
[Produces("application/json")]
[Tags("AgenteMissão")]
public class AgenteMissaoController(AgenteMissaoService service) : ControllerBase
{
    /// <summary>Vincular agente a uma missão</summary>
    /// <remarks>Cria um vínculo entre um agente e uma missão. Ambos devem estar previamente cadastrados</remarks>
    [HttpPost]
    [ProducesResponseType(typeof(AgenteMissaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Criar([FromBody] AgenteMissaoRequest req)
    {
        try
        {
            var result = await service.CriarAsync(req);
            return CreatedAtAction(nameof(BuscarPorId), new { id = result.IdAgenteMissao }, result);
        }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Buscar vínculo por ID</summary>
    /// <remarks>Retorna os dados de um vínculo AgenteMissão a partir do seu ID</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AgenteMissaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(string id)
    {
        try { return Ok(await service.ObterPorIdAsync(id)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Listar todos os vínculos</summary>
    /// <remarks>Retorna uma lista com todos os vínculos AgenteMissão cadastrados no sistema</remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AgenteMissaoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar() => Ok(await service.ObterTodosAsync());

    /// <summary>Atualizar vínculo</summary>
    /// <remarks>Atualiza os dados de um vínculo AgenteMissão existente</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AgenteMissaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(string id, [FromBody] AgenteMissaoRequest req)
    {
        try { return Ok(await service.AtualizarAsync(id, req)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Remover vínculo</summary>
    /// <remarks>Remove um vínculo AgenteMissão do sistema</remarks>
    /// <param name="id">ID UUID do vínculo</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(string id)
    {
        try { await service.DeletarAsync(id); return NoContent(); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }
}

