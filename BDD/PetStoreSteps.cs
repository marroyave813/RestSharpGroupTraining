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
		string status;

		//Restsharp components
		AddNewPet addNewPet;
		static bool result = false;
		

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

		[Given(@"a pet with an (.*) that needs to be updated")]
		public void GivenAPetWithAnThatNeedsToBeUpdated(int identificacion)
		{
			petobject = new Pet(identificacion, "", null);
		}



		[When("the two numbers are added")]
		public void WhenTheTwoNumbersAreAdded()
		{
			response.StatusCode.Should().Be(200);
		}

		[Then(@"process is executed with errors")]
		public void ThenProcessIsExecutedWithErrors()
		{
			response.StatusCode.Should().Be(400);
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

        [Then(@"a list of pets with the selected status")]
		public void ThenAListOfPetsWithTheSelectedStatus()
		{
			List<Pet> mascotas = Pet.Deserialize(response);

			if (mascotas.Count>0)
			{
				mascotas.Should().OnlyContain(x => x.status.Equals(status));
			}
		}
		
		[Then(@"an error message with text ""(.*)"" shows")]
		public void ThenAnErrorMessageWithTextShows(string errorMessage)
		{
			response.Content.Should().Be(errorMessage);
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
