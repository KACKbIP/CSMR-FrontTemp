using CSMR_Front.Interfaces;
using CSMR_Front.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CSMR_Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _repository;
        public LoginController(ILoginInterface repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AccountModel user = _repository.GetAccount(model);
                if (user.Login!=null)
                {
                    if (user.Login == "Hermes")
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ"),
                        new Claim(ClaimsIdentity.DefaultIssuer, model.Password)
                    };
                        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                        return this.RedirectToAction("GetAgents", "Agent");
                    }
                    else
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, HelperRepository.DecrypteText(user.Token)),
                        new Claim(ClaimsIdentity.DefaultIssuer, model.Password)
                    };
                        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                        return this.RedirectToAction("Index", "Cabinet");
                    }
                }
                ModelState.AddModelError("", "Неверный логин или пароль!");
                ViewBag.Validate = "Неверный логин или пароль!";
            }
            return this.View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public string ChangePassword(string password)
        {
            try
            {
                var claims = User.Claims.ToList();
                RequestClass<ChangeData> post = new RequestClass<ChangeData>
                {
                    Login = claims[0].Value,
                    Password = claims[2].Value,
                    Token = claims[1].Value,
                    Data = new ChangeData { NewPassword = password }
                };

                ResponseClass<bool> response = _repository.ChangePassword(post);
                if (response.Code == 0)
                {
                    return "true";
                }
                else
                {
                    return "Не удалось сменить пароль";
                }
            }
            catch
            {
                return "Не удалось сменить пароль";
            }
        }
    }
}
