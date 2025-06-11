using AutonomoApp.Data.Repository;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Infrastructure.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return DbSet.ToList();
        }
        public Cliente ObterPorId(Guid? id)
        {
            return DbSet.Find(id) ?? new Cliente();
        }
        public async void Add(Cliente cliente)
        {
            DbSet.Add(cliente);
            await SaveChanges();
        }
        public async void Update(Cliente cliente)
        {
            DbSet.Update(cliente);
            await SaveChanges();
        }
        public async void Remove(Guid? id)
        {
            var cliente = ObterPorId(id);

            if (cliente != null)
            {
                DbSet.Remove(cliente);
                await SaveChanges();
            }
        }
        public async void Remove(Cliente cliente)
        {
            DbSet.Remove(cliente);

            await SaveChanges();
        }
    }

}
