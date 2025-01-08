using MassTransit;
using Message.Contracts;

namespace Order.API.Consumers
{
  public class OrderProcedeedConsumer : IConsumer<IOrderProceeded>
  {
    public async Task Consume(ConsumeContext<IOrderProceeded> context)
    {
      // OrderRepo üzerinden var data = repo.FindById(context.OrderId);
      // data.status = Proceeded;
      // repo.save();
      await Console.Out.WriteLineAsync($"Order API CorId {context.CorrelationId}");
    }
  }
}
