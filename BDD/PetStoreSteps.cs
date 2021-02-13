using RestSharpGroupTraining.Model.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using RestSharp;
using FluentAssertions;
using RestSharpGroupTraining.Model.XmlModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RestSharpGroupTraining.BDD
{

	// For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef


    [Binding]
	public sealed class PetStoreSteps
	{

		//Variables
		//private readonly ScenarioContext _scenarioContext;
		private string jsonData;
		private string sessionToken;


		//Restsharp components
		IRestClient restClient = new RestClient();
		IRestRequest request;
		IRestResponse response;


		Pet petobject; // Declare the object Public so we can use it on every method

		// For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

		private readonly ScenarioContext _scenarioContext;

		public PetStoreSteps(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
		}

		[Given("the first number is (.*)")]
		public void GivenTheFirstNumberIs(int number)
		{
			//TODO: implement arrange (precondition) logic
			// For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata 
			// To use the multiline text or the table argument of the scenario,
			// additional string/Table parameters can be defined on the step definition
			// method. 

			_scenarioContext.Pending();
		}

		[Given("the second number is (.*)")]
		public void GivenTheSecondNumberIs(int number)
		{
			//TODO: implement arrange (precondition) logic
			// For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata 
			// To use the multiline text or the table argument of the scenario,
			// additional string/Table parameters can be defined on the step definition
			// method. 

			_scenarioContext.Pending();
		}

		
		[Given(@"a pet with an (.*) that needs to be updated")]
		public void GivenAPetWithAnThatNeedsToBeUpdated(int identificacion)
		{
			petobject = new Pet(identificacion, "", null);
		}



		[When("the two numbers are added")]
		public void WhenTheTwoNumbersAreAdded()
		{
			//TODO: implement act (action) logic

			_scenarioContext.Pending();
		}

		[When(@"the user updates the category with (.*) and (.*)")]
		public void WhenTheUserUpdatesTheCategory(string name, string status)
		{
			//TODO: implement act (action) logic
		
			request = new RestRequest("https://petstore.swagger.io/v2/pet/"+petobject.id);
			request.AddHeader("accept", "application/json");
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("name", name);
			request.AddParameter("status", status);
			response = restClient.Post(request); //requesting response

		}


		[Then(@"the result should be ok")]
		public void ThenTheResultShouldBeOk()
		{
			Status data = Status.Deserialize(response);
			Assert.AreEqual(200, (int)response.StatusCode);
			Assert.AreEqual(200, (int)data.code);
			Assert.AreEqual("unknown", (string)data.type);
			Assert.AreEqual("1", (string)data.message);
						
		}







	}
}
