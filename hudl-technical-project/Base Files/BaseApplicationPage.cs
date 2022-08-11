using System;
using OpenQA.Selenium;

namespace hudl_technical_project.BaseFiles
{
    public class BaseApplicationPage
    {
        /*
         *Initialize the webdriver for every page. 
         */

        protected IWebDriver Driver;

        public BaseApplicationPage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}

