using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyDiem.Models;

namespace QuanLyDiem.Areas.Admins.Controllers
{
    public class AccountAdminController : Controller
    {
        QLDHSDbContext db = new QLDHSDbContext();
        Encrytion enc = new Encrytion();

        // GET: Admins/AccountAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //Mã Hóa mật khẩu trước khi cho vào database
                acc.PassWord = enc.PasswordEncrytion(acc.PassWord);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Index", "HomeAdmin");
            }
            return View(acc);
        }
    }
}