using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests.Tests
{
    public class BaseSetup
    {
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPlaywright Playwright;

        [SetUp]
        public async Task Setup()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                { Headless = false, Channel = "chrome" });
            Context = await Browser.NewContextAsync();

            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        public async Task Teardown()
        {
            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"sii_blog_{Guid.NewGuid()}.zip"
            });
            await Browser.CloseAsync();
        }
    }
}