using GerenciarProcessos.Application.Dtos;
using GerenciarProcessos.Application.Models;

namespace GerenciarProcessos.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<UsuarioLoginDto?> LoginAsync(LoginModel login);
    }
}
