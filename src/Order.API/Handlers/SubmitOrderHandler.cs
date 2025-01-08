using MassTransit;
using MediatR;
using Message.Contracts;
using Order.API.Requests;

namespace Order.API.Handlers
{
  public class SubmitOrderHandler : IRequestHandler<SubmitOrder>
  {
    private readonly ISendEndpointProvider sendEndpointProvider;

    public SubmitOrderHandler(ISendEndpointProvider sendEndpointProvider)
    {
      this.sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Handle(SubmitOrder request, CancellationToken cancellationToken)
    {
      // Veritabanına ilgili kayıt düştü
      await Console.Out.WriteLineAsync("Order Oluştu");
      Random rdm = new Random();
      int orderId = rdm.Next(0, 100);
      string orderCode = $"Code_{orderId}";

      var uri = new Uri("queue:order-saga-queue");
      var endpoint = await this.sendEndpointProvider.GetSendEndpoint(uri);


      await endpoint.Send<IOrderCommand>(new { CorrelationId = Guid.NewGuid(), OrderId = orderId, OrderCode = orderCode });


      await Task.CompletedTask;
    }
  }
}
