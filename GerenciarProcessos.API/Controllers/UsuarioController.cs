using GerenciarProcessos.Application.DTOs.Usuario;
using GerenciarProcessos.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciarProcessos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        try
        {
            var usuario = await _usuarioService.CriarUsuario(dto);
            return CreatedAtAction(nameof(ObterUsuarios), new { id = usuario.Id }, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObterUsuarios()
    {
        var usuarios = await _usuarioService.ObterTodosUsuarios();
        return Ok(usuarios);
    }
}
