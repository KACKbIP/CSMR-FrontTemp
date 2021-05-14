using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        private readonly ICabinetInterface _repository;
        public CabinetController(ICabinetInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            var claims = User.Claims.ToList();
            ResponseClass<CabinetModel> cabinet = _repository.GetCabinet(claims[0].Value, claims[2].Value, claims[1].Value);
            RequestClass<dynamic> request = new RequestClass<dynamic>();
            request.Login = claims[0].Value;
            request.Token = claims[1].Value;
            request.Password = claims[2].Value;
            ResponseClass<List<TarifModel>> requestTarif = _repository.GetTarifByAgent(request); 
            if (cabinet.Code != 0)
            {
                ViewBag.Message = "Ошибка при получении информации по кабинету";
                return View();
            }
            else
            {
                ViewBag.Data = requestTarif.Data;
                return View(cabinet.Data);
            }
        }
        public IActionResult GetQuery(string from, string to)
        {
            try
            {
                DateTime fromDate = DateTime.Today;
                DateTime toDate = DateTime.Now;
                if (from != null || to != null)
                {
                    fromDate = Convert.ToDateTime(from);
                    toDate = Convert.ToDateTime(to);
                }
                ViewBag.From = fromDate.ToString("dd.MM.yyyy");
                ViewBag.To = toDate.ToString("dd.MM.yyyy");

                var claims = User.Claims.ToList();
                RequestClass<QueryRequestData> request = new RequestClass<QueryRequestData>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new QueryRequestData
                {
                    From = fromDate,
                    To = toDate
                };


                ResponseClass<List<QueryModel>> response = _repository.GetQuery(request);
                if (response.Code != 0)
                {
                    ViewBag.Message = "Не удалось получить отчет";
                    return View();
                }
                else
                {
                    return View(response.Data);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        public FileResult GetFile(string fileBite, string fileName, string fileType)
        {
            return File(Convert.FromBase64String(fileBite), fileType, fileName);
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult Tarif()
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<dynamic> request = new RequestClass<dynamic>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                ResponseClass<List<TarifModel>> response = _repository.GetTarifs(request);
                ResponseClass<List<TarifAgentModel>> responseAgent = _repository.GetAgentTarifs(request);
                if (response.Code != 0 || responseAgent.Code != 0)
                {
                    ViewBag.Message = "Ошибка при получении информации по тарифам";
                    return View();
                }
                else
                {
                    ViewBag.Data = responseAgent.Data;
                    return View(response.Data);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult AddTarif()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public string AddTarif(string nameTarif, int queryCount)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<TarifModel> request = new RequestClass<TarifModel>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new TarifModel
                {
                    Name = nameTarif,
                    QueryCount = queryCount
                };
                ResponseClass<bool> response = _repository.AddTarif(request);
                if (response.Code != 0)
                {
                    return "Ошибка при добавлении тарифа";
                }
                else
                {
                    return "true";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult TarifList()
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<dynamic> request = new RequestClass<dynamic>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                ResponseClass<List<TarifModel>> response = _repository.GetTarifs(request);
                if (response.Code != 0)
                {
                    ViewBag.Message = "Ошибка при получении информации по агентам";
                    return View();
                }
                else
                {
                    return View(response.Data);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult AddTarifAgent()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public string AddTarifAgent(int agent, int tarif, int sumTarif)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<TarifAgentModel> request = new RequestClass<TarifAgentModel>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new TarifAgentModel
                {
                    AgentId = agent,
                    TarifId = tarif,
                    TarifSum = sumTarif
                };
                ResponseClass<bool> response = _repository.AddTarifAgent(request);
                if (response.Code != 0)
                {
                    return "Ошибка при добавлении тарифа";
                }
                else
                {
                    return "true";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public string ChangeTarifSum(int id, int sum)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<TarifAgentModel> request = new RequestClass<TarifAgentModel>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new TarifAgentModel
                {
                    Id = id,
                    TarifSum = sum
                };
                ResponseClass<bool> response = _repository.AddTarifAgent(request);
                if (response.Code != 0)
                {
                    return "Ошибка при изменении суммы тарифа";
                }
                else
                {
                    return "true";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public string DeleteTarif(int id)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<TarifModel> request = new RequestClass<TarifModel>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new TarifModel
                {
                    Id = id
                };
                ResponseClass<bool> response = _repository.DeleteTarif(request);
                if (response.Code != 0)
                {
                    return "Ошибка при удалении тарифа";
                }
                else
                {
                    return "true";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
