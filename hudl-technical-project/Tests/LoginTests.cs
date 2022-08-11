using System.Configuration;
using hudl_technical_project.BaseFiles;
using hudl_technical_project.Pages;

namespace hudl_technical_project.Tests;

public class LoginTests : BaseTest
{

    private readonly string _validEmail = ConfigurationManager.AppSettings["ValidEmail"];
    private readonly string _validPassword = ConfigurationManager.AppSettings["ValidPassword"];
    private readonly string _invalidEmail = ConfigurationManager.AppSettings["InvalidEmail"];
    private readonly string _invalidPassword = ConfigurationManager.AppSettings["InvalidPassword"];

    /*
     * Functional tests with valid and invalid data
     */

    [Test]
    public void Test_Login_With_Valid_Credentials()
    {
        var testpath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_validEmail, _validPassword);
        string currentURL = homePage.GetCurrentURL();
        Assert.That(currentURL, Is.EqualTo("https://www.hudl.com/home"));
        Console.WriteLine(testpath);
    }

    [Test]
    public void Test_Login_With_No_Credentials()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login("", "");
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Login_With_Invalid_Credentials()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_invalidEmail, _invalidPassword);
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Login_With_Valid_Email_Invalid_Password()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_validEmail, _invalidPassword);
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Login_With_Invalid_Email_Valid_Password()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_invalidEmail, _validPassword);
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Remember_Me_Checkbox_With_Valid_Login()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.RememberMe();
        loginPage.Login(_validEmail, _validPassword);
        homePage.Logout();
        homePage.Login();
        var storedLogin = loginPage.GetRememberedLogin();
        Assert.That($"Email: {storedLogin.Item1} Password: {storedLogin.Item2}", Is.EqualTo($"Email: {_validEmail} Password: {_validPassword}"));
    }

    /*
     * Security tests for XSS vulnerabilities and SQL injection vulnerabilities
     */

    [Test]
    public void Test_Login_For_XSS_Email()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login($"<script>alert(\"{ _validEmail}\");</script>", _validPassword);
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Login_For_XSS_Password()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_validEmail, $"<script>alert(\"{_validPassword}\"</script>");
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Email_For_SQL_Injection()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login("\" or 1=1;--", _validPassword);
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }

    [Test]
    public void Test_Password_For_SQL_Injection()
    {
        HomePage homePage = new HomePage(Driver);
        LoginPage loginPage = homePage.GoTo();
        loginPage.Login(_validEmail, "\" or 1=1;--");
        string errorMessage = loginPage.LoginError();
        Assert.That(errorMessage, Is.EqualTo("We didn't recognize that email and/or password.Need help?"));
    }
}
