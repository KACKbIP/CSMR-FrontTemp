using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSMR_Front.Repositories
{  

    public class LoginRepository : ILoginInterface
    {
        private readonly string _apiUrl;
        public LoginRepository(IConfiguration configuration)
        {            
            _apiUrl = configuration["ApiUrl"];
        }
        public AccountModel GetAccount(LoginModel model)
        {
            ResponseClass<AccountModel> response = new ResponseClass<AccountModel>();
            using(var httpClient = new HttpClient())
            {
                var httpResponse =
                        httpClient.GetAsync(_apiUrl+ $"GetAccount?login={model.Login}&password={model.Password}").Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<AccountModel>>(responseContent);
                return response.Data;
            }
        }

        public ResponseClass<bool> ChangePassword(RequestClass<ChangeData> newPassword)
        {
            ResponseClass<bool> response = new ResponseClass<bool>();
            using (var httpClient = new HttpClient())
            {                
                var json = JsonConvert.SerializeObject(newPassword);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse =
                        httpClient.PostAsync(_apiUrl + $"ChangePassword", postData).Result;
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<ResponseClass<bool>>(responseContent);
                return response;
            }
        }
    }
}
