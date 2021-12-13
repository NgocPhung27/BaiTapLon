using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Models
{
    [Table("DiemHocSinhs")]
    public class DiemHocSinh : QLHocSinh
    {

        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }
        [ForeignKey("MaMH")]
        public virtual QLMonHoc QLMonHoc { get; set; }

        //[Display(Name = "Mã môn học")]
        //public string MaMH { get; set; }
        //public QLMonHoc QLMonHoc { get; set; }

        [Display(Name = "Điểm miệng")]

        public double DiemMieng { get; set; }
        [Display(Name = "Điểm 15 phút")]
        public double Diem15Phut { get; set; }
        [Display(Name = "Điểm 1 tiết")]
        public double Diem1Tiet { get; set; }
        [Display(Name = "Điểm học kỳ")]
        public double DiemHK { get; set; }
        [Display(Name = "Điểm TB học kỳ")]
        public double DiemTBHK { get; set; }
        [AllowHtml]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

    }

}