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
    public class DiemHocSinhsAdminController : Controller
    {
        private QLDHSDbContext db = new QLDHSDbContext();
        AutoGenerateKey aukey = new AutoGenerateKey();

        // GET: DiemHocSinhs
        public ActionResult Index()
        {
            return View(db.DiemHocSinhs.ToList());
        }

        // GET: DiemHocSinhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocSinh diemHocSinh = db.DiemHocSinhs.Find(id);
            if (diemHocSinh == null)
            {
                return HttpNotFound();
            }
            return View(diemHocSinh);
        }

        // GET: DiemHocSinhs/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }

        // POST: DiemHocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHS,TenHS,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhHS,MaLop,MaMH,DiemMieng,Diem15Phut,Diem1Tiet,DiemHK,DiemTBHK,GhiChu")] DiemHocSinh dhs)
        {
            var countHS = db.HocSinhs.Count();
            if (countHS == 0)
            {
                dhs.MaHS = "HS001";
            }
            else
            {
                //Lấy giá trị MaHS moi nhat
                var MaHS = db.HocSinhs.ToList().OrderByDescending(m => m.MaHS).FirstOrDefault().MaHS;
                //sinh MaHS tự dộng
                dhs.MaHS = aukey.GenerateKey(MaHS);
            }
            //luu thông tin vao database
            db.HocSinhs.Add(dhs);
            db.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", dhs.MaLop);
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", dhs.MaMH);
            return View(dhs);
        }

        // GET: Admins/DiemHocSinhsAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocSinh diemHocSinh = db.DiemHocSinhs.Find(id);
            if (diemHocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", diemHocSinh.MaLop);
            return View(diemHocSinh);
        }

        // POST: Admins/DiemHocSinhsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHS,TenHS,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhHS,MaLop,MaMH,DiemMieng,Diem15Phut,Diem1Tiet,DiemHK,DiemTBHK,GhiChu")] DiemHocSinh diemHocSinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemHocSinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", diemHocSinh.MaLop);
            return View(diemHocSinh);
        }

        // GET: Admins/DiemHocSinhsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocSinh diemHocSinh = db.DiemHocSinhs.Find(id);
            if (diemHocSinh == null)
            {
                return HttpNotFound();
            }
            return View(diemHocSinh);
        }

        // POST: Admins/DiemHocSinhsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DiemHocSinh diemHocSinh = db.DiemHocSinhs.Find(id);
            db.HocSinhs.Remove(diemHocSinh);
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
