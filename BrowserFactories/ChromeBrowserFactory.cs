using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using WebDriverManager.Clients;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;
using WebDriverManager.Services.Impl;
using WebDriverManager.Services;
using WebDriverManager.DriverConfigs.Impl;

namespace Test.UI.SpecFlow.BrowserFactories
{
    public class ChromeBrowserFactory
    {
        public string ChromeDriverPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public TimeSpan WebDriverTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public bool Incognito { get; set; } = false;
        public bool Headless { get; set; } = false;
        public bool DisableGpu { get; set; } = false;
        public bool NoSandBox { get; set; } = false;
        public bool StartMaximised { get; set; } = true;
        public IEnumerable<string> AdditionalArguments { get; set; } = new List<string>();
        public IDictionary<string, string> AdditionalUserProfilePreferences { get; set; } = new Dictionary<string, string>();
        private readonly IObjectContainer objectContainer;
        public string DownloadsFolderPath { get; set; } = null;

        public ChromeBrowserFactory(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        public IWebDriver Create(IObjectContainer objectContainer)
        {
            IWebDriver driver;
            var options = new ChromeOptions();
            var arguments = new List<string>();

            if (!string.IsNullOrEmpty(DownloadsFolderPath))
            {
                options.AddUserProfilePreference("download.default_directory", DownloadsFolderPath);
            }

            if (Incognito)
            {
                arguments.Add("incognito");
            }


            if (StartMaximised)
            {
                arguments.Add("start-maximized");
            }

            if (Headless)
            {
                arguments.Add("headless");
            }

            if (DisableGpu)
            {
                arguments.Add("disable-gpu");
            }

            if (NoSandBox)
            {
                arguments.Add("no-sandbox");
            }

            if (AdditionalArguments != null)
            {
                arguments = arguments.Union(AdditionalArguments).ToList();
            }

            options.AddArguments(arguments);

            if (AdditionalUserProfilePreferences != null)
            {
                foreach (var additionalUserProfilePreference in AdditionalUserProfilePreferences)
                {
                    options.AddUserProfilePreference(additionalUserProfilePreference.Key,
                        additionalUserProfilePreference.Value);
                }
            }

            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            options.LeaveBrowserRunning = false;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver(options);
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);

            return driver;
        }
    }
}
