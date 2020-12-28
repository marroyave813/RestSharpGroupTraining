using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpGroupTraining.Model.JsonModel;
using RestSharpGroupTraining.Model.XmlModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.RestGetEndPoint
{
    [TestClass]
    public class TestGetEndpoint
    {
        private string getUrl = "https://api.zippopotam.us/us/90210";
        private string getUrlXml = "https://httpbin.org/xml";


        [TestMethod]
        public void TestGetUsingRestSharp()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            IRestResponse restResponse = restClient.Get(restRequest);
            Console.WriteLine(restResponse.IsSuccessful);
            Console.WriteLine(restResponse.StatusCode);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content " + restResponse.Content);
            }

        }

        [TestMethod]
        public void TestGetWithJson_DeserializeRS()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<Root> restResponse = restClient.Get<Root>(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content - Country " + restResponse.Data.country + " - Country Abbreviation " + restResponse.Data.countryabbreviation + " - " + restResponse.Data.postcode);
            }

        }

        [TestMethod]
        public void TestGetWithJson_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse restResponse = restClient.Get(restRequest);
            Root data = Root.Deserialize(restResponse);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content - Country " + data.country + " - Country Abbreviation " + data.countryabbreviation + " - " + data.postcode);
            }

        }

        [TestMethod]
        public void TestGetWithXml_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrlXml);
            restRequest.AddHeader("Accept", "application/xml");

            var dotNetXmlDeserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();

            //IRestResponse<Slideshow> restResponse = restClient.Get<Slideshow>(restRequest);
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Slideshow data = dotNetXmlDeserializer.Deserialize<Slideshow>(restResponse);
                Console.WriteLine("Size of Slides " + data.Slide.Count + " - Items " + data.Slide[1].Item.Count);
            }

        }
    }
}
