using GerenciarProcessos.Domain.Entities;

namespace GerenciarProcessos.Domain.Interfaces;

public interface IProcessoRepository
{
    Task<IEnumerable<Processo>> ObterTodosPorUsuarioAsync(int usuarioId);
    Task<Processo?> ObterPorIdEUsuarioAsync(int id, int usuarioId);
    Task AdicionarAsync(Processo processo);
    Task AtualizarAsync(Processo processo);
    Task RemoverAsync(int id);
}
