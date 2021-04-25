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
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index() 
        {
            return View();
        }

    }
}