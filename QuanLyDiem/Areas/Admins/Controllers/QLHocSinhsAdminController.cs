using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyDiem.Models;

namespace QuanLyDiem.Areas.Admins.Controllers
{
    public class QLHocSinhsAdminController : Controller
    {
        private QLDiemHocSinhDbContext db = new QLDiemHocSinhDbContext();
        AutoGenerateKey aukey = new AutoGenerateKey();

        // GET: QLHocSinhs
        public ActionResult Index()
        {
            var hocSinhs = db.HocSinhs.Include(q => q.QLLop);
            return View(hocSinhs.ToList());
        }

        // GET: QLHocSinhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLHocSinh qLHocSinh = db.HocSinhs.Find(id);
            if (qLHocSinh == null)
            {
                return HttpNotFound();
            }
            return View(qLHocSinh);
        }

        // GET: QLHocSinhs/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            return View();
        }

        // POST: QLHocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHS,TenHS,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhHS,MaLop")] QLHocSinh hs)
        {
            var countHS = db.HocSinhs.Count();
            if (countHS == 0)
            {
                hs.MaHS = "HS001";
            }
            else
            {
                //Lấy giá trị MaHS moi nhat
                var MaHS = db.HocSinhs.ToList().OrderByDescending(m => m.MaHS).FirstOrDefault().MaHS;
                //sinh MaHS tự dộng
                hs.MaHS = aukey.GenerateKey(MaHS);
            }
            //luu thông tin vao database
            db.HocSinhs.Add(hs);
            db.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", hs.MaLop);
            return View(hs);
        }

        // GET: Admins/QLHocSinhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLHocSinh qLHocSinh = db.HocSinhs.Find(id);
            if (qLHocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", qLHocSinh.MaLop);
            return View(qLHocSinh);
        }

        // POST: Admins/QLHocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHS,TenHS,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhHS,MaLop")] QLHocSinh qLHocSinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qLHocSinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", qLHocSinh.MaLop);
            return View(qLHocSinh);
        }

        // GET: Admins/QLHocSinhs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLHocSinh qLHocSinh = db.HocSinhs.Find(id);
            if (qLHocSinh == null)
            {
                return HttpNotFound();
            }
            return View(qLHocSinh);
        }

        // POST: Admins/QLHocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QLHocSinh qLHocSinh = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(qLHocSinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
