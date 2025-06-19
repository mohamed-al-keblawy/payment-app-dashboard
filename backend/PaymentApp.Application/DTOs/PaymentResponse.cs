using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Application.DTOs
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; } = string.Empty;
        public string RefundCode { get; set; } = string.Empty;
    }
}
