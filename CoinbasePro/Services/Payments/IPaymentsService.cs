using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Payments.Models;

namespace CoinbasePro.Services.Payments
{
    public interface IPaymentsService
    {
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
    }
}
