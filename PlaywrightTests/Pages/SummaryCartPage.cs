using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    internal class SummaryCartPage
    {
        private readonly IPage _page;

        public SummaryCartPage(IPage page)
        {
            _page = page;
        }

        public virtual ILocator CurrentPrice => _page.Locator(".cart-total .value");

        public async Task<double> GetTotalPrice()
        {
            var currentPrice = await CurrentPrice.InnerTextAsync();
            return currentPrice.ParseEuroToDouble();
        }
    }
}