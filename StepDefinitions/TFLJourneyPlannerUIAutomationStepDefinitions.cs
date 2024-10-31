using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TFLJourneyPlanner.Pages;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text.RegularExpressions;

namespace TFLJourneyPlanner.StepDefinitions
{
    [Binding]
    public class TFLJourneyPlannerUIAutomationStepDefinitions
    {
        JourneyPlannerPage _journeyPlannerPage;
        IWebDriver _driver;
       public TFLJourneyPlannerUIAutomationStepDefinitions(JourneyPlannerPage journeyPlannerPage,IWebDriver driver) 
        {
            _journeyPlannerPage = journeyPlannerPage;
            _driver = driver;
        }

        public void BeforeScenarioEditPreferences(string start, string end)
        {
            _journeyPlannerPage.InputDynamicStartLocation("leic",start);
            _journeyPlannerPage.InputDynamicEndLocation("cov", end);
            _journeyPlannerPage.SubmitPlanMyJourney();
        }

        public void BeforeScenarioViewDetails(string start, string end)
        {
            _journeyPlannerPage.InputDynamicStartLocation("leic", start);
            _journeyPlannerPage.InputDynamicEndLocation("cov", end);
            _journeyPlannerPage.SubmitPlanMyJourney();
            _journeyPlannerPage.ClickEditPreferences();
            _journeyPlannerPage.SelectLeastWalkingRoute();
            _journeyPlannerPage.UpdateJourney();
        }

        [When(@"a user inputs the start ""([^""]*)"" and destination location ""([^""]*)"" of their journey")]
        public void WhenAUserInputsTheStartAndDestinationLocationOfTheirJourney(string Start, string End)
        {
            _journeyPlannerPage.InputDynamicStartLocation("leic", Start);
            _journeyPlannerPage.InputDynamicEndLocation("cov",End);
        }


        [When(@"the user clicks on the plan my journey button")]
        public void WhenTheUserClicksOnThePlanMyJourneyButton()
        {
            _journeyPlannerPage.SubmitPlanMyJourney();
        }



        [Then(@"the journey results should be visible with ""([^""]*)"" to ""([^""]*)""")]
        public void ThenTheJourneyResultsShouldBeVisibleWithTo(string ExpectedStartLocation, string ExpectedEndLocation)
        {
            var actualJpResultfromLocation = _journeyPlannerPage.ConfirmJpResultFromLocation();
            Assert.That(actualJpResultfromLocation.Equals(ExpectedStartLocation));

            var actualJpResultToLocation = _journeyPlannerPage.ConfirmJpResultToLocation();
            Assert.That(actualJpResultToLocation.Equals(ExpectedEndLocation));
        }


        //[Then(@"the journey results should be visible")]
        //public void ThenTheJourneyResultsShouldBeVisible()
        //{
        //   var jpResultfromLocation =  _journeyPlannerPage.ConfirmJpResultFromLocation();
        //    Assert.That(jpResultfromLocation.Equals("Leicester Square Underground Station"));

        //    var jpResultToLocation = _journeyPlannerPage.ConfirmJpResultToLocation();
        //    Assert.That(jpResultToLocation.Equals("Covent Garden Underground Station"));
        //}

        [Then(@"the walking and cyling times should be visible")]
        public void ThenTheWalkingAndCylingTimesShouldBeVisible()
        {
            // Define the pattern to match a number followed by "mins"
            string pattern = @"^\d+mins$"; 

            var cycleTime = _journeyPlannerPage.ConfirmCyclingTime();
            ClassicAssert.IsTrue(Regex.IsMatch(cycleTime, pattern));

           //ClassicAssert.AreEqual(cycleTime, "1mins");

            var walkTime = _journeyPlannerPage.ConfirmWalkingTime();
            ClassicAssert.IsTrue(Regex.IsMatch(walkTime, pattern));
            //ClassicAssert.AreEqual(walkTime, "6mins");
        }

