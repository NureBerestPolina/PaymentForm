using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentForm.Services;
using Stripe.Checkout;

namespace PaymentForm.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly IConfiguration _configuration;

        public CheckoutModel(ProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public int ProductId { get; set; }

        public void OnGet(int id)
        {
            ProductId = id;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        },
                        UnitAmount = (long)(product.Price * 100),
                    },
                    Quantity = 1,
                }
            },
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Redirect(session.Url);
        }
    }
}
