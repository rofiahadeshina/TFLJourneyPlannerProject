using System;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Test.UI.SpecFlow.BrowserFactories
{
    public class InternetExplorerBrowserFactory
    {
        private readonly IObjectContainer objectContainer;
        public string IEDriverPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public InternetExplorerBrowserFactory(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        public IWebDriver Create(IObjectContainer objectContainer)
        {
            IWebDriver driver;
            var options = new InternetExplorerOptions { IgnoreZoomLevel = true };
            driver = new InternetExplorerDriver(IEDriverPath, options, TimeSpan.FromSeconds(60));
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            return driver;
        }
    }
}
