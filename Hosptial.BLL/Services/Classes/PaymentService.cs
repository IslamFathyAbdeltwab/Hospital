using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Stripe.Checkout;

public class PaymentService : IPaymentService
{
    public async Task<string> CreateCheckout(PaymentDto dto)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(dto.Amount * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "appointment"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:4200/success",
            CancelUrl = "https://localhost:4200/cancel"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }
}