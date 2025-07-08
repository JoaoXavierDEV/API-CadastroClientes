using System.Data.Entity;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Infrastructure.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return DbSet
                .AsNoTracking()
                .Include(x => x.Endereco).ToList();
        }

        public override Cliente ObterPorId(Guid id)
        {
            return DbSet.Find(id);
            //return DbSet.Find(id) ?? new Cliente();
        }

        //public async void Adicionar(Cliente cliente)
        //{
        //    DbSet.Add(cliente);
        //    await SaveChanges();
        //}
        public new async Task Atualizar(Cliente cliente)
        {
            DbSet.Update(cliente);
            await SaveChanges();
        }
        public override async Task Remover(Guid id)
        {
            var cliente = ObterPorId(id);

            if (cliente != null)
            {
                DbSet.Remove(cliente);
                await SaveChanges();
            }
        }

        public override IQueryable<TAbela> Consultar<TAbela>() where TAbela : class
        {
            var tt = Db.Set<TAbela>();

            var tytt = DbSet;

            return tt;
        }


    }

}
