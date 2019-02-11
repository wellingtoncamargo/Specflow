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
            var client = new RestClient("http://api.postmon.com.br/v1/cep");
            var request = new RestRequest("/8105010",Method.GET) { RequestFormat = DataFormat.Json };
            var response = client.Execute(request);
            if (response.StatusDescription != "OK" )
            {
                Console.WriteLine("Erro: 405 - "+ response.StatusCode.ToString());
            }
            else
            {
                JObject obs = JObject.Parse(response.Content);
                Console.WriteLine(obs.ToString());
            }
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            var client = new RestClient("https://secure.checkout.visa.com/public/73c67ad43170eb7b22cdfad007cd4");
            var request = new RestRequest("//public/73c67ad43170eb7b22cdfad007cd4", Method.POST); //{RequestFormat = DataFormat.Json };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody( "{'sensor_data':'7a74G7m23Vrp0o5c9056971.4-1,2,-94,-100,Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36,uaend,11891,20030107,pt-BR,Gecko,3,0,0,0,381309,8599888,1366,728,1366,768,0,0,1366,,cpen:0,i1:0,dm:0,cwen:0,non:1,opc:0,fc:0,sc:0,wrc:1,isc:0,vib:1,bat:1,x11:0,x12:1,8289,0.03998241819,774869299940.5,loc:-1,2,-94,-101,do_en,dm_en,t_en-1,2,-94,-105,-1,2,-94,-102,-1,2,-94,-108,-1,2,-94,-110,-1,2,-94,-117,-1,2,-94,-111,-1,2,-94,-109,-1,2,-94,-114,-1,2,-94,-103,-1,2,-94,-112,https://secure.checkout.visa.com/checkout-widget/gtm?apikey=7NYYPMUMU44XR7XRX09014Khook5fZQrw3Dck6x5Zbwh5p-ng&externalClientId=&externalProfileId=&parentUrl=https%3A%2F%2Fwww.saraiva.com.br%2Fcheckout%2Fcart%2F&locale=pt_BR&browserLocale=&countryCode=&allowEXO=false&allowCXO=false&buttonPosition=&postmessage=true&allowRXO=true&collectShipping=true-1,2,-94,-115,1,0,0,0,0,0,0,25,0,1549738599881,-999999,16578,0,0,2763,0,0,294,0,1,2,50,-1,-1,30261693-1,2,-94,-106,0,0-1,2,-94,-119,-1-1,2,-94,-122,0,0,0,0,1,0,0-1,2,-94,-123,-1,2,-94,-70,-1-1,2,-94,-80,94-1,2,-94,-116,6269318372-1,2,-94,-118,72272-1,2,-94,-121,;278;-1;0'}"
        );
            
            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            JObject obs = JObject.Parse(response.Content.ToString());
            Console.WriteLine(obs);

        }

        [TestMethod]
        public void TestMethod3()
        {
            var client = new RestClient("https://hml-api.portocred.com.br");
            var request = new RestRequest("/oauth/grant-code", Method.POST); //{RequestFormat = DataFormat.Json };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Posts() { client_id = "f9212173-e705-373b-a698-61923e378359", redirect_uri = "http://localhost/" });

            var response = client.Execute<Posts>(request);
            
            Console.WriteLine(response.Data+ "Campo vazio...");
            Console.WriteLine(response.Content);

        }
    }
}
