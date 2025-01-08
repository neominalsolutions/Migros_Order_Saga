using MassTransit;
using Message.Contracts;
using Order.Saga.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga
{
  public class OrderSagaStateMachine : MassTransitStateMachine<OrderSagaState>
  {
    public State Proceeded { get; set; } // ilk State'den sonraki ilk state burada order işlenmiş olması lazım 
    public State Recieved { get; set; }

    // Transition yani State geçişi; bu geçiş için bizim bir event işletmemiz lazım
    // eventi saga üzerinden yönetip daha sonra o eventin işlendiği an transition ile state değiştiricem.

    // saga servis içerisindeki listenerlar
    public Event<IOrderCommand> OrderCommand { get; set; }
    public Event<IOrderProceeded> OrderProceeded { get; set; }

    public Event<IOrderRecieved> OrderRecieved { get; set; }


    public OrderSagaStateMachine()
    {
      InstanceState(i => i.CurrentState); // Instance State başlattığımız kısım

      // İlk başlangıç CorrelationId değerini saga içerisinde oluşturma işlemi

      /* Event(() => OrderSubmitted,
      orderStateInstance =>
      orderStateInstance.CorrelateBy<int>(database => database.OrderId, @event => @event.Message.OrderId)
      .SelectId(e => Guid.NewGuid()));

      */

      Event(() => OrderCommand, c=> c.CorrelateById(state => state.Message.CorrelationId));

      Event(() => OrderRecieved, c => c.CorrelateById(state => state.Message.CorrelationId));

      Event(() => OrderProceeded, c => c.CorrelateById(state => state.Message.CorrelationId));

      // Yeni pakette, context.Instance yerine context.Saga geldi, context.Data => context.Message;

      Initially(
       When(OrderCommand)
       .Then(context => // order commandan gelenleri saga instance doldur.
      {
        context.Saga.CorrelationId = context.Message.CorrelationId;
        context.Saga.OrderCode = context.Message.OrderCode;
        context.Saga.OrderId = context.Message.OrderId;

      })
      .ThenAsync(async context =>
      {
        // Log amaçlı State machine takibi
        await Console.Out.WriteLineAsync("Order Saga Command Completed");
      })
      .TransitionTo(Recieved) // Current State güncelle
      .Publish(context => new OrderRecieved // Artık OrderRecieved oldu bu event fırlat ki bunu dinleyen ms kendi iş sürecinin burada ki bilgilere göre devam ettirsin
      {
        CorrelationId = context.Message.CorrelationId,
        OrderId = context.Message.OrderId,
        OrderCode = context.Message.OrderCode

      }));

      // Recieved state girdiğimde 
      During(Recieved,
       When(OrderRecieved)
       .ThenAsync(async context =>
       {
         await Console.Out.WriteLineAsync("Order Recieved");
       })
       .TransitionTo(Proceeded)
       .Publish(context => new OrderProceeded // Artık OrderRecieved oldu bu event fırlat ki bunu dinleyen ms kendi iş sürecinin burada ki bilgilere göre devam ettirsin
       {
         CorrelationId = context.Message.CorrelationId,
         OrderId = context.Message.OrderId,
         OrderCode = context.Message.OrderCode

       }));


      // son süreç
      During(Proceeded,
      When(OrderProceeded)
      .ThenAsync(async context =>
      {
        await Console.Out.WriteLineAsync("Order Proceeded");
      })
      .Finalize());



    }


  }
}
