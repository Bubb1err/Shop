using CatalogAPI.Data.Repositories.Intefaces;
using Grpc.Core;

namespace CatalogAPI.Grpc
{
  public class CatalogService : Catalog.CatalogBase
  {
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
      _catalogRepository = catalogRepository;
    }

    public override Task<GetItemsResponse> GetItems(GetItemsRequest request, ServerCallContext context)
    {
      var items = _catalogRepository.GetItems();

      var response = new GetItemsResponse();

      response.Items.AddRange(items.Select(item => new Item 
      {
        Id = item.Id.ToString(),
        Title = item.Title,
        Descritpion = item.Description,
        Price = (double) item.Price,
        ImageUrl = item.ImageUrl 
      }));

      return Task.FromResult(response);
    }
    public override async Task<GetItemByIdResponse> GetItemById(GetItemByIdRequest request, ServerCallContext context)
    {
      var item = await _catalogRepository.GetItemByIdAsync(new Guid(request.Id));

      if (item == null)
      {
        throw new RpcException(new Status(StatusCode.NotFound, $"Item with id {request.Id} was not found."));
      }

      var response = new GetItemByIdResponse();

      response.Item = new Item
      {
        Id = item.Id.ToString(),
        Title = item.Title,
        Descritpion = item.Description,
        Price = (double)item.Price,
        ImageUrl = item.ImageUrl
      };

      return response;
    }
  }
}
