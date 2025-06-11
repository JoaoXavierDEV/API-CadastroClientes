namespace XPTO.Domain.Entities
{
    public class Endereco : EntityBase
    {
        public virtual string Rua { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Estado { get; set; }
        public virtual string Cep { get; set; }
        public Endereco(string rua, string numero, string cidade, string estado, string cep)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }

        public Endereco()
        {

        }
    }
}
