using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpGroupTraining.Model.JsonModel
{
    public class BuyProduct
    {
        public string id { get; set; }
        public string cookie { get; set; }
        public int prod_id { get; set; }
        public bool flag { get; set; }

        public BuyProduct(string cookie, int productId)
        {
            this.id = "cd921cee-1f2b-9793-477b-5e0e5c32df14";
            this.cookie = cookie;
            this.prod_id = productId;
            this.flag = true;
        }
    }

    
}
