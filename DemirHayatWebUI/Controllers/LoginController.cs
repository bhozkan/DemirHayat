using DemirHayatWebUI.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemirHayatWebUI.Controllers
{
    public class LoginController : Controller
    {
        HttpClient client;
        private string url = string.Format("https://localhost:44371/");

        public LoginController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            ResultToken rt = new ResultToken();
            ResultError re = new ResultError();
            var login = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", loginModel.UserName },
                {"password", loginModel.Password },
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url + "getToken"),
                Content = new FormUrlEncodedContent(login)
            };

            HttpResponseMessage responseMessage = client.SendAsync(requestMessage).Result;
            var test = responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                var LogResponse = responseMessage.Content.ReadAsStringAsync().Result;
                rt = JsonConvert.DeserializeObject<ResultToken>(LogResponse);

                if (rt.access_token.Length > 80)
                {
                    HttpCookie cookie = new HttpCookie("__RequestToken", rt.access_token);
                    HttpContext.Response.SetCookie(cookie);
                    FormsAuthentication.SetAuthCookie(rt.access_token, false);
                    cookie.Expires.AddHours(1);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var LogErrorResponse = responseMessage.Content.ReadAsStringAsync().Result;
                re = JsonConvert.DeserializeObject<ResultError>(LogErrorResponse);
                ViewBag.message = re.error_description;
                ViewBag.error = re.error;
            }
            return View();
        }
    }
}