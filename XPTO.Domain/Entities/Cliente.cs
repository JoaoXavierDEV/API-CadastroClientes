using XPTO.Domain.Exceptions;

namespace XPTO.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual Endereco Endereco { get; set; }

        public Cliente()
        {

        }

        public Cliente(string nome, string email, string telefone, Endereco? endereco = null)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), nameof(name),
                "Invalid name.Name is required");

            DomainExceptionValidation.When(name.Length < 3, nameof(name),
               "Invalid name, too short, minimum 3 characters");

            Nome = name;
        }

    }
}
