using SauceDemoTests.Utils;
using TechTalk.SpecFlow;

namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class CheckoutSteps : LoginFixture
    {
        [Given(@"I am logged in as a standard user")]
        public async Task GivenIAmLoggedInAsAStandardUser()
        {
            await PerformLogin("standard_user");
        }

        // You’ll soon add:
        // - [When("I add an item to the cart")]
        // - [And("I proceed to checkout")]
        // - [Then("I should see the order confirmation page")]
    }
}
