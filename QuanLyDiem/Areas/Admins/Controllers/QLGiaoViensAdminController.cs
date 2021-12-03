using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyDiem.Models;

namespace QuanLyDiem.Areas.Admins.Controllers
{
    public class QLGiaoViensAdminController : Controller
    {
        private QLDHSDbContext db = new QLDHSDbContext();
        AutoGenerateKey aukey = new AutoGenerateKey();
        ExcelProcess ExcelPro = new ExcelProcess();

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

        // GET: Admins/QLGiaoViensAdmin/Edit/5
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

        // POST: Admins/QLGiaoViensAdmin/Edit/5
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

        // GET: Admins/QLGiaoViensAdmin/Delete/5
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

        // POST: Admins/QLGiaoViensAdmin/Delete/5
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
    private DataTable CopyDataFromExcelFile(HttpPostedFileBase file)
    {
        string fileExtention = file.FileName.Substring(file.FileName.IndexOf("."));
        string _FileName = "Ten_File_Muon_Luu" + fileExtention;
        string _path = Path.Combine(Server.MapPath("~/Upload/Excels"), _FileName);
        file.SaveAs(_path);
        DataTable dt = ExcelPro.ReadDataFromExcelFile(_path, false);
        return dt;
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QLDHSDbContext"].ConnectionString);
    private void OverwriteFastData(int? MaGV)
    {
        //dt là databasecos chứa dữ liệu để import vào database
        DataTable dt = new DataTable();

        //mapping các column trong database vào các column trong table ở CSDL
        SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
        bulkcopy.DestinationTableName = "Giaoviens";
        bulkcopy.ColumnMappings.Add("MaGV", "MaGV");
        bulkcopy.ColumnMappings.Add("TenMH", "MaMH");
        bulkcopy.ColumnMappings.Add("TenGV", "TenGV");
        bulkcopy.ColumnMappings.Add("GioiTinh", "GioiTinh");
        bulkcopy.ColumnMappings.Add("NgaySinh", "NgaySinh");
        bulkcopy.ColumnMappings.Add("SoDienThoai", "SoDienThoai");
        bulkcopy.ColumnMappings.Add("DiaChi", "DiaChi");
        bulkcopy.ColumnMappings.Add("AnhGV", "AnhGV");
            con.Open();
        bulkcopy.WriteToServer(dt);
        con.Close();
    }
}
}
