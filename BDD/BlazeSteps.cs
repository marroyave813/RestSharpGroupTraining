using FluentAssertions;
using RestSharp;
using RestSharpGroupTraining.Model.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharpGroupTraining
{
    [Binding]
    public sealed class BlazeSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        //Variables
        private readonly ScenarioContext _scenarioContext;
        //private string jsonData;
        private string sessionToken;
        BlazeUser blazeUser;

        //Restsharp components
        IRestClient restClient = new RestClient();
        IRestRequest request;
        IRestResponse response;

        public BlazeSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a random user with password ""(.*)""")]
        public void GivenARandomUserWithPassword(string password)
        {
            string username = "User" + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + "@gmail.com";
            blazeUser = new BlazeUser(username, password);
        }


        [Given(@"user ""(.*)"" with password ""(.*)""")]
        public void GivenUserWithPassword(string user, string password)
        {
            blazeUser = new BlazeUser(user, password);
        }

        [When(@"the user sign's up")]
        public void WhenTheUserSignSUp()
        {
            request = new RestRequest("https://api.demoblaze.com/signup");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(blazeUser);

            response = restClient.Post(request);
        }

        [When(@"the user logs in")]
        public void WhenTheUserLogsIn()
        {
            request = new RestRequest("https://api.demoblaze.com/login");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(blazeUser);

            response = restClient.Post(request);
        }

        [Then(@"the process completes wihtout errors")]
        public void ThenTheProcessCompletesWihtoutErrors()
        {
            response.StatusCode.Should().Be(200);
            response.Content.Should().NotContain("errorMessage");
        }

        [Then(@"An error with text ""(.*)"" shows")]
        public void ThenAnErrorWithTextShows(string errorMessage)
        {
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain(errorMessage);
        }

        [Then(@"A session token generates")]
        public void ThenASessionTokenGenerates()
        {
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain("Auth_token");
        }

        [Then(@"the token is valid for user ""(.*)""")]
        public void ThenTheTokenIsValidForUser(string user)
        {
            string[] responseSplit = response.Content.Split(':', ' ', '"');
            sessionToken = responseSplit[3];

            request = new RestRequest();

            request = new RestRequest("https://api.demoblaze.com/check");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new Token(sessionToken));
            IRestResponse<TokenItem> responseToken = restClient.Post<TokenItem>(request);

            responseToken.StatusCode.Should().Be(200);
            responseToken.Data.Item.token.Should().Contain(sessionToken);
            responseToken.Data.Item.username.Should().Contain(user);
        }


    }
}
