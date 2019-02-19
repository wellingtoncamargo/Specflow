using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_SpecFlow_API.BaseSteps
{
    public class Steps
    {
        private RestRequest _restRequest;
        private RestClient _restClient;


        public Steps()
        {
            _restRequest = new RestRequest();
        }

        public Steps SetResourse(string resource)
        {
            _restRequest.Resource = resource;
            return this;
        }

        public Steps SetMethod(Method method)
        {
            _restRequest.Method = method;
            return this;
        }

        public Steps AddHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _restRequest.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }
            return this;
        }

        public Steps AddJsonContent(object data)
        {
            _restRequest.RequestFormat = DataFormat.Json;
            _restRequest.AddHeader("Content-Type", "application/json");
            _restRequest.AddJsonBody(data);
            return this;
        }

        public Steps AddEtagHeader(string value)
        {
            _restRequest.AddHeader("If-None-Match", value);
            return this;
        }


        public Steps AddParameter(string name, object value)
        {
            _restRequest.AddParameter(name, value);
            return this;
        }

        public Steps AddParameters(IDictionary<string, object> parameters)
        {
            foreach (var item in parameters)
            {
                _restRequest.AddParameter(item.Key, item.Value);
            }
            return this;
        }

        public IRestResponse Execute()
        {
            try
            {
                _restClient = new RestClient("http://localhost:50983/");
                var response = _restClient.Execute(_restRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T Execute<T>()
        {
            _restClient = new RestClient("http://localhost:50983/");
            var response = _restClient.Execute(_restRequest);
            var data = JsonConvert.DeserializeObject<T>(response.Content);
            return data;
        }

    }
}
