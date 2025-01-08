using MediatR;

namespace Order.API.Requests
{
  public record SubmitOrder(string OrderCode):IRequest;
 
}
