using hudl_technical_project.AutomationResources;
using hudl_technical_project.BaseFiles;
using OpenQA.Selenium;

namespace hudl_technical_project.Pages
{
    public class LoginPage : BaseApplicationPage
    {
        private ExplicitWait wait;

        public LoginPage(IWebDriver driver) : base(driver)
        {
            wait = new ExplicitWait(driver);
        }

        /*
         * Method to login with the provided username and password
         */

        public void Login(string email, string password)
        {
            emailField.Clear();
            emailField.SendKeys(email);

            passwordField.Clear();
            passwordField.SendKeys(password);

            loginBtn.Click();
        }

        /*
         * Method to mark Remember Me checkbox when logging in
         */

        public void RememberMe()
        {
            rememberMeCheckbox.Click();
        }

        /*
         * Method to retrieve remembered data from email and password fields
         */

        public Tuple<string, string> GetRememberedLogin()
        {
            return Tuple.Create(emailField.GetAttribute("value"), passwordField.GetAttribute("value"));
        }

        /*
         * Method to return error message when login credentials are invalid
         */

        public string LoginError()
        {
            return errorDisplay.Text;
        }



        /*
         * Page Elements
         */

        //Email Field
        IWebElement emailField => wait.FindElement(By.Id("email"));

        //Password Field
        IWebElement passwordField => wait.FindElement(By.Id("password"));

        //Log In Button
        IWebElement loginBtn => wait.FindElement(By.Id("logIn"));

        //Remember Me Checkbox
        IWebElement rememberMeCheckbox => wait.FindElement(By.XPath("//label[@data-qa-id='remember-me-checkbox-label']"));

        //Error Display
        IWebElement errorDisplay => wait.FindElement(By.XPath("//p[@data-qa-id='error-display']"));
    }
}