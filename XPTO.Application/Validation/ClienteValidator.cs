using FluentValidation;
using XPTO.Domain.Entities;

namespace XPTO.Application.Validation
{
    internal sealed class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser um endereço de email válido.")
                .Length(1, 50).WithMessage("O email deve ter entre 1 e 50 caracteres.");

            RuleFor(c => c.Telefone)
                .MaximumLength(15).WithMessage("O telefone deve ter no máximo 15 caracteres.");

        }
    }
}
