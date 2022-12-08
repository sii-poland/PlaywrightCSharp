using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    internal class DetailsProductPage
    {
        private readonly IPage _page;

        public DetailsProductPage(IPage page)
        {
            _page = page;
        }

        public virtual ILocator AddToBasketBtn => _page.Locator(".add-to-cart");
        public virtual ILocator ProceedToCheckoutBtn => _page.Locator("#blockcart-modal .col-md-7 div div a");
        public virtual ILocator CurrentPrice => _page.Locator(".current-price");

        public async void AddProductToCart()
        {
            await AddToBasketBtn.ClickAsync();
        }

        public async void ProceedToCheckout()
        {
            await ProceedToCheckoutBtn.ClickAsync();
        }

        public async Task<double> GetCurrentProductPrice()
        {
            var currentPrice = await CurrentPrice.InnerTextAsync();
            return currentPrice.ParseEuroToDouble();
        }
    }
}