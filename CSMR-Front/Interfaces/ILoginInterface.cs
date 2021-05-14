using CSMR_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Interfaces
{
    public interface ILoginInterface
    {
        AccountModel GetAccount(LoginModel model);
        ResponseClass<bool> ChangePassword(RequestClass<ChangeData> newPassword);
    }
}
