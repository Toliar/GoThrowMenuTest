using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace LiteCartTests.Framework
{
    public class CartPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".shortcuts li.shortcut")]
        private IList<IWebElement> shortcuts;

        [FindsBy(How = How.CssSelector, Using = "ul.items")]
        private IWebElement productsSlider;

        [FindsBy(How = How.CssSelector, Using = "ul.items li.item:first-child button[name='remove_cart_item']")]
        private IWebElement removeButton;

        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public CartPage RemoveAllItems()
        {
            StopAnimation();

            int itemsCount = shortcuts.Count;
            for (int i = 0; i < itemsCount; i++)
            {
                var orderSummary = driver.FindElement(By.CssSelector("#order_confirmation-wrapper"));
                removeButton.Click();
                wait.Until(ExpectedConditions.StalenessOf(orderSummary));
            }

            return this;
        }

        private void StopAnimation()
        {
            if (shortcuts.Count != 0)
                shortcuts[0].Click();

            wait.Until(d => productsSlider.GetCssValue("margin-left").Contains("0px"));
        }
    }
}