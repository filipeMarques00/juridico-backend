// GerenciarProcessos.Infrastructure/Repositories/ClienteRepository.cs

using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;
using GerenciarProcessos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciarProcessos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    // ✅ NOVO MÉTODO: Retorna todos os clientes APENAS do usuário logado.
    public async Task<IEnumerable<Cliente>> ObterTodosPorUsuarioAsync(int usuarioId)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(c => c.UsuarioId == usuarioId) // A cláusula de segurança
            .ToListAsync();
    }

    // ✅ NOVO MÉTODO: Retorna um cliente específico APENAS se ele pertencer ao usuário logado.
    public async Task<Cliente?> ObterPorIdEUsuarioAsync(int id, int usuarioId)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId); // A cláusula de segurança
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        // A verificação de posse do cliente é feita no Service antes de chamar este método.
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente is null) return;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}