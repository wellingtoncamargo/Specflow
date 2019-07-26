using FrameWork.Util.Banco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
namespace FrameWork.Page
{
    public class Page_Object
    {
        private RestRequest _restRequest;
        private RestClient _restClient;
        private IRestResponse Res;
        public int resp;
        private static ConcurrentDictionary<string, string> _returnVariables = new ConcurrentDictionary<string, string>();
        //public static IConfigurationRoot _config;
        private ConexaoMongoDB MongoDB;
        private JObject _respMongo;
        private String _respDB;
        private DateTime date;
        private DateTime date2;
        private String _parceiroMongo, _cpfMongo;

        public static IConfigurationRoot _TesteAPISteps()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            return _config;
        }

        public RestClient _UseBaseURL(string p0)
        {
            p0 = _TesteAPISteps()[p0.ToLower()].ToString();

            _restClient = new RestClient(p0);
            return _restClient;
        }

        public RestRequest _UseApi(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest = new RestRequest(p0);
            return _restRequest;
        }

        public void _UseHeader(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest.AddHeader(p0, p1);
        }

        public void _UseParametro(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest.AddParameter(p0, p1, ParameterType.QueryString);
        }

        public void _Get()
        {
            _restRequest.Method = Method.GET;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        public void _Post()
        {
            _restRequest.Method = Method.POST;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        public void _Put()
        {
            _restRequest.Method = Method.PUT;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        public void _Delete()
        {
            _restRequest.Method = Method.DELETE;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        public void _PATCH()
        {
            _restRequest.Method = Method.PATCH;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        public void _ComBody(String multilineText)
        {
            foreach (var variable in _returnVariables)
            {
                multilineText = multilineText.Replace($"{{{variable.Key}}}", variable.Value);
            }

            _restRequest.AddParameter(
                "application/json",
                multilineText,
                ParameterType.RequestBody);
            Console.WriteLine($"\n Body de Utilizado \n {multilineText}");
        }

        public IRestResponse _ReturnCode(int p0)
        {
            Res = _restClient.Execute(_restRequest);
            if (Res.IsSuccessful)
            {
                resp = (int)Res.StatusCode;
                Assert.AreEqual(p0, resp);
            }
            else if ((int)Res.StatusCode < 500)
            {
                Console.Write($"{((int)Res.StatusCode).ToString()} - {Res.StatusDescription.ToString()}");
                //dynamic obs = ;
                Console.Write(JProperty.Parse(Res.Content));
                Assert.AreEqual(p0, (int)Res.StatusCode);
            }
            else
            {
                Assert.AreEqual(p0, (int)Res.StatusCode);
                Console.Write($"{((int)Res.StatusCode).ToString()} - {Res.StatusDescription.ToString()}");
                //dynamic obs = ;
                Console.Write(Res.Content);
            }
            return Res;
        }

        public void _ReturnText()
        {
            if (Res.IsSuccessful)
            {
                //return Res.StatusDescription.ToString();
                dynamic obs = JProperty.Parse(Res.Content);
                Console.WriteLine(obs.ToString());
            }
        }

        public String _SaveVariavelDynamic(string p0, string p1)
        {
            var routes = p0.Split('/');
            JToken obs;

            if (_respDB == null)
                obs = JProperty.Parse(Res.Content);
            else
                obs = JProperty.Parse(_respDB);

            JToken result = null;

            foreach (var item in routes)
            {
                if (Int32.TryParse(item, out int value))
                {
                    if (result == null)
                        result = obs[value];
                    else
                        result = result[value];
                }
                else
                {
                    if (result == null)
                        result = obs[item];
                    else
                        result = result[item];
                }
            }

            var valor = result.ToString();
            _returnVariables.TryAdd(p1, valor);
            return valor;
        }

        public void _ComparaSeIgual(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }

            Console.WriteLine($"{p0}, {p1}");
            Assert.AreEqual(p0, p1);
        }

        public void _ComparaSeDiferente(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }

            Console.WriteLine($"{p0}, {p1}");
            Assert.AreNotEqual(p0, p1);
        }

        public void _ComparaSeEquivalente(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }

            var p0decimal = Convert.ToDecimal(p0);
            var p1decimal = Convert.ToDecimal(p1);

            Console.WriteLine($"{p0}, {p1}");
            Assert.True(AlmostEquals(p0decimal, p1decimal, 6));
        }

        private bool AlmostEquals(decimal float1, decimal float2, int precision)
        {
            return (Math.Round(float1 - float2, precision) == 0);
        }

        public void _LimpaVariaveis()
        {
            _returnVariables.Clear();
        }
    }
}
