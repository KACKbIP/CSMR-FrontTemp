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
    [ApiController]
    [AllowAnonymous]
    public class ApiController : Controller
    {
        private readonly ICabinetInterface _repositoryCab;
        private readonly IIdentityInterface _repository;
        public ApiController(IIdentityInterface repository, ICabinetInterface repositoryCab)
        {
            this._repository = repository;
            this._repositoryCab = repositoryCab;
        }
        [HttpPost("GetCabinet")]
        public ResponseClass<CabinetModel> GetQuery(RequestClass<dynamic> request)
        {
            return _repositoryCab.GetCabinet(request.Login,request.Password,request.Token);
        }
        [HttpPost("Identity")]
        public ResponseClass<decimal> Identification(RequestClass<IdentificationModel> identity)
        {
            return _repository.Identity(identity);
        }
        
    }
}
