using MassTransit;
using Order.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// asembly load ederek o assembly i�indeki t�m handlerlar Net Core IoC y�klendi.
builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssemblyContaining<Program>();
});


builder.Services.AddMassTransit(opt =>
{
  opt.AddConsumer<OrderProcedeedConsumer>();

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));

    // Not: Event bekledi�imiz i�in queue yazmad�k. event yerine send ile g�nderilseydi kuyruk tan�m� yapmal�yd�k.
    config.ReceiveEndpoint(x => x.ConfigureConsumer<OrderProcedeedConsumer>(context));
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
