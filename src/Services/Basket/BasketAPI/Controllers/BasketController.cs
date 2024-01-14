using BasketAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using BasketAPI.Data.Repositories.Interfaces;

namespace Basket;

[ApiController]
[Route("api/[controller]")]
public sealed class BasketController : ControllerBase
{
  private readonly IBasketRepository _basketRepository;

  public BasketController(IBasketRepository basketRepository)
  {
    _basketRepository = basketRepository;
  }

  [HttpGet("{userId}")]
  public IActionResult GetUserBasketItems([FromRoute] string userId, CancellationToken cancellationToken = default)
  {
    var basketItems = _basketRepository.GetUserBasketItemsAsync(userId, cancellationToken);

    return Ok(basketItems);
  }

  [HttpPost]
  public async Task<IActionResult> AddToBasketItem([FromBody] BasketItem basketItem)
  {
    _basketRepository.AddToUserBasketItem(basketItem);
    await _basketRepository.SaveChangesAsync();

    return Ok(basketItem);
  }

  [HttpPut]
  public async Task<IActionResult> UpdateBasketItem([FromBody] BasketItem basketItem)
  {
    _basketRepository.UpdateUserBasketItem(basketItem);
    await _basketRepository.SaveChangesAsync();

    return Ok(basketItem);
  }

  [HttpDelete("{userId}/clear")]
  public async Task<IActionResult> ClearUserBasket([FromRoute] string userId, CancellationToken cancellationToken = default)
  {
    await _basketRepository.ClearUserBasketItemsAsync(userId, cancellationToken);

    return Ok();
  }

  [HttpDelete("{basketItemId:guid}")]
  public async Task<IActionResult> DeleteBasketItem([FromRoute] Guid basketItemId)
  {
    var basketItem = await _basketRepository.TryGetBasketItemByIdAsync(basketItemId);

    if(basketItem is null)
    {
      return NotFound($"Basket item with id {basketItemId} not found.");
    }

    _basketRepository.DeleteUserBasketItem(basketItem);
    await _basketRepository.SaveChangesAsync();

    return Ok();
  }
}