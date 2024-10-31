using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Test.UI.SpecFlow.SetUp;

namespace TFLJourneyPlanner.Pages
{
    public class JourneyPlannerPage
    {
        IWebDriver _driver;
        public JourneyPlannerPage(IWebDriver driver) 
        { 
            _driver = driver;
        }      
        By journeyResultHeader = By.CssSelector(".journey-planner-results .jp-results-headline");

        By fromLocationMotherSpan = By.CssSelector("span[class$='twitter-typeahead']");
        By fromInputLocator = By.CssSelector("input[class$='jpFrom tt-input']");
        By suggestionsContainerLocator = By.CssSelector("div.tt-dataset-stop-points-search");
        By LocationSuggestionsBox = By.ClassName("tt-suggestions");
        By LocationSuggestionsChildren = By.ClassName("tt-suggestion");
        By desiredLocation = By.ClassName("stop-name");

        By toLocationMotherSpan = By.Id("search-filter-form-1");
        By toInputLocator = By.Id("InputTo");
        By toLocatorDropdown = By.Id("InputTo-dropdown");
        By planAJourneyButton = By.Id("plan-journey-button");

        By jpResultContainer = By.ClassName("journey-planner-results");
        By jpResultLocationWrapper = By.CssSelector(".from-to-wrapper .summary-row");
        By jpResultlocations = By.CssSelector("span.notranslate");

        By cyclingAndWalkingJpOptions = By.CssSelector(".left-journey-options");
        By cyclingOptions = By.ClassName("cycling");
        By JourneyInfo = By.ClassName("journey-info");
        By walkingOptions = By.ClassName("walking");
        By cyclingWalkingHeader = By.TagName("h4");

        By editPreferenceContent = By.Id("jp-new-content-home-");
        By editPreferenceButton = By.CssSelector(".edit-preferences button.toggle-options");

        By transportPreferenceOptions = By.CssSelector(".stacked-fields.search-public-options li.show-me-list");
        By transportOptionContent = By.CssSelector(".stacked-fields.search-public-options li.show-me-list fieldset div.form-control");
        By leastWalkingOption = By.CssSelector("label[for='JourneyPreference_2']");
        By updateJourneyButton = By.CssSelector("div[id='more-journey-options'] input.plan-journey-button");
        By updatedResultContainer = By.CssSelector("div.journey-results div.summary-results");
        By expandedResultContainer = By.CssSelector(".publictransport-box .expanded");
        By expandedResultTiming = By.CssSelector("#option-1-heading .time-box");
        By updateResultJourneyTime = By.CssSelector("#option-1-heading .journey-time");
        By expandedJourneyDetails = By.CssSelector("#option-1-content .journey-details");
        By viewDetailsButton = By.CssSelector(".price-and-details .view-hide-details");

        By fromFieldErrorMessage = By.CssSelector(".geolocation-box .field-validation-error #InputFrom-error");
        By toFieldErrorMessage = By.CssSelector(".field-validation-error #InputTo-error");
        public void InputDynamicStartLocation(string startSuggestion, string startLocationStation)
        {

            var fromMotherSpan = _driver.FindElement(fromLocationMotherSpan);
            var fromInputEl = fromMotherSpan.FindElement(fromInputLocator);
            fromInputEl.SendKeys(startSuggestion);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(suggestionsContainerLocator));

            var fromParentSearchResult = _driver.FindElement(suggestionsContainerLocator);

            var fromttSuggestions = fromParentSearchResult.FindElement(LocationSuggestionsBox);

            var fromttSuggestionsChildren = fromttSuggestions.FindElements(LocationSuggestionsChildren);

            for (var i = 0; i < fromttSuggestionsChildren.Count; i++)
            {
                var fromstopSearchSuggestion = fromttSuggestionsChildren[i].FindElement(desiredLocation);

                var stationName = fromstopSearchSuggestion.Text;

                if (stationName == startLocationStation)
                {
                    fromstopSearchSuggestion.Click();
                    break;
                }
            }

        }

        public void InputDynamicEndLocation(string endSuggestion, string endLocationStation)
        {

            var motherSpan = _driver.FindElement(toLocationMotherSpan);
            motherSpan.FindElement(toInputLocator).SendKeys(endSuggestion);

            Thread.Sleep(2000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(suggestionsContainerLocator));


            var inputToDropDown = _driver.FindElement(toLocatorDropdown);

            var parentSearchResult = inputToDropDown.FindElement(suggestionsContainerLocator);

            var ttSuggestions = parentSearchResult.FindElement(LocationSuggestionsBox);

            var ttSuggestionsChildren = ttSuggestions.FindElements(LocationSuggestionsChildren);

            for (var i = 0; i < ttSuggestionsChildren.Count; i++)
            {
                var stopSearchSuggestion = ttSuggestionsChildren[i].FindElement(desiredLocation);

                var stationName = stopSearchSuggestion.Text;

                if (stationName == endLocationStation)
                {
                    stopSearchSuggestion.Click();
                    break;
                }
            }
        }

        public void SubmitPlanMyJourney()
        {
            _driver.FindElement(planAJourneyButton).Click();
        }

