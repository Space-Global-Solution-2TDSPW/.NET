using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

[ApiController]
[Route("api/agente")]
[Produces("application/json")]
[Tags("Agente")]
public class AgenteController(AgenteService service) : ControllerBase
{
    /// <summary>Cadastrar agente</summary>
    /// <remarks>Cria um novo cadastro de agente espacial no sistema</remarks>
    [HttpPost]
    [ProducesResponseType(typeof(AgenteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] AgenteRequest req)
    {
        var result = await service.CriarAsync(req);
        return CreatedAtAction(nameof(BuscarPorId), new { id = result.IdAgente }, result);
    }

    /// <summary>Buscar agente por ID</summary>
    /// <remarks>Retorna os dados de um agente a partir do seu ID</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AgenteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(string id)
    {
        try { return Ok(await service.ObterPorIdAsync(id)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Listar todos os agentes</summary>
    /// <remarks>Retorna uma lista com todos os agentes cadastrados no sistema</remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AgenteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar() => Ok(await service.ObterTodosAsync());

    /// <summary>Atualizar agente</summary>
    /// <remarks>Atualiza os dados de um agente existente</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AgenteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(string id, [FromBody] AgenteRequest req)
    {
        try { return Ok(await service.AtualizarAsync(id, req)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Deletar agente</summary>
    /// <remarks>Remove o cadastro de um agente do sistema</remarks>
    /// <param name="id">ID UUID do agente</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(string id)
    {
        try { await service.DeletarAsync(id); return NoContent(); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }
}

