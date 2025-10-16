using GerenciarProcessos.Domain.Entities;

namespace GerenciarProcessos.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodosPorUsuarioAsync(int usuarioId);
    Task<Cliente?> ObterPorIdEUsuarioAsync(int id, int usuarioId);
    Task AdicionarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);
    Task RemoverAsync(int id);
}
