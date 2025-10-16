// GerenciarProcessos.Application/Services/ClienteService.cs
using AutoMapper;
using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;
using Microsoft.AspNetCore.Http; // ✅ Adicionar using
using System.Security.Claims;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly int _usuarioId; // ✅ Propriedade para guardar o ID do usuário logado

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;

        var usuarioIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out _usuarioId))
        {
            throw new Exception("Não foi possível identificar o usuário logado.");
        }
    }



    public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
    {
        // Filtra clientes apenas para o usuário logado
        var clientes = await _clienteRepository.ObterTodosPorUsuarioAsync(_usuarioId);
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        // Busca o cliente pelo ID E pelo ID do usuário
        var cliente = await _clienteRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> CriarAsync(CriarClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        cliente.UsuarioId = _usuarioId; // ✅ Associa o novo cliente ao usuário logado
        await _clienteRepository.AdicionarAsync(cliente);
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task AtualizarAsync(int id, CriarClienteDto dto)
    {
        // Busca o cliente para garantir que ele pertence ao usuário antes de atualizar
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
        clienteExistente.DataNascimento = dto.DataNascimento;
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