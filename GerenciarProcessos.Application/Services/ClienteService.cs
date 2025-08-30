using AutoMapper;
using GerenciarProcessos.Application.DTOs;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;

namespace GerenciarProcessos.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
    {
        var clientes = await _clienteRepository.ObterTodosAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> CriarAsync(CriarClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        await _clienteRepository.AdicionarAsync(cliente);
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task AtualizarAsync(int id, CriarClienteDto dto)
    {
        var clienteExistente = await _clienteRepository.ObterPorIdAsync(id);

        if (clienteExistente == null)
            throw new Exception("Cliente não encontrado");

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
        await _clienteRepository.RemoverAsync(id);
    }
}
