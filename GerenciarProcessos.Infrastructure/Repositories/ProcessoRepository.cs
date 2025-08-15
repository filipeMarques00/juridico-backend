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

        public async Task<IEnumerable<Processo>> ObterTodosAsync()
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .ToListAsync();
        }

        public async Task<Processo?> ObterPorIdAsync(int id)
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);
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
            var processo = await ObterPorIdAsync(id);
            if (processo != null)
            {
                _context.Processos.Remove(processo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
