
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Requests;

namespace Order.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly ISender mediator;
    // private readonly IMediator mediator1;
    public OrdersController(ISender mediator)
    {
      this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrder request)
    {
      await this.mediator.Send(request);

      return Ok();
    }
  }
}
