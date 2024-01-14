using BasketAPI.Entities;

namespace BasketAPI.Data.Repositories.Interfaces
{
  public interface IBasketRepository
  {
    Task<IEnumerable<BasketItem>> GetUserBasketItemsAsync(string userId, CancellationToken cancellationToken = default);

    Task<BasketItem?> TryGetBasketItemByIdAsync(Guid id);

    void AddToUserBasketItem(BasketItem basketItem);
    void UpdateUserBasketItem(BasketItem basketItem);
    void DeleteUserBasketItem(BasketItem basketItem);
    Task ClearUserBasketItemsAsync(string userId, CancellationToken cancellationToken = default);

    Task SaveChangesAsync();
  }

}
