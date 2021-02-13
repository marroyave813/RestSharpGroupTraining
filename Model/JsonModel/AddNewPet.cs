using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

     public class AddNewPet
    {
        public int id { get; set; }
        public CategoryItems category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<TagItems> tags { get; set; }
        public string status { get; set; }



        public AddNewPet(string PetName, string TypeName , string status)
        {
            var randomNumber = new Random().Next(10000, 99999);
            this.id = randomNumber;
            CategoryItems category = new CategoryItems();
            TagItems items = new TagItems();
            List<TagItems> listItems = new List<TagItems>();
            IList<string> testValuesURL = new string[] { "a", "b", "c" };
            category.id = 0;
            category.name = PetName;
            this.category = category;
            this.name = TypeName;
            //this.photoUrls = (List<String>)testValuesURL;
            items.id = 0;
            items.name = "string";
            listItems.Add(items);
            this.tags = listItems;
            this.status = status;
        }

        public static AddNewPet Deserialize(IRestResponse response)
        {
            AddNewPet petList = JsonConvert.DeserializeObject<AddNewPet>(response.Content);
            return petList;
        }
    }
}


