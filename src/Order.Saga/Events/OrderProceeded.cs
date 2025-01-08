using Message.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga.Events
{
  public class OrderProceeded : IOrderProceeded
  {
    public Guid CorrelationId { get; set; }
    public int OrderId { get; set; }

    public string OrderCode { get; set; }
  }
}
