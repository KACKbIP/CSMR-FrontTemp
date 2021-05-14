using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Controllers
{
    [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
    public class AgentController : Controller
    {
        private readonly IAgentInterface _repository;
        public AgentController(IAgentInterface repository)
        {
            this._repository = repository;
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult GetAgents()
        {
            try
            {
                var claims = User.Claims.ToList();
                ResponseClass<List<AgentModel>> response = _repository.GetAgents(claims[0].Value, claims[2].Value, claims[1].Value);
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
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult AddAgent()
        {
            return View();
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult AddAgentMethod(string account, string name, bool isTest)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<AgentRequest> request = new RequestClass<AgentRequest>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new AgentRequest
                {
                    Account = account,
                    Name = name,
                    IsTest = isTest
                };


                ResponseClass<AgentResponse> response = _repository.AddAgent(request);
                if (response.Code != 0)
                {
                    ViewBag.Message = "Не удалось добавить агента";
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
        public IActionResult AddQuery()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult AddQuery(int agent, int count, IFormFile file)
        {
            try
            {
                if (agent == 0 || agent == null)
                    throw new Exception("Выберите агента");
                
                var claims = User.Claims.ToList();
                RequestClass<QueryModel> request = new RequestClass<QueryModel>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;

                if (file != null)
                {
                    string fileByte;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        fileByte = Convert.ToBase64String(fileBytes);
                    }
                    request.Data = new QueryModel
                    {
                        AgentId = agent,
                        QueryCount = count,
                        FileName = file.FileName,
                        FileType = file.ContentType,
                        FileBite = fileByte
                    };
                }
                else
                {
                    request.Data = new QueryModel
                    {
                        AgentId = agent,
                        QueryCount = count
                    };
                }

                ResponseClass<bool> response = _repository.AddQuery(request);
                if (response.Code != 0)
                {
                    ViewBag.Message = "Не удалось начислить запросы агенту";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Успешно начислены запросы агенту";
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
        public IActionResult GetQuery(string from, string to, int agentId = 0)
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
                    AgentId = agentId,
                    From = fromDate,
                    To = toDate
                };


                ResponseClass<List<QueryModel>> response = _repository.GetQuery(request);
                if (response.Code != 0)
                {
                    ViewBag.Message = "Не удалось добавить агента";
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
        public FileResult GetFile(string fileBite, string fileName, string fileType)
        {
            return File(Convert.FromBase64String(fileBite), fileType, fileName);
        }
        [Authorize(Roles = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult List()
        {
            try
            {
                var claims = User.Claims.ToList();
                ResponseClass<List<AgentModel>> response = _repository.GetAgents(claims[0].Value, claims[2].Value, claims[1].Value);
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
        public string Update(int id, bool check, bool isActive)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<UpdateAgent> request = new RequestClass<UpdateAgent>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
                request.Data = new UpdateAgent
                {
                    Id = id,
                    Check = check,
                    IsActive = isActive
                };


                ResponseClass<dynamic> response = _repository.UpdateAgent(request);
                if (response.Code != 0)
                {
                    return "Ошибка при обновлении информации по агентам";
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
