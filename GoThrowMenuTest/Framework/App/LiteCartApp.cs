using OpenQA.Selenium;
using System;

namespace LiteCartTests.Framework
{
    public class LiteCartApp<TDriver> where TDriver : IWebDriver, new()
    {
        private AdminLoginPage adminLoginPage;
        private StorePage storePage;
        private ProductPage productPage;
        private CartPage cartPage;

        private IWebDriver driver;

        public LiteCartApp()
        {
            driver = new TDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            adminLoginPage = new AdminLoginPage(driver);
            storePage = new StorePage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void OpenAdminZone() => adminLoginPage.Open().LoginAsAdmin();

        public StorePage OpenStorePage() => storePage.OpenStorePage();

        public void AddPopularProductsToCart(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                storePage.OpenFirstPopularProduct().AddToCart().OpenStorePage();
            }
        }

        public void OpenCart() => storePage.OpenCartPage();

        public void ClearCart() => cartPage.RemoveAllItems();

        public void Quit()
        {
            driver?.Quit();
        }
    }
}