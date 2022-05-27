using System.Linq.Expressions;
using Api_Bitsion.DataAccess;
using Api_Bitsion.Entities;
using Api_Bitsion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Bitsion.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(FicticiaDbContext context) : base(context) {}
    public FicticiaDbContext FicticiaDbContext
    {
        get{return context as FicticiaDbContext;}
    }

    // HARD DELETE
    public async Task DeleteClientAsync(int id)
    {
        var entity = await GetByIdClientdAsync(id);

        if(entity == null)
            throw new Exception("The entity is null");

        context.Set<Client>().Remove(entity);
    }

    // FIND
    public IEnumerable<Client> FindClientAsync(Expression<Func<Client, bool>> predicate)
    {
        return context.Set<Client>().Where(predicate);
    }

    // GET ALL
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await context.Set<Client>().ToListAsync();
    }

    // GET BY ID
    public async Task<Client> GetByIdClientdAsync(int id)
    {
        return await context.Set<Client>().FindAsync(id);
    }

    // INSERT
    public async Task<Client> InsertClientAsync(Client entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        entity.IsActive = true;
        
        await context.Set<Client>().AddAsync(entity);
        return entity;
    }

    // SOFT DELETE
    public async Task<bool> SoftDeleteClientAsync(Client entity, int? id)
    {
        try
        {
            var value = await GetByIdClientdAsync(id!.Value); 
            if(value == null)
                throw new Exception("The entity is null"); 

            return entity.IsActive = false;                
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // UPDATE
    public async Task<Client> UpdateClientAsync(Client entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.UpdatedAt = DateTime.Now;
        context.Set<Client>().Update(entity);

        return entity;
    }
}
