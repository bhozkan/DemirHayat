using LoginServices.Business;
using LoginServices.Helpers;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LoginServices.Authorization
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly LoginRepository _loginRepository;
        private readonly LoginHelper _loginHelper;

        public AuthorizationServerProvider(LoginRepository loginRepository, LoginHelper loginHelper)
        {
            _loginRepository = loginRepository;
            _loginHelper = loginHelper;
        }

    
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            bool ChekUsername = _loginHelper.ValidateEmailAdressFormat(context.UserName);
            bool ChekPassword = _loginHelper.ValidatePasswordFormat(context.Password);
            var user = _loginRepository.GetLoginUser(context.UserName, context.Password);

            if (!ChekUsername)
            {
                context.SetError("Kullanıcı Adı Hatası", "Uyarı: Kullanıcı Adınız uygun formatta değil");
            }
            else if (!ChekPassword)
            {
                context.SetError("Şifre Hatası", "Uyarı: Şifreniz sadece sayılardan oluşabilir.");
            }
            else if (user == null)
            {
                context.SetError("Kullanıcı Hatası", "Uyarı: Kullanıcı adı veya şifre hatalı");
            }
            else
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("UserName", user.UserName));
                identity.AddClaim(new Claim("Role", "AuthorizedUser"));
                context.Validated(identity);
            }
        }
    }
}