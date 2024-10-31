using TechTalk.SpecFlow;
using TFLJourneyPlanner.Pages;

namespace TFLJourneyPlanner.SetUp
{
    [Binding]
    public sealed class JpHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
      

        [BeforeScenario("@EditPreferences")]
        public void BeforeScenarioWithTag()
        {
            //_journeyPlannerPage.InputStartLocation("leic");
            //_journeyPlannerPage.InputEndLocation("cov");
            //_journeyPlannerPage.SubmitPlanMyJourney();
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}