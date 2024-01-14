namespace BasketAPI.Entities
{
  public class BasketItem
  {
    public Guid Id { get; private set; }
    public Guid CatalogItemId { get; private set; }
    public string ItemName { get; private set; } = default!;
    public decimal PricePerUnit { get; private set; }
    public string UserId { get; private set; } = default!;
    public string ImageUrl { get; private set; } = default!;

    private BasketItem() { }

    protected BasketItem(
      Guid id,
      Guid catalogItemId,
      string userId,
      string itemName,
      decimal pricePerUnit,
      string imageUrl)
    {
      Id = id;
      CatalogItemId = catalogItemId;
      UserId = userId;
      ItemName = itemName;
      PricePerUnit = pricePerUnit;
      ImageUrl = imageUrl;
    }

    public static BasketItem Create(
      Guid catalogItemId,
      string itemName,
      decimal pricePerUnit,
      string userId,
      string imageUrl)
    {
      return new BasketItem
      {
        Id = Guid.NewGuid(),
        CatalogItemId = catalogItemId,
        ItemName = itemName,
        PricePerUnit = pricePerUnit,
        UserId = userId,
        ImageUrl = imageUrl
      };
    }
  }
}
