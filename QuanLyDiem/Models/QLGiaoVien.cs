using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Models
{
    [Table("Giaoviens")]
    public class QLGiaoVien
    {
        [Key]
        [Required(ErrorMessage = "Mã giáo viên không được để trống !!!")]
        [Display(Name = "Mã giáo viên")]
        public string MaGV { get; set; }

        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }
        public QLMonHoc QLMonHoc { get; set; }

        [Required(ErrorMessage = "Tên giáo viên không được để trống !!!")]
        [Display(Name = "Tên giáo viên")]
        public string TenGV { get; set; }

        [Display(Name = " Giới Tính")]
        public string GioiTinh { get; set; }
        [Display(Name = "Ngày Sinh")]
        public string NgaySinh { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [AllowHtml]
        [Display(Name = "ảnh giáo viên")]
        public string AnhGV { get; set; }
    }
}