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

        var result = clientes.Select(c => new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            CPF = c.CPF,
            Email = c.Email,
            Telefone = c.Telefone,
            TipoPessoa = c.TipoPessoa,
            DataNascimento = c.DataNascimento,
            Sexo = c.Sexo,
            Nacionalidade = c.Nacionalidade,
            CEP = c.CEP,
            Logradouro = c.Logradouro,
            Numero = c.Numero,
            Bairro = c.Bairro,
            Cidade = c.Cidade,
            Estado = c.Estado,
            Pais = c.Pais
        });

        return Ok(result);
    }

    // GET: /api/Cliente/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var c = await _clienteService.ObterPorIdAsync(id);
        if (c == null) return NotFound();

        return Ok(new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            CPF = c.CPF,
            Email = c.Email,
            Telefone = c.Telefone,
            TipoPessoa = c.TipoPessoa,
            DataNascimento = c.DataNascimento,
            Sexo = c.Sexo,
            Nacionalidade = c.Nacionalidade,
            CEP = c.CEP,
            Logradouro = c.Logradouro,
            Numero = c.Numero,
            Bairro = c.Bairro,
            Cidade = c.Cidade,
            Estado = c.Estado,
            Pais = c.Pais
        });
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Post([FromBody] CriarClienteDto dto)
    {
        var clienteCriado = await _clienteService.CriarAsync(dto);

        var resp = new ClienteDto
        {
            Id = clienteCriado.Id,
            Nome = clienteCriado.Nome,
            CPF = clienteCriado.CPF,
            Email = clienteCriado.Email,
            Telefone = clienteCriado.Telefone,
            TipoPessoa = clienteCriado.TipoPessoa,
            DataNascimento = clienteCriado.DataNascimento,
            Sexo = clienteCriado.Sexo,
            Nacionalidade = clienteCriado.Nacionalidade,
            CEP = clienteCriado.CEP,
            Logradouro = clienteCriado.Logradouro,
            Numero = clienteCriado.Numero,
            Bairro = clienteCriado.Bairro,
            Cidade = clienteCriado.Cidade,
            Estado = clienteCriado.Estado,
            Pais = clienteCriado.Pais
        };

        return CreatedAtAction(nameof(Get), new { id = resp.Id }, resp);
    }


    // PUT: /api/Cliente/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] CriarClienteDto dto)
    {
        var c = await _clienteService.ObterPorIdAsync(id);
        if (c == null) return NotFound();

        c.Nome = dto.Nome;
        c.CPF = dto.CPF;
        c.Email = dto.Email;
        c.Telefone = dto.Telefone;
        c.TipoPessoa = dto.TipoPessoa;
        c.DataNascimento = dto.DataNascimento;
        c.Sexo = dto.Sexo;
        c.Nacionalidade = dto.Nacionalidade;
        c.CEP = dto.CEP;
        c.Logradouro = dto.Logradouro;
        c.Numero = dto.Numero;
        c.Bairro = dto.Bairro;
        c.Cidade = dto.Cidade;
        c.Estado = dto.Estado;
        c.Pais = dto.Pais;

        await _clienteService.AtualizarAsync(id, dto);
        return NoContent();
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
