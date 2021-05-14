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
    [Authorize]
    public class IdentityController : Controller
    {
        private readonly IIdentityInterface _repository;
        public IdentityController(IIdentityInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string IIN, IFormFile file)
        {
            try
            {
                var claims = User.Claims.ToList();
                if (String.IsNullOrEmpty(IIN))
                    throw new Exception("Вы не указали ИИН");
                if (IIN.Length != 12)
                    throw new Exception("Введен некорректный ИИН");
                if (file == null)
                    throw new Exception("Вы не загрузили файл");
                if (file.ContentType != "image/png" && file.ContentType != "image/jpeg" && file.ContentType != "image/jpg" && file.ContentType != "image/gif")
                    throw new Exception("Файл не является изображением");
                string img;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    img = Convert.ToBase64String(fileBytes);
                }
                IdentificationModel data = new IdentificationModel
                {
                    IIN = IIN,
                    Photo = img
                };
                RequestClass<IdentificationModel> request = new RequestClass<IdentificationModel>
                {
                    Login = claims[0].Value,
                    Password = claims[2].Value,
                    Token = claims[1].Value,
                    Data = data
                };
                ResponseClass<decimal> identity = _repository.Identity(request);
                if (identity.Code == 0)
                {
                        ViewBag.Validate = "ИИН " + IIN + "\n Результат: " + Convert.ToDecimal(identity.Data).ToString("0.0000");                    
                }
                else
                {
                    ViewBag.Validate = "Не удалось провести идентификацию";
                }
                return View("Index");
            }
            catch(Exception e)
            {
                ViewBag.Validate = e.Message;
                return View("Index");
            }
        }
        public IActionResult Report(string from, string to)
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
            ReportData data = new ReportData
            {
                DateFrom = fromDate,
                DateTo = toDate.AddHours(23).AddMinutes(59).AddSeconds(59)
            };
            var claims = User.Claims.ToList();
            ResponseClass<List<ReportModel>> report = _repository.GetReport(claims[0].Value, claims[2].Value, claims[1].Value, data);
            if (report.Code != 0)
            {
                ViewBag.Message = "Ошибка при получении информации по отчету индентификации";
                return View();
            }
            else
            {
                return View(report.Data);
            }
        }
        [Authorize(Roles= "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ")]
        public IActionResult ReportAll(string from, string to, int agentId = 0)
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
            RequestClass<ReportData> request = new RequestClass<ReportData>();
                request.Login = claims[0].Value;
                request.Token = claims[1].Value;
                request.Password = claims[2].Value;
            request.Data = new ReportData
            {
                AgentId = agentId,
                DateFrom = fromDate,
                DateTo = toDate.AddHours(23).AddMinutes(59).AddSeconds(59)
            };
            ResponseClass<List<ReportModel>> response = _repository.GetReportByAgent(request);
            if (response.Code != 0)
            {
                ViewBag.Message = "Ошибка при получении информации по отчету индентификации";
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
    }
}
