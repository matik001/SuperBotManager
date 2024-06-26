﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace IntercityActionsConsumer
{
    internal class IntercitySearchPage
    {
        private static By BY_FROM_CITY = By.Id("stname-0");
        private static By BY_CONFIRM_FROM = By.XPath("//input[@id='stname-0']/following-sibling::ul/li/a");

        private static By BY_TO_CITY = By.Id("stname-1");
        private static By BY_CONFIRM_TO = By.XPath("//input[@id='stname-1']/following-sibling::ul/li/a");
        private static By BY_COOKIE_ACCEPT = By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");


        private static By BY_DATE = By.Id("date_picker");
        private static By BY_TIME = By.Id("ic-seek-time");
        private static By BY_SEARCH_BTN = By.XPath("//button[@name='search']");
        private IWebDriver _driver;


        public IntercitySearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IntercitySearchPage NavigateToSearching()
        {
            _driver.Navigate().GoToUrl("https://www.intercity.pl/pl/");
            return this;
        }
        public IntercityTicketPage SearchConnections(string from, string to, DateTime when)
        {
            string date = when.ToString("yyyy-MM-dd");
            string time = when.ToString("HH:mm");

            var oldImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var fromInput = _driver.FindElement(BY_FROM_CITY);
            var toInput = _driver.FindElement(BY_TO_CITY);
            var dateInput = _driver.FindElement(BY_DATE);
            var timeInput = _driver.FindElement(BY_TIME);
            var searchBtn = _driver.FindElement(BY_SEARCH_BTN);

            try
            {
                var acceptCookieBtn = _driver.FindElements(BY_COOKIE_ACCEPT);
                if(acceptCookieBtn.Count > 0)
                {
                    acceptCookieBtn[0].Click();
                    Thread.Sleep(1000);
                }
            }
            catch(Exception)
            {
                // ignore
            }

            /// can be stale
            fromInput = _driver.FindElement(BY_FROM_CITY);
            toInput = _driver.FindElement(BY_TO_CITY);
            dateInput = _driver.FindElement(BY_DATE);
            timeInput = _driver.FindElement(BY_TIME);
            searchBtn = _driver.FindElement(BY_SEARCH_BTN);


            fromInput.SendKeys(Keys.LeftControl + "a");
            fromInput.SendKeys(from);
            Thread.Sleep(1500);
            _driver.WaitUntilClicable(BY_CONFIRM_FROM).ScrollTo(_driver).Click();


            fromInput.SendKeys(Keys.LeftControl + "a");
            toInput.SendKeys(to);
            Thread.Sleep(1500);
            _driver.WaitUntilClicable(BY_CONFIRM_TO).ScrollTo(_driver).Click();


            dateInput.SendKeys(Keys.LeftControl + "a");
            dateInput.SendKeys(Keys.Delete);
            dateInput.SendKeys(date);
            dateInput.SendKeys(Keys.Return);


            timeInput.SendKeys(Keys.LeftControl + "a");
            timeInput.SendKeys(Keys.Delete);
            timeInput.SendKeys(time);


            timeInput.SendKeys(Keys.Return);
            Thread.Sleep(1000);
            timeInput.SendKeys(Keys.Return);


            _driver.Manage().Timeouts().ImplicitWait = oldImplicitWait;

            return new IntercityTicketPage(_driver);
        }
    }
}