        [When(@"a valid journey has been planned from ""([^""]*)"" to ""([^""]*)""")]
        public void WhenAValidJourneyHasBeenPlannedFromTo(string StartLocationStation, string EndLocationStation)
        {
            BeforeScenarioEditPreferences(StartLocationStation, EndLocationStation);
        }

        [When(@"the user clicks Edit preferences")]
        public void WhenTheUserClicksEditPreferences()
        {
            _journeyPlannerPage.ClickEditPreferences();
        }

        [When(@"select the route preference for least walking")]
        public void WhenSelectTheRoutePreferenceForLeastWalking()
        {
            _journeyPlannerPage.SelectLeastWalkingRoute();
        }

        [When(@"update the journey plan")]
        public void WhenUpdateTheJourneyPlan()
        {
            _journeyPlannerPage.UpdateJourney();
        }

        [Then(@"the updated journey results should be visible and the journey time should be validated according to the ""([^""]*)"" preference")]
        public void ThenTheUpdatedJourneyResultsShouldBeVisibleAndTheJourneyTimeShouldBeValidatedAccordingToThePreference(string p0)
        {
            var displayedJourneyTime = _journeyPlannerPage.ValidateJourneyTime();
            Assert.That(displayedJourneyTime.Displayed);
        }

        [Given(@"a valid journey has been planned from ""([^""]*)"" to ""([^""]*)"" with the least walking preference")]
        public void GivenAValidJourneyHasBeenPlannedWithTheLeastWalkingPreference(string startStation, string endStation)
        {
            BeforeScenarioViewDetails(startStation, endStation);
        }

        [When(@"I click on ""([^""]*)"" for the journey")]
        public void WhenIClickOnForTheJourney(string p0)
        {
            _journeyPlannerPage.ClickViewDetailsButton();
        }

        [Then(@"I should see complete access information for Covent Garden Underground Station")]
        public void ThenIShouldSeeCompleteAccessInformationForCoventGardenUndergroundStation()
        {
            _journeyPlannerPage.ObtainAccessInformation();
        }

        [When(@"a user fill-in From and To field with Invalid location data")]
        public void WhenIFill_InFromAndToFieldWithInvalidLocationData()
        {
            _journeyPlannerPage.FillFromLocation("*****");
            _journeyPlannerPage.FillToLocation("!!!!!");
        }

        [When(@"clicks on Plan a journey button")]
        public void WhenIClickOnPlanAJourneyButton()
        {
            _journeyPlannerPage.SubmitPlanMyJourney();
        }

        [Then(@"the result page Journey results Must be displayed")]
        public void ThenTheResultPageJourneyResultsMustBeDisplayed()
        {
           var jpHeaderText =  _journeyPlannerPage.obtainJourneyHeader();
            Assert.That(jpHeaderText.Equals("Journey results"));
        }

        [Then(@"the result ""([^""]*)"" must be displayed")]
        public void ThenTheResultMustBeDisplayed(string errorMessage)
        {
            var errorResponse = _journeyPlannerPage.ValidateInvalidJourneyResponse();
            Assert.That(errorResponse.Equals(errorMessage));
        }

        [When(@"a user leaves the from field blank")]
        public void WhenAUserLeavesTheFromfieldBlank()
        {
            _journeyPlannerPage.FillFromLocation();


        }

        [When(@"a user leaves the to field blank")]
        public void WhenAUserLeavesTheTofieldBlank()
        {
            _journeyPlannerPage.FillToLocation();
        }

        [Then(@"an error message (.*) should be displayed")]
        public void ThenAnErrorMessageTheFromFieldIsRequired_ShouldBeDisplayed(string expectedErrorMessage)
        {
            string actualErrormessage = string.Empty;

            if (expectedErrorMessage.Equals("The From field is required."))
            {
                actualErrormessage = _journeyPlannerPage.GetFromFieldErrorMessage();
            }
            else if (expectedErrorMessage.Equals("The To field is required."))
            {
                actualErrormessage = _journeyPlannerPage.GetToFieldErrorMessage();
            }

            ClassicAssert.IsTrue(actualErrormessage.Equals(expectedErrorMessage));
        }




    }
}
