using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Application.Interfaces.Services
{
    public interface ICardService
    {
        Task<bool> ValidateCardAsync(string cardNumber, string holderName, DateTime expiryDate);
    }
}
