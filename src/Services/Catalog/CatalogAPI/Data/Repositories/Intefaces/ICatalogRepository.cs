using CatalogAPI.Entities;
using System.Linq.Expressions;

namespace CatalogAPI.Data.Repositories.Intefaces
{
  public interface ICatalogRepository
  {
    IEnumerable<CatalogItem> GetItems(int page = 1, int pageSize = 10, bool noTracking = false);
    IEnumerable<CatalogItem> GetItems(Expression<Func<CatalogItem, bool>> expression, int page = 1, int pageSize = 10, bool noTracking = false);

    Task<CatalogItem?> GetItemByIdAsync(Guid id, bool noTracking = false, CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetItemByExpressionAsync(Expression<Func<CatalogItem, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default);

    void CreateItem(CatalogItem catalogItem);
    void UpdateItem(CatalogItem catalogItem);
    void DeleteItem(CatalogItem catalogItem);

    Task SaveChangesAsync();
  }
}
