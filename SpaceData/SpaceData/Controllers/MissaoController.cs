using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

[ApiController]
[Route("api/missao")]
[Produces("application/json")]
[Tags("Missão")]
public class MissaoController(MissaoService service) : ControllerBase
{
    /// <summary>Cadastrar missão</summary>
    /// <remarks>Cria uma nova missão espacial no sistema</remarks>
    [HttpPost]
    [ProducesResponseType(typeof(MissaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] MissaoRequest req)
    {
        var result = await service.CriarAsync(req);
        return CreatedAtAction(nameof(BuscarPorId), new { id = result.IdMissao }, result);
    }

    /// <summary>Buscar missão por ID</summary>
    /// <remarks>Retorna os dados de uma missão a partir do seu ID</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MissaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(string id)
    {
        try { return Ok(await service.ObterPorIdAsync(id)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Listar todas as missões</summary>
    /// <remarks>Retorna uma lista com todas as missões cadastradas no sistema</remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MissaoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar() => Ok(await service.ObterTodasAsync());

    /// <summary>Atualizar missão</summary>
    /// <remarks>Atualiza os dados de uma missão existente</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MissaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(string id, [FromBody] MissaoRequest req)
    {
        try { return Ok(await service.AtualizarAsync(id, req)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }

    /// <summary>Deletar missão</summary>
    /// <remarks>Remove o cadastro de uma missão do sistema</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(string id)
    {
        try { await service.DeletarAsync(id); return NoContent(); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
    }
}

