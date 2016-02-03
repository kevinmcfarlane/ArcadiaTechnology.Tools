using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ArcadiaTechnology.Tools.Specs
{
    [Binding]
    public class ValidEmailAddressSteps
    {
        private string Email { get; set; }

        [When(@"I enter ""(.*)""")]
        public void ValidEmailInUsername(string email)
        {
            Email = email;
        }

        [Then(@"the email address should be valid")]
        public void ThenTheEmailAddressShouldBeValid()
        {
            Assert.IsTrue(ValidationTool.IsValidEmail(Email));
        }
    }
}