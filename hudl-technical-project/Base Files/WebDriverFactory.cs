using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace hudl_technical_project.BaseFiles
{
    public class WebDriverFactory
    {
        /*
         * This method takes in a browser type enum and calls a method to retrieve the appropriate drive. The driver is then returned as an IWebDriver.
         */
        public IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return GetChromeDriver();
                default:
                    throw new ArgumentOutOfRangeException("No such browser exists");
            }
        }

        /*
         * This method retrieves the ChromeDriver from it's location in the directory.
         */

        private IWebDriver GetChromeDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outPutDirectory);
        }
    }
}

