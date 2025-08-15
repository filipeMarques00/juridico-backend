using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciarProcessos.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var usuarioDto = await _autenticacaoService.LoginAsync(login);

            if (usuarioDto == null)
                return Unauthorized(new { mensagem = "Credenciais inválidas" });

            return Ok(usuarioDto); // Agora retorna JSON válido
        }


    }
}
