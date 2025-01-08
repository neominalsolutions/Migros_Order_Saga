using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Contracts
{
  public interface IOrderProceeded
  {
    Guid CorrelationId { get; set; } // Saga State Machine da süreç takibi için inital olarak başlatıp tüm state süreci bu ıd üzerinden devam edecek ki operasyonlar birbirinde izole olsun ve takip edilsin
    int OrderId { get; set; }

    string OrderCode { get; set; }

  }
}
