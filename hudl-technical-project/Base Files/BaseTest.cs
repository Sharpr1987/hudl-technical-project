using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace hudl_technical_project.BaseFiles
{
    public class BaseTest
    {
        public IWebDriver Driver { get; private set; }

        /*
         *This method gets the webdriver before every test. 
         */

        [SetUp]
        public void SetupBeforeEveryTestMethod()
        {
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
            Driver.Manage().Window.Maximize();
        }

        /*
         * This method closes the webdriver after every test
         */

        [TearDown]
        public void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}

