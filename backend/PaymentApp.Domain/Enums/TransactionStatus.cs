using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Domain.Enums
{
    public enum TransactionStatus
    {
        Held = 0,
        Confirmed = 1,
        Refunded = 2
    }
}
