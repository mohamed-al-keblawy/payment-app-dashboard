using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repo;

        public CardService(ICardRepository repo) => _repo = repo;

        public async Task<bool> ValidateCardAsync(string cardNumber, string holderName, DateTime expiryDate)
        {
            var card = await _repo.GetByNumberAsync(cardNumber);
            return card != null &&
                   card.CardHolder == holderName &&
                   card.ExpiryDate.Date >= expiryDate.Date;
        }
    }
}
