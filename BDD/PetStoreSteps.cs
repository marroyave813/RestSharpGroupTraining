using RestSharpGroupTraining.Model.JsonModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;
using RestSharpGroupTraining.Model.XmlModel;


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
	

		//Restsharp components

		AddNewPet addNewPet;
		static bool result = false;
		

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


		[Given(@"i'm gonna create a Pet with Pet Name: ""(.*)"", Type Name: ""(.*)"", photoUrls: ""(.*)"" and status: ""(.*)""")]
		public void GivenIMGonnaCreateAPetWithPetNameTypeNamePhotoUrlsAndStatus(string PetName, string TypeName, string photoUrls, string status)
		{
			addNewPet = new AddNewPet(PetName, TypeName, status);
		}

		[When(@"i add the Pet to the shelter")]
		public void WhenIAddThePetToTheShelter()
		{
			request = new RestRequest("https://petstore.swagger.io/v2/pet");
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*/*");
			request.AddJsonBody(addNewPet);
			IRestResponse<AddNewPet> response = restClient.Post<AddNewPet>(request);
			Assert.AreEqual(200, (int)response.StatusCode);
			Assert.IsFalse(response.Content.Contains("errorMessage"));
		}


		[Then(@"the Pet is now present in the shelter with Name: ""(.*)""")]
		public void ThenThePetIsNowPresentInTheShelterWithName(string expectedName)
		{
			restClient = new RestClient();
			request = new RestRequest("https://petstore.swagger.io/v2/pet/" + addNewPet.id);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*/*");
			response = restClient.Get(request);

			AddNewPet Pets = AddNewPet.Deserialize(response);

			Assert.AreEqual(200, (int)response.StatusCode);
			Assert.IsFalse(response.Content.Contains("error") || response.Content.Contains("Pet not found"));

			string petResponse = response.Content.Substring(6, 5);

			if (petResponse == addNewPet.id.ToString() && Pets.category.name == expectedName)
			{
				result = true;
			}
			Assert.AreEqual(true, result);
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
