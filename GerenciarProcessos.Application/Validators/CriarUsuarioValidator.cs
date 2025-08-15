using FluentValidation;
using GerenciarProcessos.Application.DTOs.Usuario;

namespace GerenciarProcessos.Application.Validators.Usuario;

public class CriarUsuarioValidator : AbstractValidator<CriarUsuarioDto>
{
    public CriarUsuarioValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido.");
        RuleFor(x => x.Senha).MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres.");
        RuleFor(x => x.Perfil).IsInEnum().WithMessage("Perfil inválido.");
    }
}
