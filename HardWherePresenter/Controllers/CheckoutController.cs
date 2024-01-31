using Application.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Session = Stripe.Checkout.Session;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        Dictionary<string, string> productQuantities = new Dictionary<string, string>();

        public CheckoutController()
        {
            // Add values for each key
            productQuantities["Dc-Motor"] = "price_1OXQTvCZ8fe8KVea1gjHnIXl";
            productQuantities["Servo"] = "price_1OXQUMCZ8fe8KVeaQHW4WKx4";
            productQuantities["stepper"] = "price_1OXQVGCZ8fe8KVear0GUhxWx";
        }

        [HttpPost]
        public ActionResult Checkout([FromBody] OrderDTO order)
        {
            {
                var domain = "http://localhost:3000";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions> { },
                    Mode = "payment",
                    SuccessUrl = domain + "/success",
                    CancelUrl = domain + "/checkout",
                    ShippingOptions = new List<Stripe.Checkout.SessionShippingOptionOptions>
                    {
                        new Stripe.Checkout.SessionShippingOptionOptions
                        {
                            ShippingRateData =
                                new Stripe.Checkout.SessionShippingOptionShippingRateDataOptions
                                {
                                    Type = "fixed_amount",
                                    FixedAmount =
                                        new Stripe.Checkout.SessionShippingOptionShippingRateDataFixedAmountOptions
                                        {
                                            Amount = 0,
                                            Currency = "ils",
                                        },
                                    DisplayName = "on site",
                                    DeliveryEstimate =
                                        new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                                        {
                                            Minimum =
                                                new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                                {
                                                    Unit = "day",
                                                    Value = 5,
                                                },
                                            Maximum =
                                                new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                                {
                                                    Unit = "day",
                                                    Value = 7,
                                                },
                                        },
                                },
                        },
                        new Stripe.Checkout.SessionShippingOptionOptions
                        {
                            ShippingRateData =
                                new Stripe.Checkout.SessionShippingOptionShippingRateDataOptions
                                {
                                    Type = "fixed_amount",
                                    FixedAmount =
                                        new Stripe.Checkout.SessionShippingOptionShippingRateDataFixedAmountOptions
                                        {
                                            Amount = 10 * 100,
                                            Currency = "ils",
                                        },
                                    DisplayName = "Deliver The Order",
                                    DeliveryEstimate =
                                        new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                                        {
                                            Minimum =
                                                new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                                {
                                                    Unit = "day",
                                                    Value = 2,
                                                },
                                            Maximum =
                                                new Stripe.Checkout.SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                                {
                                                    Unit = "day",
                                                    Value = 3,
                                                },
                                        },
                                },
                        },
                    }
                };
                foreach (var item in order.contents)
                {
                    var key = "";
                    if (item.product.name.StartsWith("Dc"))
                    {
                        key = productQuantities["Dc-Motor"];
                    }
                    else if (item.product.name.StartsWith("Ser"))
                    {
                        key = productQuantities["Servo"];
                    }
                    else if (item.product.name.StartsWith("ste"))
                    {
                        key = productQuantities["stepper"];
                    }
                    else
                    {
                        key = item.priceRegular;
                    }

                    var lineItem = new SessionLineItemOptions
                    {
                        Price = key,
                        // Replace with the actual price ID from Stripe
                        Quantity = item.quantity,
                        // Assuming each item in the order has a quantity property
                    };

                    options.LineItems.Add(lineItem);
                }
                var service = new SessionService();

                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                return Ok(Response.Headers);
            }
        }
    }
}
