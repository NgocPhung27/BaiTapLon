using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyDiem.Models;

namespace QuanLyDiem.Controllers
{ 
    public class QLGiaoViensController : Controller
    {
        private QLDiemHocSinhDbContext db = new QLDiemHocSinhDbContext();
        AutoGenerateKey aukey = new AutoGenerateKey();

        // GET: QLGiaoViens
        public ActionResult Index()
        {
            var giaoViens = db.GiaoViens.Include(q => q.QLMonHoc);
            return View(giaoViens.ToList());
        }

        // GET: QLGiaoViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLGiaoVien qLGiaoVien = db.GiaoViens.Find(id);
            if (qLGiaoVien == null)
            {
                return HttpNotFound();
            }
            return View(qLGiaoVien);
        }

        // GET: QLGiaoViens/Create
        public ActionResult Create()
        {
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }

        // POST: QLGiaoViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,MaMH,TenGV,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhGV")] QLGiaoVien gv)
        {
            var countGV = db.GiaoViens.Count();
            if (countGV == 0)
            {
                gv.MaGV = "GV001";
            }
            else
            {
                //Lấy giá trị MaHS moi nhat
                var MaGV = db.GiaoViens.ToList().OrderByDescending(m => m.MaGV).FirstOrDefault().MaGV;
                //sinh MaHS tự dộng
                gv.MaGV = aukey.GenerateKey(MaGV);
            }
            //luu thông tin vao database
            db.GiaoViens.Add(gv);
            db.SaveChanges();

            return RedirectToAction("Index");

            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", gv.MaMH);
            return View(gv);
        }

        // GET: QLGiaoViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLGiaoVien qLGiaoVien = db.GiaoViens.Find(id);
            if (qLGiaoVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", qLGiaoVien.MaMH);
            return View(qLGiaoVien);
        }

        // POST: QLGiaoViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,MaMH,TenGV,GioiTinh,NgaySinh,SoDienThoai,DiaChi,AnhGV")] QLGiaoVien qLGiaoVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qLGiaoVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", qLGiaoVien.MaMH);
            return View(qLGiaoVien);
        }

        // GET: QLGiaoViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLGiaoVien qLGiaoVien = db.GiaoViens.Find(id);
            if (qLGiaoVien == null)
            {
                return HttpNotFound();
            }
            return View(qLGiaoVien);
        }

        // POST: QLGiaoViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QLGiaoVien qLGiaoVien = db.GiaoViens.Find(id);
            db.GiaoViens.Remove(qLGiaoVien);
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
