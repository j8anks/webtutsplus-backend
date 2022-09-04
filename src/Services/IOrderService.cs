using Stripe.Checkout;
using DapperASPNetCore.Dto;

namespace DapperASPNetCore.Services
{
        public interface IOrderService
        {
            Session CreateSession(List<CheckoutItemDto> checkoutItemDtoList);

        }
}
