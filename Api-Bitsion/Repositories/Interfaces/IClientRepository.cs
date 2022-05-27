using System.Linq.Expressions;
using Api_Bitsion.Entities;

namespace Api_Bitsion.Repositories.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    IEnumerable<Client> FindClientAsync(Expression<Func<Client, bool>> predicate);
    Task<Client> GetByIdClientdAsync(int id);
    Task<Client> InsertClientAsync(Client entity);
    Task<Client> UpdateClientAsync(Client entity);  

    // Soft Delete
    Task<bool> SoftDeleteClientAsync(Client entity, int? id);
    
    // Hard Delete
    Task DeleteClientAsync(int id);
}
