using GerenciarProcessos.Domain.Entities;

namespace GerenciarProcessos.Domain.Interfaces;

public interface IProcessoRepository
{
    Task<IEnumerable<Processo>> ObterTodosAsync();
    Task<Processo?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Processo processo);
    Task AtualizarAsync(Processo processo);
    Task RemoverAsync(int id);
}
