using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSMR_Front.Repositories
{
    public class AgentRepository : IAgentInterface
    {
        private readonly string _apiUrl;
        public AgentRepository(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }
        public ResponseClass<List<AgentModel>> GetAgents(string login, string password, string token)
        {
                ResponseClass<List<AgentModel>> response = new ResponseClass<List<AgentModel>>();
                using (var httpClient = new HttpClient())
                {
                    var post = new
                    {
                        Login = login,
                        Password = password,
                        Token = token
                    };
                    var json = JsonConvert.SerializeObject(post);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var httpResponse =
                            httpClient.PostAsync(_apiUrl + $"GetAgents", data).Result;
                    string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ResponseClass<List<AgentModel>>>(responseContent);
                    return response;
                }
        }
        public ResponseClass<AgentResponse> AddAgent(RequestClass<AgentRequest> request)
        {
            ResponseClass<AgentResponse> response = new ResponseClass<AgentResponse>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"AddAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<AgentResponse>>(responseContent);
                return response;
            }
        }

        public ResponseClass<bool> AddQuery(RequestClass<QueryModel> request)
        {
            ResponseClass<bool> response = new ResponseClass<bool>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"AddQuery", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<bool>>(responseContent);
                return response;
            }
        }

        public ResponseClass<List<QueryModel>> GetQuery(RequestClass<QueryRequestData> request)
        {
            ResponseClass<List<QueryModel>> response = new ResponseClass<List<QueryModel>>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetQuery", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<QueryModel>>>(responseContent);
                return response;
            }
        }
        public ResponseClass<dynamic> UpdateAgent(RequestClass<UpdateAgent> request)
        {
            ResponseClass<dynamic> response = new ResponseClass<dynamic>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"UpdateAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<dynamic>>(responseContent);
                return response;
            }
        }
    }
}
