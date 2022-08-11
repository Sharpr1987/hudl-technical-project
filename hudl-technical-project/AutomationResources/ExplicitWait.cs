using System;
using hudl_technical_project.BaseFiles;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace hudl_technical_project.AutomationResources
{
    public class ExplicitWait : BaseApplicationPage
    {

        public WebDriverWait wait;

        public ExplicitWait(IWebDriver driver) : base(driver)
        {
            this.Driver = driver;
            TimeSpan timeout = TimeSpan.FromSeconds(30);
            this.wait = new WebDriverWait(driver, timeout);

            wait.Message = "wait timed out after 30 seconds";
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
        }

        /*
         * Method to wait till the element is enabled.
         */

        private void WaitUntilElementIsEnabled(By locator)
        {
            Func<IWebDriver, bool> condition =
                d =>
                {
                    IWebElement e = d.FindElement(locator);
                    return e.Enabled;
                };

            wait.Until(condition);
        }

        /*
         * Method to wait till the element is displayed.
         */

        private void WaitUntilElementIsDisplayed(By locator)
        {
            wait.Until(d =>
            {
                IWebElement e = d.FindElement(locator);
                return e.Displayed;
            });
        }

        /*
         * Method to find the element when it is enabled.
         */

        public IWebElement FindEnabledElement(By locator)
        {
            WaitUntilElementIsEnabled(locator);
            IWebElement element = Driver.FindElement(locator);
            return element;
        }

        /*
         * Method to find the element when it is displayed.
         */

        public IWebElement FindElement(By locator)
        {
            WaitUntilElementIsDisplayed(locator);
            IWebElement element = Driver.FindElement(locator);
            return element;
        }
    }
}

