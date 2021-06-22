using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QulixWeb.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("~/Home")]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
