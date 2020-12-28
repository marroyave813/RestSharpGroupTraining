using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class Token
    {
        public string token { get; set; }

        public Token(string sessionToken)
        {
            this.token = sessionToken;
        }
    }
}
