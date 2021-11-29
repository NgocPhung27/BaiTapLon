using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Areas.HSClient.Controllers
{
    public class HomeHocSinhController : Controller
    {
        [Authorize(Roles = "HS")]
        // GET: HSClient/HomeHocSinh
        public ActionResult Index()
        {
            return View();
        }
    }
}