using GerenciarProcessos.Domain.Entities;

namespace GerenciarProcessos.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterTodosAsync();
    }
}
