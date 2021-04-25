using LoginServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServices.Business.Abstract
{
    public interface ILoginRepository
    {
        LoginCredentials GetLoginUser(string userName, string pasword);
    }
}
