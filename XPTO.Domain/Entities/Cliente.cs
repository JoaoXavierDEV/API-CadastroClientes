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

        public Cliente(string nome, string email, string telefone, Endereco endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

    }
}
