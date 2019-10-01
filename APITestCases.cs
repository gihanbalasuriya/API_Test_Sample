using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace API_Test_Sample
{
    [TestClass]
    public class APITestCases
    {
        private string basepath = "https://api.tmsandbox.co.nz/v1/Categories/";
        private IRestClient client;
        private RestRequest request;


        public APITestCases()
        {
            //Common test Setup
            client = new RestClient(basepath);
            //Creating the rest request with headers
            request = new RestRequest("{category}/Details.json", Method.GET);
            request.AddUrlSegment("category", 6328);
            request.AddQueryParameter("catalogue", "false");
            request.AddHeader("Content-Type", "application/json");

        }

        [TestMethod]
        public void TestOne()
        {
            //Trigger the request
            IRestResponse resp = client.Execute(request);       
            //Response to JSON
            JObject json = JObject.Parse(resp.Content);
            //Response to JSON
            string val = (string)json["Name"];
            //Verify the value
            Assert.AreEqual(val, "Badges");
        }



        [TestMethod]
        public void TestTwo()
        {
            //Trigger the request
            IRestResponse resp = client.Execute(request);
            //Response to JSON
            JObject json = JObject.Parse(resp.Content);
            //Response to JSON
            string val = (string)json["CanListClassifieds"];
            //Verify the value
            Assert.AreEqual(val, "False");

        }


        [TestMethod]
        public void TestThree()
        {
            //Trigger the request
            IRestResponse resp = client.Execute(request);
            //Response to JSON
            JObject json = JObject.Parse(resp.Content);
            //string tagline = "";
            //Loop thtough Charities
            foreach (var c in json["Charities"])
            {
                var description = (string)c["Description"];
                if (string.Equals(description, "Plunket"))
                {
                 String tagline = (string)c["Tagline"];
                    break;
                }
            }
            //Verify the value
            Assert.IsTrue(tagline.Contains("well child health services"));
        }
    }
}
