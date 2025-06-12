using FluentValidation.Results;
using XPTO.Domain.Validation;

namespace XPTO.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual Endereco? Endereco { get; set; }

        public Cliente()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
            Endereco = null;
        }

        public Cliente(string nome, string email, string telefone, Endereco? endereco = null)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }


        public override bool EhValido()
        {
            ValidationResult = new ClienteValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
