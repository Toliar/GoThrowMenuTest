using NUnit.Framework;
using OpenQA.Selenium;

namespace LiteCartTests.Framework
{
    public class BaseTest<TDriver> where TDriver : IWebDriver, new()
    {
        protected LiteCartApp<TDriver> app;

        [SetUp]
        public void InitApp()
        {
            app = new LiteCartApp<TDriver>();
        }

        [TearDown]
        public void QuitApp()
        {
            app.Quit();
        }
    }
}