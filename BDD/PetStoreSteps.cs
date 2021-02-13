using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharpGroupTraining.Model.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharpGroupTraining.BDD
{
	[Binding]
	public sealed class PetStoreSteps
	{
		// For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

		private readonly ScenarioContext _scenarioContext;
		string status;

		//Restsharp components
		IRestClient restClient = new RestClient();
		IRestRequest request;
		IRestResponse response;

		public PetStoreSteps(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
		}

		[Given(@"pets with status (.*)")]
		public void GivenPetsWithStatus(string petStatus)
		{
			status = petStatus;

			Pet mascota = new Pet(12, "pet", "active");

			Category cat = new Category();
			cat.name = "cat 1";

			mascota.category = cat;
		}

		[Given(@"pets with no status")]
		public void GivenPetsWithNoStatus()
		{
			status = "";
		}


		[When(@"user search with status")]
		public void WhenUserSearchWithStatus()
		{
			request = new RestRequest("https://petstore.swagger.io/v2/pet/findByStatus?status="+status);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*/*");

			response = restClient.Get(request);
		}

		[Then(@"process is executed succesfully")]
		public void ThenProcessIsExecutedSuccesfully()
		{
			response.StatusCode.Should().Be(200);
		}

		[Then(@"process is executed with errors")]
		public void ThenProcessIsExecutedWithErrors()
		{
			response.StatusCode.Should().Be(400);
		}


		[Then(@"a list of pets with the selected status")]
		public void ThenAListOfPetsWithTheSelectedStatus()
		{
		   Pet.Deserialize(response).Should().OnlyContain(x => x.status.Equals(status));
		}

		[Then(@"an error message with text ""(.*)"" shows")]
		public void ThenAnErrorMessageWithTextShows(string errorMessage)
		{
			response.Content.Should().Be(errorMessage);
		}

	}
}
