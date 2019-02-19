using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDD_SpecFlow_API
{
    [Binding]
    public class TesteAPISteps 
    {
        private RestRequest _restRequest;
        private RestClient _restClient;
        private IRestResponse Res;
        private readonly IRestResponse p1;

        [Given(@"Eu salvo a variavel '(.*)' como '(.*)'")]
        public void GivenEuSalvoAVariavelComo(string p0, string p1)
        {
            // p0 = estado_info/nome

            var routes = p0.Split('/');

            JObject obs = JObject.Parse(Res.Content);
            
            //p1 = obs[$"{p0}"].ToString();
            //p2 = p1[
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

            p1 = result.ToString();
            Console.WriteLine(result.ToString());
        }


        [Given(@"I Run Scenario '(.*)'")]
        public void GivenIRunScenario(string p0)
        {
            Console.WriteLine(p0);
        }


        [Given(@"que eu use a API '(.*)'")]
        public void DadoQueEuUseAAPI(string p0)
        {
             _restClient = new RestClient(p0);
        }

        
        [Given(@"informo o CEP '(.*)'")]
        public void DadoInformoOCEP(string p0)
        {
            _restRequest = new RestRequest(p0, Method.GET) { RequestFormat = DataFormat.Json };
        }


        [When(@"realizar um GET")]
        public void QuandoRealizarUmGET()
        {
            Res = _restClient.Execute(_restRequest);
        }

        [When(@"informando o body '(.*)'")]
        public void WhenInformoOBody(string data)
        {
            _restRequest = new RestRequest();
            _restRequest.AddHeader("Content-Type", "application/json");
            _restRequest.AddJsonBody(data);
        }

        [When(@"realizo um POST em '(.*)'")]
        public void WhenRealizoUmPOSTEm(string p0)
        {
            _restRequest = new RestRequest(p0, Method.POST) { RequestFormat = DataFormat.Json };
            Res = _restClient.Execute(_restRequest);
        }

        [When(@"realizo um PUT em '(.*)'")]
        public void WhenRealizoUmPUTEm(string p0)
        {
            _restRequest = new RestRequest(p0, Method.PUT) { RequestFormat = DataFormat.Json };
            Res = _restClient.Execute(_restRequest);
        }

        [Then(@"retorno devera ser '(.*)'")]
        public void EntaoRetornoDeveraSer(string p0)
        {
            Assert.AreEqual(p0.ToString(), Res.StatusCode.ToString());
        }


        [Then(@"as informações solicitadas")]
        public void EntaoAsInformacoesSolicitadas()
        {
            if (Res.StatusDescription == "OK")
            {
                Console.WriteLine(Res.StatusDescription.ToString());
                JObject obs = JObject.Parse(Res.Content);
                Console.Write(obs.ToString());
                
            }
            else
            {
               // JObject obs = JObject.Parse(Res.Content);
                Console.Write(Res.StatusDescription.ToString());
            }
            
        }
    }
}
