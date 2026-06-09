using Microsoft.AspNetCore.Mvc;
using SpaceData.DTOs.Request;
using SpaceData.DTOs.Response;
using SpaceData.Services;

namespace SpaceData.Controllers;

[ApiController]
[Route("api/missao")]
public class MissaoController : ControllerBase
{
    private readonly MissaoService _missaoService;

    public MissaoController(MissaoService missaoService)
    {
        _missaoService = missaoService;
    }

    /// <summary>Cadastrar missão</summary>
    /// <remarks>Cria uma nova missão espacial no sistema</remarks>
    [HttpPost]
    public IActionResult CriarMissao([FromBody] MissaoRequest missaoRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = _missaoService.CriarMissao(missaoRequest);
        return CreatedAtAction(nameof(BuscarMissaoPorId), new { id = response.IdMissao }, response);
    }

    /// <summary>Buscar missão por ID</summary>
    /// <remarks>Retorna os dados de uma missão a partir do seu ID</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpGet("{id}")]
    public IActionResult BuscarMissaoPorId([FromRoute] string id)
    {
        try
        {
            var response = _missaoService.ObterMissaoPorId(id);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Listar todas as missões</summary>
    /// <remarks>Retorna uma lista com todas as missões cadastradas no sistema</remarks>
    [HttpGet]
    public IActionResult ListarMissoes()
    {
        var response = _missaoService.ObterTodasMissoes();
        return Ok(response);
    }

    /// <summary>Atualizar missão</summary>
    /// <remarks>Atualiza os dados de uma missão existente</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpPut("{id}")]
    public IActionResult AtualizarMissao([FromRoute] string id, [FromBody] MissaoRequest missaoRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = _missaoService.AtualizarMissao(id, missaoRequest);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    /// <summary>Deletar missão</summary>
    /// <remarks>Remove o cadastro de uma missão do sistema</remarks>
    /// <param name="id">ID UUID da missão</param>
    [HttpDelete("{id}")]
    public IActionResult DeletarMissao([FromRoute] string id)
    {
        try
        {
            _missaoService.DeletarMissao(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
