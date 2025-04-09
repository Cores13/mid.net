using AbySalto.Mid.Application.UserCommands.Cart.Commands.AddToCart;
using AbySalto.Mid.Application.UserCommands.Cart.Commands.RemoveFromCart;
using AbySalto.Mid.Application.UserCommands.Cart.Queries.GetCart;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasketController : BaseController
    {
        public BasketController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart(CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = GetCurrentUserId();
                var command = new GetCartQuery(userId);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartProductRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = GetCurrentUserId();
                var command = new AddToCartCommand(request.ProductId, request.Quantity, userId);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(CartProductRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = GetCurrentUserId();
                var command = new RemoveFromCartCommand(request.ProductId, request.Quantity, userId);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }
        
    }
}
