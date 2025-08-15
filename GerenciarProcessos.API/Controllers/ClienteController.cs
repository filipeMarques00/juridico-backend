using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciarProcessos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET: api/cliente
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> ObterTodos()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes);
    }

    // GET: api/cliente/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // POST: api/cliente
    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Criar([FromBody] CriarClienteDto dto)
    {
        var clienteCriado = await _clienteService.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = clienteCriado.Id }, clienteCriado);
    }

    // PUT: api/cliente/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarClienteDto dto)
    {
        try
        {
            await _clienteService.AtualizarAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    // DELETE: api/cliente/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _clienteService.RemoverAsync(id);
        return NoContent();
    }
}
