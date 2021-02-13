using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using RestSharp;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class Status
    {
        public int code { get; set; }
        public string type { get; set; }
        public string message { get; set; }


        public static Status Deserialize(IRestResponse response)
        {
            Status repoReponse = JsonConvert.DeserializeObject<Status>(response.Content.ToString());
            return repoReponse;
        }

    }
}
