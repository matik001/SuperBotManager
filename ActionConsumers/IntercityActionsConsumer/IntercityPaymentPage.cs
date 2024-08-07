﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace IntercityActionsConsumer
{
    internal class IntercityPaymentPage
    {
        private IWebDriver _driver;

        private static By BY_PAY_LATER_BTN = By.CssSelector("input[value=\"Płatność później\"]");
        private static By BY_GO_ON_BTN = By.CssSelector("input[value = \"Dalej\"]");
        private static By BY_ACCEPT_RULE_CHECKBOX = By.Id("zgoda");
        private static By BY_ACCEPT_RULE_SUBMIT = By.Id("submit_zgoda");

        public IntercityPaymentPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IntercityPaymentPage AcceptRules()
        {
            if(_driver.Url.Contains("regulamin"))
            {
                var oldImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
                var acceptRuleCheckbox = _driver.FindElement(BY_ACCEPT_RULE_CHECKBOX);
                acceptRuleCheckbox.Click();
                var acceptRuleSubmit = _driver.FindElement(BY_ACCEPT_RULE_SUBMIT);
                acceptRuleSubmit.Click();
                _driver.Manage().Timeouts().ImplicitWait = oldImplicitWait;
            }
            return this;
        } 
        public IntercityPaymentPage PayLater()
        {
            var oldImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var payLaterBtn = _driver.FindElement(BY_PAY_LATER_BTN);
            payLaterBtn.Click();
            _driver.Manage().Timeouts().ImplicitWait = oldImplicitWait;
            return this;
        }

        public IntercityPaymentPage GoOn()
        {
            var oldImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var goOnBtn = _driver.FindElement(BY_GO_ON_BTN);
            goOnBtn.Click();
            _driver.Manage().Timeouts().ImplicitWait = oldImplicitWait;
            return this;
        }
    }
}