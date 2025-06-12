using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Infrastructure.Data.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Endereco> ObterTodosEnderecos()
        {
            return DbSet.ToList();
        }

        public Endereco GetById(Guid id)
        {
            return DbSet.Find(id) ?? new Endereco();
        }

        public async void Add(Endereco endereco)
        {
            DbSet.Add(endereco);
            await SaveChanges();
        }

        public async void Update(Endereco endereco)
        {
            DbSet.Update(endereco);
            await SaveChanges();
        }

        public async void Remove(Guid id)
        {
            var endereco = GetById(id);

            if (endereco != null)
            {
                DbSet.Remove(endereco);
                await SaveChanges();
            }
        }

        public async void Remove(Endereco endereco)
        {
            DbSet.Remove(endereco);

            await SaveChanges();
        }
    }

}
