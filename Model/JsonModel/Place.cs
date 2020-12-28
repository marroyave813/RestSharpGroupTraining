using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class Place
    {
        [JsonProperty("place name")]
        public string placename { get; set; }
        public string longitude { get; set; }
        public string state { get; set; }
        [JsonProperty("state abbreviation")]
        public string stateabbreviation { get; set; }
        public string latitude { get; set; }
    }
}
