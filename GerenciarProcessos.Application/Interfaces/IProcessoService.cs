using GerenciarProcessos.Application.Dtos;

namespace GerenciarProcessos.Application.Interfaces
{
    public interface IProcessoService
    {
        Task<IEnumerable<ProcessoDto>> ObterTodosAsync();
        Task<ProcessoDto?> ObterPorIdAsync(int id);
        Task<ProcessoDto> CriarAsync(CriarProcessoDto dto);
        Task AtualizarAsync(int id, CriarProcessoDto dto);
        Task RemoverAsync(int id);
    }
}
