using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LiteCartTests.Framework
{
    [TestFixture(typeof(ChromeDriver))]
    class CartTest<TDriver> : BaseTest<TDriver> where TDriver : IWebDriver, new()
    {
        [Test]
        public void AddToCartTest()
        {
            app.OpenStorePage();
            app.AddPopularProductsToCart(4);
            app.OpenCart();
            app.ClearCart();
        }
    }
}