using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace BDD_SpecFlow_API
{
    [Binding]
    public class TesteAPISteps 
    {
        private RestRequest _restRequest;
        private RestClient _restClient;
        private IRestResponse Res;

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


        [Then(@"retorno devera ser '(.*)'")]
        public void EntaoRetornoDeveraSer(string p0)
        {
            Assert.AreEqual(p0.ToString(), Res.StatusCode.ToString());
        }


        [Then(@"as informações solicitadas")]
        public void EntaoAsInformacoesSolicitadas()
        {
            if (Res.StatusDescription != "OK")
            {
                Console.WriteLine(Res.StatusCode.ToString());
            }
            else
            {
                JObject obs = JObject.Parse(Res.Content);
                Console.Write(obs.ToString());
            }
            
        }
    }
}
