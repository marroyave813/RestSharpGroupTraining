using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class Root
    {
        [JsonProperty("post code")]
        public string postcode { get; set; }
        public string country { get; set; }
        [JsonProperty("country abbreviation")]
        public string countryabbreviation { get; set; }
        public List<Place> places { get; set; }

        public static Root Deserialize(IRestResponse response)
        {
            Root repoReponse = JsonConvert.DeserializeObject<Root>(response.Content.ToString());
            return repoReponse;
        }
    }

    
}
