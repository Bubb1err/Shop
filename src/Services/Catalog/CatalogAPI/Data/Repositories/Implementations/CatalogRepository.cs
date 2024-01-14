using CatalogAPI.Data.Repositories.Intefaces;
using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogAPI.Data.Repositories.Implementations;

public class CatalogRepository : ICatalogRepository
{
  private readonly CatalogDbContext _catalogDbContext;
  private readonly DbSet<CatalogItem> _catalogItems;

  public CatalogRepository(CatalogDbContext catalogDbContext)
  {
    _catalogDbContext = catalogDbContext;
    _catalogItems = _catalogDbContext.Set<CatalogItem>();
  }

  public IEnumerable<CatalogItem> GetItems(int page = 1, int pageSize = 10, bool noTracking = false)
  {
    return GetItems(noTracking).Skip((page - 1) * pageSize).Take(pageSize);
  }

  public IEnumerable<CatalogItem> GetItems(Expression<Func<CatalogItem, bool>> expression, int page = 1, int pageSize = 10, bool noTracking = false)
  {
    return GetItems(noTracking).Where(expression).Skip((page - 1) * pageSize).Take(pageSize);
  }

  public Task<CatalogItem?> GetItemByIdAsync(Guid id, bool noTracking = false, CancellationToken cancellationToken = default)
  {
    return GetItems(noTracking).FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
  }

  public Task<CatalogItem?> GetItemByExpressionAsync(Expression<Func<CatalogItem, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default)
  {
    return GetItems(noTracking).FirstOrDefaultAsync(expression, cancellationToken);
  }

  public void CreateItem(CatalogItem catalogItem)
  {
    _catalogItems.Entry(catalogItem).State = EntityState.Added;
  }

  public void UpdateItem(CatalogItem catalogItem)
  {
    _catalogItems.Entry(catalogItem).State = EntityState.Modified;
  }

  public void DeleteItem(CatalogItem catalogItem)
  {
    _catalogItems.Entry(catalogItem).State = EntityState.Deleted;
  }

  public Task SaveChangesAsync()
  {
    return _catalogDbContext.SaveChangesAsync();
  }

  private IQueryable<CatalogItem> GetItems(bool noTracking)
  {
    var items = _catalogDbContext.Items;

    if(noTracking)
    {
      return items.AsNoTracking();
    }

    return items;
  }
}
