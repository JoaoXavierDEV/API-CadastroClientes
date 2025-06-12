using FluentValidation;
using XPTO.Domain.Entities;

namespace XPTO.Domain.Validation
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
                .Length(1, 15).WithMessage("O Telefone deve ter entre 1 e 15 caracteres.")
                .MaximumLength(15).WithMessage("O telefone deve ter no máximo 15 caracteres.");

        }
    }
}
