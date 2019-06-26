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

namespace FrameWork.Steps.ApiSteps
{
    [Binding]
    public class TesteAPISteps
    {
        private RestRequest _restRequest;
        private RestClient _restClient;
        private IRestResponse Res;
        public int resp;
        private static ConcurrentDictionary<string, string> _returnVariables = new ConcurrentDictionary<string, string>();
        private readonly IConfigurationRoot _config;
        private ConexaoMongoDB MongoDB;
        private JObject _respMongo;
        private String _respDB;
        private DateTime date;
        private DateTime date2;
        private String _parceiroMongo, _cpfMongo;

        public TesteAPISteps()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
        }


        [Given(@"que eu use a BaseURL '(.*)'")]
        public void DadoQueEuUseABaseURL(string p0)
        {
            p0 = _config[p0.ToLower()].ToString();

            _restClient = new RestClient(p0);
        }

        [Given(@"Eu use a api '(.*)'")]
        public void GivenEuUseAApi(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest = new RestRequest(p0);

        }

        [Given(@"Eu use a rota '(.*)'")]
        public void GivenEuUseARota(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest = new RestRequest(p0.ToLower());
        }

        [Given(@"informo no Header '(.*)' com o valor '(.*)'")]
        public void GivenInformoNoHeader(string p0, string p1)
        {
            //foreach (var variable in _returnVariables)
            //{
            //    p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            //}
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }
            _restRequest.AddHeader(p0, p1);
        }

        [Given(@"informo o Parametro '(.*)' com o valor '(.*)'")]
        public void GivenInformoOParametro(string p0, string p1)
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

        //[Given(@"Eu salvo o valor '(.*)' como '(.*)'")]
        //public void GivenEuSalvoOValorComo(string p0, string p1)
        //{
        //    _returnVariables.TryAdd(p1, p0);

        //}

        //[Given(@"Eu consulte um parceiro '(.*)' no Mongo")]
        //public void GivenEuConsulteUmContratoNoMongo(string p0)
        //{
        //    foreach (var variable in _returnVariables)
        //    {
        //        p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
        //    }
        //    var data = new ConexaoMongoDB();
        //    var par = data._getParceiro(Convert.ToInt32(p0));
        //    _respDB = par;
        //    Console.WriteLine(JProperty.Parse(_respDB));
        //}


        [Given(@"salvo o CPF como '(.*)'")]
        public void GivenSalvoOCPFComo(string p0)
        {
            string valor = "";
            valor = _cpfMongo;
            _returnVariables.TryAdd(p0, valor);
            Console.WriteLine(valor);
        }


        [Given(@"Eu salvo a variavel '(.*)' como '(.*)'")]
        public void GivenEuSalvoAVariavelComo(string p0, string p1)
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
            Console.WriteLine(valor);
        }




        [Given(@"o valor das (.*) parcelas tem como '(.*)' do '(.*)'")]
        public void GivenOTotalDasParcelasE(string p0, string p1, string p2)
        {
            int p0int = 0;
            if (p0.Contains("{") && p0.Contains("}"))
            {
                var variableName = p0.Substring(p0.IndexOf("{") + 1, p0.IndexOf("}") - 1);
                p0int = Convert.ToInt32(_returnVariables.GetValueOrDefault(variableName));
            }
            else
            {
                p0int = Convert.ToInt32(p0);
            }

            var obs = JProperty.Parse(Res.Content);

            JArray parcelas = (JArray)obs["contratos"][0]["parcelas"];

            decimal total = 0;
            foreach (var item in parcelas.Take(p0int))
            {
                total += Decimal.Parse(item["valorSaldoParcela"].ToString());
            }

            Console.Write(total);
            _returnVariables.TryAdd(p1, total.ToString("n2").Replace(",", string.Empty));
            _returnVariables.TryAdd(p2, p0int.ToString());
        }

        private long GetSum(JArray arr, string avoid)
        {
            return arr.Sum((dynamic a) => (long)GetSum(a, avoid));
        }

        [Given(@"Eu executo o cenario '(.*)'")]
        public void GivenIRunScenario(string p0)
        {
            Console.WriteLine(p0);
        }



        [Given(@"informo o CEP '(.*)'")]
        public void DadoInformoOCEP(string p0)
        {
            _restRequest = new RestRequest(p0, Method.GET) { RequestFormat = DataFormat.Json };
        }


        [When(@"realizo um GET")]
        public void WhenRealizoUmGETEm()
        {
            _restRequest.Method = Method.GET;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"realizo um DELETE")]
        public void WhenRealizoUmDELETE()
        {
            _restRequest.Method = Method.DELETE;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"informando o body")]
        public void WhenInformandoOBody(String multilineText)
        {
            //_restRequest.AddHeader("Content-Type", "application/json");
            foreach (var variable in _returnVariables)
            {
                multilineText = multilineText.Replace($"{{{variable.Key}}}", variable.Value);
            }

            _restRequest.AddParameter(
                "application/json",
                multilineText,
                ParameterType.RequestBody);
            //_restRequest.AddJsonBody(multilineText);
            Console.WriteLine($"\n Body de saída \n {multilineText}");
        }

        [When(@"realizo um POST")]
        public void WhenRealizoUmPOSTEm()
        {
            _restRequest.Method = Method.POST;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"realizo um PUT")]
        public void WhenRealizoUmPUTEm()
        {
            _restRequest.Method = Method.PUT;
            _restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"aguardo (.*) segundos")]
        public void WhenAguardoSegundos(int p0)
        {
            int sec = p0 * 1000;
            Thread.Sleep(sec);
        }

        [Then(@"retorno devera ser (.*)")]
        public void EntaoRetornoDeveraSer(int p0)
        {
            Res = _restClient.Execute(_restRequest);
            if (Res.IsSuccessful)
            {
                resp = (int)Res.StatusCode;
                Assert.AreEqual(p0, resp);
            }
            else
            {
                Console.Write($"{((int)Res.StatusCode).ToString()} - {Res.StatusDescription.ToString()}");
                //dynamic obs = ;
                Console.Write(Res.Content.ToString());
                Assert.AreEqual(p0, (int)Res.StatusCode);
            }

        }


        [Then(@"as informações solicitadas")]
        public void EntaoAsInformacoesSolicitadas()
        {
            if (Res.IsSuccessful)
            {
                Console.WriteLine(Res.StatusDescription.ToString());
                dynamic obs = JProperty.Parse(Res.Content);
                Console.Write(obs.ToString());
            }
        }


        [Given(@"que eu conecte no Mongo")]
        public void GivenQueEuConecteNoMongo()
        {
            MongoDB = new ConexaoMongoDB();
        }



        [Then(@"retorno dever a '(.*)' ser igual a '(.*)'")]
        public void ThenRetornoDeverASerIgualA(string p0, string p1)
        {
            if (_respMongo == null)
            {
                Console.WriteLine($"{p0} não encontrada");
                Assert.AreNotEqual(_respMongo, p1);
            }
            else
            {
                Console.WriteLine(_respMongo[p0]);
                Assert.AreEqual(_respMongo[p0].ToString(), p1);
            }

        }

        [Given(@"que eu informo a data (.*)")]
        public void GivenQueEuInformoAData(String data)
        {
            var dataConvertida = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            date = dataConvertida;
        }

        [Given(@"que eu informo as datas (.*) e (.*)")]
        public void GivenQueEuInformoAsDatasE(String data, String data2)
        {
            var dataConvertida = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            date = dataConvertida;
            var dataConvertida2 = DateTime.ParseExact(data2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            date2 = dataConvertida2;
        }

        [When(@"consulto a SP a parcela '(.*)' do contrato '(.*)' com a data '(.*)'")]
        public void WhenConsultoASPAParcelaDoContratoComAData(string p0, string p1, string p2)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p1 = p1.Replace($"{{{variable.Key}}}", variable.Value);
            }
            foreach (var variable in _returnVariables)
            {
                p2 = p2.Replace($"{{{variable.Key}}}", variable.Value);
            }
            var consulta = new Consulta_SP();
            _respDB = consulta.ConsultaSaldoParcela(p1, p0, p2);
        }

        [Then(@"o retorno e")]
        public void ThenORetornoE()
        {
            Console.WriteLine(_respDB);
        }

        [Then(@"comparo se '(.*)' e '(.*)' sao iguais")]
        public void ThenComparoSeESaoIguais(string p0, string p1)
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

        [Then(@"comparo se '(.*)' e '(.*)' sao diferentes")]
        public void ThenComparoSeESaoDiferentes(string p0, string p1)
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

        [Then(@"comparo se o valor '(.*)' e '(.*)' sao equivalentes")]
        public void ThenComparoSeESaoEquivalentes(string p0, string p1)
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

            //100.503789 - 100.503788 = 0.000001 == 0

            Assert.True(AlmostEquals(p0decimal, p1decimal, 6));
        }

        [Then(@"Limpo as Variaveis")]
        public void ThenLimpoAsVariaveis()
        {
            _returnVariables.Clear();
        }


        private bool AlmostEquals(decimal float1, decimal float2, int precision)
        {
            return (Math.Round(float1 - float2, precision) == 0);
        }

    }
}
