using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Models
{
    public class RequestClass<T>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public T Data { get; set; }
    }
}
