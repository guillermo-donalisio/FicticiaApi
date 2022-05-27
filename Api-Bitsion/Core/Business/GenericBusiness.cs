using System.Linq.Expressions;
using Api_Bitsion.Core.Interfaces;
using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Repositories.Interfaces;

namespace Api_Bitsion.Core.Business;

public class GenericBusiness<TEntity> : IGenericBusiness<TEntity> where TEntity : class
{
    private IGenericRepository<TEntity> _genericRepository;
    private IUnitOfWork _uow;

    public GenericBusiness(IGenericRepository<TEntity> genericRepository, IUnitOfWork uow)
    {
        this._genericRepository = genericRepository;
        this._uow = uow;
    }

    public async Task Delete(int id)
    {
        await _genericRepository.Delete(id);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _genericRepository.Find(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _genericRepository.GetAll();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await _genericRepository.GetById(id);
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        return await _genericRepository.Insert(entity);
    }

    public async Task<bool> SoftDelete(TEntity entity, int? id)
    {
        return await _genericRepository.SoftDelete(entity, id);
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        return await _genericRepository.Update(entity);
    }
}
