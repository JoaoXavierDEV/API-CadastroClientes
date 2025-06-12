using FluentValidation;
using XPTO.Domain.Entities;

namespace XPTO.Domain.Validation
{
    internal sealed class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.Rua).NotEmpty()
                .WithMessage("Logradouro é obrigatório.");

            RuleFor(x => x.Numero).NotEmpty()
                .WithMessage("Número é obrigatório.");

            RuleFor(x => x.Cep).NotEmpty().WithMessage("CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato XXXXX-XXX.");

            RuleFor(x => x.Cidade).NotEmpty()
                .WithMessage("Cidade é obrigatória.");

            RuleFor(x => x.Estado).NotEmpty()
                .WithMessage("Estado é obrigatório.")
                .Length(2).WithMessage("Estado deve ter 2 caracteres.");
        }
    }
}
