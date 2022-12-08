using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class TestForShopWorkingParallel : PlaywrightTest
    {
        [TestCase("Pull Imprimé Colibri")]
        [TestCase("T-Shirt Imprimé Colibri")]
        [TestCase("Mug The Adventure Begins")]
        public async Task EnterToShop_AddProductToBasket_CheckProductPriceInSummary(string option)
        {
            var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                { Headless = false, Channel = "chrome" });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync(TestSettings.EnvUrl);
            var homePage = new HomePage(page);
            await homePage.SelectDefineProduct(option);

            var detailsProductPage = new DetailsProductPage(page);
            var productPrice = await detailsProductPage.GetCurrentProductPrice();
            detailsProductPage.AddProductToCart();
            detailsProductPage.ProceedToCheckout();

            var summaryCartPage = new SummaryCartPage(page);
            var currentPrice = await summaryCartPage.GetTotalPrice();
            productPrice.Should().Be(currentPrice);
        }
    }
}