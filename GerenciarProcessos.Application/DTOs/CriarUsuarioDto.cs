﻿using GerenciarProcessos.Domain.Enums;

namespace GerenciarProcessos.Application.DTOs.Usuario;

public class CriarUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; } 
}
