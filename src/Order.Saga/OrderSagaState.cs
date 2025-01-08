using MassTransit;
using Message.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga
{
  // Order ait State takibi yapacak olan sınıf. Bu sınıfta state ile ilgili bilgileri tutacağız.
  //     public Guid CorrelationId { get; set; }, public State CurrentState { get; set; } State takibi tyapmak için minimum gereksinim
  public class OrderSagaState : SagaStateMachineInstance, IOrderCommand
  {
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; }
    public int OrderId { get; set; }
    public string OrderCode { get; set; }
  }
}
