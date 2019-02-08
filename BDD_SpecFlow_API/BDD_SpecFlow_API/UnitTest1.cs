using System;
using System.Collections.Generic;
using BDD_SpecFlow_API.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;

namespace BDD_SpecFlow_API
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://api.postmon.com.br/v1/cep/07179260");
            var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
            var response = client.Execute(request);
            JObject obs = JObject.Parse(response.Content);
            Console.WriteLine(obs.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            var client = new RestClient("https://hml-api.portocred.com.br");
            var request = new RestRequest("/oauth/grant-code", Method.POST); //{RequestFormat = DataFormat.Json };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new {client_id="f9212173-e705-373b-a698-61923e378359",redirect_uri="http://localhost/" });
            
            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            JObject obs = JObject.Parse(response.StatusDescription.ToString());
            Console.WriteLine(obs);

        }

        [TestMethod]
        public void TestMethod3()
        {
            var client = new RestClient("https://hml-api.portocred.com.br");
            var request = new RestRequest("/oauth/grant-code", Method.POST); //{RequestFormat = DataFormat.Json };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Model.Posts() { client_id = "f9212173-e705-373b-a698-61923e378359", redirect_uri = "http://localhost/" });

            var response = client.Execute<Posts>(request);
            
            Console.WriteLine(response.Data+ "Campo vazio...");
            Console.WriteLine(response.Content);

        }
    }
}
