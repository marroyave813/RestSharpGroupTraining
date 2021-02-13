using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
	public class Pet
	{
        public object id { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }

        public Pet (object id, string name, string status)
		{
            this.id = id;
            this.name = name;
            this.status = status;
		}

        public static List<Pet> Deserialize(IRestResponse response)
        {
            List<Pet> petList = JsonConvert.DeserializeObject<List<Pet>>(response.Content);
            return petList;
        }
    }
}

