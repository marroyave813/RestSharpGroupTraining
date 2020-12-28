using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class ResponseToken
    {
        public int expiration { get; set; }
        public string token { get; set; }
        public string username { get; set; }
    }
}
