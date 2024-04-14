using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercityActionsConsumer
{
    public static class SeleniumUtils
    {
        public static IWebElement WaitUntilClicable(this IWebDriver driver, By by, int timeout = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        public static void ClickJS(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
        public static IWebElement ScrollTo(this IWebElement element, IWebDriver driver)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
            return element;
        }


    }

}
