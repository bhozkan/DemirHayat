using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemirHayatWebUI.Models.Login
{
    public class LoginModel
    {   
        
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}