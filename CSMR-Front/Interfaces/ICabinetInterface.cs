using CSMR_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Interfaces
{
    public interface ICabinetInterface
    {
        ResponseClass<CabinetModel> GetCabinet(string login, string password,string token);
        ResponseClass<List<QueryModel>> GetQuery(RequestClass<QueryRequestData> request);
        ResponseClass<List<TarifModel>> GetTarifs(RequestClass<dynamic> request);
        ResponseClass<List<TarifAgentModel>> GetAgentTarifs(RequestClass<dynamic> request);
        ResponseClass<bool> AddTarif(RequestClass<TarifModel> request);
        ResponseClass<bool> AddTarifAgent(RequestClass<TarifAgentModel> request);
        ResponseClass<List<TarifModel>> GetTarifByAgent(RequestClass<dynamic> request);
        ResponseClass<bool> DeleteTarif(RequestClass<TarifModel> request);
    }
}
