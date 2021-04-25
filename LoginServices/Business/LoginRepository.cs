using LoginServices.Business.Abstract;
using LoginServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginServices.Business
{
    public class LoginRepository : ILoginRepository
    {
        public static List<LoginCredentials> loginCredentials = new List<LoginCredentials>() {
            new LoginCredentials
            {
                UserName = "bugra.ozkan@gmail.com",
                Password = "123456"
            },
            new LoginCredentials
            {
                UserName = "demir@yahoo.com",
                Password = "123456"
            },
        };

        public LoginCredentials GetLoginUser(string userName, string password)
        {
            return loginCredentials.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }


    }
}