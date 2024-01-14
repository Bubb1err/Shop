namespace CatalogAPI.Entities
{
  public class CatalogItem
  {
    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
    public string ImageUrl { get; private set; } = default!;

    protected CatalogItem(
        Guid id,
        string title,
        string description,
        decimal price,
        string imageUrl)
    {
      Id = id;
      Title = title;
      Description = description;
      Price = price;
      ImageUrl = imageUrl;
    }
    private CatalogItem() { }

    public static CatalogItem Create(
      string title,
      string description,
      decimal price,
      string imageUrl)
    {
      if (string.IsNullOrWhiteSpace(title))
      {
        // THROW ERROR.
      }

      if (string.IsNullOrWhiteSpace(description))
      {
        // THROW ERROR.
      }

      if (string.IsNullOrWhiteSpace(imageUrl))
      {
        // THROW ERROR.
      }

      return new CatalogItem
      {
        Id = Guid.NewGuid(),
        Title = title,
        Description = description,
        Price = price,
        ImageUrl = imageUrl
      };
    }

    public void Modify(
      string title,
      string description,
      decimal price,
      string imageUrl)
    {
      Title = title;
      Description = description;
      Price = price;
      ImageUrl = imageUrl;
    }
  }
}
