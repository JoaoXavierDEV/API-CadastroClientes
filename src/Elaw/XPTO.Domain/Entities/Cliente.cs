using FluentValidation.Results;
using XPTO.Domain.Exceptions;
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

        public bool hasEndereco() => Endereco is not null;

        public void SetEndereco(Endereco endereco)
        {
            if (endereco is null)
                throw new ArgumentNullException(nameof(endereco), "Endereço não pode ser nulo.");

            if (!endereco.EhValido())
                throw new DomainExceptionValidation(endereco.ValidationResult.ToDictionary());

            if (Endereco is not null)
            {
                Endereco.Cidade = endereco.Cidade;
                Endereco.Estado = endereco.Estado;
                Endereco.Rua = endereco.Rua;
                Endereco.Numero = endereco.Numero;
                Endereco.Cep = endereco.Cep;
            }
            else
            {
                Endereco = endereco;
            }
        }

        public void RemoverEndereco() => this.Endereco = null;


    }

}
