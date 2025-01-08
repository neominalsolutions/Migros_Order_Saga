using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Contracts
{
  // Not: OrderCommand Saga sürecini ilk başlatan bir komut olması sebebi ile Correlationıd burada tutmadık. Bundan sonraki süreçlerde operasyonların tek bir ıd üzerinden işlenmesi ve takibi amaçlı tanımlanacaktır.

  public interface IOrderCommand
  {
    int OrderId { get; set; }
    string OrderCode { get; set; }
  }
}
