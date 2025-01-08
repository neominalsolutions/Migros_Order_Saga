


using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(cfg =>
{

  cfg.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));
    config.ReceiveEndpoint("order-saga-queue", configurator =>
    {
      
    });
  });
});





