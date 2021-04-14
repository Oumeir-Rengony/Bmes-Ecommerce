using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Services.Contracts
{
    public interface ICheckoutService
    {
        Task ProcessCheckout();
    }
}
