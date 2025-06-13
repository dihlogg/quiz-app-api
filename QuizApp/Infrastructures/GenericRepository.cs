using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuizApp.DataContext;
using QuizApp.Entities;

namespace QuizApp.Infrastructures;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntities
{
    protected readonly QuizDbContext _quizDbContext;

    public GenericRepository(QuizDbContext quizDbContext)
    {
        _quizDbContext = quizDbContext;
    }

    public T? GetById(Guid id)
    {
        return _quizDbContext.Set<T>().Find(id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _quizDbContext.Set<T>().FindAsync(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _quizDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _quizDbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> expression)
    {
        return _quizDbContext.Set<T>().Where(expression);
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
    {
        var resultData = await _quizDbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        return resultData;
    }

    public T? GetObject(Expression<Func<T, bool>> expression)
    {
        return _quizDbContext.Set<T>().Find(expression);
    }

    public async Task<T?> GetObjectAsync(Expression<Func<T, bool>> expression)
    {
        return await _quizDbContext.Set<T>().FindAsync(expression);
    }

    private bool Insert(T pObj)
    {
        try
        {
            _quizDbContext.Entry(pObj).State = EntityState.Added;
            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<T> AddReturnModelAsync(T entity)
    {
        await using var transaction = await _quizDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return entity;
            }
            await _quizDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return entity;
        }
        catch
        {
            await _quizDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool Add(T entity)
    {
        using var transaction = _quizDbContext.Database.BeginTransaction();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return false;
            }
            _quizDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _quizDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool> AddAsync(T entity)
    {
        await using var transaction = await _quizDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return false;
            }
            await _quizDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _quizDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool AddMany(IEnumerable<T> entity)
    {
        using var transaction = _quizDbContext.Database.BeginTransaction();
        try
        {
            _quizDbContext.Set<T>().AddRange(entity);
            _quizDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _quizDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool> AddManyAsync(IEnumerable<T> entity)
    {
        await using var transaction = await _quizDbContext.Database.BeginTransactionAsync();
        try
        {
            await _quizDbContext.Set<T>().AddRangeAsync(entity);
            await _quizDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _quizDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public T? GetObject(params object[] pKeys)
    {
        return _quizDbContext.Set<T>().Find(pKeys);
    }

    public async Task<T?> GetObjectAsync(params object[] pKeys)
    {
        return await _quizDbContext.Set<T>().FindAsync(pKeys);
    }

    private bool UpdateWithObject(T pObj, params string[] pNotUpdatedProperties)
    {
        try
        {
            var keyNames = GetPrimaryKey();
            var keyValues = keyNames.Select(p => pObj.GetType().GetProperty(p)?.GetValue(pObj, null)).ToArray();
            if (keyValues != null)
            {
                T exist = GetObject(keyValues!)!;
                if (exist != null)
                {
                    _quizDbContext.Entry(exist).State = EntityState.Detached;
                    _quizDbContext.Attach(pObj);
                    var entry = _quizDbContext.Entry(pObj);
                    entry.State = EntityState.Modified;

                    if (pNotUpdatedProperties.Any())
                    {
                        foreach (string prop in pNotUpdatedProperties)
                        {
                            entry.Property(prop).IsModified = false;
                        }
                    }

                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }

    public bool Update(T pObj)
    {
        return UpdateWithTransaction(pObj, "CreateDate", "CreateBy");
    }

    public async Task<bool> UpdateAsync(T pObj)
    {
        return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
    }

    public async Task<bool> UpdateStatusAsync(T pObj)
    {
        return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
    }

    private bool UpdateWithTransaction(T pObj, params string[] pNotUpdatedProperties)
    {
        using var transaction = _quizDbContext.Database.BeginTransaction();
        try
        {
            bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
            if (isOk)
            {
                _quizDbContext.SaveChanges();
                transaction.Commit();
            }

            return isOk;
        }
        catch (Exception ex)
        {
            _quizDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    private async Task<bool> UpdateWithTransactionAsync(T pObj, params string[] pNotUpdatedProperties)
    {
        await using var transaction = await _quizDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
            if (isOk)
            {
                await _quizDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }

            return isOk;
        }
        catch
        {
            await _quizDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool UpdateMany(IEnumerable<T> entity)
    {
        using var transaction = _quizDbContext.Database.BeginTransaction();
        try
        {
            _quizDbContext.Set<T>().UpdateRange(entity);
            _quizDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _quizDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool?> DeleteByKey(Guid pKey)
    {
        await using var transaction = _quizDbContext.Database.BeginTransaction();
        try
        {
            var obj = GetById(pKey);
            if (obj == null)
            {
                return null;
            }
            _ = _quizDbContext.Remove(obj);
            await _quizDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _quizDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    private string[] GetPrimaryKey()
    {
        return _quizDbContext.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name)?.ToArray() ?? [];
    }
}
