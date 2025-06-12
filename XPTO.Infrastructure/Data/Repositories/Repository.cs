using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Infrastructure.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : EntityBase, new()
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<T> DbSet;

    protected Repository(ApplicationDbContext db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }
    public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    // public IQueryable<TAbela> Consultar<TAbela>() where TAbela : EntityBase
    public IQueryable<TAbela> Consultar<TAbela>() where TAbela : class
    {
        return Db.Set<TAbela>();
    }

    public virtual async Task Adicionar(T entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public T ObterPorId(Guid id)
    {
        return DbSet.Find(id) ?? Activator.CreateInstance<T>();
    }

    public async Task<List<T>> ObterTodos()
    {
        return await DbSet.ToListAsync();
    }

    public async Task Atualizar(T entity)
    {
        try
        {
            //DbSet.Attach(entity);

            DbSet.Update(entity);

            await SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public virtual async Task Remover(Guid id)
    {
        DbSet.Remove(new T { Id = id });
        await SaveChanges();
    }


    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }
    public void Dispose()
    {
        Db?.Dispose();
    }

    public virtual IQueryable<T> Consultar()
    {
        return Consultar<T>();
    }
}