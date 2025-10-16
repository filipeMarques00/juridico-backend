using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciarProcessos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly HttpClient _http;
    public ClienteController(IClienteService clienteService, IHttpClientFactory httpFactory)
    {
        _clienteService = clienteService;
        _http = httpFactory.CreateClient();
    }




    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes); // ✅ Simplificado: Retorna diretamente o DTO do serviço
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var clienteDto = await _clienteService.ObterPorIdAsync(id);
        if (clienteDto == null) return NotFound();
        return Ok(clienteDto); // ✅ Simplificado
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Post([FromBody] CriarClienteDto dto)
    {
        var clienteCriadoDto = await _clienteService.CriarAsync(dto);
        // ✅ Simplificado
        return CreatedAtAction(nameof(Get), new { id = clienteCriadoDto.Id }, clienteCriadoDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] CriarClienteDto dto)
    {
        try
        {
            // A verificação de existência e permissão agora é feita dentro do serviço
            await _clienteService.AtualizarAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message); // Retorna a mensagem de erro do serviço
        }
    }

    // 🔹 CEP: /api/Cliente/endereco/{cep}
    [HttpGet("endereco/{cep}")]
    [AllowAnonymous] // se preferir, mantenha Authorize
    public async Task<IActionResult> ObterEnderecoPorCep(string cep)
    {
        var resp = await _http.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        if (!resp.IsSuccessStatusCode) return NotFound("CEP não encontrado");

        var viacep = await resp.Content.ReadFromJsonAsync<ViaCepResponse>();
        if (viacep == null || viacep.Erro) return NotFound("CEP inválido");

        return Ok(new
        {
            cidade = viacep.Localidade,
            estado = viacep.Uf,
            pais = "Brasil"
        });
    }

    private class ViaCepResponse
    {
        public string Cep { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public bool Erro { get; set; } // quando CEP inválido, ViaCEP retorna {"erro": true}
    }
}
