using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Entities;
using CatalogAPI.Data.Repositories.Intefaces;

namespace CatalogAPI.Controllers
{
  public sealed record ItemDto(string Title, string Description, decimal Price, string ImageUrl);
  public sealed record UpdateItemDto(Guid Id, string Title, string Description, decimal Price, string ImageUrl);

  [Route("api/[controller]")]
  [ApiController]
  public class CatalogController : ControllerBase
  {
    private const int MIN_PAGE = 1;
    private const int MIN_PAGE_SIZE = 5;

    private readonly ICatalogRepository _catalogRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(
      ICatalogRepository catalogRepository,
      ILogger<CatalogController> logger)
    {
      _catalogRepository = catalogRepository;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult GetItems(
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 10)
    {
      if(page < MIN_PAGE)
      {
        throw new ArgumentException($"Page cannot be less than {MIN_PAGE}.");
      }

      if(pageSize < MIN_PAGE_SIZE)
      {
        throw new ArgumentException($"Page size cannot be less than {MIN_PAGE_SIZE}");
      }

      var items = _catalogRepository.GetItems(page, pageSize);

      if(!items.Any())
      {
        return NotFound($"On page {page} items not found.");
      }

      return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItemById([FromRoute] Guid id)
    {
      var item = await _catalogRepository.GetItemByIdAsync(id);
      
      if(item is null)
      {
        return NotFound($"Item with id {id} not found.");
      }
      
      return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] ItemDto itemDto)
    {
      // RAISE EVENT 'CATALOG ITEM CREATED TO OTHER SERVICES'
      try
      {
        var catalogItem = CatalogItem.Create(
          title: itemDto.Title,
          description: itemDto.Description,
          price: itemDto.Price,
          imageUrl: itemDto.ImageUrl);

        _catalogRepository.CreateItem(catalogItem);
        await _catalogRepository.SaveChangesAsync();

        return Ok(catalogItem);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception, exception.Message);

        return BadRequest();
      }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateItem(UpdateItemDto updateItemDto)
    {
      try
      {
        var catalogItem = await _catalogRepository.GetItemByIdAsync(updateItemDto.Id);

        if(catalogItem is null)
        {
          return NotFound($"Item with id {updateItemDto.Id} not found.");
        }

        catalogItem.Modify(
          updateItemDto.Title,
          updateItemDto.Description,
          updateItemDto.Price,
          updateItemDto.ImageUrl);

        _catalogRepository.UpdateItem(catalogItem);
        await _catalogRepository.SaveChangesAsync();

        return Ok(catalogItem);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception, exception.Message);

        return BadRequest();
      }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
      try
      {
        var item = await _catalogRepository.GetItemByIdAsync(id);

        if(item is null)
        {
          return NotFound($"Item with id {id} not found.");
        }

        _catalogRepository.DeleteItem(item);
        await _catalogRepository.SaveChangesAsync();

        return Ok();
      }
      catch(Exception exception)
      {
        _logger.LogError(exception, exception.Message);

        return BadRequest();
      }
    }
  }
}
