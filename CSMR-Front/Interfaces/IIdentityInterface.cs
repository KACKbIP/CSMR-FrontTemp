using CSMR_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Interfaces
{
    public interface IIdentityInterface
    {
        ResponseClass<List<ReportModel>> GetReport(string login, string password, string token, ReportData data);
        ResponseClass<List<ReportModel>> GetReportByAgent(RequestClass<ReportData> request);
        ResponseClass<decimal> Identity(RequestClass<IdentificationModel> request);
    }
}
