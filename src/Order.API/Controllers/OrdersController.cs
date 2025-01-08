using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Requests;

namespace Order.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IMediator mediator;

    public OrdersController(IMediator mediator)
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
