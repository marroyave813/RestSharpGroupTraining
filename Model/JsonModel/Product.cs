using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
   public class Product
    {
        public string cat { get; set; }
        public string desc { get; set; }
        public int id { get; set; }
        public string img { get; set; }
        public double price { get; set; }
        public string title { get; set; }
    }
}
