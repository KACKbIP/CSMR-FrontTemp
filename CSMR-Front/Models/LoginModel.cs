using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Неверно указана учетная запись")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Неверно указан пароль")]
        public string Password { get; set; }
    }
}
