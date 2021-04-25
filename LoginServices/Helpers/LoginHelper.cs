using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginServices.Helpers
{
    public class LoginHelper
    {
        public bool ValidateEmailAdressFormat(string username) 
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(username);
                return addr.Address == username;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidatePasswordFormat(string password)
        {
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");
            return pattern.IsMatch(password);
        }
    }
}