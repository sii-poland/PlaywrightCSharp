using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class TestForShop : BaseSetup
    {
        [TestCase("Pull Imprimé Colibri")]
        public async Task EnterToShop_AddProductToBasket_CheckProductPriceInSummary(string option)
        {
            var page = await Context.NewPageAsync();
            var homePage = new HomePage(page);
            await page.GotoAsync(TestSettings.EnvUrl);
            await homePage.SelectDefineProduct(option);
            var detailsProductPage = new DetailsProductPage(page);
            var productPrice = await detailsProductPage.GetCurrentProductPrice();
            detailsProductPage.AddProductToCart();
            detailsProductPage.ProceedToCheckout();
            var summaryCartPage = new SummaryCartPage(page);
            var currentTotalPrice = await summaryCartPage.GetTotalPrice();
            currentTotalPrice.Should().Be(productPrice);
        }
    }
}