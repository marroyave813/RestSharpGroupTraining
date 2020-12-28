using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpGroupTraining.Model.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestSharpGroupTraining.RestGetEndPoint
{
    /// <summary>
    /// Functionalities to test:
    /// - Sign up (valid user, existing user, serialize, not serialize)
    /// - Login (correct login, login with non existing user, login with bad password) - grab the token
    /// - List of products (Get products and search for specific one (deserialize), for each category)
    /// - Add products to the cart
    /// - Place an order
    /// * Use serialization, deserialization, linq*, BDD*
    /// </summary>
    [TestClass]
    public class DemoBlazeTest
    {
        private string signUpUrl = "https://api.demoblaze.com/signup";
        private string loginUrl = "https://api.demoblaze.com/login";
        private string checkUrl = "https://api.demoblaze.com/check";
        private string getProductUrl = "https://api.demoblaze.com/bycat";
        private string addToCartUrl = "https://api.demoblaze.com/addtocart";
        private string viewCartUrl = "https://api.demoblaze.com/viewcart";
        private string deleteProduct = "https://api.demoblaze.com/deletecart";
        string username = "User" + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + "@gmail.com";
        string sessionToken;
        Product myProduct;
        
        [TestMethod]
        public void SignUpValidUser()
        {
            string jsonData = "{\"username\":\" "+ username +"\",\"password\":\"MTIzcGFzcw==\"}";

            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(signUpUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(jsonData);
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().NotContain("errorMessage");

            //Regular MSTest Assertion
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsFalse(response.Content.Contains("errorMessage"));

        }

        [TestMethod]
        public void SignUpValidUserSerialized()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(signUpUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new BlazeUser(username, "MTIzcGFzcw=="));
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().NotContain("errorMessage");

            //Regular MSTest Assertion
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsFalse(response.Content.Contains("errorMessage"));

        }

        [TestMethod]
        public void SignUpExistingUser()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(signUpUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new BlazeUser("test1122@hotmail.com", "MTIzcGFzcw =="));
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain("This user already exist.");

            //Regular MSTest Assertion
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("This user already exist."));

        }

        [TestMethod]
        public void LogInExistingUser()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(loginUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new BlazeUser("mauricioarroyave@gmail.com", "cGFzc3dvcmQx"));
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain("Auth_token");

            //Regular MSTest Assertion
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content.Contains("Auth_token"));

            string[] responseSplit = response.Content.Split(':',' ','"');
            sessionToken = responseSplit[3];      
        }

        [TestMethod]
        public void ValidateToken()
        {
            LogInExistingUser();

            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(checkUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new Token(sessionToken));
            IRestResponse<TokenItem> response = restClient.Post<TokenItem>(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Data.Item.token.Should().Contain(sessionToken);
            response.Data.Item.username.Should().Contain("mauricioarroyave@gmail.com");
        }

        [TestMethod]
        public void LogInNonExistingUser()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(loginUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(new BlazeUser("mauricioarroyave@yimail.com", "cGFzc3dvcmQx"));
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain("User does not exist.");
        }

        [TestMethod]
        public void GetProducts()
        {
            string jsonData = "{\"cat\":\"phone\"}";

            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(getProductUrl);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(jsonData);
            IRestResponse<ProductItem> response = restClient.Post<ProductItem>(request);
            List<Product> productList = response.Data.Items;

            Assert.AreEqual(200, (int)response.StatusCode);

            //Fluent Assertion
            //productList.Should().OnlyContain(x => x.cat.Equals("phone"));
           // productList.Should().OnlyContain(x => x.title.Equals("Samsung galaxy s6"));

            //MSTest Assertion
            Assert.IsTrue(productList.All(x => x.cat.Equals("phone")));
            Assert.IsTrue(productList.Any(x => x.title.Equals("Sony xperia z5")));

            myProduct = productList.Find(x => x.title.Equals("Sony xperia z5"));

        }

        [TestMethod]
        public void AddProductoToCart()
        {

            GetProducts();
            ValidateToken();

            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(addToCartUrl);

            //Code to serialize the product to add to cart
            BuyProduct product = new BuyProduct(sessionToken, myProduct.id);

            //Body to view the cart information
            string jsonBody = "{\"cookie\":\"user=" + product.id + "\",\"flag\":true}";

            //Request to add product to cart
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(product);
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);

            Assert.AreEqual(200, (int)response.StatusCode);

            //Request to view product in cart
            request.Resource = viewCartUrl;
            request.AddJsonBody(jsonBody);
            IRestResponse<CartItem> cartResponse = restClient.Post<CartItem>(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            cartResponse.Data.Items[0].prod_id.Should().Be(myProduct.id);
            cartResponse.Data.Items[0].id.Should().Be(product.id);

            Assert.AreEqual(200, (int)cartResponse.StatusCode);
            Assert.AreEqual(cartResponse.Data.Items[0].prod_id, myProduct.id);
            Assert.AreEqual(cartResponse.Data.Items[0].id, product.id);
        }

        [TestMethod]
        public void DeleteProduct()
        {

            AddProductoToCart();

            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest(deleteProduct);

            //Code to serialize the product to add to cart
            BuyProduct product = new BuyProduct(sessionToken, myProduct.id);

            //Body to view the cart information and confirmis empty
            string jsonBodyDelete = "{\"cookie\":\"user=" + sessionToken + "\",\"flag\":false}";

            //Request to add product in the cart
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddJsonBody(product);
            IRestResponse response = restClient.Post(request);

            //Fluent Assertion
            response.StatusCode.Should().Be(200);
            response.Content.Should().Contain("Item deleted");

            //Request to check empty elements in the cart
            IRestRequest requestView = new RestRequest(viewCartUrl);
            requestView.AddJsonBody(jsonBodyDelete);
            IRestResponse<CartItem> cartResponse = restClient.Post<CartItem>(requestView);

            //Fluent Assertion
            cartResponse.StatusCode.Should().Be(200);
            cartResponse.Data.Items.Count.Should().Be(0);
        }
    }
}
