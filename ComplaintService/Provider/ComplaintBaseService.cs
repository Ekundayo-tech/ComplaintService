using ComplaintService.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace ComplaintService.Provider 
{
    public interface IComplaintBaseService : IHttpclientHelper
    {
    }

    public class ComplaintBaseService : HttpclientHelper, IComplaintBaseService
    {
        private readonly IConfiguration _config;

        public ComplaintBaseService(IConfiguration config, IHttpClientFactory httpClient) : base(httpClient)
        {
            _config = config;

            base.Headers.Add("Accept", _config["ComplaintConfig:application/json"]);
            base.Headers.Add("Authorization", "Basic " + _config["ComplaintConfig:token"]);
        }

        public override Uri BaseUrl => new Uri(_config["ComplaintConfig:BaseUrl"]);
    }
}
