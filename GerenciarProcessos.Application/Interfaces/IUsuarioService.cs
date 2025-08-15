using GerenciarProcessos.Application.DTOs.Usuario;

namespace GerenciarProcessos.Application.Services;

public interface IUsuarioService
{
    Task<UsuarioDto> CriarUsuario(CriarUsuarioDto dto);
    Task<IEnumerable<UsuarioDto>> ObterTodosUsuarios();
}
