using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Application.DTOs.Requests
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
