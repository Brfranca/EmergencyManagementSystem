using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Services
{
    public class RestBase<TModel> : IRestBase<TModel> where TModel : class
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;
        private readonly string _controller;
        public RestBase(IConfiguration configuration, string key, string controller)
        {
            _configuration = configuration;
            var url = _configuration.GetValue<string>(key);
            _restClient = new RestClient(url);
            _controller = controller;
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

        public Result Register(TModel model)
        {
            return Post<Result, TModel>(model, $"{_controller}/Register");
        }

        public Result Delete(TModel model)
        {
            return Post<Result, TModel>(model, $"{_controller}/Delete");
        }

        public Result<TModel> Find(IFilter model)
        {
            return Post<Result<TModel>, IFilter>(model, $"{_controller}/Find");
        }

        public Result Update(TModel model)
        {
            return Post<Result, TModel>(model, $"{_controller}/Update");
        }

        public PagedList<TModel> FindPaginated(IFilter filter)
        {
            var result = Post<PaginationModel<TModel>, IFilter>(filter, $"{_controller}/FindPaginated");
            return new PagedList<TModel>(result.DataPagination, result.Models);
        }
    }
}
