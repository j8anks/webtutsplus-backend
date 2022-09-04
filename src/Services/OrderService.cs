
using DapperASPNetCore.Dto;
using DapperASPNetCore.Contracts;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections;

namespace DapperASPNetCore.Services
{
    // https://github.com/stripe/stripe-dotnet/tree/master/src/Stripe.net/Services
    // https://stackoverflow.com/questions/67591732/the-specified-checkout-session-could-not-be-found

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IConfiguration _config;

        public OrderService() { }
       
        
        public OrderService(IOrderRepository orderRepo, IConfiguration config)
        {
            _orderRepo = orderRepo;
            _config = config;
        }


        // build each product in the stripe checkout page
       private SessionLineItemOptions CreateSessionLineItem(CheckoutItemDto checkoutItemDto)
       {
            var lineItem = new SessionLineItemOptions();
            {
                new SessionLineItemOptions
                {
                    Quantity = long.Parse(checkoutItemDto.quantity.ToString()),
                    Price = checkoutItemDto.price.ToString(),
                };
            };

            return lineItem;           

       }

        // create session from list of checkout items
        public Session CreateSession(List<CheckoutItemDto> checkoutItemDtoList)
        {

            // supply success and failure url for stripe

            string baseURL = _config.GetValue<string>("BaseUrl");           

            String successURL = baseURL + "payment/success";
            String failedURL = baseURL + "payment/failed";

            List<SessionLineItemOptions> sessionItemsList = new List<SessionLineItemOptions>();

            // for each product compute SessionCreateParams.LineItem

            foreach (var item in checkoutItemDtoList)
            {
                var lineItem = CreateSessionLineItem(item);

                sessionItemsList.Add(new SessionLineItemOptions { Name = item.productName, Quantity = item.quantity, Currency = "usd", Amount = long.Parse(item.price.ToString()) });
                               
            }

            var test2 = sessionItemsList;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = sessionItemsList,
                Mode = "payment",
                SuccessUrl = successURL,
                CancelUrl = failedURL,
            };

            var requestOptions = new Stripe.RequestOptions
            {
                StripeAccount = "", // stripeConnectedAccountId,
                ApiKey = "tGN0bIwXnHdwOa85VABjPdSn8nWY7G7I"
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options, requestOptions);

            return session;

        }


    }
}


