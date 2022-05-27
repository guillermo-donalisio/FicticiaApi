using System.Linq.Expressions;
using Api_Bitsion.DataAccess;
using Api_Bitsion.Entities;
using Api_Bitsion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Bitsion.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected DbContext context;

    public GenericRepository(DbContext context)
    {
        this.context = context;
    }

    // HARD DELETE
    public async Task Delete(int id)
    {
        var entity = await GetById(id);

        if(entity == null)
            throw new Exception("The entity is null");

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return context.Set<TEntity>().Where(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        entity.UpdatedAt = DateTime.Now;

        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    // SOFT DELETE
    public async Task<bool> SoftDelete(TEntity entity, int? id)
    {
        try
        {
            var value = await GetById(id!.Value); 
            if(value == null)
                throw new Exception("The entity is null"); 

            var Ent = entity.IsActive = false; 
            await context.SaveChangesAsync();             

            return Ent;                 
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.UpdatedAt = DateTime.Now;

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}
