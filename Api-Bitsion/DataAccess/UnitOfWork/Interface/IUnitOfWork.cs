using Api_Bitsion.Repositories.Interfaces;

namespace Api_Bitsion.DataAccess.UnitOfWork.Interface;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients{get;}
    Task SaveAsync();
}
