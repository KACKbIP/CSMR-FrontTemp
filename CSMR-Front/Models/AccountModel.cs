using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Models
{
    public class AccountModel
    {
        public string Login { get; set; }
        public string Token { get; set; }
    }
    public class ChangeData
    {
        public string NewPassword { get; set; }
    }
}
