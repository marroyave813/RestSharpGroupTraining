using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    class Pet
    {
        public long id { get; set; }
        public PetCategory category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<PetTag> tags { get; set; }
        public string status { get; set; }

        public Pet(int id, string name, PetCategory petCategories) //Constructor: is used everytime an object is instanciated
        {
            this.id = id;  
            this.name = name;
            this.category = petCategories;
        }
    }
}
