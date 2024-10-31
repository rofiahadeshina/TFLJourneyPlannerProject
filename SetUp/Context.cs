using System;
using OpenQA.Selenium;
using BoDi;
using Test.UI.SpecFlow.BrowserFactories;
using NUnit.Framework.Legacy;

namespace Test.UI.SpecFlow.SetUp
{
    public class Context
    {
        ChromeBrowserFactory _chromeBrowserFactory;        
        InternetExplorerBrowserFactory _internetExplorerBrowserFactory;
        private readonly IObjectContainer objectContainer;
        private IWebDriver driver;
        string baseUrl = EnvironmentData.baseUrl;
        string browser = EnvironmentData.browser;

        public Context(IObjectContainer objectContainer
                      , ChromeBrowserFactory chromeBrowserFactory                      
                      , InternetExplorerBrowserFactory internetExplorerBrowserFactory)
        {
            this.objectContainer = objectContainer;            
            _chromeBrowserFactory = chromeBrowserFactory;
            _internetExplorerBrowserFactory = internetExplorerBrowserFactory;
        }

        public void LoadJourneyPlannerApplication()
        {
            switch (browser.ToLower())
            {            

                case "chrome":
                    driver = _chromeBrowserFactory.Create(objectContainer);
                    break;

                case "ie":
                    driver = _internetExplorerBrowserFactory.Create(objectContainer);
                    break;

                default:
                    driver = _chromeBrowserFactory.Create(objectContainer);
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Navigate().GoToUrl(baseUrl);
        }

        public void AcceptCookie()
        {
            var cookieBanner = driver.FindElement(By.Id("cb-cookiebanner"));

            var acceptCookieButton = cookieBanner.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));

            ClassicAssert.IsNotNull(acceptCookieButton);

            acceptCookieButton.Click();
        }
        public void ShutDownJourneyPlannerApplication()
        {
            driver?.Quit();
        }

        public void TakeScreenshotAtThePointOfTestFailure(string directory, string scenarioName)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string path = directory + scenarioName + DateTime.Now.ToString("yyyy-MM-dd") + ".png";
            string Screenshot = screenshot.AsBase64EncodedString;
            byte[] screenshotAsByteArray = screenshot.AsByteArray;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }            
            screenshot.SaveAsFile(path);
        }
    }
}