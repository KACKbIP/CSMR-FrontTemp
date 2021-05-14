using CSMR_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Interfaces
{
    public interface IAgentInterface
    {
        ResponseClass<List<AgentModel>> GetAgents(string login, string password, string token);
        ResponseClass<AgentResponse> AddAgent(RequestClass<AgentRequest> request);
        ResponseClass<bool> AddQuery(RequestClass<QueryModel> request);
        ResponseClass<List<QueryModel>> GetQuery(RequestClass<QueryRequestData> request);
        ResponseClass<dynamic> UpdateAgent(RequestClass<UpdateAgent> request);
    }
}
