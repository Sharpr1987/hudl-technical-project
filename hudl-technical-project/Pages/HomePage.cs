using hudl_technical_project.AutomationResources;
using hudl_technical_project.BaseFiles;
using OpenQA.Selenium;

namespace hudl_technical_project.Pages
{
    public class HomePage : BaseApplicationPage
    {
        private ExplicitWait wait;

        public HomePage(IWebDriver driver) : base(driver)
        {
            wait = new ExplicitWait(Driver);
        }

        /*
         *Method to go to the Login page 
         */

        public LoginPage GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.hudl.com/login");
            return new LoginPage(Driver);
        }

        /*
         *Method to get the current URL
         */

        public string GetCurrentURL()
        {
            wait.FindElement(By.XPath("//input[@title='Search']"));
            return Driver.Url;
        }

        /*
         * Method to logout
         */

        public void Logout()
        {
            userMenu.Click();
            logoutButton.Click();
        }

        public void Login()
        {
            loginButton.Click();
        }

        /*
         * Page Elements
         */

        //User Menu
        public IWebElement userMenu => wait.FindElement(By.ClassName("hui-globalusermenu"));

        //Logout Button
        public IWebElement logoutButton => wait.FindElement(By.XPath("//a[@data-qa-id='webnav-usermenu-logout']"));

        //Login Button
        public IWebElement loginButton => wait.FindElement(By.XPath("//a[@data-qa-id='login']"));
    }
}