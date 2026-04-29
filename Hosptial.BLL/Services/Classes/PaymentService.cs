using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Stripe.Checkout;

public class PaymentService : IPaymentService
{
    //public async Task<string> CreateCheckout(PaymentDto dto)
    //{
    //    var options = new SessionCreateOptions
    //    {
    //        PaymentMethodTypes = new List<string> { "card" },
    //        LineItems = new List<SessionLineItemOptions>
    //        {
    //            new SessionLineItemOptions
    //            {
    //                PriceData = new SessionLineItemPriceDataOptions
    //                {
    //                    Currency = "usd",
    //                    UnitAmount = (long)(dto.Amount * 100),
    //                    ProductData = new SessionLineItemPriceDataProductDataOptions
    //                    {
    //                        Name = "appointment"
    //                    }
    //                },
    //                Quantity = 1
    //            }
    //        },
    //        Mode = "payment",
    //        SuccessUrl = "https://localhost:4200/success",
    //        CancelUrl = "https://localhost:4200/cancel"
    //    };

    //    var service = new SessionService();
    //    var session = await service.CreateAsync(options);

    //    return session.Url;
    //}

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
                    Currency   = "usd",
                    UnitAmount = (long)(dto.Amount * 100),  // already multiplied by 100
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Medical Appointment"
                    }
                },
                Quantity = 1
            }
        },
            Mode = "payment",

            // ✅ Pass booking id in success URL
            SuccessUrl = $"http://localhost:4200/payment-success?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"http://localhost:4200/payment-cancelled",

            // ✅ Store booking id in Stripe metadata
            Metadata = new Dictionary<string, string>
        {
            { "bookingId", dto.BookingId.ToString() }
        }
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session.Url;
    }
}