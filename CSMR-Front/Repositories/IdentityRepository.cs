using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSMR_Front.Repositories
{
    public class IdentityRepository : IIdentityInterface
    {
        private readonly string _apiUrl;
        private readonly string _certPass;
        public IdentityRepository(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
            _certPass = configuration["Cert Pass"];
        }
        public ResponseClass<List<ReportModel>> GetReport(string login, string password, string token, ReportData data)
        {
            ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
            using (var httpClient = new HttpClient())
            {
                var post = new
                {
                    Login = login,
                    Password = password,
                    Token = token,
                    Data = data
                };
                var json = JsonConvert.SerializeObject(post);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetReport", postData).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<ReportModel>>>(responseContent);
                return response;
            }
        }

        public ResponseClass<List<ReportModel>> GetReportByAgent(RequestClass<ReportData> request)
        {
            ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"GetReportByAgent", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<List<ReportModel>>>(responseContent);
                return response;
            }
        }
        public ResponseClass<decimal> Identity(RequestClass<IdentificationModel> request)
        {
            ResponseClass<decimal> response = new ResponseClass<decimal>();
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"Identification", data).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<decimal>>(responseContent);
                return response;
            }
        }
    }
}
