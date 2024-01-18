using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorytelActionsConsumer
{
    internal class StorytelBot
    {
        IWebDriver driver;
        public StorytelBot(IWebDriver driver)
        {
            this.driver = driver;
        }
        internal void CreateAccount(StorytelSignupInput input)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://www.storytel.com/ro/en/signup/7777");
            var cookieAcceptBtn = driver.FindElement(By.Id("cb-button-accept"));
            cookieAcceptBtn.Click();

            var emailInput = driver.FindElement(By.Id("emailInput1"));
            var passInput = driver.FindElement(By.Id("passwordInput1"));
            var termsCheckbox = driver.FindElement(By.XPath("//label[@for='agreementCheckbox1']"));
            var submitBtn = driver.FindElement(By.Id("submitButton"));

            emailInput.SendKeys(input.Email);
            passInput.SendKeys(input.Password);
            termsCheckbox.Click();
            submitBtn.Click();

            var iframeCardNumber = driver.FindElement(By.XPath("//iframe[@title='Iframe for card number']"));
            driver.SwitchTo().Frame(iframeCardNumber);
            var cardNumberInput = driver.FindElement(By.XPath("//input[@type='text']"));
            cardNumberInput.SendKeys(input.CardNumber);
            driver.SwitchTo().ParentFrame();

                
            var iframeExpiryDate = driver.FindElement(By.XPath("//iframe[@title='Iframe for expiry date']"));
            driver.SwitchTo().Frame(iframeExpiryDate);
            var expDateInput = driver.FindElement(By.XPath("//input[@type='text']"));
            expDateInput.SendKeys(input.CardExpiration.ToString("MM yy", CultureInfo.InvariantCulture));
            driver.SwitchTo().ParentFrame();



            var iframeCCV = driver.FindElement(By.XPath("//iframe[@title='Iframe for security code']"));
            driver.SwitchTo().Frame(iframeCCV);
            var ccvInput = driver.FindElement(By.XPath("//input[@type='text']"));
            ccvInput.SendKeys(input.CardCCV);
            driver.SwitchTo().ParentFrame();

            var tosCheckbox = driver.FindElement(By.XPath("//div[contains(@class, 'checkout-tos')]"));
            tosCheckbox.Click();

            var submitBtn2 = driver.FindElement(By.XPath("//div[contains(@class, 'checkout-button')]/button"));
            submitBtn2.Click();
            Thread.Sleep(10000);
        }
    }
}
