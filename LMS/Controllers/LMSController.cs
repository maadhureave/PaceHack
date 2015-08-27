using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class LMSController : Controller
    {
        // GET: LMS
        public ActionResult Index()
        {
            return View();
        }
    }
}