using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Areas.Admins.Controllers
{
    public class HomeAdminController : Controller
    {
        [Authorize(Roles = "Admin")]

            // GET: Admins/HomeAdmin
            public ActionResult Index()
            {
                return View();
            }
        }
}