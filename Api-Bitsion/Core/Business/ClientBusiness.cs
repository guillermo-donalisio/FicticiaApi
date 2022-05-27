using System.Linq.Expressions;
using Api_Bitsion.Core.Interfaces;
using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Entities;
using Api_Bitsion.Repositories.Interfaces;

namespace Api_Bitsion.Core.Business;

public class ClientBusiness : GenericBusiness<Client>, IClientBusiness
{
    private IClientRepository _clientRepository;
    private IUnitOfWork _uow;
    public ClientBusiness(IClientRepository clientRepository, IUnitOfWork uow) : base(clientRepository, uow)
    {
        this._clientRepository = clientRepository;
        this._uow = uow;
    }

    // HARD DELETE
    public async Task DeleteClientAsync(int id)
    {
        await _uow.Clients.DeleteClientAsync(id);
        await _uow.SaveAsync();
    }

    // FIND
    public IEnumerable<Client> FindClientAsync(Expression<Func<Client, bool>> predicate)
    {
        return _uow.Clients.FindClientAsync(predicate);
    }

    // GET ALL
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _uow.Clients.GetAllClientsAsync();
    }

    // GET BY ID
    public async Task<Client> GetByIdClientdAsync(int id)
    {
        return await _uow.Clients.GetByIdClientdAsync(id);
    }

    // INSERT
    public async Task<Client> InsertClientAsync(Client entity)
    {
        var insert = await _uow.Clients.InsertClientAsync(entity);
        await _uow.SaveAsync();
        return insert;
    }

    // SOFT DELETE
    public async Task<bool> SoftDeleteClientAsync(Client entity, int? id)
    {
        var softDelete = await _uow.Clients.SoftDeleteClientAsync(entity, id);
        await _uow.SaveAsync();
        return softDelete;
    }

    // UPDATE
    public async Task<Client> UpdateClientAsync(Client entity)
    {
        var update = await _uow.Clients.UpdateClientAsync(entity);
        await _uow.SaveAsync();
        return update;
    }
}
