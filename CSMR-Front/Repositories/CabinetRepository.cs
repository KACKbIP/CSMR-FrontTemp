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
    public class CabinetRepository : ICabinetInterface
    {
        private readonly string _apiUrl;
        public CabinetRepository(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }
        public ResponseClass<CabinetModel> GetCabinet(string login, string password, string token)
        {
            ResponseClass<CabinetModel> response = new ResponseClass<CabinetModel>();
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
                        httpClient.PostAsync(_apiUrl + $"GetCabinet", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<CabinetModel>>(responseContent);
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
                        httpClient.PostAsync(_apiUrl + $"GetQueryForAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<QueryModel>>>(responseContent);
                return response;
            }
        }
        public ResponseClass<List<TarifModel>> GetTarifs(RequestClass<dynamic> request)
        {
            ResponseClass<List<TarifModel>> response = new ResponseClass<List<TarifModel>>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetTarifs", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<TarifModel>>>(responseContent);
                return response;
            }
        }
        public ResponseClass<List<TarifAgentModel>> GetAgentTarifs(RequestClass<dynamic> request)
        {
            ResponseClass<List<TarifAgentModel>> response = new ResponseClass<List<TarifAgentModel>>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetAgentTarifs", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<TarifAgentModel>>>(responseContent);
                return response;
            }
        }
        public ResponseClass<bool> AddTarif(RequestClass<TarifModel> request)
        {
            ResponseClass<bool> response = new ResponseClass<bool>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"AddTarif", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<bool>>(responseContent);
                return response;
            }
        }
        public ResponseClass<bool> AddTarifAgent(RequestClass<TarifAgentModel> request)
        {
            ResponseClass<bool> response = new ResponseClass<bool>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"AddTarifAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<bool>>(responseContent);
                return response;
            }
        }

        public ResponseClass<List<TarifModel>> GetTarifByAgent(RequestClass<dynamic> request)
        {
            ResponseClass<List<TarifModel>> response = new ResponseClass<List<TarifModel>>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetTarifByAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<TarifModel>>>(responseContent);
                return response;
            }
        }

        public ResponseClass<bool> DeleteTarif(RequestClass<TarifModel> request)
        {
            ResponseClass<bool> response = new ResponseClass<bool>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"DeleteTarif", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<bool>> (responseContent);
                return response;
            }
        }
    }
}
