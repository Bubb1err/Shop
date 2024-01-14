using CatalogAPI;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
  public class CatalogController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly string _catalogHost;

    public CatalogController(IConfiguration configuration)
        {
      _configuration = configuration;
      _catalogHost = _configuration["CatalogHost"];
    }
 
    public async Task<IActionResult> GetCatalogItems()
    {
      using var channel = GrpcChannel.ForAddress(_catalogHost);
      var client = new Catalog.CatalogClient(channel);
      
      var result = await client.GetItemsAsync(new GetItemsRequest());

      IEnumerable<Item> items = result.Items;

      return View(items);
    }

    public async Task<IActionResult> GetCatalogItemById(string id)
    {
      using var channel = GrpcChannel.ForAddress(_catalogHost);
      var client = new Catalog.CatalogClient(channel);

      var result = await client.GetItemByIdAsync(new GetItemByIdRequest() { Id = id.ToString() });

      return View(result.Item);
    }

  }
}
