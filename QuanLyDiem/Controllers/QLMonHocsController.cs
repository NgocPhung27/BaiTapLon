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
    public class QLMonHocsController : Controller
    {
        private QLDHSDbContext db = new QLDHSDbContext();
        AutoGenerateKey aukey = new AutoGenerateKey();

        // GET: QLMonHocs
        public ActionResult Index()
        {
            return View(db.MonHocs.ToList());
        }

        // GET: QLMonHocs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLMonHoc qLMonHoc = db.MonHocs.Find(id);
            if (qLMonHoc == null)
            {
                return HttpNotFound();
            }
            return View(qLMonHoc);
        }

        // GET: QLMonHocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLMonHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMH,TenMH,GhiChu")] QLMonHoc mh)
        {
            var countMH = db.MonHocs.Count();
            if (countMH == 0)
            {
                mh.MaMH = "MH001";
            }
            else
            {
                //Lấy giá trị MaHS moi nhat
                var MaMH = db.MonHocs.ToList().OrderByDescending(m => m.MaMH).FirstOrDefault().MaMH;
                //sinh MaHS tự dộng
                mh.MaMH = aukey.GenerateKey(MaMH);
            }
            //luu thông tin vao database
            db.MonHocs.Add(mh);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: QLMonHocs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLMonHoc qLMonHoc = db.MonHocs.Find(id);
            if (qLMonHoc == null)
            {
                return HttpNotFound();
            }
            return View(qLMonHoc);
        }

        // POST: QLMonHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMH,GhiChu")] QLMonHoc qLMonHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qLMonHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qLMonHoc);
        }

        // GET: QLMonHocs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLMonHoc qLMonHoc = db.MonHocs.Find(id);
            if (qLMonHoc == null)
            {
                return HttpNotFound();
            }
            return View(qLMonHoc);
        }

        // POST: QLMonHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QLMonHoc qLMonHoc = db.MonHocs.Find(id);
            db.MonHocs.Remove(qLMonHoc);
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