        public string ConfirmJpResultFromLocation()
        {
            var jpResults = _driver.FindElement(jpResultContainer);

            var jpResultFromLocation = jpResults.FindElements(jpResultLocationWrapper)[0];
            var jpResultFromLocationText = jpResultFromLocation.FindElement(jpResultlocations).Text;
            return jpResultFromLocationText;
            

        }

        public string ConfirmJpResultToLocation()
        {
            var jpResults = _driver.FindElement(jpResultContainer);

            var jpResultToLocation = jpResults.FindElements(jpResultLocationWrapper)[1];
            var jpResultToLocationText = jpResultToLocation.FindElement(jpResultlocations).Text;

            return jpResultToLocationText;
        }

        public string ConfirmCyclingTime()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(cyclingAndWalkingJpOptions));

            var leftJourneyOptions = _driver.FindElement(cyclingAndWalkingJpOptions);

            var cyclingOption = leftJourneyOptions.FindElement(cyclingOptions);

            var cyclingHeader = cyclingOption.FindElement(cyclingWalkingHeader);

            ClassicAssert.AreEqual(cyclingHeader.Text, "Cycling");

            var cyclingJourneyInfo = cyclingOption.FindElement(JourneyInfo);

            var cycleTime = cyclingJourneyInfo.Text;

            return cycleTime;

            
        }

        public string ConfirmWalkingTime()
        {
            var leftJourneyOptions = _driver.FindElement(cyclingAndWalkingJpOptions);

            var walkingOption = leftJourneyOptions.FindElement(walkingOptions);

            var walkingHeader = walkingOption.FindElement(cyclingWalkingHeader);

            ClassicAssert.AreEqual(walkingHeader.Text, "Walking");

            var walkingJourneyInfo = walkingOption.FindElement(JourneyInfo);

            var walkTime = walkingJourneyInfo.Text;

            return walkTime;

            
        }

        public void ClickEditPreferences()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(editPreferenceContent));

            _driver.FindElement(editPreferenceButton).Click();

            
        }

        public void SelectLeastWalkingRoute()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(transportPreferenceOptions));

            var walkingShowMePreferences = _driver.FindElement(transportOptionContent);

            walkingShowMePreferences.FindElement(leastWalkingOption).Click();

        }

        public void UpdateJourney()
        {
            _driver.FindElement(updateJourneyButton).Click();
        }

        public IWebElement ValidateJourneyTime()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(updatedResultContainer));

            var journeyOption1 = _driver.FindElement(expandedResultContainer);

            var journeyOption1TimeBox = journeyOption1.FindElement(expandedResultTiming);

            Assert.That(journeyOption1TimeBox.Displayed);

            var journeyOption1Time = journeyOption1.FindElement(updateResultJourneyTime);
            return journeyOption1Time;

            
        }

        public void ClickViewDetailsButton()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(updatedResultContainer));

            var journeyOption1 = _driver.FindElement(expandedResultContainer);

            var journeyOption1Content = journeyOption1.FindElement(expandedJourneyDetails);

            journeyOption1Content.FindElement(viewDetailsButton).Click();
        }

        public void ObtainAccessInformation()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#option-1-content .journey-details .step-heading .access-information")));

            var journeyOption1 = _driver.FindElement(expandedResultContainer);
            var journeyOption1Content = journeyOption1.FindElement(expandedJourneyDetails);

            var accessInformation = journeyOption1Content.FindElements(By.CssSelector(".step-heading .access-information"))[1];

            var accessTooltipContainers = accessInformation.FindElements(By.TagName("a"));

            Actions actions = new Actions(_driver);

            for (var i = 0; i < accessTooltipContainers.Count; i++)
            {

                var accessTooltipContainer = accessTooltipContainers[i];
                actions.MoveToElement(accessTooltipContainer).Perform();

                var tooltip = accessTooltipContainer.FindElement(By.ClassName("tooltip"));

                var toolTipText = tooltip.Text;

            }

            Assert.That(accessTooltipContainers.Count.Equals(3));
        }

        public void FillFromLocation(string fromLocation = null)
        {
            if (!string.IsNullOrEmpty(fromLocation))
            {
                var fromMotherSpan = _driver.FindElement(fromLocationMotherSpan);
                var fromInputEl = fromMotherSpan.FindElement(fromInputLocator);
                fromInputEl.SendKeys(fromLocation);
            }
            
        }

        public void FillToLocation(string EndLocation = null) 
        {
            if (!string.IsNullOrEmpty(EndLocation)) 
            {
                var motherSpan = _driver.FindElement(toLocationMotherSpan);
                motherSpan.FindElement(toInputLocator).SendKeys(EndLocation);
            }
            
        }

        public string obtainJourneyHeader()
        {
           var jpHeader =  _driver.FindElement(journeyResultHeader).Text;
            return jpHeader;
        }

        public string ValidateInvalidJourneyResponse()
        {
            var errorResponse = _driver.FindElement(By.CssSelector(".ajax-response li.field-validation-error")).Text.Trim();
            return errorResponse;
        }

        public string GetFromFieldErrorMessage()
        {
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(fromFieldErrorMessage));
            var fromErrorMessage = _driver.FindElement(fromFieldErrorMessage).Text;
            return fromErrorMessage;
        }

        public string GetToFieldErrorMessage()
        {
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(toFieldErrorMessage));
            var ToErrorMessage = _driver.FindElement(toFieldErrorMessage).Text;
            return ToErrorMessage;
        }
    }
}
