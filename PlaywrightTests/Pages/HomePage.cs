using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    internal class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
        }

        public string ProductTitles => ".product-title";

        public async Task SelectDefineProduct(string name)
        {
            var products = await _page.QuerySelectorAllAsync(ProductTitles);
            foreach (var product in products)
                if (await product.InnerTextAsync() == name)
                {
                    await product.ClickAsync();
                    break;
                }
        }
    }
}