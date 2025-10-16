using AutoMapper;
using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly int _usuarioId;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;

        var usuarioIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out _usuarioId))
        {
            throw new Exception("Não foi possível identificar o usuário logado.");
        }
    }

    public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
    {
        var clientes = await _clienteRepository.ObterTodosPorUsuarioAsync(_usuarioId);
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> CriarAsync(CriarClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        cliente.UsuarioId = _usuarioId; 

        cliente.DataNascimento = DateTime.SpecifyKind(cliente.DataNascimento, DateTimeKind.Utc);

        await _clienteRepository.AdicionarAsync(cliente);
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task AtualizarAsync(int id, CriarClienteDto dto)
    {
        var clienteExistente = await _clienteRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);

        if (clienteExistente == null)
            throw new Exception("Cliente não encontrado ou você não tem permissão para editá-lo.");

        clienteExistente.Nome = dto.Nome;
        clienteExistente.CPF = dto.CPF;
        clienteExistente.Email = dto.Email;
        clienteExistente.Telefone = dto.Telefone;
        clienteExistente.TipoPessoa = dto.TipoPessoa;
        clienteExistente.Sexo = dto.Sexo;
        clienteExistente.Nacionalidade = dto.Nacionalidade;
        clienteExistente.CEP = dto.CEP;
        clienteExistente.Cidade = dto.Cidade;
        clienteExistente.Estado = dto.Estado;

        clienteExistente.DataNascimento = DateTime.SpecifyKind(dto.DataNascimento, DateTimeKind.Utc);

        clienteExistente.Logradouro = dto.Logradouro;
        clienteExistente.Numero = dto.Numero;
        clienteExistente.Bairro = dto.Bairro;
        clienteExistente.Pais = dto.Pais;

        await _clienteRepository.AtualizarAsync(clienteExistente);
    }

    public async Task RemoverAsync(int id)
    {
        var clienteExistente = await _clienteRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
        if (clienteExistente == null)
            throw new Exception("Cliente não encontrado ou você não tem permissão para removê-lo.");

        await _clienteRepository.RemoverAsync(id);
    }
}