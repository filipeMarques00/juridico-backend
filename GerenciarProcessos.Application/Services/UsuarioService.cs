using GerenciarProcessos.Application.DTOs.Usuario;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;

namespace GerenciarProcessos.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto> CriarUsuario(CriarUsuarioDto dto)
    {
        var existe = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (existe != null)
            throw new Exception("Já existe um usuário com esse e-mail.");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Perfil = dto.Perfil
        };

        await _usuarioRepository.AdicionarAsync(usuario);

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil.ToString()
        };
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodosUsuarios()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();

        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Perfil = u.Perfil.ToString()
        });
    }
}
