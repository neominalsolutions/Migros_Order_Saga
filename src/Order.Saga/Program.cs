


using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Order.Saga;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(cfg =>
{
  cfg.AddSagaStateMachine<OrderSagaStateMachine, OrderSagaState>().InMemoryRepository();

  cfg.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));
    // Not: Süreç bir command ile başlayacağından minimum 1 tane saga queue olması lazım.
    config.ReceiveEndpoint("order-saga-queue", configurator =>
    {
      configurator.ConfigureSaga<OrderSagaState>(context);
    });
  });
});


builder.Build().Run();



