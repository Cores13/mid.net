using MediatR;
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
    }
}
