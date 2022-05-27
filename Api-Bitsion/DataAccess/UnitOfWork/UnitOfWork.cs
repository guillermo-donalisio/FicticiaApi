using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Repositories;
using Api_Bitsion.Repositories.Interfaces;

namespace Api_Bitsion.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private FicticiaDbContext _context;
    public IClientRepository Clients {get; private set;}

    public UnitOfWork(FicticiaDbContext context)
    {
        this._context = context;
        Clients = new ClientRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
