For a project like this, your README file should serve as both a guide to using your test suite and a rationale for your development choices. Here’s a suggested outline to cover the essentials:

### 1. **Project Overview**
   This is a BDDFramework (SpecFlow Project) UI automation tests written in C# using SpecFlow with Selenium WebDriver.

### 2. **Setup and Installation**
   - **Prerequisites**: Visual Studio 2022, .NET SDK, Selenium Webdriver, and SpecFlow.

### 3. **Automation Framework Design Approach and Development Decisions**
   - **Page Object Model (POM)**: The framework used for this project is a Page Object Model (POM) automation framework in Selenium with SpecFlow and C#. This design pattern that helps to organize and maintain UI tests by separating the test logic from the underlying page structure. This separation is achieved through the use of “page objects,” which are classes that represent the elements and actions on a particular page of the application. Here’s how this framework is structured:
```
├ TFLJourneyPlanner/
├── Pages/
│   ├── JourneyPlannerPage.cs
├── StepDefinitions/
│   ├── CommonDefinitions.cs
│   └── TFLJourneyPlannerUIAutomationStepDefinitions.cs
├── Features/
│   └── JourneyPlanner.feature
│── SetUp/
│   └── Context.cs
│   └── WebDriverExtension.cs
├── BrowserFactories/
│   └── ChromeBrowserFactory.cs
│    └─ InternetExplorerBrowserFactory.cs
└── README.md
└── Local.runsettings
└── EnvironmentData.cs
```

* Pages: Contains the page object classes, each representing a different page.
* Features: Holds Gherkin .feature files with test scenarios.
* StepDefinitions: Maps steps in .feature files to C# code that performs actions, typically using page objects.
* SetUp: Contains classes such setup and teardown methods for initializing and closing the browser and performing various driver activities on the browser
* BrowserFactories: Helper classes, like browser management to spin up instances of browsers.

The Pages folder contains the page object classes, which encapsulate:

* Element Locators: Using Selenium By locators for elements.
* Page Actions: Methods that represent user interactions (like clicking a button, entering text).
* Page Validations: Methods that verify the page state (like checking if a message is displayed).

- **Other Key Decisions and Key Info**:
     - Dynamic elements were handled using wait times
     - Reports are created using ExtentReports and ScreenShots is used to capture failed Scenarios. 
     - In Project Solution Runsettings file included to show that test can be run in different environments. 
     - Locators are defined in JourneyPlannerPage 
     - NuGet packages needs to be restored before test run 
     - Contains a Local Run Settings which must be selected before any test run. This can be done by navigating to Test and then Configure Run Settings.
   

### 4. **Test Scenarios**
   - **Implemented Test Scenarios**: 
   1. verify that a valid journey can be planned 
   2. Verify that a user can edit journey preferences after planning a journey
   3. View details and verify access information at Covent Garden 
   4. Verify that a user cannot plan a journey with invalid location data
   5. Verify that a user cannot plan a journey with empty location fields

   - **Additonal Test Scenarios**: 

   
### 7. **Additonal Test Scenarios**
     Functional: 
   - A user can plan a journey successfully based on arrival time Scenario
   - Recents tab display recently planned journeys
   - Test Journey Planner for Out-of-Service Locations: verify that the widget provides an appropriate message or alternative route
   
    Non-Functional:
   - Verify that the widget functions consistently across multiple browsers (e.g., Chrome, Firefox, Safari, Edge) and browser versions.
   - Measure the time taken to process and display journey results, especially under different loads. Ensure results load within an acceptable time frame.

