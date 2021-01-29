using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class RestBase
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;

        public RestBase(IConfiguration configuration, string key)
        {
            _configuration = configuration;
            var url = _configuration.GetValue<string>(key);
            _restClient = new RestClient(url);
        }

        public R Post<R, M>(M model, string controller)
        {
            var result = _restClient.Post(new JsonRequest<M, R>(controller, model));
            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception($"StatusCode: {result.StatusCode} message: {result.ErrorMessage}", result.ErrorException);

            return JsonConvert.DeserializeObject<R>(result.Content);
        }

        public R Get<R>(string controller)
        {
            var result = _restClient.Get<R>(new RestRequest(controller));
            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception($"StatusCode: {result.StatusCode} message: {result.ErrorMessage}", result.ErrorException);

            return JsonConvert.DeserializeObject<R>(result.Content);
        }
    }
}
