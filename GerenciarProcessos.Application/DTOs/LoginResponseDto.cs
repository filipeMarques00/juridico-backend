using GerenciarProcessos.Application.Dtos;

public class LoginResponseDto
{
    public string Token { get; set; }
    public UsuarioLoginDto User { get; set; }
}
