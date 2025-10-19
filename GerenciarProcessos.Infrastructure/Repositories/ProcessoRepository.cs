// GerenciarProcessos.Infrastructure/Repositories/ProcessoRepository.cs

using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;
using GerenciarProcessos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciarProcessos.Infrastructure.Repositories
{
    public class ProcessoRepository : IProcessoRepository
    {
        private readonly AppDbContext _context;

        public ProcessoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Processo>> ObterTodosPorUsuarioAsync(int usuarioId)
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId) 
                .ToListAsync();
        }

        public async Task<Processo?> ObterPorIdEUsuarioAsync(int id, int usuarioId)
        {
            return await _context.Processos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.UsuarioId == usuarioId); 
        }

        public async Task AdicionarAsync(Processo processo)
        {
            await _context.Processos.AddAsync(processo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Processo processo)
        {
            _context.Processos.Update(processo);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var processo = await _context.Processos.FindAsync(id);
            if (processo != null)
            {
                _context.Processos.Remove(processo);
                await _context.SaveChangesAsync();
            }
        }
    }
}