using BasketAPI.Data.Repositories.Interfaces;
using BasketAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Data.Repositories.Implementations
{
  public class BasketRepository : IBasketRepository
  {
    private readonly BasketDbContext _basketContext;
    private readonly DbSet<BasketItem> _basketItems;

    public BasketRepository(BasketDbContext basketContext)
    {
      _basketContext = basketContext;
      _basketItems = _basketContext.Set<BasketItem>();
    }

    public async Task<IEnumerable<BasketItem>> GetUserBasketItemsAsync(string userId, CancellationToken cancellationToken = default)
    {
      return await _basketItems.Where(item => item.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<BasketItem?> TryGetBasketItemByIdAsync(Guid id)
    {
      return await _basketItems.FirstOrDefaultAsync(item => item.Id == id);
    }

    public void AddToUserBasketItem(BasketItem basketItem)
    {
      _basketItems.Add(basketItem);
    }

    public void UpdateUserBasketItem(BasketItem basketItem)
    {
      _basketItems.Update(basketItem);
    }

    public void DeleteUserBasketItem(BasketItem basketItem)
    {
      _basketItems.Remove(basketItem);
    }

    public Task ClearUserBasketItemsAsync(string userId, CancellationToken cancellationToken = default)
    {
      return _basketItems.Where(item => item.UserId == userId).ExecuteDeleteAsync(cancellationToken);
    }

    public Task SaveChangesAsync()
    {
      return _basketContext.SaveChangesAsync();
    }
  }
}
