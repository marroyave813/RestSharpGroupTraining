using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class BlazeUser
    {
        public string username { get; set; }
        public string password { get; set; }

        public string variable { get; set; }

        public BlazeUser(string user, string pass)
        {
            this.username = user;
            this.password = pass;
            this.variable = "test";
        }
    }
}
