using XPTO.Domain.Validation;

namespace XPTO.Domain.Entities
{
    public class Endereco : EntityBase
    {
        public virtual string Rua { get; internal set; }
        public virtual string Numero { get; internal set; }
        public virtual string Cidade { get; internal set; }
        public virtual string Estado { get; internal set; }
        public virtual string Cep { get; internal set; }
        public Endereco(string rua, string numero, string cidade, string estado, string cep)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;

            EhValido();
        }

        public Endereco()
        {
            Rua = string.Empty;
            Numero = string.Empty;
            Cidade = string.Empty;
            Estado = string.Empty;
            Cep = string.Empty;
        }

        public Endereco(Endereco other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Id = other.Id;
            Rua = other.Rua;
            Numero = other.Numero;
            Cidade = other.Cidade;
            Estado = other.Estado;
            Cep = other.Cep;
        }

        public override bool EhValido()
        {
            ValidationResult = new EnderecoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
