using GerenciarProcessos.Application.DTOs;

namespace GerenciarProcessos.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ObterTodosAsync();
    Task<ClienteDto?> ObterPorIdAsync(int id);
    Task<ClienteDto> CriarAsync(CriarClienteDto dto);
    Task AtualizarAsync(int id, CriarClienteDto dto);
    Task RemoverAsync(int id);
}
