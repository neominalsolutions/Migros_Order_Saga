using MassTransit;
using Message.Contracts;

namespace Billing.API.Consumers
{
  public class OrderReceivedConsumer : IConsumer<IOrderRecieved>
  {
    public async Task Consume(ConsumeContext<IOrderRecieved> context)
    {
      await Console.Out.WriteLineAsync($"Billing API Recieved CorId: {context.Message.CorrelationId}");
    }
  }
}
