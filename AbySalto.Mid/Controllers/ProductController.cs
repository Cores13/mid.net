using AbySalto.Mid.Application.UserCommands.Products.Queries.GetAll;
using AbySalto.Mid.Application.UserCommands.Users.Queries.GetOne;
using AbySalto.Mid.Domain.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : BaseController
    {
        public ProductController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new GetAllProductsQuery();

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
    }
}
